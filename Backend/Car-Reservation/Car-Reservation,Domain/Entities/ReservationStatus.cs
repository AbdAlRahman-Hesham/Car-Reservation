using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation_Domain.Entities
{
    public enum ReservationStatus
    {
        [EnumMember(Value = "Confirmed")]
        Confirmed,
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Cancelled")]
        Cancelled

    }
}
