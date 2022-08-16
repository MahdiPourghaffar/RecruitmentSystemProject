using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Announcement
    {
        public int Id { get; set; }
        [DisplayName("عنوان موقعیت شغلی")]
        public string JobName { get; set; }

        [DisplayName("نوع همکاری")]
        public CooperationTypeEnum CooperationType { get; set; }

        [DisplayName("دسته بندی")]
        public Category Category { get; set; }

        public int? CategoryId { get; set; }

        [DisplayName("شهر محل استخدام")]
        public CitiesEnum City { get; set; }

        [DisplayName("میزان حقوق")]
        public long Salary { get; set; }

        [DisplayName("جنسیت")]
        public GenderEnum Gender { get; set; }

        [DisplayName("وضعیت سربازی")]
        public MilitarySituationEnum MilitarySituation { get; set; }

        [DisplayName("حداقل سابقه")]
        public int MinExperience { get; set; }

        [DisplayName("حداقل مدرک تحصیلی")]
        public string MinDegree { get; set; }

        [DisplayName("شرح موقعیت شغلی")]
        public string JobDescription { get; set; }

        public User User { get; set; }
        public int? UserId { get; set; }

        [DefaultValue(false)]
        public bool Confirmed { get; set; }

        public string UserName { get; set; }
        public enum CooperationTypeEnum
        {
            FullTime,
            PartTime,
            Remote
        }

        public enum CitiesEnum
        {
            Tehran,
            Tabriz,
            Esfehan,
            Shiraz,
            Mashhad
        }

        public enum GenderEnum
        {
            Male,
            Female
        }
        public enum MilitarySituationEnum
        {
            Payankhedmat,
            Moaf,
            Mashmul
        }
    }
}
