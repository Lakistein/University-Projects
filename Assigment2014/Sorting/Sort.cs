using System;

namespace Sorting
{
    public class Sort
    {
        #region Insertion sort
        /// <summary>
        /// Sort array by insertion sort
        /// </summary>
        /// <param name="arr">Array of unsorted elements</param>
        public static void Insertion(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++) {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key) {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }
        #endregion

        #region Shell Sort
        public static void Shell(int[] arr)
        {
            int n = arr.Length;
            int increment = n / 2;
            while (increment > 0) {
                int last = increment;
                while (last < n) {
                    int current = last - increment;
                    while (current >= 0) {
                        if (arr[current] > arr[current + increment]) {
                            
                            int tmp = arr[current];
                            arr[current] = arr[current + increment];
                            arr[current + increment] = tmp;
                            current -= increment;
                        }
                        else { break; }
                    }
                    last++;
                }
                increment /= 2;
            }
        }
        #endregion
    }
}

