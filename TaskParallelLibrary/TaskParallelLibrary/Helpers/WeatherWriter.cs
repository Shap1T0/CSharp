using System;
using System.Collections.Generic;
using System.Text;
using TaskParallelLibrary.DTO;

namespace TaskParallelLibrary.Helpers
{
    public static class WeatherWriter
    {
        public static void WriteCoordinates(CurrentWeather taskResult)
        {
            Console.WriteLine($"Город {taskResult.Name} находится по координатам {taskResult.Coord.Lon} {taskResult.Coord.Lat}");
        }

        public static void WriteCoordinates(FiveDayWeather taskResult)
        {
            Console.WriteLine($"Город {taskResult.City.Name} находится по координатам {taskResult.City.Coord.Lon} {taskResult.City.Coord.Lat}");
        }

        public static void WriteTemperature(CurrentWeather taskResult)
        {
            Console.WriteLine($"В городе {taskResult.Name} текущая температура {taskResult.Main.Temp} C");
        }

        public static void WriteWind(FiveDayWeatherList taskResult)
        {
            Console.WriteLine($"Время: {Convert.ToDateTime(taskResult.DtTxt.DateTime)} скорость ветра {taskResult.Wind.Speed} м/с");
        }

    }
}
