using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Captcha.Helper
{
    public interface ICaptchaHelper
    {
        Task<bool> CaptchaIsValid(string InputValue, Models.CaptchaModel CaptchaValue);
        Task<bool> CaptchaIsValid(string InputValue, string CaptchaValue);
        Task<Models.CaptchaModel> GenerateForSumQuestionCaptcha();
    }

    public class CaptchaHelper : ICaptchaHelper
    {
        private readonly ICryptoHelper cryptoHelper;

        public CaptchaHelper()
        {
            cryptoHelper = new MD5CryptoHelper();
        }


        public async Task<Models.CaptchaModel> GenerateForSumQuestionCaptcha()
        {
            int Random1, Random2, RandomTotal;
            Random1 = new Random().Next(10, 99);
            Random2 = new Random().Next(10, 99);
            RandomTotal = Random1 + Random2;

            return await Task.FromResult(new Models.CaptchaModel
            {
                CaptchaText = $"{Random1} + {Random2} = ?",
                CaptchaValue = await cryptoHelper.Encrypt(RandomTotal.ToString())
            });
        }

        public async Task<bool> CaptchaIsValid(string InputValue, string CaptchaValue)
        {
            string CryptedInputValue = await cryptoHelper.Encrypt(InputValue);
            bool CaptchaControl = CryptedInputValue == CaptchaValue;
            return await Task.FromResult(CaptchaControl);
        }

        public async Task<bool> CaptchaIsValid(string InputValue, Models.CaptchaModel CaptchaValue)
        {
            string CryptedInputValue = await cryptoHelper.Encrypt(InputValue);
            bool CaptchaControl = CryptedInputValue == CaptchaValue.CaptchaValue;
            return await Task.FromResult(CaptchaControl);
        }
    }
}
