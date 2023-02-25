using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskParallelLibrary.DTO;

namespace TaskParallelLibrary.Services
{
    internal class FiveDayWeatherService
    {
        private const string _urlFiveDayWeather = "https://api.openweathermap.org/data/2.5/forecast?{0}&appid=baeba02ddc02cd0fec249682efcb66e9&units=metric&lang=ru";

        /// <summary>
        /// Получить данные о погоде за 5 дней для города.
        /// </summary>
        /// <param name="city"> Наименование города.</param>
        /// <param name="token"> Токен отмены.</param>
        /// <returns></returns>
        public static FiveDayWeather GetFiveDayWeatherForCity(string city, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            var requestUrl = string.Format(_urlFiveDayWeather, $"q={city}");
            return GetFiveDayWeather(requestUrl, token);
        }

        /// <summary>
        /// Получает данные от сервера.
        /// </summary>
        /// <param name="url"> Адресс.</param>
        /// <param name="token"> Токен отмены.</param>
        /// <returns></returns>
        private static FiveDayWeather GetFiveDayWeather(string url, CancellationToken token)
        {
            var httWebRequest = WebRequest.Create(url);
            var httpWebResponse = httWebRequest.GetResponse();

            string response;

            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                response = streamReader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<FiveDayWeather>(response);
        }
    }
}
