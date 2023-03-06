﻿// Copyright © 2020 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;

namespace SilentNotes.Services
{
    /// <summary>
    /// The environment service offers informations about the operating system and app environment.
    /// This information can be used from the shared code.
    /// </summary>
    public interface IEnvironmentService
    {
        /// <summary>
        /// Gets the current operating system.
        /// </summary>
        OperatingSystem Os { get; }

        /// <summary>
        /// Gets a value indicating whether the system is running in night-mode and the app should
        /// prefer a dark theme.
        /// </summary>
        bool InDarkMode { get; }

        /// <summary>
        /// Gets an interface which can be used to keep the screen on, or prevents that an app is
        /// going to sleep. This property can be null, if the OS doesn't support/need this
        /// functionality.
        /// </summary>
        IKeepScreenOn KeepScreenOn { get; }

        /// <summary>
        /// Gets an interface which can be used to prevent screenshots. This property can be null,
        /// if the OS doesn't support/need this functionality.
        /// </summary>
        IScreenshots Screenshots { get; }
    }

    /// <summary>
    /// Enumeration of all known cloud storage service types.
    /// </summary>
    public enum OperatingSystem
    {
        /// <summary>Microsoft Windows OS</summary>
        Windows,

        /// <summary>Android OS</summary>
        Android,
    }
}
