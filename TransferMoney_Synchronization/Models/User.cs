using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferMoney_Synchronization.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public BankCard Card { get; set; }
    }
}
