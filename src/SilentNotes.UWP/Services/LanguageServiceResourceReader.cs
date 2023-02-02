﻿// Copyright © 2021 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.IO;
using System.Threading.Tasks;
using SilentNotes.Services;

namespace SilentNotes.UWP.Services
{
    /// <summary>
    /// Implementation of the <see cref="ILanguageServiceResourceReader"/> interface for the UWP platform.
    /// </summary>
    internal class LanguageServiceResourceReader : ILanguageServiceResourceReader
    {
        /// <inheritdoc/>
        public async Task<Stream> TryOpenResourceStream(string domain, string languageCode)
        {
            string resourceFileName = BuildResourceFilePath(string.Empty, domain, languageCode);

            try
            {
                Windows.Storage.IStorageFolder appFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                return await appFolder.OpenStreamForReadAsync(resourceFileName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Builds a filename for specific resourcefile, but does not check whether that file
        /// really exists.
        /// </summary>
        /// <param name="directory">Search directory of resource file.</param>
        /// <param name="domain">Domain to which the resource belongs to.</param>
        /// <param name="languageCode">Two letter language code of the resource.</param>
        /// <returns>Expected file path of the resource file.</returns>
        private static string BuildResourceFilePath(string directory, string domain, string languageCode)
        {
            string resFileName = string.Format("Lng.{0}.{1}", domain, languageCode);
            return Path.Combine(directory, resFileName);
        }
    }
}
