using System;
using System.Collections.Generic;

namespace MeuPedido
{
    public class Sale
    {
        public class Policy
        {
            public long Min { get; set; }
            public double Discount { get; set; }
        }
        public string Name { get; set; }
        public long Category_id { get; set; }
        public List<Policy> Policies { get; set; }
    }
}
