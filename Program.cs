using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContractBank
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dummy data and testing.

            Bank hundigeBank = new Bank("Hundige Bank Inc.");
            var acc1 = new Account(1);
            Customer cust1 = new Customer(1, "Soren Borring Sørensen");
            cust1.Accounts.Add(acc1.Number, acc1);

            var acc2 = new Account(2);
            Customer cust2 = new Customer(2, "Martin Bagge Rasmussen");
            cust2.Accounts.Add(acc2.Number, acc2);

            var acc3 = new Account(3);
            Customer cust3 = new Customer(3, "Adolf Arlofelt Jørgensen");
            cust3.Accounts.Add(acc3.Number, acc3);

            hundigeBank.Accounts.Add(acc1.Number, acc1);
            hundigeBank.Accounts.Add(acc2.Number, acc2);
            hundigeBank.Accounts.Add(acc3.Number, acc3);
            hundigeBank.Customers.Add(cust1.Id, cust1);
            hundigeBank.Customers.Add(cust2.Id, cust2);
            hundigeBank.Customers.Add(cust3.Id, cust3);

            hundigeBank.Move(10000, 3, 1);
            hundigeBank.Move(17, 3, 2);
            hundigeBank.Move(17, 1, 2);
            hundigeBank.Move(22000, 2, 1);

            hundigeBank.MakeStatement(1, 1);

            Console.ReadKey();

        }
    }
}
