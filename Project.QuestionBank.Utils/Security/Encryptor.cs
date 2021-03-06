﻿using System.Security.Cryptography;
using System.Text;

namespace Project.QuestionBank.Utils.Security
{
    /// <summary>
    /// 加密静态类
    /// Author:刘健
    /// </summary>
    public static class EncryptHelper
    {
        //MD5加密一个字符串
        public static string Md5Hash(this string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            foreach (var t in result)
            {
                strBuilder.Append(t.ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
