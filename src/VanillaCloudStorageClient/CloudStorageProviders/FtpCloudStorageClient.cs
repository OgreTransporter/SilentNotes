﻿// Copyright © 2019 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Flurl;

namespace VanillaCloudStorageClient.CloudStorageProviders
{
    /// <summary>
    /// Implementation of the <see cref="ICloudStorageClient"/> interface,
    /// which can handle cloud storage on FTP servers.
    /// </summary>
    public class FtpCloudStorageClient : CloudStorageClientBase, ICloudStorageClient
    {
        private const int UploadTimeoutSeconds = 90;
        private const int DownloadTimeoutSeconds = 60;
        private const int DeleteTimeoutSeconds = 20;
        private const int ListTimeoutSeconds = 20;
        private readonly IFtpFakeResponse _fakeResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpCloudStorageClient"/> class.
        /// </summary>
        public FtpCloudStorageClient()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpCloudStorageClient"/> class.
        /// </summary>
        /// <param name="fakeResponse">If this parameter is not null, the storage client is put
        /// into a test mode and no real calls to the Ftp server are made. Instead a Mock can
        /// define fake responses.</param>
        public FtpCloudStorageClient(IFtpFakeResponse fakeResponse)
        {
            _fakeResponse = fakeResponse;
        }

        /// <inheritdoc/>
        public override CloudStorageCredentialsRequirements CredentialsRequirements
        {
            get { return CloudStorageCredentialsRequirements.Username | CloudStorageCredentialsRequirements.Password | CloudStorageCredentialsRequirements.Url | CloudStorageCredentialsRequirements.Secure | CloudStorageCredentialsRequirements.AcceptUnsafeCertificate; }
        }

        /// <inheritdoc/>
        public override async Task UploadFileAsync(string filename, byte[] fileContent, CloudStorageCredentials credentials)
        {
            credentials.ThrowIfInvalid(CredentialsRequirements, true);
            SanitizeCredentials(credentials);

            try
            {
                Url fileUrl = new Url(credentials.Url).AppendPathSegment(filename);
                using (var certificateAcceptor = new CertificateAcceptor(credentials.AcceptInvalidCertificate))
                using (WebClient webClient = new CustomFtpWebClient(request =>
                    {
                        request.Timeout = (int)TimeSpan.FromSeconds(UploadTimeoutSeconds).TotalMilliseconds;
                        request.UseBinary = true;
                        request.EnableSsl = credentials.Secure;
                    }))
                {
                    webClient.Credentials = new NetworkCredential(credentials.Username, credentials.Password);
                    if (!IsInTestMode)
                        await webClient.UploadDataTaskAsync(fileUrl, fileContent);
                }
            }
            catch (Exception ex)
            {
                throw ConvertToCloudStorageException(ex);
            }
        }

        /// <inheritdoc/>
        public override async Task<byte[]> DownloadFileAsync(string filename, CloudStorageCredentials credentials)
        {
            credentials.ThrowIfInvalid(CredentialsRequirements, true);
            SanitizeCredentials(credentials);

            try
            {
                Url fileUrl = new Url(credentials.Url).AppendPathSegment(filename);
                byte[] responseData;
                using (var certificateAcceptor = new CertificateAcceptor(credentials.AcceptInvalidCertificate))
                using (WebClient webClient = new CustomFtpWebClient(request =>
                    {
                        request.Timeout = (int)TimeSpan.FromSeconds(DownloadTimeoutSeconds).TotalMilliseconds;
                        request.UseBinary = true;
                        request.EnableSsl = credentials.Secure;
                    }))
                {
                    webClient.Credentials = new NetworkCredential(credentials.Username, credentials.Password);
                    if (IsInTestMode)
                        responseData = _fakeResponse.GetFakeServerResponseBytes(fileUrl);
                    else
                        responseData = await webClient.DownloadDataTaskAsync(fileUrl);
                }
                return responseData;
            }
            catch (Exception ex)
            {
                throw ConvertToCloudStorageException(ex);
            }
        }

        /// <inheritdoc/>
        public override async Task DeleteFileAsync(string filename, CloudStorageCredentials credentials)
        {
            credentials.ThrowIfInvalid(CredentialsRequirements, true);
            SanitizeCredentials(credentials);

            try
            {
                Url fileUrl = new Url(credentials.Url).AppendPathSegment(filename);

                using (var certificateAcceptor = new CertificateAcceptor(credentials.AcceptInvalidCertificate))
                using (WebClient webClient = new CustomFtpWebClient(request =>
                    {
                        request.Timeout = (int)TimeSpan.FromSeconds(DeleteTimeoutSeconds).TotalMilliseconds;
                        request.Method = WebRequestMethods.Ftp.DeleteFile; // "DELE"
                        request.EnableSsl = credentials.Secure;
                    }))
                {
                    webClient.Credentials = new NetworkCredential(credentials.Username, credentials.Password);
                    if (!IsInTestMode)
                        await webClient.DownloadDataTaskAsync(fileUrl);
                }
            }
            catch (Exception ex)
            {
                throw ConvertToCloudStorageException(ex);
            }
        }

