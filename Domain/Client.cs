﻿
namespace AccountingSystemApi.Domain
{
    public class Client
    {
        public int ClientId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
