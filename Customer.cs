using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace CodeContractBank
{
    class Customer
    {
        public int Id { get; private set; }// private mutator as id is unique identifier.
        public string Name { get; set; }
        public Dictionary<int, Account> Accounts { get; set; }

        public Customer(int id, string name)
        {    // Create Customer without existing account. Will be assigned new one.
            // Precondition: id isn't 0 or negative, and name is not null, empty or whitespace.
            Contract.Requires<ArgumentException>(id > 0 && !String.IsNullOrEmpty(name) && !String.IsNullOrWhiteSpace(name));

            // Postcondition: Name and Id is assigned values given as parameters.
            // Revised - No new account added. Because of unique number..
            Contract.EnsuresOnThrow<AggregateException>(Name == name && Id == id);

            Id = id;
            Name = name;
            Accounts = new Dictionary<int, Account>();
        }

        public Customer(int id, string name, Account firstAccount)
        {  // Create Customer with existing account.
           // Precondition: id isn't 0, and name is not null, empty or whitespace.
            Contract.Requires<ArgumentException>(id > 0 && !String.IsNullOrEmpty(name) && !String.IsNullOrWhiteSpace(name) && firstAccount != null);

            // Postcondition: Name and Id is assigned values given as parameters. firstAccount is added to Accounts.
            Contract.EnsuresOnThrow<AggregateException>(Name == name && Id == id && Accounts.Count == 1 && Accounts[firstAccount.Number] == firstAccount);

            Id = id;
            Name = name;
            Accounts = new Dictionary<int, Account>();
            Accounts.Add(firstAccount.Number, firstAccount);
        }
    }
}
