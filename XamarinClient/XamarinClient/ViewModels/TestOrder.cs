using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinClient.ViewModels
{
    public class TestOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeliveryAddress { get; set; }
        public int Status { get; set; }
        public string Driver { get; set; }
    }
}
