﻿// Copyright © 2018 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Security.Cryptography;
using SilentNotes.Services;

namespace SilentNotes.Platforms.Services
{
    /// <summary>
    /// Implementation of the <see cref="ICryptoRandomService"/> interface for the UWP platform.
    /// </summary>
    internal class CryptoRandomService : ICryptoRandomService
    {
        /// <inheritdoc/>
        public byte[] GetRandomBytes(int numberOfBytes)
        {
            return RandomNumberGenerator.GetBytes(numberOfBytes);
        }
    }
}
