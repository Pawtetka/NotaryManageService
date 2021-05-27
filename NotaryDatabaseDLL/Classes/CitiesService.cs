using Microsoft.EntityFrameworkCore;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotaryDatabaseDLL.Classes
{
    public class CitiesService
    {
        public List<City> GetAllCities(NotaryOfficeContext context)
        {
            List<City> result = new List<City>();
            var cities = context.Cities;
            foreach (City city in cities)
            {
                result.Add(city);
            }
            return result;
        }

        public List<City> GetCity(string name, NotaryOfficeContext context)
        {
            var result = context.Cities.Where(c => c.CityName == name);
            List<City> cities = new List<City>();
            foreach (City city in result)
            {
                cities.Add(city);
            }
            return cities;
        }

        public void AddNewCity(string name, string type, NotaryOfficeContext context)
        {
            City city = new City { CityName = name, CityType = type };
            context.Cities.Add(city);
            context.SaveChanges();
        }
    }
}
