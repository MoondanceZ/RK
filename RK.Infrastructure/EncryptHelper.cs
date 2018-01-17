using NETCore.Encrypt;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Infrastructure
{
    public static class EncryptHelper
    {
        private const string AES_KEY = "bAt0zg5mD3gn7irMHzXuvB9GgqSL0sHJ";
        private const string AES_IV = "p0cXJsGsuzSzEpc6";

        public static string AESEncrypt(string srcString)
        {
            return EncryptProvider.AESEncrypt(srcString, AES_KEY);
        }

        public static string AESEncrypt_IV(string srcString)
        {
            return EncryptProvider.AESEncrypt(srcString, AES_KEY, AES_IV);
        }

        public static string AESDecrypt(string encryptedStr)
        {
            return EncryptProvider.AESDecrypt(encryptedStr, AES_KEY);
        }

        public static string AESDecrypt_IV(string encryptedStr)
        {
            return EncryptProvider.AESDecrypt(encryptedStr, AES_KEY, AES_IV);
        }
    }
}
