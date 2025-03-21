﻿using Car_Reservation_Domain.Entities;
using System.ComponentModel;

namespace Car_Reservation_Domain.Entities.CarEntity
{
    public class Model : BaseEntity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}