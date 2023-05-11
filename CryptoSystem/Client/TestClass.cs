using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    //public sealed class TestClass
    //{
    //    public MyClient Client;
    //    public TestClass()
    //    {
    //        Client = new();
    //    }
    //    public async void StartRecieving()
    //    {
    //        try
    //        {
    //            Client.Connect();
    //            Task.Run(() => { Client.StartRecievingResponses(); }); 
    //        }
    //        catch (StreamNotOpenedException ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //            //MessageBox.Show($"Impossible to open buffer for data exchanging. Message {ex.Message}", "StreamNotOpenedException", MessageBoxButton.OK, MessageBoxImage.Error);
    //            //Close();

    //        }
    //        catch (SocketException ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //        }
    //    }

    //    public async void Send()
    //    {
    //        CryptMessage msg = new();
    //        await Client.SendMessageAsync(msg, 10000);
    //    }
    //}
}
