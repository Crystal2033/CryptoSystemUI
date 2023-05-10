using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using XTR_TWOFISH.CypherEnums;

namespace CryptoSystem.Model
{
    public sealed class EncryptionDTO : INotifyPropertyChanged
    {
		private string fileToEncrypt;

		public string FileToEncrypt
		{
			get { return fileToEncrypt; }
			set { fileToEncrypt = value; OnPropertyChanged(nameof(FileToEncrypt)); }
		}


		private string resultEncryptFile;

		public string ResultEncryptFile
		{
			get { return resultEncryptFile; }
			set { resultEncryptFile = value; OnPropertyChanged(nameof(ResultEncryptFile)); }
		}

		private BigInteger secretA;

		public BigInteger SecretA
		{
			get { return secretA; }
			set { secretA = value; OnPropertyChanged(nameof(SecretA)); }
		}

		private int keySize = 192;

		public int KeySize
		{
			get { return keySize; }
			set { keySize = value; OnPropertyChanged(nameof(KeySize)); }
		}

		private CypherMode symmetricMode = CypherMode.RD;
		public CypherMode SymmetricMode
		{
			get { return symmetricMode; }
			set { symmetricMode = value; OnPropertyChanged(nameof(SymmetricMode)); }
		}


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
