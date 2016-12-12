using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace CodeContractBank
{
    class Account
    {
        public int Number { get; private set; } // private mutator as accountNumber is unique identifier.
        public List<Movement> Movements { get; set; }

        public double Balance
        {
            // Described as a calculated field. Therefore no instance field, only return calculated value.
            get
            {
                return Movements.Sum(m => m.Amount);
            }
        }

        public Account(int number)
        {
            // Precondition: number is not 0 or negative
            Contract.Requires<ArgumentException>( number > 0);

            // Postcondition: Properties are given values from constructor parameter.
            Contract.EnsuresOnThrow<AggregateException>(Number == number);

            Number = number;
            Movements = new List<Movement>();
        }
    }
}
