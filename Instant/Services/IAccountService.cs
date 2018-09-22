using Instant.APIModels;
using Instant.EFData;
using System.Collections.Generic;

namespace Instant.Services
{
    public interface IAccountService
    {
        CardAccount ProvisionCardAccount(string ownerId, double amount);
        IList<CardAccountSummary> GetAccountSummaries();
        void UpdateCardAccount(long cardNumber, double amount);
    }
}
