﻿using System;
using NUnit.Framework;

namespace MazeLibrary.Tests
{
    [TestFixture]
    public class MazeSolverTests
    {
        private readonly int[] startXs = { 3, 0, 1, 0 };

        private readonly int[] startYs = { 5, 4, 0, 1 };

        private readonly int[][,] sourceData = new int[][,]
        {
            new int[,]
            {
                { -1, -1, -1, -1, -1, -1 },
                {  0,  0,  0, -1,  0, -1 },
                { -1,  0, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0,  0 },
                { -1,  0,  0,  0, -1, -1 },
                { -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1, -1, -1, -1,  0, -1 },
                {  0,  0,  0, -1,  0, -1 },
                { -1,  0, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0, -1 },
                { -1,  0,  0,  0, -1, -1 },
                { -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                {  0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0, -1 },
                { -1,  0, -1,  0, -1, -1,  0,  0, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0, -1,  0,  0,  0,  0,  0, -1 },
                { -1,  0, -1, -1,  0, -1, -1, -1, -1, -1, -1, -1 },
                { -1,  0, -1,  0,  0, -1,  0, -1,  0,  0,  0, -1 },
                { -1,  0, -1,  0, -1, -1,  0,  0,  0, -1,  0, -1 },
                { -1,  0, -1,  0,  0,  0,  0, -1, -1, -1,  0, -1 },
                { -1,  0, -1,  0, -1,  0,  0, -1,  0, -1,  0, -1 },
                { -1,  0, -1, -1, -1, -1,  0, -1,  0, -1,  0, -1 },
                { -1,  0,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0 },
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1,  0, -1, -1, -1, -1, -1, -1, -1,  0, -1, -1 },
                { -1,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0, -1 },
                { -1,  0, -1,  0, -1, -1,  0,  0, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0, -1,  0,  0,  0,  0,  0, -1 },
                { -1,  0, -1, -1,  0, -1, -1, -1, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0, -1,  0, -1,  0,  0,  0, -1 },
                { -1,  0, -1,  0, -1, -1,  0,  0,  0, -1,  0, -1 },
                { -1,  0, -1,  0,  0,  0,  0, -1, -1, -1,  0, -1 },
                { -1,  0, -1,  0, -1,  0,  0, -1,  0, -1,  0, -1 },
                { -1,  0, -1, -1, -1, -1,  0, -1,  0, -1,  0, -1 },
                { -1,  0,  0,  0,  0,  0,  0, -1,  0,  0,  0, -1 },
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            }          
        };

        private readonly int[][,] result = new int[][,]
        {
            new int[,]
            {
                { -1, -1, -1, -1, -1, -1 },
                { 10,  9,  0, -1,  0, -1 },
                { -1,  8, -1, -1,  0, -1 },
                { -1,  7, -1,  3,  2,  1 },
                { -1,  6,  5,  4, -1, -1 },
                { -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1, -1, -1, -1,  1, -1 },
                { 12, 11,  0, -1,  2, -1 },
                { -1, 10, -1, -1,  3, -1 },
                { -1,  9, -1,  5,  4, -1 },
                { -1,  8,  7,  6, -1, -1 },
                { -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                {  1,  2, -1,  0,  0,  0,  0,  0, -1,  0,  0, -1 },
                { -1,  3, -1,  0, -1, -1,  0,  0, -1, -1,  0, -1 },
                { -1,  4, -1,  0,  0, -1,  0,  0,  0,  0,  0, -1 },
                { -1,  5, -1, -1,  0, -1, -1, -1, -1, -1, -1, -1 },
                { -1,  6, -1,  0,  0, -1,  0, -1, 23, 24, 25, -1 },
                { -1,  7, -1,  0, -1, -1, 20, 21, 22, -1, 26, -1 },
                { -1,  8, -1,  0,  0,  0, 19, -1, -1, -1, 27, -1 },
                { -1,  9, -1,  0, -1,  0, 18, -1,  0, -1, 28, -1 },
                { -1, 10, -1, -1, -1, -1, 17, -1,  0, -1, 29, -1 },
                { -1, 11, 12, 13, 14, 15, 16, -1,  0,  0, 30, 31 },
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            },
            new int[,]
            {
                { -1,  1, -1, -1, -1, -1, -1, -1, -1, 31, -1, -1 },
                { -1,  2, -1,  0,  0,  0,  0,  0, -1, 30, 29, -1 },
                { -1,  3, -1,  0, -1, -1,  0,  0, -1, -1, 28, -1 },
                { -1,  4, -1,  0,  0, -1,  0,  0,  0,  0, 27, -1 },
                { -1,  5, -1, -1,  0, -1, -1, -1, -1, -1, 26, -1 },
                { -1,  6, -1,  0,  0, -1,  0, -1, 23, 24, 25, -1 },
                { -1,  7, -1,  0, -1, -1, 20, 21, 22, -1,  0, -1 },
                { -1,  8, -1,  0,  0,  0, 19, -1, -1, -1,  0, -1 },
                { -1,  9, -1,  0, -1,  0, 18, -1,  0, -1,  0, -1 },
                { -1, 10, -1, -1, -1, -1, 17, -1,  0, -1,  0, -1 },
                { -1, 11, 12, 13, 14, 15, 16, -1,  0,  0,  0, -1 },
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
            }
        };

        [Test]
        public void MazeSolverConstructor_WithNull_ThrowsArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => new MazeSolver(null, 1, 2));

        [Test]
        public void MazeSolverConstructor_WithInvalidStartIndexX_ThrowsArgumentException()
            => Assert.Throws<ArgumentException>(() => new MazeSolver(sourceData[1], -12, 2));

        [Test]
        public void MazeSolverConstructor_WithInvalidStartIndexY_ThrowsArgumentException()
            => Assert.Throws<ArgumentException>(() => new MazeSolver(sourceData[1], 0, -2));

        [Test]
        public void PassMaze_SuccessfulTests()
        {
            for (int i = 0; i < sourceData.Length; i++)
            {
                MazeSolver solver = new MazeSolver(sourceData[i], startXs[i], startYs[i]);

                solver.PassMaze();

                if (!MatrixAreEquals(solver.MazeWithPass(), result[i]))
                {
                    Assert.Fail($"Maze number {i} is not correctly passed.");
                }
            }
        }

        private static bool MatrixAreEquals(int[,] lhs, int[,] rhs)
        {
            if (lhs.Length != rhs.Length)
            {
                return false;
            }

            for (int i = 0; i < lhs.GetLength(0); i++) 
            {
                for (int j = 0; j < lhs.GetLength(1); j++)
                {
                    if (lhs[i, j] != rhs[i, j]) 
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        #region tests of mine
        private readonly int[][,] sourceData2 = new int[][,]
        {
            new int[,]
            {
                { -1, -1, -1, -1, -1, -1 },
                {  -1,  0,  0, -1,  0, -1 },
                { -1,  0, -1, -1,  0, -1 },
                { -1,  0, -1,  0,  0,  0 },
                { -1,  0,  0,  0, -1, -1 },
                { -1, -1, -1, -1, -1, -1 }
            }
        };

        [Test]
        public void MazeSolver_NoExits()
    => Assert.Throws<ArgumentException>(() => new MazeSolver(sourceData2[0], 3, 5));

        [Test]
        public void MazeSolver_NoPointOfEnter()
=> Assert.Throws<ArgumentException>(() => new MazeSolver(sourceData2[0], 4, 5));
        #endregion
    }
}
