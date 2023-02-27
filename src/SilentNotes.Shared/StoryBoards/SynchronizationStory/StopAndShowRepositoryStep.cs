﻿// Copyright © 2018 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Threading.Tasks;
using SilentNotes.Controllers;
using SilentNotes.Services;

namespace SilentNotes.StoryBoards.SynchronizationStory
{
    /// <summary>
    /// This step is an end point of the <see cref="SynchronizationStoryBoard"/>. It aborts the
    /// story and goes back to the repository overview.
    /// </summary>
    public class StopAndShowRepositoryStep : SynchronizationStoryBoardStepBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly INavigationService _navigationService;
        private readonly IStoryBoardService _storyBoardService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopAndShowRepositoryStep"/> class.
        /// </summary>
        public StopAndShowRepositoryStep(
            Enum stepId,
            IStoryBoard storyBoard,
            IFeedbackService feedbackService,
            INavigationService navigationService,
            IStoryBoardService storyBoardService)
            : base(stepId, storyBoard)
        {
            _feedbackService = feedbackService;
            _navigationService = navigationService;
            _storyBoardService = storyBoardService;
        }

        /// <inheritdoc/>
        public override Task Run()
        {
            StoryBoard.Session.Clear();
            _storyBoardService.ActiveStory = null;
            _feedbackService.ShowBusyIndicator(false);
            if (StoryBoard.Mode.ShouldUseGui())
            {
                _navigationService.Navigate(new Navigation(ControllerNames.NoteRepository));
            }
            return Task.CompletedTask;
        }
    }
}
