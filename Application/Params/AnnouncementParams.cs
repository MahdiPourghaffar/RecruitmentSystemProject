using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using static Domain.Announcement;

namespace Application.Params
{
    public class AnnouncementParams
    {
        public long Salary { get; set; }
        public string City { get; set; }
        public string CooperationType { get; set; }
        public string Name { get; set; }

    }
}
