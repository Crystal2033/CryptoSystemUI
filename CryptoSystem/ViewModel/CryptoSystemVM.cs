using Client;
using CryptoSystem.Client.Models;
using CryptoSystem.Commands;
using CryptoSystem.Converters;
using CryptoSystem.Model;
using CryptoSystem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        }

        public void SetCryptStatus(string id, MessageType cryptOp, Status status)
        {
            if (cryptOp == MessageType.ENCRYPTION)
            {
                EncryptionDTO widget = EncryptionWidgets.FirstOrDefault(encrDto => encrDto.Id == id);
                if (widget != null)
                {
                    widget.CryptStatus = status;
                }
            }
            else if (cryptOp == MessageType.DECRYPTION)
            {
                DecryptionDTO widget = DecryptionWidgets.FirstOrDefault(decrDto => decrDto.Id == id);
                
                if (widget != null)
                {
                    widget.CryptStatus = status;
                }
            }
        }

        public bool DeleteCryptOperationFromWidgets(string id)
        {
            
            EncryptionDTO encryptionWidget = EncryptionWidgets.FirstOrDefault(encrDto => encrDto.Id == id);
            if (encryptionWidget != null)
            {
                EncryptionWidgets.Remove(encryptionWidget);
                return true;
            }
                
            DecryptionDTO decryptionWidget = DecryptionWidgets.FirstOrDefault(decrDto => decrDto.Id == id);
            if (decryptionWidget != null)
            {
                DecryptionWidgets.Remove(decryptionWidget);
                return true;
            }
          
            return false;
        }

        private bool CanAddEncryption(object param)
        {
            return true;
        }


        private long GetFileSize(string path)
        {
            if (File.Exists(path))
            {
                return new FileInfo(path).Length;
            }
            return 0;
        }
        private async Task MakeEncryption(EncryptionDTO encryptionDTO)
        {
            CryptMessage cryptMessage = EncryptDTOIntoCryptMessage.Convert(encryptionDTO);
            if (File.Exists(encryptionDTO.ResultEncryptFile))
            {
                File.Delete(encryptionDTO.ResultEncryptFile);
            }
            await Client.SendMessageAsync(cryptMessage, encryptionDTO.SecretA);
            encryptionDTO.FileSize = GetFileSize(encryptionDTO.FileToEncrypt);
            await Task.Run(() =>
            {
                while (encryptionDTO.CypheredBytes < encryptionDTO.FileSize && encryptionDTO.CryptStatus == Status.RUNNING)
                {
                    encryptionDTO.CypheredBytes = GetFileSize(encryptionDTO.ResultEncryptFile);
                    if(encryptionDTO.CypheredBytes > encryptionDTO.FileSize)
                    {
                        encryptionDTO.CypheredBytes = encryptionDTO.FileSize;
                    }
                }
                if(encryptionDTO.CryptStatus == Status.RUNNING)
                {
                    encryptionDTO.CryptStatus = Status.SUCCESS;
                }
            });
        }

        private ICommand onAddDecryption;

        public ICommand OnAddDecryption
        {
            get { return onAddDecryption ?? (onAddDecryption = new RelayCommand(AddDecryption, CanAddDecryption)); }
            set { onAddDecryption = value; }
        }
        private void AddDecryption(object param)
        {
            DecryptionWindow decryptionWindow = new();
            if (decryptionWindow.ShowDialog() == true)
            {
                DecryptionDTO decryptionDTO = decryptionWindow.getContext().DecryptionInfo;
                Task.Run(() => { MakeDecryption(decryptionDTO); });
                decryptionWidgets.Add(decryptionDTO);
            }
        }

        private bool CanAddDecryption(object param)
        {
            return true;
        }

        private async Task MakeDecryption(DecryptionDTO decryptionDTO)
        {
            CryptMessage cryptMessage = DecryptDTOIntoCryptMessage.Convert(decryptionDTO);
            if (File.Exists(decryptionDTO.ResultDecryptFile))
            {
                File.Delete(decryptionDTO.ResultDecryptFile);
            }

            await Client.SendMessageAsync(cryptMessage, 0);
            decryptionDTO.FileSize = GetFileSize(decryptionDTO.FileToDecrypt);
            await Task.Run(() =>
            {
                while (decryptionDTO.CypheredBytes < decryptionDTO.FileSize && decryptionDTO.CryptStatus == Status.RUNNING)
                {
                    decryptionDTO.CypheredBytes = GetFileSize(decryptionDTO.ResultDecryptFile);
                    if (decryptionDTO.FileSize - decryptionDTO.CypheredBytes < 16) //PADDING PCKS7
                    {
                        decryptionDTO.CypheredBytes = decryptionDTO.FileSize;
                        break;
                    }
                }
                if(decryptionDTO.CryptStatus == Status.RUNNING)
                {
                    decryptionDTO.CryptStatus = Status.SUCCESS;
                }
                
            });
        }

       
    }
}
