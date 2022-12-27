using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TransferMoney_Synchronization.Commands;

namespace TransferMoney_Synchronization.ViewModels
{
    public class MainViewModel:BaseViewModel
    {

        public RelayCommand CloseCommand { get; set; }

        public Window MainWindow { get; set; }
        public RelayCommand LoadDataCommand { get; set; }


        private string cardNumber;

        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; OnPropertyChanged(); }
        }


        private string pinCode;

        public string PinCode
        {
            get { return pinCode; }
            set { pinCode = value;OnPropertyChanged(); }
        }


        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }


        private string surname;

        public string Surname
        {
            get { return surname; }
            set { surname = value;OnPropertyChanged(); }
        }


        private double amount;

        public double Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(); }
        }



        private double from;

        public double From
        {
            get { return from; }
            set { from = value;OnPropertyChanged(); }
        }

        private double to;

        public double To
        {
            get { return to; }
            set { to = value;OnPropertyChanged(); }
        }

        public RelayCommand TransferCommand { get; set; }

        public MainViewModel()
        {
            CloseCommand = new RelayCommand(c =>
            {
                MainWindow.Close();
            });



            LoadDataCommand = new RelayCommand(c =>
            {
                if(App.Users.Any(d=>d.Card.CardNumber==CardNumber && d.Card.PinCode==PinCode))
                {
                    var user = App.Users.FirstOrDefault(u => u.Card.CardNumber == CardNumber && u.Card.PinCode == PinCode);
                    Name = user.Name;
                    Surname = user.Surname;
                    PinCode = "";
                    CardNumber = "";
                }
                else
                {
                    MessageBox.Show("Wrong inputs","Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
                    PinCode = "";
                    CardNumber = "";
                }
            });

            TransferCommand = new RelayCommand(c =>
            {

            });

        }

    }
}
