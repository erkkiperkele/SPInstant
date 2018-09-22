using System;
using System.Collections.Concurrent;

namespace Instant.Services
{
    public class CardNumberGenerator : ICardNumberGenerator
    {
        // Safe to use but:
        // - can get hard to have access to the dictionary when scaling up the requests
        // - can get slow if having many accounts (bucket collision)
        // - might not fit in memory when Shakepay has millions of accounts (but you might have the money to fix it by then)
        ConcurrentDictionary<long, long> numbersInUse;

        public CardNumberGenerator()
        {
            numbersInUse = new ConcurrentDictionary<long, long>();
        }

        public void FreeCardNumber(long toFree)
        {
            numbersInUse.TryRemove(toFree, out long removed);
        }

        // Current Limitation: no '0' at index 0 and 7
        public long GenerateCardNumber()
        {
            var rand = new Random();
            var firstHalf = rand.Next(10000000, 99999999);
            var secondHalf = rand.Next(10000000, 99999999);

            var candidateCardNumber = Convert.ToInt64($"{firstHalf}{secondHalf}");

            var numberAlreadyExists = true;

            while (numberAlreadyExists)
            {
                numberAlreadyExists = numbersInUse.TryAdd(candidateCardNumber, candidateCardNumber);
            }

            return candidateCardNumber;
        }
    }
}
