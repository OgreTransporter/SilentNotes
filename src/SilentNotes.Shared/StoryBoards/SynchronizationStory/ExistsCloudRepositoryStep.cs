﻿// Copyright © 2018 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Threading.Tasks;
using SilentNotes.Models;
using SilentNotes.Services;
using VanillaCloudStorageClient;

namespace SilentNotes.StoryBoards.SynchronizationStory
{
    /// <summary>
    /// This step belongs to the <see cref="SynchronizationStoryBoard"/>. It checks whether a
    /// repository exists in the cloud storage.
    /// </summary>
    public class ExistsCloudRepositoryStep : SynchronizationStoryBoardStepBase
    {
        protected readonly ILanguageService _languageService;
        protected readonly IFeedbackService _feedbackService;
        protected readonly ISettingsService _settingsService;
        protected readonly ICloudStorageClientFactory _cloudStorageClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExistsCloudRepositoryStep"/> class.
        /// </summary>
        public ExistsCloudRepositoryStep(
            Enum stepId,
            IStoryBoard storyBoard,
            ILanguageService languageService,
            IFeedbackService feedbackService,
            ISettingsService settingsService,
            ICloudStorageClientFactory cloudStorageClientFactory)
            : base(stepId, storyBoard)
        {
            _languageService = languageService;
            _feedbackService = feedbackService;
            _settingsService = settingsService;
            _cloudStorageClientFactory = cloudStorageClientFactory;
        }

        /// <inheritdoc/>
        public override async Task Run()
        {
            SerializeableCloudStorageCredentials credentials = StoryBoard.Session.Load<SerializeableCloudStorageCredentials>(SynchronizationStorySessionKey.CloudStorageCredentials);
            ICloudStorageClient cloudStorageClient = _cloudStorageClientFactory.GetByKey(credentials.CloudStorageId);
            try
            {
                bool stopBecauseNewOAuthLoginIsRequired = false;
                if ((cloudStorageClient is OAuth2CloudStorageClient oauthStorageClient) &&
                    credentials.Token.NeedsRefresh())
                {
                    try
                    {
                        // Get a new access token by using the refresh token
                        credentials.Token = await oauthStorageClient.RefreshTokenAsync(credentials.Token);
                        SaveCredentialsToSettings(credentials);
                    }
                    catch (RefreshTokenExpiredException)
                    {
                        // Refresh-token cannot be used to get new access-tokens anymore, a new
                        // authorization by the user is required.
                        stopBecauseNewOAuthLoginIsRequired = true;
                    }
                }

                if (stopBecauseNewOAuthLoginIsRequired)
                {
                    switch (StoryBoard.Mode)
                    {
                        case StoryBoardMode.GuiAndToasts:
                            await StoryBoard.ContinueWith(SynchronizationStoryStepId.ShowCloudStorageAccount);
                            break;
                        case StoryBoardMode.ToastsOnly:
                            _feedbackService.ShowToast(_languageService["sync_error_oauth_refresh"]);
                            break;
                    }
                }
                else
                {
                    bool repositoryExists = await cloudStorageClient.ExistsFileAsync(Config.RepositoryFileName, credentials);

                    // If no error occured the credentials are ok and we can safe them
                    SaveCredentialsToSettings(credentials);

                    if (repositoryExists)
                        await StoryBoard.ContinueWith(SynchronizationStoryStepId.DownloadCloudRepository);
                    else
                        await StoryBoard.ContinueWith(SynchronizationStoryStepId.StoreLocalRepositoryToCloudAndQuit);
                }
            }
            catch (Exception ex)
            {
                // Keep the current page open and show the error message
                ShowExceptionMessage(ex, _feedbackService, _languageService);
            }
        }

        protected void SaveCredentialsToSettings(SerializeableCloudStorageCredentials credentials)
        {
            SettingsModel settings = _settingsService.LoadSettingsOrDefault();
            if (!credentials.AreEqualOrNull(settings.Credentials))
            {
                settings.Credentials = credentials;
                _settingsService.TrySaveSettingsToLocalDevice(settings);
            }
        }
    }
}
