﻿// Copyright © 2018 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;

namespace SilentNotes.Crypto.KeyDerivation
{
    /// <summary>
    /// Enumeration of the cost factor types for a key derivation function.
    /// </summary>
    public enum KeyDerivationCostType
    {
        /// <summary>Can be used with very strong passwords or tokens, which do not require
        /// key stretching.</summary>
        Low,

        /// <summary>Can be used for weak user passwords in a client app. The target time is
        /// about ¼ sec on a mid-range mobile device.</summary>
        High
    }
}
