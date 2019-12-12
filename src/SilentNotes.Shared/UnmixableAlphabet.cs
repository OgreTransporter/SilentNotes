﻿// Copyright © 2019 Martin Stoeckli.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;

namespace SilentNotes
{
    /// <summary>
    /// Alphabet of 56 characters which cannot be easily mixed up, similar looking characters are
    /// excluded. This is especially useful for codes or passwords which must be typed in by a user.
    /// A code generated by this alphabet will never contain characters like (0/O 1/I/l) and doesn't
    /// leave the user guessing what to enter.
    /// </summary>
    /// <remarks>
    /// The alphabet contains all characters 0..9, A..Z, a..z without following characters:
    /// 0, 1, l, I, J, O
    /// </remarks>
    public static class UnmixableAlphabet
    {
        /// <summary>
        /// Array of 56 non-interchangeable characters.
        /// </summary>
        public static readonly char[] Characters = new char[] { '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        /// <summary>
        /// Checks whether a string contains only characters of the unmixable alphabet.
        /// </summary>
        /// <param name="code">String to test.</param>
        /// <returns>Returns true if only unmixable characters where found, otherwise false.</returns>
        public static bool IsOfCorrectAlphabet(string code)
        {
            foreach (char letter in code)
            {
                if (!IsOfCorrectAlphabet(letter))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks whether a character belongs to the unmixable alphabet.
        /// </summary>
        /// <param name="letter">Character to test.</param>
        /// <returns>Returns true if the character is unmixable, otherwise false.</returns>
        public static bool IsOfCorrectAlphabet(char letter)
        {
            // Because the characters are sorted, checking can be done with a fast binary search.
            return Array.BinarySearch(Characters, letter) >= 0;
        }
    }
}
