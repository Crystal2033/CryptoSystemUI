using CryptoSystem.Commands;
using CryptoSystem.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoSystem.Model
{
    public sealed class DecryptionDTO : INotifyPropertyChanged
    {
        public string Id { get; } = Guid.NewGuid().ToString();

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
            if(CryptStatus == Status.RUNNING)
            {
                return false;
            }
            return true;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
