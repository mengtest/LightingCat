using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System;


public static class Security
{
    #region 获取字符串的MD5值
    public static string Md5Encrypt(string input)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        var data = System.Text.Encoding.UTF8.GetBytes(input);
        var encs = md5.ComputeHash(data);
        return System.BitConverter.ToString(encs).Replace("-", "");
    }

    public static string Md5Encrypt(byte [] data)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        var encs = md5.ComputeHash(data);
        return System.BitConverter.ToString(encs).Replace("-", "");
    }
    #endregion
    #region 获取文件的MD5值
    public static string md5file(string file)
    {
        try
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("md5file() fail, error:" + ex.Message);
        }
    }
    #endregion
    #region Des加密
    //密钥  
    private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    public static string default_keyss = "bfbf2333";
    public static byte[] EncryptDES(byte[] bytes, string encryptKey)
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = bytes;
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return mStream.ToArray();
        }
        catch
        {
            return bytes;
        }
    }

    public static string EncryptDESByStr(string encryptString, string encryptKey)
    {
        return Convert.ToBase64String(EncryptDES(Encoding.UTF8.GetBytes(encryptString), encryptKey));
    }

    public static byte[] DecryptDES(byte[] bytes, string decryptKey)
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = bytes;
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return mStream.ToArray();
        }
        catch
        {
            return bytes;
        }
    }

    public static string DecryptDESByStr(string decryptString, string decryptKey)
    {
        return Convert.ToBase64String(DecryptDES(Encoding.UTF8.GetBytes(decryptString), decryptKey));
    }
    #endregion
}
