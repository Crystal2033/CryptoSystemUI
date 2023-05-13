using Client;
using Client.Exceptions;
using CryptoSystem.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using XTR_TwofishAlgs.Exceptions;

namespace CryptoSystem.ViewModel
{
    public sealed class ClientRecieverVM
    {

        private CancellationTokenSource cts;
        private Dictionary<string, (MessageType cryptOp, Task task)> tasks;

        public Dictionary<string, (MessageType cryptOp, Task task)> Tasks
        {
            get { return tasks ?? (tasks = new()); }
            set { tasks = value; }
        }

        private MyClient client;

        public ClientRecieverVM(MyClient client)
        {
            this.client = client;
            tasks = new();
            cts = new();
        }

        public void Connect()
        {
            client.Connect();
        }

        public Task Processing()
        {
            try
            {
                return Task.Run(() =>
                {
                    while (true)
                    {
                        try
                        {//TODO: Collections with task and CTS (task, CTS)
                            var task = client.ReceiveMessageAsync(tasks, cts.Token);
                        }
                        catch (MyClientException ex)
                        {
                            //cryptSystem.DecryptionWidgets[cryptSystem.DecryptionWidgets.Count - 1].FileToDecrypt = "Failed";
                            MessageBox.Show($"Client exception. Message {ex.Message}", "MyClientException", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (DamagedFileException ex)
                        {
                            //cryptSystem.DecryptionWidgets[cryptSystem.DecryptionWidgets.Count - 1].FileToDecrypt = "Damaged";
                            MessageBox.Show($"Decrypting file had been damaged. Message {ex.Message}", "DamagedFileException", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (StreamNotOpenedException ex)
                        {
                            //cryptSystem.DecryptionWidgets[cryptSystem.DecryptionWidgets.Count - 1].FileToDecrypt = "Damaged";
                            MessageBox.Show($"Buffer was unexpectedly closed. Message {ex.Message}", "StreamNotOpenedException", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        catch (IOException ex)
                        {
                            //cryptSystem.DecryptionWidgets[cryptSystem.DecryptionWidgets.Count - 1].FileToDecrypt = "Damaged";
                            MessageBox.Show($"There are problems with IO streams. Message {ex.Message}", "IOException", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                });
            }
            catch (StreamNotOpenedException ex)
            {
                MessageBox.Show($"Impossible to open buffer for data exchanging. Message {ex.Message}", "StreamNotOpenedException", MessageBoxButton.OK, MessageBoxImage.Error);
                return Task.FromException(ex);
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Impossible to open buffer for data exchanging. Message {ex.Message}", "SocketException", MessageBoxButton.OK, MessageBoxImage.Error);
                return Task.FromException(ex);
            }

            
        }
    }
}
