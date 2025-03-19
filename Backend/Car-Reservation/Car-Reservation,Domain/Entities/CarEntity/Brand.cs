using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation_Domain.Entities.CarEntity
{
    public class Brand : BaseEntity
    {
        public string name { get; set; }
        public ICollection<Model> Models { get; set; }

        //BMW,
        //Mercedes,
        //Audi,
        //Toyota,
        //Honda,
        //Ford,
        //Chevrolet,
        //Hyundai,
        //Kia,
        //Nissan,
        //Volkswagen,
        //Peugeot,
        //Renault,
        //Skoda,
        //Fiat,
        //Opel,
        //Mazda,
        //Volvo,
        //Subaru,
        //Suzuki,
        //Mitsubishi,
        //Citroen,
        //Seat,
        //Dacia,
        //Jeep,
        //LandRover,
        //Jaguar,
        //Porsche,
        //Mini,


    }
}
