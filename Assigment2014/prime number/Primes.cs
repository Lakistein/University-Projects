using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Prime_Number
{
    abstract class Primes
    {
        #region Data part
        private int limit;
        private StringBuilder sb;

        protected readonly List<int> primes;
        protected readonly double LimSqrt;
        protected bool[] isPrime;
        protected int Limit
        {
            set { limit = value; }
            get { return limit; }
        }
        #endregion

        #region Constructor
        protected Primes(int limit)
        {
            this.LimSqrt = Math.Sqrt(limit);
            this.Limit = limit;
            this.isPrime = new bool[limit + 1];
            this.primes = new List<int>();
            this.sb = new StringBuilder();
        }
        #endregion

        #region Property NumberOfPrimes
        public int NumberOfPrimes
        {
            get
            {
                if (primes.Count == 0) {
                    int count = 0;
                    for (int i = 0; i < isPrime.Length; i++)
                        if (isPrime[i])
                            count++;
                    return count;
                }
                else
                    return primes.Count;
            }
        }
        #endregion

        #region FindAllPrimesTillLimit
        public abstract void FindAllPrimesTillLimit();
        #endregion

        #region IsPrime
        public bool IsPrime(int num)
        {
            if (this.NumberOfPrimes == 0)
                FindAllPrimesTillLimit();

            return isPrime[num];
        }
        #endregion

        #region ToList
        private void ToList()
        {
            primes.Add(2);
            for (int i = 3; i <= limit; i += 2)
                if (isPrime[i])
                    primes.Add(i);
        }
        #endregion

        #region TraversePrimes()
        private void TraversePrimes()
        {
            foreach (int p in primes)
                sb.Append(p).Append(',').Append(' ');

            sb.Remove(sb.Length - 2, 2).Append('.');
        }
        #endregion

        #region PrintPrimes
        public void PrintPrimes()
        {
            if (NumberOfPrimes == 0) {
                Console.WriteLine("No prime numbers in the list...");
                return;
            }
            ToList();
            TraversePrimes();
            using (StreamWriter stream = new StreamWriter(Console.OpenStandardOutput())) {
                stream.Write(sb);
            }
        }
        #endregion
    }
}
