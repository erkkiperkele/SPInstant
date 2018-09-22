using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instant.EFData
{
    [Table("CardAccount")]
    public class CardAccount
    {
        public CardAccount()
        {
        }

        [Column]
        public int Id { get; private set; }

        [Column]
        public long CurrentCardNumber { get; private set; }

        [Column]
        public string OwnerId { get; private set; }

        [Column]
        public double CurrentAmount { get; set; }

        public bool HasMoney => CurrentAmount > 0;

        public void EmitCardNumber(string ownerId, double amount, long cardNumber)
        {
            OwnerId = ownerId;
            CurrentAmount = amount;
            CurrentCardNumber = cardNumber;
        }
    }
}
