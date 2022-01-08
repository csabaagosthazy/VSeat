using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface ICityDB
    {
        City GetCityById(int cityId);
    }
}
