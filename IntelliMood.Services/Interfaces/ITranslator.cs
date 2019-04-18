using Google.Cloud.Translation.V2;

namespace IntelliMood.Services.Interfaces
{
    public interface ITranslator
    {
        TranslationResult Translate(string text, string targetLanguage);
    }
}
