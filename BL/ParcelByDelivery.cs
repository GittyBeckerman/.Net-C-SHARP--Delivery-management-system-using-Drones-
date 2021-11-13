﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelByDelivery
    {
        public int ID { get; set; }
        public bool DeliveryStatus { get; set; }
        public double latitude { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Property  { get; set; }
        public CustomerInParcel sender { get; set; }
        public CustomerInParcel target { get; set; }
        public Location PickUpLocation { get; set; }
        public Location TargetLocation { get; set; }
        public double Distance { get; set; }

    }
}
