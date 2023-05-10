using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using XTR_TWOFISH.CypherEnums;

namespace CryptoSystem.Model
{
    public sealed class EncryptionDTO
    {
		private string fileToEncrypt;

		public string FileToEncrypt
		{
			get { return fileToEncrypt; }
			set { fileToEncrypt = value; }
		}


		private string resultEncryptFile;

		public string ResultEncryptFile
		{
			get { return resultEncryptFile; }
			set { resultEncryptFile = value; }
		}

		private BigInteger secretA;

		public BigInteger SecretA
		{
			get { return secretA; }
			set { secretA = value; }
		}

		private int keySize;

		public int KeySize
		{
			get { return keySize; }
			set { keySize = value; }
		}

		private CypherMode symmetricMode;

		public CypherMode SymmetricMode
        {
			get { return symmetricMode; }
			set { symmetricMode = value; }
		}



	}
}
