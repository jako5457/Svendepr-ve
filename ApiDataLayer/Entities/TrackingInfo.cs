using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class TrackingInfo
    {
        public int TrackingInfoId { get; set; }

        public Guid TrackingCode { get; set; }

        public string Longitude { get; set; } = default!;

        public string Latitude { get; set; } = default!;

        public DateTime CreationDate { get; set; }
    }
}
