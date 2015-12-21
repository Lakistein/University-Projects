namespace Prime_Number
{
    class Sieve_Of_Lazar : Primes
    {
        #region Constructor
        public Sieve_Of_Lazar(int limit)
            : base(limit)
        {
            if (limit > 1) {
                for (int i = 3; i <= Limit; i += 2)
                    isPrime[i] = true;
                isPrime[2] = true;
            }
        }
        #endregion

        #region FindAllPrimesTillLimit
        public override void FindAllPrimesTillLimit()
        {
            for (int i = 3; i <= Limit; i += 2)
                for (int j = 2; j * j <= i; j++) {
                    if (i % j == 0) {
                        isPrime[i] = false;
                        break;
                    }
                }
        }
        #endregion
    }
}
