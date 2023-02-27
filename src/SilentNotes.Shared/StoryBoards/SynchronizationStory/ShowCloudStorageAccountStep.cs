﻿// Copyright © 2018 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Threading.Tasks;
using SilentNotes.Controllers;
using SilentNotes.Crypto;
using SilentNotes.Services;
using VanillaCloudStorageClient;

namespace SilentNotes.StoryBoards.SynchronizationStory
{
    /// <summary>
    /// This step belongs to the <see cref="SynchronizationStoryBoard"/>. It shows the dialog to
    /// enter credentials for the cloud storage. In case of an OAuth2 login, it opens the external
    /// browser and waits for the confirmation of the open auth login.
    /// </summary>
    public class ShowCloudStorageAccountStep : SynchronizationStoryBoardStepBase
    {
        private readonly INavigationService _navigationService;
        private readonly INativeBrowserService _nativeBrowserService;
        private readonly ICryptoRandomService _randomSource;
        private readonly ICloudStorageClientFactory _cloudStorageClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowCloudStorageAccountStep"/> class.
        /// </summary>
        public ShowCloudStorageAccountStep(
            Enum stepId,
            IStoryBoard storyBoard,
            INavigationService navigationService,
            INativeBrowserService nativeBrowserService,
            ICryptoRandomService randomSource,
            ICloudStorageClientFactory cloudStorageClientFactory)
            : base(stepId, storyBoard)
        {
            _navigationService = navigationService;
            _nativeBrowserService = nativeBrowserService;
            _randomSource = randomSource;
            _cloudStorageClientFactory = cloudStorageClientFactory;
        }

        /// <inheritdoc/>
        public override Task Run()
        {
            if (StoryBoard.Mode.ShouldUseGui())
            {
                SerializeableCloudStorageCredentials credentials = StoryBoard.Session.Load<SerializeableCloudStorageCredentials>(SynchronizationStorySessionKey.CloudStorageCredentials);
                ICloudStorageClient cloudStorageClient = _cloudStorageClientFactory.GetByKey(credentials.CloudStorageId);
                if (cloudStorageClient is IOAuth2CloudStorageClient oauthStorageClient)
                {
                    // Show waiting page
                    _navigationService.Navigate(new Navigation(ControllerNames.CloudStorageOauthWaiting));

                    // Open OAuth2 login page in external browser
                    string oauthState = CryptoUtils.GenerateRandomBase62String(16, _randomSource);
                    string oauthCodeVerifier = CryptoUtils.GenerateRandomBase62String(64, _randomSource);
                    StoryBoard.Session.Store(SynchronizationStorySessionKey.OauthState, oauthState);
                    StoryBoard.Session.Store(SynchronizationStorySessionKey.OauthCodeVerifier, oauthCodeVerifier);

                    string url = oauthStorageClient.BuildAuthorizationRequestUrl(oauthState, oauthCodeVerifier);
                    _nativeBrowserService.OpenWebsiteInApp(url);
                }
                else
                {
                    _navigationService.Navigate(new Navigation(ControllerNames.CloudStorageAccount));
                }
            }
            return Task.CompletedTask;
        }
    }
}
