using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Dtos.DashBoard
{
    public class Statistic
    {
  
        public int TotalCars { get; set; }
        public int AvailableCars { get; set; }
        public int RentedCars { get; set; }
        public int TotalCustomers { get; set; }
        public decimal MonthlyRevenue { get; set; }

    }
}
