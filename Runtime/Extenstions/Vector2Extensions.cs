using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Device;

namespace CommonUtils.Extensions
{
    /// <summary>
    /// Extensions for vector2.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Checks if this vector IS zero.
        /// </summary>
        public static bool IsZero(this Vector2 vec2)
        {
            return vec2.x == 0 && vec2.y == 0;
        }

        /// <summary>
        /// Checks if this vector is NOT zero.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZero(this Vector2 vec2)
        {
            return vec2.x != 0 || vec2.y != 0;
        }
    }
}