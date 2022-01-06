using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace BLL
{
    public class CityManager : ICityManager
    {
        private ICityDB CityDb { get; }
        public CityManager(ICityDB CityDb)
        {
            CityDb = CityDb;
        }
        public List<City> getCities()
        {
            return CityDb.GetCities();
        }
    }
}
