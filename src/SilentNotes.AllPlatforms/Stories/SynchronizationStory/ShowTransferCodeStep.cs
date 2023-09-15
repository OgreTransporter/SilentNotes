﻿// Copyright © 2018 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using Microsoft.Extensions.DependencyInjection;
using SilentNotes.Services;

namespace SilentNotes.Stories.SynchronizationStory
{
    /// <summary>
    /// This step belongs to the <see cref="SynchronizationStory"/>. It shows the dialog to
    /// enter the transfer code.
    /// </summary>
    internal class ShowTransferCodeStep : SynchronizationStoryStepBase
    {
        /// <inheritdoc/>
        public override ValueTask<StoryStepResult<SynchronizationStoryModel>> RunStep(SynchronizationStoryModel model, IServiceProvider serviceProvider, StoryMode uiMode)
        {
            if (uiMode == StoryMode.Gui)
            {
                var navigation = serviceProvider.GetService<INavigationService>();
                navigation.NavigateTo(Routes.TransferCode);
            }
            return ToTask(ToResultEndOfStory());
        }
    }
}
