using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskParallelLibrary.DTO;
using TaskParallelLibrary.Helpers;
using TaskParallelLibrary.Services;

namespace TaskParallelLibrary
{
    class Program
    {
        static void Main(string[] args) 
        {
            var cancelToken = new CancellationTokenSource();

            try
            {
                WriteFiveDayWindData("Kursk", cancelToken);
                WriteCoordinatesData("Kursk", cancelToken);
                WriteAverageTemperatureData("Kursk", cancelToken);

                var cityHelper = new CityHelper();

                var citiesParallels = cityHelper.GetRandomCountCities(5);
                WriteParallelAllData(citiesParallels, cancelToken);

                var citiesConsistently = cityHelper.GetRandomCountCities(5);
                WriteConsistentlyAllData(citiesConsistently, cancelToken);

                WriteCurrenTemperatureData("London", cancelToken);
            }
            catch (AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    Console.WriteLine(e is TaskCanceledException ? "Операция прервана" : e.Message);
                }
            }
            finally
            {
                cancelToken.Dispose();
            }

            Console.WriteLine($"Основной поток завершил работу");
            Console.ReadLine();
        }

        static void WriteCurrenTemperatureData(string cityName, CancellationTokenSource cancelToken)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return;
            }
            var task = new Task<CurrentWeather>(() => WeatherService.GetCurrentWeatherByCity(cityName, cancelToken.Token));
            task.Start();
            //task.Wait();
            WeatherWriter.WriteTemperature(task.Result);
        }

        static void WriteParallelAllData(List<City> cities, CancellationTokenSource cancelToken)
        {
            if (cities.Count < 1)
            {
                return;
            }
            var watch = Stopwatch.StartNew();
            Parallel.ForEach(cities, (city) => WriteAllData(city.Id, cancelToken));
            watch.Stop();
            Console.WriteLine($"Паралельное выполнение заняло {watch.ElapsedMilliseconds}с");
            Console.WriteLine("##############################################################");
        }

        static void WriteConsistentlyAllData(List<City> cities, CancellationTokenSource cancelToken)
        {
            if (cities.Count < 1)
            {
                return;
            }

            var watch = Stopwatch.StartNew();
            foreach (var item in cities)
            {
                WriteAllData(item.Id, cancelToken);
            }
            watch.Stop();
            Console.WriteLine($"Последовательное выполнение заняло {watch.ElapsedMilliseconds}с");
            Console.WriteLine("##############################################################");
        }

        static void WriteAllData(string cityId, CancellationTokenSource cancelToken)
        {
            if (string.IsNullOrWhiteSpace(cityId))
            {
                return;
            }

            var result = WeatherService.GetCurrentWeatherById(cityId, cancelToken.Token);

            string data = "";

            if (result.Rain == null)
            {
                data += $"\nВ городе {result.Name} не идет дождь\n";
            }
            else if (result.Rain.The1H > 0)
            {
                data += $"В городе {result.Name} идет дождь {result.Rain.The1H} ч\n";
            }

            data += $"Город {result.Name} находится по координатам {result.Coord.Lon} {result.Coord.Lat}\n";
            data += $"Текущая температура {result.Main.Temp} C\n";
            data += $"На улице {result.CurrentWeatherList[0].Description}\n";
            Console.WriteLine(data);
        }

        static void WriteCoordinatesData(string cityName, CancellationTokenSource cancelToken)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return;
            }

            var prepareConsoleTask = new Task<CurrentWeather>(() => WeatherService.GetCurrentWeatherByCity(cityName, cancelToken.Token));
            var continueWith = prepareConsoleTask.ContinueWith((task) =>
            {
                WeatherWriter.WriteCoordinates(task.Result);
                WeatherWriter.WriteTemperature(task.Result);
            }, cancelToken.Token);

            //cancelToken.Cancel();
            prepareConsoleTask.Start();
            continueWith.Wait();
        }

        static void WriteAverageTemperatureData(string cityName, CancellationTokenSource cancelToken)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return;
            }

            var result = Task.Run(() => FiveDayWeatherService.GetFiveDayWeatherForCity(cityName, cancelToken.Token));

            var continueWith = result.ContinueWith((task) =>
            {
                double average = task.Result.FiveDayWeatherList.Sum(item => item.Main.Temp);

                average = average / task.Result.FiveDayWeatherList.Length;
                Console.WriteLine($"В городе {task.Result.City.Name} средняя температура за прошлые 5 дней {average:#.00} C");
            });
            continueWith.Wait();
        }

        static void WriteFiveDayWindData(string cityName, CancellationTokenSource cancelToken)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return;
            }

            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Подготовка данных по ветру за 5 дней для {cityName}");

                var inner = Task.Factory.StartNew(() =>
                {
                    var result = FiveDayWeatherService.GetFiveDayWeatherForCity(cityName, cancelToken.Token);

                    foreach (var item in result.FiveDayWeatherList)
                    {
                        WeatherWriter.WriteWind(item);
                    }
                }, TaskCreationOptions.AttachedToParent);
            });
            task.Wait();
        }
    }
}
