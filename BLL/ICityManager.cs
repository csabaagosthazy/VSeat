using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    public interface ICityManager
    {
        List<City> GetCities();
        City GetCityById(int CityId);
    }
}
