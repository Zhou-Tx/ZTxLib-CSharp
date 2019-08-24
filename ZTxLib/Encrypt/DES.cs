using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace ZTxLib.Encrypt
{
    /// <summary>
    /// DES对称密钥加解密算法
    /// </summary>
    public static class DES
    {
        private static readonly byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密算法 
        /// </summary>
        /// <param name="text">原串</param>
        /// <param name="desKey">密钥</param>
        /// <returns>密串</returns>
        public static string DES_Encrypt(this string text, string desKey = "")
        {
            while (desKey.Length < 8)
                desKey += ' ';
            byte[] byKey = null;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(text);
            byKey = Encoding.UTF8.GetBytes(desKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// DES解密算法
        /// </summary>
        /// <param name="text">密串</param>
        /// <param name="desKey">密钥</param>
        /// <returns>原串</returns>
        public static string DES_Decrypt(this string text, string desKey = "")
        {
            while (desKey.Length < 8)
                desKey += ' ';
            byte[] byKey = null;
            byte[] inputByteArray = new byte[text.Length];
            byKey = Encoding.UTF8.GetBytes(desKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(text);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
