using System;

namespace DTO
{
    public class order_DTO
    {
        public DateTime OrderDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime OrderHour { get; set; }
        public DateTime ReturnHour { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
    }
}
