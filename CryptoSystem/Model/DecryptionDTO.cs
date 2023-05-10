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
        private string fileToDecrypt;

        public string FileToDecrypt
        {
            get { return fileToDecrypt; }
            set { fileToDecrypt = value; OnPropertyChanged(nameof(FileToDecrypt)); }
        }


        private string resultDecryptFile;

        public string ResultDecryptFile
        {
            get { return resultDecryptFile; }
            set { resultDecryptFile = value; OnPropertyChanged(nameof(ResultDecryptFile)); }
        }

        private string keyFile;

        public string KeyFile
        {
            get { return keyFile; }
            set { keyFile = value; OnPropertyChanged(nameof(KeyFile)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
