﻿// Copyright © 2018 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SilentNotes.Services;

namespace SilentNotes.StoryBoards
{
    /// <summary>
    /// Base class for all classes implementing the <see cref="IStoryBoard"/> interface.
    /// </summary>
    public class StoryBoardBase : IStoryBoard
    {
        private readonly List<IStoryBoardStep> _steps;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryBoardBase"/> class.
        /// Derrived classes should register their steps in the constructor with
        /// method <see cref="RegisterStep(IStoryBoardStep)"/>.
        /// </summary>
        /// <param name="mode">Sets the property <see cref="Mode"/>.</param>
        public StoryBoardBase(StoryBoardMode mode = StoryBoardMode.Gui)
        {
            Mode = mode;
            Session = new StoryBoardSession();
            _steps = new List<IStoryBoardStep>();
        }

        /// <inheritdoc/>
        public StoryBoardMode Mode { get; private set; }

        /// <inheritdoc/>
        public void RegisterStep(IStoryBoardStep step)
        {
            _steps.Add(step);
        }

        /// <inheritdoc/>
        public async Task Start()
        {
            if (_steps.Count > 0)
                await _steps[0].Run();
        }

        /// <inheritdoc/>
        public async Task ContinueWith(Enum stepId)
        {
            if (stepId == null)
                return;

            IStoryBoardStep step = FindRegisteredStep(stepId);
            if (step != null)
                await step.Run();
        }

        /// <inheritdoc/>
        public static async Task ShowFeedback(StoryBoardStepResult stepResult, IFeedbackService feedbackService, ILanguageService languageService)
        {
            if (stepResult.HasError)
            {
                if ((feedbackService != null) && (languageService != null))
                    SilentNotes.StoryBoards.SynchronizationStory.SynchronizationStoryBoardStepBase.ShowExceptionMessage(stepResult.Error, feedbackService, languageService);
                return;
            }

            if (stepResult.HasMessage)
            {
                if (feedbackService != null)
                    await feedbackService.ShowMessageAsync(stepResult.Message, null, MessageBoxButtons.Ok, false);
            }

            if (stepResult.HasToast)
            {
                if (feedbackService != null)
                    feedbackService.ShowToast(stepResult.Toast);
            }
        }

        /// <inheritdoc/>
        async Task IStoryBoard.ShowFeedback(StoryBoardStepResult stepResult, IFeedbackService feedbackService, ILanguageService languageService)
        {
            await ShowFeedback(stepResult, feedbackService, languageService);
        }

        /// <inheritdoc/>
        public IStoryBoardSession Session { get; }

        /// <summary>
        /// Searches for a registered step in the story board.
        /// </summary>
        /// <param name="stepId">Id of the step to search for.</param>
        /// <returns>The found step, or null if no such step could be found.</returns>
        protected IStoryBoardStep FindRegisteredStep(Enum stepId)
        {
            if (stepId == null)
                return null;
            return _steps.Find(step => step.Id.Equals(stepId));
        }
    }
}
