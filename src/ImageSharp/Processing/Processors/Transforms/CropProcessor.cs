﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

// TODO: Convert this into a cloning processor inheriting TransformProcessor once Anton's memory PR is merged
namespace SixLabors.ImageSharp.Processing.Processors
{
    /// <summary>
    /// Provides methods to allow the cropping of an image.
    /// </summary>
    /// <typeparam name="TPixel">The pixel format.</typeparam>
    internal class CropProcessor<TPixel> : ImageProcessor<TPixel>
        where TPixel : struct, IPixel<TPixel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CropProcessor{TPixel}"/> class.
        /// </summary>
        /// <param name="cropRectangle">The target cropped rectangle.</param>
        public CropProcessor(Rectangle cropRectangle)
        {
            this.CropRectangle = cropRectangle;
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public Rectangle CropRectangle { get; }

        /// <inheritdoc/>
        protected override void OnApply(ImageFrame<TPixel> source, Rectangle sourceRectangle, Configuration configuration)
        {
            if (this.CropRectangle == sourceRectangle)
            {
                return;
            }

            int minY = Math.Max(this.CropRectangle.Y, sourceRectangle.Y);
            int maxY = Math.Min(this.CropRectangle.Bottom, sourceRectangle.Bottom);
            int minX = Math.Max(this.CropRectangle.X, sourceRectangle.X);
            int maxX = Math.Min(this.CropRectangle.Right, sourceRectangle.Right);

            using (Buffer2D<TPixel> targetPixels = configuration.MemoryManager.Allocate2D<TPixel>(this.CropRectangle.Size))
            {
                Parallel.For(
                    minY,
                    maxY,
                    configuration.ParallelOptions,
                    y =>
                    {
                        Span<TPixel> sourceRow = source.GetPixelRowSpan(y).Slice(minX);
                        Span<TPixel> targetRow = targetPixels.GetRowSpan(y - minY);
                        SpanHelper.Copy(sourceRow, targetRow, maxX - minX);
                    });

                Buffer2D<TPixel>.SwapContents(source.PixelBuffer, targetPixels);
            }
        }

        /// <inheritdoc/>
        protected override void AfterImageApply(Image<TPixel> source, Rectangle sourceRectangle)
            => TransformHelpers.UpdateDimensionalMetData(source);
    }
}