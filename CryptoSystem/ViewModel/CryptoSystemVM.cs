using CryptoSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
