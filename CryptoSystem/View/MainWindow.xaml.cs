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
using XTR_TwofishAlgs.Exceptions;

namespace CryptoSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool hasToRecieveMsgs = true;
        private CancellationTokenSource cancellationTokenSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CryptoSystemVM();
            cancellationTokenSource = new();
            try
            {
                getContext().Client.Connect();
                var cryptSystem = getContext();
                var client = cryptSystem.Client;

                Task.Run(() =>
                {
                    while (hasToRecieveMsgs)
                    {
                        try
                        {//TODO: Collections with task and CTS (task, CTS)
                            var task = client.ReceiveMessageAsync(cancellationTokenSource.Token);

                            //over of try
                        }
                        catch (MyClientException ex)
                        {
                            cryptSystem.DecryptionWidgets[cryptSystem.DecryptionWidgets.Count - 1].FileToDecrypt = "Failed";
                            MessageBox.Show($"Client exception. Message {ex.Message}", "MyClientException", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (DamagedFileException ex)
                        {
                            cryptSystem.DecryptionWidgets[cryptSystem.DecryptionWidgets.Count - 1].FileToDecrypt = "Damaged";
                            MessageBox.Show($"Decrypting file had been damaged. Message {ex.Message}", "DamagedFileException", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                });
            }
            catch (StreamNotOpenedException ex)
            {
                MessageBox.Show($"Impossible to open buffer for data exchanging. Message {ex.Message}", "StreamNotOpenedException", MessageBoxButton.OK, MessageBoxImage.Error);
                hasToRecieveMsgs = false;
                Close();
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Impossible to open buffer for data exchanging. Message {ex.Message}", "SocketException", MessageBoxButton.OK, MessageBoxImage.Error);
                hasToRecieveMsgs = false;
                Close();
            }
        }

        public CryptoSystemVM? getContext()
        {
            return DataContext as CryptoSystemVM;
        }
    }
}
