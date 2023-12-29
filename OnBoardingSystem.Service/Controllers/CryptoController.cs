using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingSystem.Data.Abstractions.Models;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace OnBoardingSystem.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
      //  private readonly byte[] key;
        //private readonly byte[] _key = Encoding.UTF8.GetBytes("11e6e89555d0e78beb62260b56e7d8f8033da42cb37291046bce2cd4445c01a1");
        private AesCryptoServiceProvider _aesCryptoProvider;
        private byte[] _initVector;
        public CryptoController()
        {
             _aesCryptoProvider = new AesCryptoServiceProvider();
            // this.key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
        
        }
        [HttpPost("Encrypt")]
        public IActionResult Encrypt([FromBody] string Plaintext)
        {
           
            try
            {
                byte[] key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
                byte[] nonce = Encoding.UTF8.GetBytes("0123456789AB");
                byte[] ciphertext = EncryptAesGcm(Encoding.UTF8.GetBytes(Plaintext), key, nonce);
                //string encryptedData = GCM(Plaintext);
                return Ok(new { ciphertext = ciphertext });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("Decrypt")]
        public IActionResult Decrypt([FromBody] byte[] ciphertext)
        {
            try
            {
                byte[] key = GenerateRandomBytes(32);
                byte[] nonce = GenerateRandomBytes(12);
                string decryptedText = DecryptAesGcm(ciphertext, key, nonce);
                return Ok(new { plaintext = decryptedText });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        //public string EncryptData(string data)
        //{
        //    ImportRawKey("11e6e89555d0e78beb62260b56e7d8f8033da42cb37291046bce2cd4445c01a1");
        //    // Generate a random initialization vector (IV)
        //    byte[] initVector = GenerateRandomBytes(16);

        //    try
        //    {
        //        // Set the IV in the AES provider
        //        _aesCryptoProvider.IV = initVector;
        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, _aesCryptoProvider.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        //                cryptoStream.Write(dataBytes, 0, dataBytes.Length);
        //                cryptoStream.FlushFinalBlock();
        //            }
        //            // Combine IV and encrypted data
        //            byte[] encryptedData = memoryStream.ToArray();
        //            byte[] result = new byte[initVector.Length + encryptedData.Length];
        //            Buffer.BlockCopy(initVector, 0, result, 0, initVector.Length);
        //            Buffer.BlockCopy(encryptedData, 0, result, initVector.Length, encryptedData.Length);

        //            string encryptedString = Convert.ToBase64String(result);
        //            return encryptedString;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle encryption error
        //        return ("Encryption error: " + ex.Message);
        //    }
        //}
        //private string DecryptData(string encryptedData)
        //{
        //    ImportRawKey("11e6e89555d0e78beb62260b56e7d8f8033da42cb37291046bce2cd4445c01a1");
        //    byte[] key = _aesCryptoProvider.Key;

        //    try
        //    {
        //        // Convert the base64-encoded encrypted data to a byte array
        //        byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
        //        // Extract IV from the beginning of the byte array
        //        _initVector = new byte[16];
        //        Buffer.BlockCopy(encryptedBytes, 0, _initVector, 0, _initVector.Length);
        //        // Remove IV from the byte array
        //        byte[] cipherText = new byte[encryptedBytes.Length - _initVector.Length];
        //        Buffer.BlockCopy(encryptedBytes, _initVector.Length, cipherText, 0, cipherText.Length);
        //        using (MemoryStream memoryStream = new MemoryStream(cipherText))
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, _aesCryptoProvider.CreateDecryptor(_aesCryptoProvider.Key, _initVector), CryptoStreamMode.Read))
        //            {
        //                using (StreamReader streamReader = new StreamReader(cryptoStream, Encoding.UTF8))
        //                {
        //                    string decryptedData = streamReader.ReadToEnd();
        //                    return decryptedData;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle decryption error
        //        return ("Decryption error: " + ex.Message);
        //    }
        //}

        //////private string DecryptData(string base64EncodedString)
        //////{
        //////    string encreptedDate;

        //////    try
        //////    {
        //////        //ImportRawKey("11e6e89555d0e78beb62260b56e7d8f8033da42cb37291046bce2cd4445c01a1");
        //////        // byte[] key = _aesCryptoProvider.Key;
        //////        // Convert the key from hexadecimal string to a byte array
        //////        string keyhex = "11e6e89555d0e78beb62260b56e7d8f8033da42cb37291046bce2cd4445c01a1";
        //////        byte[] key = StringToByteArray(keyhex);

        //////        // Convert the Base64-encoded string to a byte array
        //////        byte[] encryptedBytes = Convert.FromBase64String(base64EncodedString);

        //////        // Extract IV from the beginning of the byte array
        //////        byte[] iv = new byte[16]; // Assuming a 96-bit IV for AES-GCM
        //////        Buffer.BlockCopy(encryptedBytes, 0, iv, 0, iv.Length);

        //////        // Remove IV from the byte array
        //////        byte[] ciphertext = new byte[encryptedBytes.Length - iv.Length];
        //////        Buffer.BlockCopy(encryptedBytes, iv.Length, ciphertext, 0, ciphertext.Length);

        //////        // Prepare a buffer for the decrypted data
        //////        byte[] decryptedData = new byte[ciphertext.Length];

        //////        // Use AesGcm for decryption
        //////        using (AesGcm aesGcm = new AesGcm(key))
        //////        {
        //////            aesGcm.Decrypt(iv, ciphertext, null, decryptedData, null);
        //////        }

        //////        // Convert the decrypted data to a string using UTF-8 encoding
        //////        string result = Encoding.UTF8.GetString(decryptedData);
        //////        return result;
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        // Handle decryption error
        //////        return ("Decryption error: " + ex.Message);
        //////    }
        //////}
        //private byte[] StringToByteArray(string hex)
        //{
        //    int length = hex.Length / 2;
        //    byte[] bytes = new byte[length];
        //    for (int i = 0; i < length; i++)
        //    {
        //        bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        //    }
        //    return bytes;
        //}
        ////private void ImportRawKey(string rawKeyString)
        ////{
        ////    if (string.IsNullOrEmpty(rawKeyString))
        ////    {
        ////        Console.WriteLine("Invalid rawKeyString");
        ////        return;
        ////    }
        ////    // Convert rawKeyString to byte array
        ////    _aesCryptoProvider.Key = ConvertHexStringToByteArray(rawKeyString);
        ////}

        //private byte[] GenerateRandomBytes(int length)
        //{
        //    byte[] randomBytes = new byte[length];
        //    using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
        //    {
        //        rng.GetBytes(randomBytes);
        //    }
        //    return randomBytes;
        //}

        //private byte[] ConvertHexStringToByteArray(string hexString)
        //{
        //    int numberChars = hexString.Length;
        //    byte[] bytes = new byte[numberChars / 2];

        //    for (int i = 0; i < numberChars; i += 2)
        //    {
        //        bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        //    }
        //    return bytes;
        //}
        #region New Code
        //////private string Encryptata(string data)
        //////{
        //////    //ImportRawKey("11e6e89555d0e78beb62260b56e7d8f8033da42cb37291046bce2cd4445c01a1");
        //////    using (AesGcm aesGcm = new AesGcm(key))
        //////    {
        //////        byte[] iv = new byte[16]; // 96-bit IV for AES-GCM
        //////        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        //////        {
        //////            rng.GetBytes(iv);
        //////        }

        //////        byte[] plaintext = Encoding.UTF8.GetBytes(data);

        //////        byte[] ciphertext = new byte[plaintext.Length];
        //////        aesGcm.Encrypt(iv, plaintext, ciphertext, null);

        //////        byte[] result = new byte[iv.Length + ciphertext.Length];
        //////        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        //////        Buffer.BlockCopy(ciphertext, 0, result, iv.Length, ciphertext.Length);

        //////        return Convert.ToBase64String(result);
        //////    }
        //////}

        //private  string Encryptata(string PlainText)
        //{

        //    byte[] key = new byte[32];
        //    byte[] iv = new byte[16];
        //    string sR = string.Empty;

        //    try
        //    {
        //        byte[] plainBytes = Encoding.UTF8.GetBytes(PlainText);

        //        GcmBlockCipher cipher = new GcmBlockCipher(new AesFastEngine());
        //        AeadParameters parameters =
        //                     new AeadParameters(new KeyParameter(key), 128, iv, null);

        //        cipher.Init(true, parameters);

        //        byte[] encryptedBytes =
        //               new byte[cipher.GetOutputSize(plainBytes.Length)];
        //        Int32 retLen = cipher.ProcessBytes
        //                       (plainBytes, 0, plainBytes.Length, encryptedBytes, 0);
        //        cipher.DoFinal(encryptedBytes, retLen);
        //        sR = Convert.ToBase64String
        //             (encryptedBytes, Base64FormattingOptions.None);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.StackTrace);
        //    }

        //    return sR;
        //}
        //private string DecryptData(string encryptedData)
        //{
        //    byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

        //    using (AesGcm aesGcm = new AesGcm(key))
        //    {
        //        byte[] iv = new byte[16];
        //        Buffer.BlockCopy(encryptedBytes, 0, iv, 0, iv.Length);

        //        byte[] ciphertext = new byte[encryptedBytes.Length - iv.Length];
        //        Buffer.BlockCopy(encryptedBytes, iv.Length, ciphertext, 0, ciphertext.Length);

        //        byte[] decryptedData = new byte[ciphertext.Length];
        //        aesGcm.Decrypt(iv, ciphertext, decryptedData, null);

        //        return Encoding.UTF8.GetString(decryptedData);
        //    }
        //}

        #endregion

        #region(Vimal)
        static byte[] EncryptAesGcm(byte[] plaintext, byte[] key, byte[] nonce)
        {
            using AesGcm aesGcm = new AesGcm(key);
            byte[] ciphertext = new byte[plaintext.Length];
            byte[] tag = new byte[16]; // GCM tag size is 128 bits

            aesGcm.Encrypt(nonce, plaintext, ciphertext, tag);

            // Concatenate nonce + ciphertext + tag
            byte[] result = new byte[nonce.Length + ciphertext.Length + tag.Length];
            Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
            Buffer.BlockCopy(ciphertext, 0, result, nonce.Length, ciphertext.Length);
            Buffer.BlockCopy(tag, 0, result, nonce.Length + ciphertext.Length, tag.Length);

            return result;
        }

        static string DecryptAesGcm(byte[] ciphertext, byte[] key, byte[] nonce)
        {
            using AesGcm aesGcm = new AesGcm(key);

            int nonceLength = 12;
            int tagLength = 16;

            // Split nonce + ciphertext + tag
            byte[] nonceFromCiphertext = new byte[nonceLength];
            byte[] ciphertextOnly = new byte[ciphertext.Length - nonceLength - tagLength];
            byte[] tagFromCiphertext = new byte[tagLength];

            Buffer.BlockCopy(ciphertext, 0, nonceFromCiphertext, 0, nonceLength);
            Buffer.BlockCopy(ciphertext, nonceLength, ciphertextOnly, 0, ciphertext.Length - nonceLength - tagLength);
            Buffer.BlockCopy(ciphertext, nonceLength + ciphertextOnly.Length, tagFromCiphertext, 0, tagLength);
            // Decrypt
            byte[] decrypted = new byte[ciphertextOnly.Length];
            //if (aesGcm.Decrypt(nonceFromCiphertext, ciphertextOnly, tagFromCiphertext, decrypted))
            //{
            //    return Encoding.UTF8.GetString(decrypted);
            //}
            //else
            //{
                throw new CryptographicException("Failed to decrypt the ciphertext.");
           //}
        }
        static byte[] GenerateRandomBytes(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);
                return randomBytes;
            }
        }
        #endregion(Vimal)
    }
}
