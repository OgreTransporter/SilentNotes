﻿// Copyright © 2020 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using SilentNotes.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SilentNotes.UWP.Services
{
    /// <summary>
    /// Implementation of the <see cref="IFolderPickerService"/> interface for the UWP platform.
    /// </summary>
    internal class FolderPickerService : IFolderPickerService
    {
        private Windows.Storage.IStorageFolder _pickedFolder;

        /// <inheritdoc/>
        public async Task<bool> PickFolder()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.SettingsIdentifier = "D6EDA6465535";
            folderPicker.FileTypeFilter.Add("*");

            _pickedFolder = await folderPicker.PickSingleFolderAsync();
            return _pickedFolder != null;
        }

        /// <inheritdoc/>
        public async Task<bool> TrySaveFileToPickedFolder(string fileName, byte[] content)
        {
            if (_pickedFolder == null)
                return false;

            try
            {
                using (Stream stream = await _pickedFolder.OpenStreamForWriteAsync(fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting))
                {
                    await stream.WriteAsync(content, 0, content.Length);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
