using Client.Exceptions;
using CryptoSystem.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using XTR_TwofishAlgs.Exceptions;

namespace Client
{
    public sealed class MyClient
    {
        private readonly string host = "localhost";
        private readonly int port = 8080;
        private StreamReader? Reader = null;
        private StreamWriter? Writer = null;
        private TcpClient client;

        public MyClient()
        {
            client = new TcpClient();
        }

        public void Connect()
        {
            
            client.Connect(host, port); //подключение клиента
            Reader = new StreamReader(client.GetStream());
            Writer = new StreamWriter(client.GetStream());

            if (Writer is null || Reader is null)
            {
                throw new StreamNotOpenedException("Stream are not opened. Can`t make data exchange.");
            }
        }

        public void CloseBuffers()
        {
            Writer?.Close();
            Reader?.Close();
        }

        //public async void StartRecievingResponses(Dictionary<string, Task> tasks)
        //{
        //    // запускаем новый поток для получения данных
        //    try
        //    {
        //        await ReceiveMessageAsync(tasks);
        //    }
        //    catch (MyClientException ex)
        //    {
        //        throw new MyClientException(ex.Message);
        //    }
            
        //}
        public async Task SendMessageAsync(CryptMessage cryptInfo, BigInteger secretA)
        {
            if (cryptInfo.MessageType == MessageType.ENCRYPTION)
            {
                CryptoMachine.PrepareDataBeforeEncryption(cryptInfo);
                CryptoMachine.CreateKeyFile(cryptInfo, secretA);
            }
            else if (cryptInfo.MessageType == MessageType.DECRYPTION)
            {
                CryptoMachine.InsertDataFromKeyFileIntoCryptMessage(cryptInfo);
            }

            string message = JsonConvert.SerializeObject(cryptInfo, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            Console.WriteLine(message);

            await Writer.WriteLineAsync(message);
            await Writer.FlushAsync();

        }
        // получение сообщений

        public Task ReceiveMessageAsync(Dictionary<string, (MessageType cryptOp, Task task)> tasks,CancellationToken token = default)
        {
            while (true)
            {
                if(Reader == null)
                {
                    throw new StreamNotOpenedException("Reader is null");
                }
                string? message = Reader.ReadLine();

                CryptMessage cryptMessage = JsonConvert.DeserializeObject<CryptMessage>(message);
                if (cryptMessage.HasError)
                {
                    MyClientException myClientExc = new(cryptMessage.ErrorMsg);
                    tasks[cryptMessage.Id] = (cryptMessage.MessageType.Value, Task.FromException(myClientExc));
                    return tasks[cryptMessage.Id].task;
                }
                SecretKeyData keyData;
                try
                {
                    keyData = CryptoMachine.GetKeyDataFromFile(cryptMessage.KeyFile);
                }
                catch (Exception ex)
                {
                    throw new MyClientException(ex.Message);
                }
                CryptoMachine.SetPartOfKeyForCryptOperation(cryptMessage, keyData.SecretA);// tr(g^ab)

                if (cryptMessage.MessageType == MessageType.ENCRYPTION)
                {
                    tasks[cryptMessage.Id] = (cryptMessage.MessageType.Value, Task.Run(() => 
                    {
                        CryptoMachine.EncryptAsync(cryptMessage, token);
                    }, token));
                }
                else if (cryptMessage.MessageType == MessageType.DECRYPTION)
                {
                    tasks[cryptMessage.Id] = (cryptMessage.MessageType.Value, Task<Exception>.Run(() =>
                    {
                        Task innerTask = CryptoMachine.DecryptAsync(cryptMessage, token);
                        innerTask.GetAwaiter().GetResult();
                        return innerTask.Exception;
                    }, token));
                }
            }
        }
    }
}
