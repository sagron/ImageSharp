﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using SixLabors.ImageSharp.Memory;

namespace SixLabors.ImageSharp.Processing.Convolution.Processors
{
    /// <summary>
    /// Contains the kernels used for Scharr edge detection
    /// </summary>
    internal static class ScharrKernels
    {
        /// <summary>
        /// Gets the horizontal gradient operator.
        /// </summary>
        public static Fast2DArray<float> ScharrX =>
            new float[,]
                {
                    { -3, 0, 3 },
                    { -10, 0, 10 },
                    { -3, 0, 3 }
                };

        /// <summary>
        /// Gets the vertical gradient operator.
        /// </summary>
        public static Fast2DArray<float> ScharrY =>
            new float[,]
            {
                { 3, 10, 3 },
                { 0, 0, 0 },
                { -3, -10, -3 }
            };
    }
}