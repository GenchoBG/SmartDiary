using System;
using System.IO;
using Google.Cloud.Translation.V2;
using IntelliMood.Services.Interfaces;

namespace IntelliMood.Services.Implementations
{
    public class GoogleTranslator : ITranslator
    {
        private readonly TranslationClient client;

        public GoogleTranslator()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Path.Combine(Environment.CurrentDirectory, @"..\IntelliMood.Services\ApiKeys", "GoogleTranslatorKey.json"));

            this.client = TranslationClient.Create();
        }

        public TranslationResult Translate(string text, string targetLanguage)
        {
            var response = this.client.TranslateText(text: text, targetLanguage: targetLanguage);

            return response;
        }
    }
}
