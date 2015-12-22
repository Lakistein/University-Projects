namespace Prime_Number
{
    sealed class Sieve_of_Eratosthenes : Primes
    {
        #region Constructor
        public Sieve_of_Eratosthenes(int limit)
            : base(limit)
        {
            for (int i = 2; i <= Limit; i++)
                isPrime[i] = true;
        }
        #endregion

        #region FindPrimes FindAllPrimesTillLimit
        public override void FindAllPrimesTillLimit()
        {
            for (int i = 0; i < LimSqrt; i++)
                if (isPrime[i]) {
                    for (int j = i * i; j <= Limit; j += i)
                        isPrime[j] = false;
                }
        }
        #endregion
    }

}
