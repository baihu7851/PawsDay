using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class SaveCookieCartDTO
    {
        public string Id { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public string ShapeTypes { get; set; }
    }
}
