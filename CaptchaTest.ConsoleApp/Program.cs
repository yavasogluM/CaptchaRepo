using System;
using Captcha.Helper;

namespace CaptchaTest.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ICaptchaHelper captchaHelper = new CaptchaHelper();

            while (true)
            {
                var captchaValue = captchaHelper.GenerateForSumQuestionCaptcha().Result;

                Console.WriteLine($"{captchaValue.CaptchaText}");

                string input = Console.ReadLine();

                bool captchaControl = captchaHelper.CaptchaIsValid(input, captchaValue).Result;

                if (captchaControl)
                {
                    Console.WriteLine("Doğru Cevap!");
                }
                else
                {
                    Console.WriteLine("Yanlış Cevap");
                }
            }
        }
    }
}
