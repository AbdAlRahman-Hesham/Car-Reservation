using System.Runtime.Serialization;

namespace Car_Reservation_Domain.Entities.CarEntity;

public enum Category
{
    [EnumMember(Value = "Economic")]
    Economic,
    [EnumMember(Value = "Medium")]
    Medium,
    [EnumMember(Value = "Luxury")]
    Luxury,
    [EnumMember(Value = "Compact")]
    Compact,
    [EnumMember(Value = "SUV")]
    SUV,
    [EnumMember(Value = "Minivan")]
    Minivan,
    [EnumMember(Value = "Sedan")]
    Sedan,
    [EnumMember(Value = "Hatchback")]
    Hatchback,
    [EnumMember(Value = "Sports")]
    Sports,
    [EnumMember(Value = "Convertible")]
    Convertible,
    [EnumMember(Value = "Pickup")]
    Pickup,
    [EnumMember(Value = "Electric")]
    Electric,
    [EnumMember(Value = "Hybrid")]
    Hybrid
}