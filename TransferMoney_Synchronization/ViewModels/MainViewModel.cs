using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TransferMoney_Synchronization.Commands;
using TransferMoney_Synchronization.Models;

namespace TransferMoney_Synchronization.ViewModels
{
    public class MainViewModel : BaseViewModel
    {


        public User CurrentUser { get; set; }
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
            set { pinCode = value; OnPropertyChanged(); }
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
            set { surname = value; OnPropertyChanged(); }
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
            set { from = value; OnPropertyChanged(); }
        }

        private double to;

        public double To
        {
            get { return to; }
            set { to = value; OnPropertyChanged(); }
        }

        public RelayCommand TransferCommand { get; set; }


        public string MutexName { get; set; }

        public Mutex Mutex { get; set; }



        public MainViewModel()
        {
            MutexName = "Mutex";
            Mutex = new Mutex(false, MutexName);

            bool hasFinished = false;
            CloseCommand = new RelayCommand(c =>
            {
                MainWindow.Close();
            });



            LoadDataCommand = new RelayCommand(c =>
            {
                if (App.Users.Any(d => d.Card.CardNumber == CardNumber && d.Card.PinCode == PinCode))
                {
                    CurrentUser = App.Users.FirstOrDefault(u => u.Card.CardNumber == CardNumber && u.Card.PinCode == PinCode);
                    Name = CurrentUser.Name;
                    Surname = CurrentUser.Surname;
                    PinCode = "";
                    CardNumber = "";
                    hasFinished = false;
                }
                else
                {
                    MessageBox.Show("Wrong inputs", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    PinCode = "";
                    CardNumber = "";
                }
            });
            TransferCommand = new RelayCommand(c =>
            {

                Thread thread = new Thread(d =>
                {

                    if (CurrentUser.Card.Balance >= Amount)
                    {

                        if (!Mutex.WaitOne())
                        {

                            MessageBox.Show("Process going");


                        }
                        else if (!hasFinished)
                        {
                            var amount = Amount / 10;

                            for (int i = 0; i < 10; i++)
                            {
                                Thread.Sleep(1000);
                                CurrentUser.Card.Balance -= amount;
                                From = CurrentUser.Card.Balance;
                                To += amount;
                            }
                            hasFinished = true;
                            MessageBox.Show("Transfer has been done successfully");

                            From = 0;
                            To = 0;
                            Amount = 0;
                            Name = "";
                            Surname = "";
                        }



                        Mutex.ReleaseMutex();


                    }
                    else
                    {
                        MessageBox.Show("There is not enough money in your balance");
                    }

                });
                thread.Start();

            }, (a) =>
            {
                if (Amount != 0 && Surname != null && Name != null)
                {
                    return true;
                }
                return false;
            }
            );

        }

    }
}
