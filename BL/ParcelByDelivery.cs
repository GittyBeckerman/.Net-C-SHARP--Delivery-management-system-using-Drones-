using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelByDelivery
    {
        public int ID { get; set; }
        public bool DeliveryStatus { get; set; }
        public double Latitude { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Property { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public Location PickUpLocation { get; set; }
        public Location TargetLocation { get; set; }
        public double Distance { get; set; }
        public override string ToString()
        {
            return "Parcel: id:" + ID + " sender: " + Sender + " target: " + Target + "DeliveryStatus(T/F): " + DeliveryStatus + "\n " ;
        }
    }
}
