using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskParallelLibrary.DTO;

namespace TaskParallelLibrary.Helpers
{
    public class CityHelper
    {
        private List<City> _cities = new List<City>();
        public CityHelper()
        {
            var file = File.ReadAllText(@"JSON\\city.list.json");
            _cities = JsonConvert.DeserializeObject<List<City>>(file);
        }

        /// <summary>
        /// Получает рандомные города в определенном количестве
        /// </summary>
        /// <param name="number"> Необходимое количество городов</param>
        /// <returns></returns>
        public List<City> GetRandomCountCities(int number)
        {
            var randomList = new List<City>();
            for (int i = 0; i < number; i++)
            {
                var random = new Random().Next(_cities.Count);
                randomList.Add(_cities[random]);
            }
            return randomList;
        }

        /// <summary>
        /// Получает все города по стране
        /// </summary>
        /// <param name="country"> Код страны.</param>
        /// <returns></returns>
        public List<City> GetCitiesByCountry(string country)
        {
            var ruCities = new List<City>();
            Parallel.ForEach(_cities, city =>
            {
                if (city.Country == country)
                {
                    ruCities.Add(city);
                }
            });
            return ruCities;
        }

        /// <summary>
        /// Получает dct города
        /// </summary>
        /// <returns></returns>
        public List<City> GetAllCities()
        {
            return _cities;
        }
    }
}
