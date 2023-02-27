﻿// Copyright © 2018 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Threading.Tasks;
using SilentNotes.Models;
using SilentNotes.Services;

namespace SilentNotes.StoryBoards.SynchronizationStory
{
    /// <summary>
    /// This step belongs to the <see cref="SynchronizationStoryBoard"/>. It checks whether the
    /// downloaded repository is the same repository as the one stored locally (has the same id).
    /// </summary>
    public class IsSameRepositoryStep : SynchronizationStoryBoardStepBase
    {
        protected readonly IRepositoryStorageService _repositoryStorageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsSameRepositoryStep"/> class.
        /// </summary>
        public IsSameRepositoryStep(Enum stepId, IStoryBoard storyBoard, IRepositoryStorageService repositoryStorageService)
            : base(stepId, storyBoard)
        {
            _repositoryStorageService = repositoryStorageService;
        }

        /// <inheritdoc/>
        public override async Task Run()
        {
            _repositoryStorageService.LoadRepositoryOrDefault(out NoteRepositoryModel localRepository);
            NoteRepositoryModel cloudRepository = StoryBoard.Session.Load<NoteRepositoryModel>(SynchronizationStorySessionKey.CloudRepository);

            if (localRepository.Id == cloudRepository.Id)
            {
                await StoryBoard.ContinueWith(SynchronizationStoryStepId.StoreMergedRepositoryAndQuit);
            }
            else
            {
                await StoryBoard.ContinueWith(SynchronizationStoryStepId.ShowMergeChoice);
            }
        }
    }
}
