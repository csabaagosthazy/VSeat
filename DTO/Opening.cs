using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Opening
    {
        public int WeekDay { get; set; }
        public DateTime OpenHour { get; set; }
        public DateTime CloseHour { get; set; }
        public DateTime PauseStart { get; set; }
        public DateTime PauseEnd { get; set; }
        public int RestaurantId { get; set; }

    }
}
