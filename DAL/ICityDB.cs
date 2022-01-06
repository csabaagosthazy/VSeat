using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DAL
{
    public interface ICityDB
    {
        public List<City> GetCities();
    }
}
