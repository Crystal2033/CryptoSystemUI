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
    public sealed class EncryptDTOIntoCryptMessage
    {
        public static CryptMessage Convert(EncryptionDTO encryptionDTO)
        {
            CryptMessage message = new CryptMessage();
            message.FileIn = encryptionDTO.FileToEncrypt;
            message.FileOut = encryptionDTO.ResultEncryptFile;
            message.KeySize = encryptionDTO.KeySize;
            message.SymmetricMode = encryptionDTO.SymmetricMode;
            message.MessageType = MessageType.ENCRYPTION;
            return message;
        }
    }
}
