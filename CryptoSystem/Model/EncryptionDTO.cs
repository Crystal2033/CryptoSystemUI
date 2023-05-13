using CryptoSystem.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XTR_TWOFISH.CypherEnums;

namespace CryptoSystem.Model
{
    public sealed class EncryptionDTO : INotifyPropertyChanged
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        private string fileToEncrypt = "";

		public string FileToEncrypt
		{
			get { return fileToEncrypt; }
			set { fileToEncrypt = value; OnPropertyChanged(nameof(FileToEncrypt)); }
		}


		private string resultEncryptFile = "";

		public string ResultEncryptFile
		{
			get { return resultEncryptFile; }
			set { resultEncryptFile = value; OnPropertyChanged(nameof(ResultEncryptFile)); }
		}

		private BigInteger secretA = 0;

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

        private long fileSize = long.MaxValue;

        public long FileSize
        {
            get { return fileSize; }
            set { fileSize = value; OnPropertyChanged(nameof(FileSize)); }
        }

        private long cypheredBytes = 0;

        public long CypheredBytes
        {
            get { return cypheredBytes; }
            set { cypheredBytes = value; OnPropertyChanged(nameof(CypheredBytes)); OnPropertyChanged(nameof(ProgressPercents)); }
        }


        private double progressPercents = 0;

        public double ProgressPercents
        {
            get { return Math.Round((double)CypheredBytes / (double)FileSize, 2) * 100; }
            set { progressPercents = value; OnPropertyChanged(nameof(ProgressPercents)); }
        }

        private Status cryptStatus;

        public Status CryptStatus
        {
            get { return cryptStatus; }
            set { cryptStatus = value; OnPropertyChanged(nameof(CryptStatus)); }
        }

        private string errorMsg = "";
        public string ErrorMessage
        {
            get { return errorMsg; }
            set { errorMsg = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }


        private ICommand onDeleteWidget;

        public ICommand OnDeleteWidget
        {
            get { return onDeleteWidget ?? (onDeleteWidget = new RelayCommand(DeleteWidget, CanDeleteWidget)); }
            set { onDeleteWidget = value; }
        }
        private void DeleteWidget(object param)
        {

        }

        private bool CanDeleteWidget(object param)
        {
            if (CryptStatus == Status.RUNNING)
            {
                return false;
            }
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
