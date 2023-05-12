using CryptoSystem.Client.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using XTR_TWOFISH.CypherModes;
using XTR_TwofishAlgs.MathBase;
using XTR_TwofishAlgs.MathBase.GaloisField;
using XTR_TwofishAlgs.XTR;

namespace Client
{
    public sealed class CryptoMachine
    {
        public static void PrepareDataBeforeEncryption(CryptMessage dataToEnrich)
        {
            (dataToEnrich.P, dataToEnrich.Q) = XTR_TwofishAlgs.MathBase.StandartMathTricks.GeneratePQ(170, 160);
            GFP2 galoisFieldP2 = new GFP2(dataToEnrich.P.Value);
            XTRFunctions xtrFunctions = new(galoisFieldP2.Primary, dataToEnrich.Q.Value);
            dataToEnrich.TraceG = xtrFunctions.GenerateTrace().Second;

            Random r = new();
            dataToEnrich.InitVector = new byte[16];
            r.NextBytes(dataToEnrich.InitVector);
        }

        public static Task EncryptAsync(CryptMessage cryptMessage, CancellationToken token = default)
        {
            byte[] symmetricKey = CreateSymmetricKeyByTrace(cryptMessage.TraceG, cryptMessage.KeySize.Value);
            AdvancedCypherSym advancedCypherSym = new(symmetricKey, cryptMessage.SymmetricMode.Value, XTR_TWOFISH.CypherEnums.SymmetricAlgorithm.TWOFISH, cryptMessage.InitVector);
            return advancedCypherSym.EncryptAsync(cryptMessage.FileIn, cryptMessage.FileOut);
        }

        public static Task DecryptAsync(CryptMessage cryptMessage, CancellationToken token = default)
        {
            byte[] symmetricKey = CreateSymmetricKeyByTrace(cryptMessage.TraceG, cryptMessage.KeySize.Value);
            AdvancedCypherSym advancedCypherSym = new(symmetricKey, cryptMessage.SymmetricMode.Value, XTR_TWOFISH.CypherEnums.SymmetricAlgorithm.TWOFISH, cryptMessage.InitVector);
            return advancedCypherSym.DecryptAsync(cryptMessage.FileIn, cryptMessage.FileOut);
        }

        public static byte[] CreateSymmetricKeyByTrace(GFP2.Polynom1DegreeCoeffs trace, int keySize)
        {
            byte[] symmetricKey = new byte[keySize / 8];
            string firstPartOfKey = trace.First.ToString();
            string secondPartOfKey = trace.Second.ToString();
            for(int i = 0; i < keySize / 16; i++)
            {
                symmetricKey[i] = (byte)firstPartOfKey[i];
                symmetricKey[i + symmetricKey.Length / 2] = (byte)secondPartOfKey[i];
            }
            return symmetricKey;
        }
        public static void CreateKeyFile(CryptMessage cryptMessage, BigInteger secretA)
        {
            var fileName = new DirectoryInfo(cryptMessage?.FileIn).Name;
            var fullDirName = Path.GetDirectoryName(cryptMessage?.FileIn);

            Regex regex = new Regex(@"([\w\-\s]*)\.");
            MatchCollection matches = regex.Matches(fileName);
            string fileNameWithoutExtension;
            if (matches.Count > 0)
            {
                fileNameWithoutExtension = matches[0].Value;
            }
            else
            {
                Console.WriteLine("Wrong file name. Regular expression can not parse this value.");
                return;
            }
            SecretKeyData secretKeyData = new();
            secretKeyData.TraceG = cryptMessage.TraceG;
            secretKeyData.P = cryptMessage.P;
            secretKeyData.Q = cryptMessage.Q;
            if(secretA == 0)
            {
                secretKeyData.SecretA = GenRandomBigValue(cryptMessage);
            }
            else
            {
                secretKeyData.SecretA = secretA;
            }

            string jsonSecretKey = JsonConvert.SerializeObject(secretKeyData, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            string secretKeyFilePath = fullDirName + @$"\{fileNameWithoutExtension}private_key.txt";
            cryptMessage.KeyFile = secretKeyFilePath;

            InsertJSONInFile(secretKeyFilePath, jsonSecretKey);
        }

        public static void InsertJSONInFile(string secretKeyFilePath, string jsonSecretKey)
        {
            FileStream fs;
            if (File.Exists(secretKeyFilePath))
            {
                File.SetAttributes(secretKeyFilePath, File.GetAttributes(secretKeyFilePath) & ~FileAttributes.ReadOnly);
                fs = File.Open(secretKeyFilePath, FileMode.Truncate);
            }
            else
            {
                fs = File.Open(secretKeyFilePath, FileMode.OpenOrCreate);
            }

            fs.Write(Encoding.UTF8.GetBytes(jsonSecretKey), 0, jsonSecretKey.Length);

            File.SetAttributes(secretKeyFilePath, FileAttributes.ReadOnly);
            fs.Close();
        }

        public static void InsertDataFromKeyFileIntoCryptMessage(CryptMessage cryptMessage)
        {
            SecretKeyData keyData = GetKeyDataFromFile(cryptMessage.KeyFile);
            cryptMessage.TraceG = keyData.TraceG;
            cryptMessage.P = keyData.P;
            cryptMessage.Q = keyData.Q;
        }

        public static SecretKeyData GetKeyDataFromFile(string path)
        {
            string keyDataStr = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<SecretKeyData>(keyDataStr);
        }
        
        public static void InsertSecretAValueInKeyFile(BigInteger secretA, string path)
        {
            string keyDataStr = File.ReadAllText(path);
            SecretKeyData keyData = JsonConvert.DeserializeObject<SecretKeyData>(keyDataStr);
            keyData.SecretA = secretA;

            string newKeyDataWithSecretA = JsonConvert.SerializeObject(keyData, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            InsertJSONInFile(path, newKeyDataWithSecretA);
        }

        public static void SetPartOfKeyForCryptOperation(CryptMessage dataForEncrypt, BigInteger powValue)
        {
            GFP2 galoisFieldP2 = new GFP2(dataForEncrypt.P.Value);
            XTRFunctions xtrFunctions = new(galoisFieldP2.Primary, dataForEncrypt.Q.Value);
            dataForEncrypt.TraceG = xtrFunctions.SFunction(powValue, dataForEncrypt.TraceG).Second;
        }

        public static BigInteger GenRandomBigValue(CryptMessage dataCrypt)
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                return StandartMathTricks.RandomInRange(rng, dataCrypt.Q.Value / 4, dataCrypt.P.Value / 2);
            }
        }
    }
}
