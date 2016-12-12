using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace CodeContractBank
{
    class Bank
    {
        public string Name { get; set; }
        public Dictionary<int, Customer> Customers { get; set; }
        public Dictionary<int, Account> Accounts { get; set; }

        public Bank(string name)
        {
            // Precondition: name is not null, empty or just whitespace.
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(name) && !String.IsNullOrWhiteSpace(name));
            // Postcondition: Name is now name
            Contract.EnsuresOnThrow<AggregateException>(Name == name);
            Name = name;
            Customers = new Dictionary<int, Customer>();
            Accounts = new Dictionary<int, Account>();
        }

        public void Move(double amount, int sourceAccount, int targetAccount)
        {
            // Precondition: Amount is not negative or 0, and neither of the 2 accounts are null.
            Contract.Requires<ArgumentException>(amount > 0 && Accounts[sourceAccount] != null && Accounts[targetAccount] != null);

            // Postcondition: target and source contain movements, which sum to 0 (double entry bookkeeping)
            var sourceMovement = (Movement)Accounts[sourceAccount].Movements.Where(m => m.Amount == amount * -1);
            var targetMovement = (Movement)Accounts[targetAccount].Movements.Where(m => m.Amount == amount);
            Contract.EnsuresOnThrow<AggregateException>(sourceMovement != null && targetMovement != null && (targetMovement.Amount + sourceMovement.Amount) == 0);

            Account source = Accounts[sourceAccount];
            Account target = Accounts[targetAccount];

            source.Movements.Add(new Movement(DateTime.Now, amount * -1));
            target.Movements.Add(new Movement(DateTime.Now, amount));
        }

        public void MakeStatement(int customerId, int accountNumber)
        {
            Contract.Requires<ArgumentException>(Accounts[accountNumber] != null && Customers[customerId] != null);
            // Should probably just do Console.Writeline. therefore return void

            var customer = Customers[customerId];
            var account = Accounts[accountNumber];

            Console.WriteLine(Name);
            Console.WriteLine("Bank Statement");
            Console.WriteLine("________________________________________________");
            Console.WriteLine(customer.Name);
            Console.Write("Customer Id: " + customer.Id);
            Console.WriteLine("         " + "Account No: " + account.Number);
            Console.WriteLine("________________________________________________");
            Console.WriteLine("Total balance: " + account.Balance);
            Console.WriteLine("________________________________________________");
            foreach (var movement in account.Movements)
            {
                Console.Write(movement.Amount + "DKK" + "  --  ");
                Console.WriteLine(movement.Date);
            }

        }
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            // Invariably, the sum of all movements in all accounts should ALWAYS equals 0.
            // Reason; Double Entry Bookkeeping.
            
           // var sum = Accounts.ToList().ForEach(a => a.Value.Movements.Sum(m => m.Amount));
            Contract.Invariant(GetSumOfMovements() == 0.0);
        }

        [Pure]
        private double GetSumOfMovements()
        {
            double sum = 0.0;
            Accounts.Values.ToList().ForEach(a => sum += a.Movements.Sum(m => m.Amount));
            return sum;
        }
    }
}
