using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace CodeContractBank
{
    class Movement
    {
        // All mutators are private, as a movement should not be able to be changed once its created.
        public DateTime Date { get; private set; }
        public double Amount { get; private set; }

        public Movement(DateTime date, double amount)
        {
            // Precondition: DateTime is not null, and amount is not 0 or negative.
            Contract.Requires<ArgumentException>(date != null && amount > 0);

            // Postcondition: Properties are given values from constructor parameter.
            Contract.EnsuresOnThrow<AggregateException>(Date == date && Amount == amount);
        }
    }
}
