using Client;
using CryptoSystem.Client.Models;
using CryptoSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSystem.Converters
{
    public sealed class DecryptDTOIntoCryptMessage
    {
        public static CryptMessage Convert(DecryptionDTO decryptionDTO)
        {
            CryptMessage message = new CryptMessage();
            message.FileIn = decryptionDTO.FileToDecrypt;
            message.FileOut = decryptionDTO.ResultDecryptFile;
            message.KeyFile = decryptionDTO.KeyFile;
            message.MessageType = MessageType.DECRYPTION;
            return message;
        }
    }
}
