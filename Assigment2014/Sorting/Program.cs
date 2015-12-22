using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferHeight = Int16.MaxValue - 1;
            Stopwatch sw = new Stopwatch();
            Random r = new Random();
            int[] arrayToBeSortByInsertion = new int[16384];
            //int[] secondArray = new int[100];
            //int[] mergeArray = new int[1024];
            int[] arrayToBeSortByShell = new int[16384];

            //generating random numbers
            for (int i = 0; i < arrayToBeSortByInsertion.Length; i++) {
                arrayToBeSortByInsertion[i] = r.Next();
            }
            //Console.WriteLine("Sorting array by insertion!");
            //Sort.Insertion(arrayToBeSortByInsertion);

            //Console.WriteLine("Array sorted! Printing values:");
            //for (int i = 0; i < arrayToBeSortByInsertion.Length; i++) {
            //    Console.WriteLine(arrayToBeSortByInsertion[i] + ",");
            //}

            //generating random numbers
            for (int i = 0; i < arrayToBeSortByShell.Length; i++)
            {
                arrayToBeSortByShell[i] = r.Next();
            }
            Console.WriteLine("Sorting array by Shell");
            Sort.Shell(arrayToBeSortByShell);
            Console.WriteLine("Array sorted, printing elements");
            foreach (int item in arrayToBeSortByShell) {
                Console.WriteLine(item + ",");
            }

            Console.ReadKey();

        }
    }
}
