using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace TTS
{
    class Program
    {
        static void Main()
        {
            Tts().GetAwaiter().GetResult();
        }

        static async Task Tts()
        {
            const string iamToken = "7f6e18e76c35deff77fba0922cba747f2a115ec8%3A1595872765"; // Укажите IAM-токен.

            HttpClient client = new HttpClient();
            // client.DefaultRequestHeaders.Add("Authorization", "Bearer " + iamToken);
            client.DefaultRequestHeaders.Add("x-csrf-token",iamToken);
            var values = new Dictionary<string, string>
      {
      {"emotion", "good"},
      {"format", "oggopus"},
      {"language", "ru-RU"},
      {"message", "Привет! Я Yandex SpeechKit. Я могу превратить любой текст в речь. Теперь и в+ы - можете"},
      {"speed", "1"},
      {"voice", "alena"}
      };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://cloud.yandex.ru/api/speechkit/tts", content);
            var responseBytes = await response.Content.ReadAsByteArrayAsync();
            File.WriteAllBytes("speech.ogg", responseBytes);
        }
    }
}