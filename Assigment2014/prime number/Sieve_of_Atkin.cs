using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime_Number
{
    sealed class Sieve_of_Atkin : Primes
    {
        #region Private variables
        private double sqrt;
        #endregion

        #region Constructor
        public Sieve_of_Atkin(int limit)
            : base(limit)
        {
        }
        #endregion

        #region FindAllPrimesTillLimit
        /// <summary>
        /// Finds all primes till specified limit
        /// </summary>
        public override void FindAllPrimesTillLimit()
        {
            for(int x = 1; x <= LimSqrt; x++)
                for (int y = 1; y <= LimSqrt; y++) {
                    int n = 4 * x * x + y * y;
                    if(n <= Limit && (n % 12 == 1 || n % 12 == 5))
                        isPrime[n] ^= true;

                    n = 3 * x * x + y * y;
                    if(n <= Limit && n % 12 == 7)
                        isPrime[n] ^= true;

                    n = 3 * x * x - y * y;
                    if(x > y && n <= Limit && n % 12 == 11)
                        isPrime[n] ^= true;
                }

            for (int n = 5; n <= LimSqrt; n++)
                if(isPrime[n]) {
                    int nSquared = n * n;
                    for(int k = nSquared; k <= Limit; k += nSquared)
                        isPrime[k] = false;
                }

            if (Limit == 2)
                isPrime[2] = true;

            else if(Limit > 2){
                isPrime[2] = true;
                isPrime[3] = true;
            }
        }
        #endregion
    }
}
