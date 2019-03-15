using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using unirest;
using unirestt;

namespace IntelliMood.Services
{
    public class EmotionGetter : IEmotionGetter
    {

        public string GetEmotionFromText(string text)
        {

            HttpResponse<EmotionGetterData> response = Unirest
                .get("https://qemotion.p.rapidapi.com/v1/emotional_analysis/get_emotions")
                .header("X-RapidAPI-Key", "4b5190dbbcmshb5fb4ffa955fd61p14b864jsn5c89fd1d4138")
                .header("Authorization", "Token token=\"bc55ca0a8f5c8c41556f499a93f7077a\"")
                .header("lang", "en")
                .header("text", text)
                .asJson<EmotionGetterData>();

            return response.Body.Content.Emotions.GetDominantEmotion();
        }
    }

    public class EmotionGetterData
    {
        public Content Content { get; set; }
    }

    public class Content
    {
        public Emotions Emotions { get; set; }
    }

    public class Emotions
    {
        public double Happiness { get; set; }
        public double Surprise { get; set; }
        public double Calm { get; set; }
        public double Fear { get; set; }
        public double Sadness { get; set; }
        public double Anger { get; set; }
        public double Disgust { get; set; }

        public int Emotional_intensity_rate { get; set; }

        public string GetDominantEmotion()
        {
            var maxName = "";
            var maxValue = double.MinValue;

            var properties = this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.Name != nameof(Emotional_intensity_rate))
                {
                    var value = (double)propertyInfo.GetValue(this);
                    if (value > maxValue)
                    {
                        maxValue = value;
                        maxName = propertyInfo.Name;
                    }
                }
            }

            return maxName;

        }
    }
}
