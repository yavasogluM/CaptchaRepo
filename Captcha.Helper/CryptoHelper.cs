using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Captcha.Helper
{
    public interface ICryptoHelper
    {
        Task<string> Encrypt(string InputText);
        Task<string> Decrypt(string EnctyptedText);
    }
    public class MD5CryptoHelper : ICryptoHelper
    {
        private string HashText = "yourhashtext!";

        public async Task<string> Decrypt(string EnctyptedText)
        {
            try
            {

                byte[] data = Convert.FromBase64String(EnctyptedText);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(HashText));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        string result = Encoding.UTF8.GetString(results);
                        return await Task.FromResult(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> Encrypt(string InputText)
        {
            try
            {

                byte[] data = Encoding.UTF8.GetBytes(InputText);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(HashText));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        string result = Convert.ToBase64String(results, 0, results.Length);
                        return await Task.FromResult(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
