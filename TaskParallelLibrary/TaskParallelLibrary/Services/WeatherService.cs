using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskParallelLibrary.DTO;

namespace TaskParallelLibrary.Services
{
    public static class WeatherService
    {
        private const string _urlCurrentWeather = "https://api.openweathermap.org/data/2.5/weather?{0}&appid=baeba02ddc02cd0fec249682efcb66e9&units=metric&lang=ru";

        /// <summary>
        /// Получает текущую погоду для города.
        /// </summary>
        /// <param name="city"> Наименование города.</param>
        /// <param name="token"> Токен отмены.</param>
        /// <returns></returns>
        public static CurrentWeather GetCurrentWeatherByCity(string city, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            var requestUrl = string.Format(_urlCurrentWeather, $"q={city}");
            return GetCurrentWeather(requestUrl, token);
        }

        /// <summary>
        /// Получает текущую погоду для города по Id города.
        /// </summary>
        /// <param name="id"> Id города.</param>
        /// <param name="token"> Токен отмены.</param>
        /// <returns></returns>
        public static CurrentWeather GetCurrentWeatherById(string id, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var requestUrl = string.Format(_urlCurrentWeather, $"id={id}");
            return GetCurrentWeather(requestUrl, token);
        }

        /// <summary>
        /// Получает данные от сервера по текущей погоде.
        /// </summary>
        /// <param name="url"> Адрес запроса.</param>
        /// <param name="token"> Токен отмены.</param>
        /// <returns></returns>
        private static CurrentWeather GetCurrentWeather(string url, CancellationToken token)
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
            return JsonConvert.DeserializeObject<CurrentWeather>(response);
        }
    }
}
