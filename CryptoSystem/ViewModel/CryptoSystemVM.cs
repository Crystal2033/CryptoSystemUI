using CryptoSystem.Commands;
using CryptoSystem.Model;
using CryptoSystem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoSystem.ViewModel
{
    public sealed class CryptoSystemVM
    {
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
                MakeEncryption(encryptionWindow.getContext().EncryptionInfo);
            }
            else
            {
                MessageBox.Show("Add Encryption cancelled");
            }
        }

        private void MakeEncryption(EncryptionDTO encryptionDTO)
        {
            encryptionWidgets.Add(encryptionDTO);
            //TODO: MAKE CALL CLIENT ENCRYPTION, map into CryptoMessage
        }

        private bool CanAddEncryption(object param)
        {
            return true;
        }
    }
}
