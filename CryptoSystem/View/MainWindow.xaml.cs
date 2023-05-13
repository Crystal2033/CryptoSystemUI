using Client;
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
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt= @"D:\Paul\Programming\C#\XTR_Twofish\ClientServer\testFiles", ResultEncryptFile= @"D:\Paul\Programming\C#\XTR_Twofish\ClientServer\testFiles", CypheredBytes = (long)(long.MaxValue*0.75), CryptStatus=Model.Status.RUNNING });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { CypheredBytes = long.MaxValue/2, CryptStatus=Model.Status.RUNNING });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { CypheredBytes = long.MaxValue/2, CryptStatus=Model.Status.RUNNING });
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
            timer.Interval = TimeSpan.FromSeconds(2);
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
                    MessageBox.Show($"Is faulted: {task.Key}");
                    clientReciever.Tasks.Remove(task.Key);
                    getContext().SetCryptStatus(task.Key, task.Value.cryptOp, Model.Status.FAILED);
                    //getContext().DeleteCryptOperationFromWidgets(task.Key, task.Value.cryptOp);
                    //set error status
                }
                else if (task.Value.task.IsCompletedSuccessfully)
                {
                    clientReciever.Tasks.Remove(task.Key);
                    //getContext().SetCryptStatus(task.Key, task.Value.cryptOp, Model.Status.SUCCESS);
                    //getContext().DeleteCryptOperationFromWidgets(task.Key, task.Value.cryptOp);
                    //set done status
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