        /// <inheritdoc/>
        public override async Task<List<string>> ListFileNamesAsync(CloudStorageCredentials credentials)
        {
            credentials.ThrowIfInvalid(CredentialsRequirements, true);
            SanitizeCredentials(credentials);

            try
            {
                // Call the list command
                Uri directoryUri = new Uri(IncludeTrailingSlash(credentials.Url));
                string responseData = null;
                using (var certificateAcceptor = new CertificateAcceptor(credentials.AcceptInvalidCertificate))
                using (WebClient webClient = new CustomFtpWebClient(request =>
                    {
                        request.Timeout = (int)TimeSpan.FromSeconds(ListTimeoutSeconds).TotalMilliseconds;
                        request.Proxy = WebRequest.DefaultWebProxy;
                        request.Method = WebRequestMethods.Ftp.ListDirectory; // "NLST"
                        request.EnableSsl = credentials.Secure;
                    }))
                {
                    webClient.Credentials = new NetworkCredential(credentials.Username, credentials.Password);
                    if (IsInTestMode)
                        responseData = _fakeResponse.GetFakeServerResponseString(credentials.Url);
                    else
                        responseData = await webClient.DownloadStringTaskAsync(directoryUri);
                }

                // Interpret the response
                string unixDelimitedResponse = responseData.Replace("\r\n", "\n");
                string[] fileNames = unixDelimitedResponse.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                List<string> result = new List<string>(fileNames);
                result.Remove("..");
                result.Remove(".");
                return result;
            }
            catch (Exception ex)
            {
                throw ConvertToCloudStorageException(ex);
            }
        }

        /// <summary>
        /// DotNet only supports FTP urls of the form "ftp://", but no "ftps://". Since SSL does
        /// not depend on this prefix and is rather controlled by the secure flag, we can change
        /// the prefix to a valid and accepted "ftp://".
        /// </summary>
        public static void SanitizeCredentials(CloudStorageCredentials credentials)
        {
            const string DisallowedPrefix = "ftps:";
            if (!string.IsNullOrEmpty(credentials.Url) && credentials.Url.StartsWith(DisallowedPrefix, StringComparison.OrdinalIgnoreCase))
            {
                credentials.Url = "ftp:" + credentials.Url.Remove(0, DisallowedPrefix.Length);
                credentials.Secure = true;
            }
        }

        private bool IsInTestMode
        {
            get { return _fakeResponse != null; }
        }

        /// <summary>
        /// Extends a WebClient with the possibility to adjust the FtpWebRequest parameters.
        /// </summary>
        private class CustomFtpWebClient : WebClient
        {
            private readonly Action<FtpWebRequest> _adjustWebRequest;

            /// <summary>
            /// Initializes a new instance of the <see cref="CustomFtpWebClient"/> class.
            /// </summary>
            /// <param name="adjustWebRequest">A delegate which allows to adjust the ftp web
            /// request.</param>
            public CustomFtpWebClient(Action<FtpWebRequest> adjustWebRequest)
            {
                _adjustWebRequest = adjustWebRequest;
            }

            /// <summary>
            /// Overrides creation of the web request to set custom parameters.
            /// </summary>
            /// <param name="uri">The uri to get a webrequest from.</param>
            /// <returns>The web request object.</returns>
            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest request = base.GetWebRequest(uri);

                FtpWebRequest ftpRequest = request as FtpWebRequest;
                if (ftpRequest == null)
                    throw new ConnectionFailedException(string.Format("The Ftp web client cannot connect to the non Ftp url '{0}'.", uri.AbsoluteUri), null);

                _adjustWebRequest?.Invoke(ftpRequest);
                return request;
            }
        }

        /// <summary>
        /// Helper class which can add a callback to the certificate validation, which accepts
        /// even invalid certificates.
        /// </summary>
        private class CertificateAcceptor : IDisposable
        {
            private readonly bool _acceptInvalidCertificates;
            private bool _disposed;
            private RemoteCertificateValidationCallback _oldCertificateValidationCallback;

            /// <summary>
            /// Initializes a new instance of the <see cref="CertificateAcceptor"/> class.
            /// </summary>
            /// <param name="acceptInvalidCertificates">A value indicating whether all certificates
            /// should be accepted.</param>
            public CertificateAcceptor(bool acceptInvalidCertificates)
            {
                _acceptInvalidCertificates = acceptInvalidCertificates;
                if (_acceptInvalidCertificates)
                {
                    _oldCertificateValidationCallback = ServicePointManager.ServerCertificateValidationCallback;
                    ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallback;
                }
            }

            /// <summary>
            /// Finalizes an instance of the <see cref="CertificateAcceptor"/> class.
            /// </summary>
            ~CertificateAcceptor()
            {
                Dispose();
            }

            /// <inheritdoc/>
            public void Dispose()
            {
                if (!_disposed)
                {
                    _disposed = true;
                    if (_acceptInvalidCertificates)
                        ServicePointManager.ServerCertificateValidationCallback = _oldCertificateValidationCallback;
                }
            }

            private bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                // Accept every certificate, even if it is invalid.
                return true;
            }
        }
    }

    /// <summary>
    /// This interface can be mocked and passed to the <see cref="FtpCloudStorageClient"/> for unittesting.
    /// </summary>
    public interface IFtpFakeResponse
    {
        /// <summary>
        /// This method can be mocked to provide faked server responses.
        /// </summary>
        /// <param name="url">The url called by the <see cref="FtpCloudStorageClient"/>.</param>
        /// <returns>The mock returns the fake response.</returns>
        string GetFakeServerResponseString(string url);

        /// <summary>
        /// This method can be mocked to provide faked server responses.
        /// </summary>
        /// <param name="url">The url called by the <see cref="FtpCloudStorageClient"/>.</param>
        /// <returns>The mock returns the fake response.</returns>
        byte[] GetFakeServerResponseBytes(string url);
    }
}
