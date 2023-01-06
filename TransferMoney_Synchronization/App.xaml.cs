using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TransferMoney_Synchronization.Models;

namespace TransferMoney_Synchronization
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static List<User> Users { get; set; }
        public App()
        {
            Users = new List<User>
            {
                new User
                {
                     Name="John",
                      Surname="Johnlu",
                       Card=new BankCard
                       {
                            CardNumber="1234123412341234",
                             PinCode="1234",
                             Balance=4500
                       }
                },
                  new User
                {
                     Name="Ali",
                      Surname="Ahmadov",
                       Card=new BankCard
                       {
                            CardNumber="4321432143214321",
                             PinCode="4321",
                             Balance=1200
                       }
                },

            };
        }
    }
}
