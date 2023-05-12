using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSystem.Model
{
    public sealed class DecryptionDTO : INotifyPropertyChanged
    {
        private string fileToDecrypt = "";

        public string FileToDecrypt
        {
            get { return fileToDecrypt; }
            set { fileToDecrypt = value; OnPropertyChanged(nameof(FileToDecrypt)); }
        }


        private string resultDecryptFile = "";

        public string ResultDecryptFile
        {
            get { return resultDecryptFile; }
            set { resultDecryptFile = value; OnPropertyChanged(nameof(ResultDecryptFile)); }
        }

        private string keyFile = "";

        public string KeyFile
        {
            get { return keyFile; }
            set { keyFile = value; OnPropertyChanged(nameof(KeyFile)); }
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
            set { cryptStatus = value; }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
