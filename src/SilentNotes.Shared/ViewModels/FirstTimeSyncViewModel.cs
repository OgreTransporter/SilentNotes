﻿// Copyright © 2018 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using SilentNotes.HtmlView;
using SilentNotes.Services;
using SilentNotes.StoryBoards.SynchronizationStory;

namespace SilentNotes.ViewModels
{
    /// <summary>
    /// View model to present the info dialog to the user.
    /// </summary>
    public class FirstTimeSyncViewModel : ViewModelBase
    {
        private readonly IStoryBoardService _storyBoardService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstTimeSyncViewModel"/> class.
        /// </summary>
        public FirstTimeSyncViewModel(
            INavigationService navigationService,
            ILanguageService languageService,
            ISvgIconService svgIconService,
            IThemeService themeService,
            IBaseUrlService webviewBaseUrl,
            IStoryBoardService storyBoardService)
            : base(navigationService, languageService, svgIconService, themeService, webviewBaseUrl)
        {
            _storyBoardService = storyBoardService;

            GoBackCommand = new RelayCommand(GoBack);
            ContinueCommand = new RelayCommand(Continue);
        }

        /// <summary>
        /// Gets the command when the user presses the continue button.
        /// </summary>
        [VueDataBinding(VueBindingMode.Command)]
        public ICommand ContinueCommand { get; private set; }

        private async void Continue()
        {
            await (_storyBoardService.ActiveStory?.ContinueWith(SynchronizationStoryStepId.ShowCloudStorageChoice)
                ?? Task.CompletedTask);
        }

        /// <summary>
        /// Gets the command to go back to the note overview.
        /// </summary>
        [VueDataBinding(VueBindingMode.Command)]
        public ICommand GoBackCommand { get; private set; }

        private async void GoBack()
        {
            await (_storyBoardService.ActiveStory?.ContinueWith(SynchronizationStoryStepId.StopAndShowRepository)
                ?? Task.CompletedTask);
        }

        /// <inheritdoc/>
        public override void OnGoBackPressed(out bool handled)
        {
            handled = true;
            GoBack();
        }
    }
}
