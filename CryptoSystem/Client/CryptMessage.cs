using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CryptoSystem.Client.Models;
using XTR_TWOFISH.CypherEnums;
using XTR_TwofishAlgs.MathBase.GaloisField;

namespace Client
{
    public class CryptMessage
    {
		private GFP2.Polynom1DegreeCoeffs? traceG;

		public GFP2.Polynom1DegreeCoeffs? TraceG
		{
			get { return traceG; }
			set { traceG = value; }
		}

        private BigInteger? p;

        public BigInteger? P
        {
            get { return p; }
            set { p = value; }
        }

        private BigInteger? q;

        public BigInteger? Q
        {
            get { return q; }
            set { q = value; }
        }

        private int? keySize;

        public int? KeySize
        {
            get { return keySize; }
            set { keySize = value; }
        }

        private string? fileIn;

        public string? FileIn
        {
            get { return fileIn; }
            set { fileIn = value; }
        }

        private string? fileOut;

        public string? FileOut
        {
            get { return fileOut; }
            set { fileOut = value; }
        }

        private CypherMode? symmetricMode;

        public CypherMode? SymmetricMode
        {
            get { return symmetricMode; }
            set { symmetricMode = value; }
        }

        private byte[]? initVector;

        public byte[]? InitVector
        {
            get { return initVector; }
            set { initVector = value; }
        }

        private MessageType? messageType;

        public MessageType? MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        private string? keyFile;

        public string? KeyFile
        {
            get { return keyFile; }
            set { keyFile = value; }
        }

        private bool hasError = false;

        public bool HasError
        {
            get { return hasError; }
            set { hasError = value; }
        }

        private string errorMsg;

        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }


    }
}
