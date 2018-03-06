﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using SixLabors.ImageSharp.Memory;

namespace SixLabors.ImageSharp.Processing.Convolution.Processors
{
    /// <summary>
    /// Contains the kernels used for Sobel edge detection
    /// </summary>
    internal static class SobelKernels
    {
        /// <summary>
        /// Gets the horizontal gradient operator.
        /// </summary>
        public static Fast2DArray<float> SobelX =>
            new float[,]
                {
                    { -1, 0, 1 },
                    { -2, 0, 2 },
                    { -1, 0, 1 }
                };

        /// <summary>
        /// Gets the vertical gradient operator.
        /// </summary>
        public static Fast2DArray<float> SobelY =>
            new float[,]
                {
                    { -1, -2, -1 },
                    { 0, 0,  0 },
                    { 1, 2, 1 }
                };
    }
}