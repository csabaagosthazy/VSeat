using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    public interface ICourierManager
    {
        Courier GetCourier(string email, string password);
        Courier GetFreeCourierInCity(DateTime deliveryDateTime, int cityId);
        Courier CreateCourier(Courier courier);
    }
}
