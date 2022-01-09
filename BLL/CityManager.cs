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
        public CityManager(ICityDB _CityDb)
        {
            CityDb = _CityDb;
        }
        public List<City> getCities()
        {
            return CityDb.GetCities();
        }
    }
}
