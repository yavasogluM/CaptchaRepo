using System;
using Xunit;

namespace Captcha.Tests
{
    public class UnitTest1
    {
        private readonly Helper.ICaptchaHelper _captchaHelper = new Helper.CaptchaHelper();

        [Fact]
        public void CreateCaptcha()
        {
            var result = _captchaHelper.GenerateForSumQuestionCaptcha().Result;
        }

        [Theory]
        [InlineData("")]
        public void CaptchaValidationTest(string InputValue)
        {
            var captchaValue = _captchaHelper.GenerateForSumQuestionCaptcha().Result;
            var result = _captchaHelper.CaptchaIsValid(InputValue, captchaValue).Result;
        }
    }
}
