﻿using Client;
using Client.Exceptions;
using CryptoSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using XTR_TwofishAlgs.Exceptions;

namespace CryptoSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClientRecieverVM clientReciever;
        private Task proceesingTask;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CryptoSystemVM();
            clientReciever = new(getContext().Client);
            try
            {
                clientReciever.Connect();
            }
            catch (StreamNotOpenedException ex)
            {
                MessageBox.Show($"Impossible to open buffer for data exchanging. Message {ex.Message}", "StreamNotOpenedException", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Impossible to open buffer for data exchanging. Message {ex.Message}", "SocketException", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

            proceesingTask = clientReciever.Processing();

            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += CheckTaskResultsAndMakeActions;
            timer.Start();
        }

        public void CheckTaskResultsAndMakeActions(object sender, EventArgs e)
        {
            if (proceesingTask.IsFaulted)
            {
                MessageBox.Show($"Your application is unexpectedly finished because of server`s problems. Message {proceesingTask.Exception.Message}", $"{proceesingTask.Exception}", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

            foreach(var task in clientReciever.Tasks)
            {
                if (task.Value.task.IsFaulted) //thrown exception
                {
                    MessageBox.Show($"{task.Value.cryptOp} operation has been failed because of {task.Value.task.Exception.Message}.", task.Value.task.Exception.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    clientReciever.Tasks.Remove(task.Key);
                    getContext().SetCryptStatus(task.Key, task.Value.cryptOp, Model.Status.FAILED, task.Value.task.Exception.Message);
                }
                else if (task.Value.task.IsCompletedSuccessfully)
                {
                    clientReciever.Tasks.Remove(task.Key);
                    getContext().SetCryptStatus(task.Key, task.Value.cryptOp, Model.Status.SUCCESS);
                }
            }
        }

        public CryptoSystemVM? getContext()
        {
            return DataContext as CryptoSystemVM;
        }
        private void OnDeleteWidgetClicked(object sender, RoutedEventArgs e)
        {
            string id = (string)((Button)sender).Tag;
            getContext().DeleteCryptOperationFromWidgets(id);
        }
    }
}
