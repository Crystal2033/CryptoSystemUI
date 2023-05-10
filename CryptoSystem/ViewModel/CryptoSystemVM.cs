using Client;
using CryptoSystem.Commands;
using CryptoSystem.Converters;
using CryptoSystem.Model;
using CryptoSystem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoSystem.ViewModel
{
    public sealed class CryptoSystemVM
    {
        private MyClient client;
        public MyClient Client { get => client; }
        public CryptoSystemVM()
        {
            client = new();
        }

		private ObservableCollection<EncryptionDTO> encryptionWidgets;

		public ObservableCollection<EncryptionDTO> EncryptionWidgets
		{
			get => encryptionWidgets ?? (encryptionWidgets = new());

            set { encryptionWidgets = value; }
		}
        private ObservableCollection<DecryptionDTO> decryptionWidgets;

        public ObservableCollection<DecryptionDTO> DecryptionWidgets
        {
            get => decryptionWidgets ?? (decryptionWidgets = new());

            set { decryptionWidgets = value; }
        }


        private ICommand onAddEncryption;

        public ICommand OnAddEncryption
        {
            get { return onAddEncryption ?? (onAddEncryption = new RelayCommand(AddEncryption, CanAddEncryption)); }
            set { onAddEncryption = value; }
        }
        private void AddEncryption(object param)
        {
            EncryptionWindow encryptionWindow = new();
            if (encryptionWindow.ShowDialog() == true)
            {
                EncryptionDTO encryptionDTO = encryptionWindow.getContext().EncryptionInfo;
                Task.Run(() => { MakeEncryption(encryptionDTO);});
                encryptionWidgets.Add(encryptionDTO);
            }
            else
            {
                MessageBox.Show("Add Encryption cancelled");
            }
        }

        private async Task MakeEncryption(EncryptionDTO encryptionDTO)
        {
            CryptMessage cryptMessage = EncryptDTOIntoCryptMessage.Convert(encryptionDTO);
            await Client.SendMessageAsync(cryptMessage, encryptionDTO.SecretA);
            //TODO: MAKE CALL CLIENT ENCRYPTION, map into CryptoMessage
        }

        private bool CanAddEncryption(object param)
        {
            return true;
        }
    }
}
