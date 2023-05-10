using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSystem.Model
{
    public sealed class DecryptionDTO
    {
        private string fileToDecrypt;

        public string FileToDecrypt
        {
            get { return fileToDecrypt; }
            set { fileToDecrypt = value; }
        }


        private string resultDecryptFile;

        public string ResultDecryptFile
        {
            get { return resultDecryptFile; }
            set { resultDecryptFile = value; }
        }

        private string keyFile;

        public string KeyFile
        {
            get { return keyFile; }
            set { keyFile = value; }
        }
    }
}
