using System;

namespace MatrixMult
{
    class Program
    {
        static readonly short MAX = 16;

        #region Main
        static void Main(string[] args)
        {
            Console.WindowWidth = Console.LargestWindowWidth -90;
            Console.WindowHeight = Console.LargestWindowHeight - 25;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();

            Random r = new Random();

            short[,] matrix1 = new short[MAX, MAX];
            short[,] matrix2 = new short[MAX, MAX];
            short[,] matrixResult = new short[MAX, MAX];


            for(short i = 0; i < MAX; i++) {
                for(short j = 0; j < MAX; j++) {
                    matrix1[i, j] = (short)r.Next(short.MinValue, short.MaxValue);
                    matrix2[i, j] = (short)r.Next(short.MinValue, short.MaxValue);
                }
            }
            matrixResult = Mult(matrix1, matrix2, matrixResult);

            Console.WriteLine("First matrix: ");
            Print(matrix1);

            Console.WriteLine("\nSecond matrix: ");
            Print(matrix2);

            Console.WriteLine("\nResult matrix: ");
            Print(matrixResult);

            Console.ReadKey();
        }
        #endregion

        #region Mult
        /// <summary>
        /// Method that multiplies two 2D matrices 16x16
        /// </summary>
        /// <param name="matrix1">First matrix 16x16</param>
        /// <param name="matrix2">Second matrix 16x16</param>
        /// <param name="matrix3">Matrix to be stored result</param>
        /// <returns>Returns 16x16 matrix with result</returns>
        public static short[,] Mult(short[,] matrix1, short[,] matrix2, short[,] matrix3)
        {
            for(short i = 0; i < MAX; i++) {
                for(short j = 0; j < MAX; j++) {
                    short result = 0;
                    for(short k = 0; k < MAX; k++) {
                        result += (short)(matrix1[i, k] * matrix2[k, j]);
                    }
                    matrix3[i, j] = result;
                }
            }
            return matrix3;
        }
        #endregion

        #region Print
        /// <summary>
        /// Method that prints the matrices
        /// </summary>
        /// <param name="arr">Matrix to be printed</param>
        public static void Print(short[,] arr)
        {
            for(short i = 0; i < MAX; i++) {
                for(short j = 0; j < MAX; j++) {
                    Console.Write(String.Format("{0,9}", arr[i, j]));
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}
