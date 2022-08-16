using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Application.AnnouncementCRUD.Dtos
{
    public class AnnouncementResultDto
    {
        public int Id { get; set; }
        public string JobName { get; set; }

        public Announcement.CooperationTypeEnum CooperationType { get; set; }

        public int? CategoryId { get; set; }

        public Announcement.CitiesEnum City { get; set; }

        public long Salary { get; set; }

        public Announcement.GenderEnum Gender { get; set; }

        public Announcement.MilitarySituationEnum MilitarySituation { get; set; }

        public int MinExperience { get; set; }

        public string MinDegree { get; set; }

        public string JobDescription { get; set; }

        public string UserName { get; set; }

        public bool Confirmed { get; set; }
    }
}
