using Instant.APIModels;
using Instant.EFData;
using Instant.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Instant.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ICardNumberGenerator numberGenerator;

        public AccountService(
            ApplicationDbContext applicationDbContext,
            ICardNumberGenerator numberGenerator
            )
        {
            this.applicationDbContext = applicationDbContext;
            this.numberGenerator = numberGenerator;
        }

        public IList<CardAccountSummary> GetAccountSummaries()
        {
            return applicationDbContext
                .CardAccounts
                .Select(x => x.ToSummary())
                .ToList();
        }

        public CardAccount ProvisionCardAccount(string ownerId, double amount)
        {
            var cardAccount = applicationDbContext
                .CardAccounts
                .FirstOrDefault(x => !x.HasMoney);

            if (cardAccount is null)
            {
                return CreateAccount(ownerId, amount);
            }
            else
            {
                var cardNumber = numberGenerator.GenerateCardNumber();
                cardAccount.EmitCardNumber(ownerId, amount, cardNumber);

                applicationDbContext.SaveChanges();

                return cardAccount;
            }
        }

        public void UpdateCardAccount(long cardNumber, double amount)
        {
            var card = applicationDbContext
                .CardAccounts
                .FirstOrDefault(x => x.CurrentCardNumber == cardNumber);

            if (card is null)
            {
                // TODO: handle bad client request (ex: no card number provided)
                return;
            }

            card.CurrentAmount = 0;
            numberGenerator.FreeCardNumber(card.CurrentCardNumber);

            applicationDbContext.SaveChanges();
        }

        private CardAccount CreateAccount(string ownerId, double amount)
        {
            var newAccount = new CardAccount();
            var cardNumber = numberGenerator.GenerateCardNumber();

            newAccount.EmitCardNumber(ownerId, amount, cardNumber);

            applicationDbContext.Add(newAccount);
            applicationDbContext.SaveChanges();

            return newAccount;
        }
    }
}
