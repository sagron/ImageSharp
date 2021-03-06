﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

namespace SixLabors.ImageSharp.Tests.Memory
{
    using System;

    using SixLabors.ImageSharp.Memory;

    using Xunit;

    public class Fast2DArrayTests
    {
        private static readonly float[,] FloydSteinbergMatrix =
        {
            { 0, 0, 7 },
            { 3, 5, 1 }
        };

        [Fact]
        public void Fast2DArrayThrowsOnNullInitializer()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var fast = new Fast2DArray<float>(null);
            });
        }

        [Fact]
        public void Fast2DArrayThrowsOnEmptyZeroWidth()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var fast = new Fast2DArray<float>(0, 10);
            });
        }

        [Fact]
        public void Fast2DArrayThrowsOnEmptyZeroHeight()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var fast = new Fast2DArray<float>(10, 0);
            });
        }

        [Fact]
        public void Fast2DArrayThrowsOnEmptyInitializer()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var fast = new Fast2DArray<float>(new float[0, 0]);
            });
        }

        [Fact]
        public void Fast2DArrayReturnsCorrectDimensions()
        {
            var fast = new Fast2DArray<float>(FloydSteinbergMatrix);
            Assert.True(fast.Width == FloydSteinbergMatrix.GetLength(1));
            Assert.True(fast.Height == FloydSteinbergMatrix.GetLength(0));
        }

        [Fact]
        public void Fast2DArrayGetReturnsCorrectResults()
        {
            Fast2DArray<float> fast = FloydSteinbergMatrix;

            for (int row = 0; row < fast.Height; row++)
            {
                for (int column = 0; column < fast.Width; column++)
                {
                    Assert.True(Math.Abs(fast[row, column] - FloydSteinbergMatrix[row, column]) < Constants.Epsilon);
                }
            }
        }

        [Fact]
        public void Fast2DArrayGetSetReturnsCorrectResults()
        {
            var fast = new Fast2DArray<float>(4, 4);
            const float Val = 5F;

            fast[3, 3] = Val;

            Assert.True(Math.Abs(Val - fast[3, 3]) < Constants.Epsilon);
        }
    }
}