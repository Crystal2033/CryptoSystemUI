using CryptoSystem.Commands;
using CryptoSystem.DialogFunctions;
using CryptoSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoSystem.ViewModel
{
    public sealed class DecryptionDialogVM
    {
        private DecryptionDTO decryptionInfo;
        public DecryptionDTO DecryptionInfo { get => decryptionInfo ?? (decryptionInfo = new()); set => decryptionInfo = value; }


        private ICommand onLoadFileToDecrypt;

        public ICommand OnLoadFileToDecrypt
        {
            get { return onLoadFileToDecrypt ?? (onLoadFileToDecrypt = new RelayCommand(LoadFileToDecrypt, CanLoadFileToDecrypt)); ; }
            set { onLoadFileToDecrypt = value; }
        }

        private void LoadFileToDecrypt(object param)
        {
            decryptionInfo.FileToDecrypt = SaverLoaderFiles.GetFileNameToLoad();
        }

        private bool CanLoadFileToDecrypt(object param)
        {
            return true;
        }


        private ICommand onLoadKeyFile;

        public ICommand OnLoadKeyFile
        {
            get { return onLoadKeyFile ?? (onLoadKeyFile = new RelayCommand(LoadKeyFile, CanLoadKeyFile));}
            set { onLoadKeyFile = value; }
        }

        private void LoadKeyFile(object param)
        {
            decryptionInfo.KeyFile = SaverLoaderFiles.GetFileNameToLoad("Keys|*.private_key*.txt");
        }

        private bool CanLoadKeyFile(object param)
        {
            return true;
        }


        private ICommand onLoadResultDecryptFile;

        public ICommand OnLoadResultDecryptFile
        {
            get { return onLoadResultDecryptFile ?? (onLoadResultDecryptFile = new RelayCommand(LoadResultDecryptFile, CanLoadResultDecryptFile)); ; }
            set { onLoadResultDecryptFile = value; }
        }

        private void LoadResultDecryptFile(object param)
        {
            decryptionInfo.ResultDecryptFile = SaverLoaderFiles.GetFileNameToSave();
        }

        private bool CanLoadResultDecryptFile(object param)
        {
            return true;
        }


        private ICommand onMakeDecryption;

        public ICommand OnMakeDecryption
        {
            get { return onMakeDecryption ?? (onMakeDecryption = new RelayCommand(MakeDecryption, CanMakeDecryption)); }
            set { onMakeDecryption = value; }
        }
        private void MakeDecryption(object param)
        {

        }

        private bool CanMakeDecryption(object param)
        {
            if (DecryptionInfo.FileToDecrypt != "" && DecryptionInfo.ResultDecryptFile != "" &&
                DecryptionInfo.KeyFile != "" )
            {
                if (DecryptionInfo.FileToDecrypt != DecryptionInfo.ResultDecryptFile)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
