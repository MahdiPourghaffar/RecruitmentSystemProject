using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.AnnouncementCRUD.Dtos
{
    public class AnnouncementRequestDto
    {
        [DisplayName("عنوان موقعیت شغلی")]
        public string JobName { get; set; }

        [DisplayName("نوع همکاری")]
        public Announcement.CooperationTypeEnum CooperationType { get; set; }

        public int? CategoryId { get; set; }

        [DisplayName("شهر محل استخدام")]
        public Announcement.CitiesEnum City { get; set; }

        [DisplayName("میزان حقوق")]
        public long Salary { get; set; }

        [DisplayName("جنسیت")]
        public Announcement.GenderEnum Gender { get; set; }

        [DisplayName("وضعیت سربازی")]
        public Announcement.MilitarySituationEnum MilitarySituation { get; set; }

        [DisplayName("حداقل سابقه")]
        public int MinExperience { get; set; }

        [DisplayName("حداقل مدرک تحصیلی")]
        public string MinDegree { get; set; }

        [DisplayName("شرح موقعیت شغلی")]
        public string JobDescription { get; set; }

    }
}
