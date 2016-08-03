using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;

namespace FdDecode
{
    internal class MD5Creater
    {
        public enum KeyFormat { None, ToUpper, ToLower }
        private static byte[] IV_KEY = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 将会把传入的Key转换为大写
        /// </summary>
        /// <param name="key"></param>
        /// <param name="KeyToUpper"></param>
        /// <returns></returns>
        static public string Create(string key, KeyFormat formater = KeyFormat.ToUpper)
        {
            switch (formater)
            {
                case KeyFormat.ToUpper:
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key.ToUpper(), "MD5");
                case KeyFormat.ToLower:
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key.ToLower(), "MD5");
                case KeyFormat.None:
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "MD5");
                default:
                    return null;
            }
        }

        /// <summary>
        /// DES 加密函数
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <param name="key">密钥，且必须为8位</param>
        /// <returns>以Base64格式返回的加密字符串</returns>
        public static string DES_Encrypt(string encryptString, string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptString));
                byte[] inputByteArray = Encoding.UTF8.GetBytes(base64String);

                des.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                des.IV = IV_KEY;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.Zeros;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string result = Convert.ToBase64String(ms.ToArray());
                ms.Close();

                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DES_Decrypt(string decryptString, string key)
        {
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                des.IV = IV_KEY;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.Zeros;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string base64String = Encoding.UTF8.GetString(ms.ToArray());
                base64String = base64String.TrimEnd('\0');
                string result = Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
                ms.Close();

                return result;
            }
        }



        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";

            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }

            return returnStr;
        }



        /// <summary>
        /// DES 加密函数
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <param name="key">密钥，且必须为8位</param>
        /// <returns>以Base64格式返回的加密字符串</returns>
        public static string DES_Encrypt2(string encryptString, string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                string base64String = Base64Code(Encoding.UTF8.GetBytes(encryptString));
                byte[] inputByteArray = Encoding.UTF8.GetBytes(base64String);

                des.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                des.IV = IV_KEY;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.Zeros;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string result = Convert.ToBase64String(ms.ToArray());
                ms.Close();

                return result;
            }
        }
        /// <summary>

        /// Base64加密

        /// </summary>

        /// <param name="str"></param>

        /// <returns></returns>

        public static string Base64Code(byte[] inputArray)
        {

            char[] Base64Code = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',

　　'U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n',

　　'o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7',

　　'8','9','+','/','='};

            byte empty = (byte)0;

            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(inputArray);

            System.Text.StringBuilder outmessage;

            int messageLen = byteMessage.Count;

            //将字符分成3个字节一组，如果不足，则以0补齐

            int page = messageLen / 3;

            int use = 0;

            if ((use = messageLen % 3) > 0)
            {

                for (int i = 0; i < 3 - use; i++)

                    byteMessage.Add(empty);

                page++;

            }

            //将3个字节的每组字符转换成4个字节一组的。3个一组，一组一组变成4个字节一组

            //方法是：转换成ASCII码，按顺序排列24 位数据，再把这24位数据分成4组，即每组6位。再在每组的的最高位前补两个0凑足一个字节。

            outmessage = new System.Text.StringBuilder(page * 4);

            for (int i = 0; i < page; i++)
            {

                //取一组3个字节的组

                byte[] instr = new byte[3];

                instr[0] = (byte)byteMessage[i * 3];

                instr[1] = (byte)byteMessage[i * 3 + 1];

                instr[2] = (byte)byteMessage[i * 3 + 2];

                //六个位为一组，补0变成4个字节

                int[] outstr = new int[4];

                //第一个输出字节：取第一输入字节的前6位，并且在高位补0，使其变成8位（一个字节）

                outstr[0] = instr[0] >> 2;

                //第二个输出字节：取第一输入字节的后2位和第二个输入字节的前4位（共6位），并且在高位补0，使其变成8位（一个字节）

                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);

                //第三个输出字节：取第二输入字节的后4位和第三个输入字节的前2位（共6位），并且在高位补0，使其变成8位（一个字节）

                if (!instr[1].Equals(empty))

                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);

                else

                    outstr[2] = 64;

                //第四个输出字节：取第三输入字节的后6位，并且在高位补0，使其变成8位（一个字节）

                if (!instr[2].Equals(empty))

                    outstr[3] = (instr[2] & 0x3f);

                else

                    outstr[3] = 64;

                outmessage.Append(Base64Code[outstr[0]]);

                outmessage.Append(Base64Code[outstr[1]]);

                outmessage.Append(Base64Code[outstr[2]]);

                outmessage.Append(Base64Code[outstr[3]]);

            }

            return outmessage.ToString();

        }


        #region 新增加密解密
        /// <summary>
        /// DES加密 没有BASE64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String Encrypt_DES(string str, string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(str);

                des.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                des.IV = IV_KEY;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.Zeros;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string result = Convert.ToBase64String(ms.ToArray());
                ms.Close();

                return result;
            }
        }

        /// <summary>
        ///  DES解密 没有BASE64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decrypt_DES(string str, string key)
        {

            byte[] inputByteArray = Convert.FromBase64String(str);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                des.IV = IV_KEY;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.Zeros;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string base64String = Encoding.UTF8.GetString(ms.ToArray());
                base64String = base64String.TrimEnd('\0');
                string result = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(base64String));
                ms.Close();

                return result;
            }
        }


        private static int getStringHashCode(string str)
        {
            byte[] bts = System.Text.Encoding.Default.GetBytes(str);
            int code = 0;
            for (int j = 0; j < bts.Length; j++)
            {
                code += getHashCode(bts[j]);
            }
            return code;
        }

        private static int getHashCode(byte b)
        {
            return (b << 2) ^ 37;
        }

        public static string[] chars = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I",
		"J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
		"W", "X", "Y", "Z","0", "1", "2", "3", "4", "5",
			"6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i",
			"j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v",
			"w", "x", "y", "z", "+","="};


        /**
         * 从加密的字符串中，提取8位密钥
         * @param sKey
         * @return
         * @throws Exception
         */
        public static string getKey(string sKey)
        {
            //加盐
            string content = sKey;
            content += getStringHashCode(sKey);
            //md5散列
            content = Create(content, KeyFormat.None);
            //8位散列
            StringBuilder shortBuffer = new StringBuilder();
            int leng = chars.Length;
            for (int i = 0; i < 8; i++)
            {
                string str = content.Substring(i * 4, 4);
                int x = Convert.ToInt32(str, 16);
                shortBuffer.Append(chars[x % leng]);
            }
            return shortBuffer.ToString();
        }
        #endregion


    }
}
