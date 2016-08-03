using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FdDecode
{
    public class HeaderSecurity
    {
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Encrypt(string text, string key)
        {
            var targetKey = MD5Creater.getKey(key);
            return MD5Creater.Encrypt_DES(text, targetKey);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public string Decrypt(string text, string sKey)
        {
            var key = MD5Creater.getKey(sKey);
            return MD5Creater.Decrypt_DES(text, key);
        }
    }
}
