using Instant.APIModels;
using Instant.EFData;

namespace Instant.Extensions
{
    public static class EntityExtensions
    {
        public static CardAccountSummary ToSummary(this CardAccount account)
        {
            return new CardAccountSummary
            {
                CardNumber = account.CurrentCardNumber,
                Amount = account.CurrentAmount
            };
        }
    }
}
