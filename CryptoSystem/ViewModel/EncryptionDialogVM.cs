using CryptoSystem.Commands;
using CryptoSystem.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoSystem.ViewModel
{
    public sealed class EncryptionDialogVM
    {
		private EncryptionDTO encryptionInfo;

		public EncryptionDTO EncryptionInfo
		{
			get { return encryptionInfo ?? (encryptionInfo = new()); }
			set { encryptionInfo = value; }
		}

		private ICommand onLoadFileToEncrypt;

		public ICommand OnLoadFileToEncrypt
		{
			get { return onLoadFileToEncrypt ?? (onLoadFileToEncrypt = new RelayCommand(LoadFileToEncrypt, CanLoadFileToEncrypt)); }
			set { onLoadFileToEncrypt = value; }
		}
		private void LoadFileToEncrypt(object param)
		{
            encryptionInfo.FileToEncrypt = GetFileNameToLoad();
        }

        private bool CanLoadFileToEncrypt(object param)
        {
			return true;
        }



        private ICommand onSaveResultFile;

        public ICommand OnSaveResultFile
        {
            get { return onSaveResultFile ?? (onSaveResultFile = new RelayCommand(SaveResultFile, CanSaveResultFile)); }
            set { onSaveResultFile = value; }
        }
        private void SaveResultFile(object param)
        {
            encryptionInfo.ResultEncryptFile = GetFileNameToSave();
        }

        private bool CanSaveResultFile(object param)
        {
            return true;
        }

        private string GetFileNameToLoad()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text|*.*|All|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                return fileDialog.FileName;
            }
            return "";
        }

        private string GetFileNameToSave()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Text|*.*|All|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                return fileDialog.FileName;
            }
            return "";
        }


        private ICommand onMakeEncryption;

        public ICommand OnMakeEncryption
        {
            get { return onMakeEncryption ?? (onMakeEncryption = new RelayCommand(MakeEncryption, CanMakeEncryption)); }
            set { onMakeEncryption = value; }
        }
        private void MakeEncryption(object param)
        {

        }

        private bool CanMakeEncryption(object param)
        {
            if(EncryptionInfo.FileToEncrypt != "" && EncryptionInfo.ResultEncryptFile != "" &&
                EncryptionInfo.SecretA > 0 && EncryptionInfo.KeySize != 0 && EncryptionInfo.SymmetricMode > 0)
            {
                return true;
            }
            return false;
        }

    }
}
