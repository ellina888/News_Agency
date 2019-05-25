using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAl
{
    public class Page
    {
        [Key]
        public int PageID { get; set; }
        public int GroupID { get; set; }
        [Display(Name = "عنوان خبر")]
        [Required(ErrorMessage = "فیلد {0} اجباری است.")]
        [MaxLength(100)]
        public string PageTitle { get; set; }
        [Display(Name = "تصویر خبر")]
        //[Required(ErrorMessage = "فیلد {0} اجباری است.")]
        [MaxLength(100)]
        public string ImagePath { get; set; }
        [Display(Name = "خلاصه خبر")]
        [Required(ErrorMessage = "فیلد {0} اجباری است.")]
        [MaxLength(200)]
        public string ShortDescription { get; set; }
        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "فیلد {0} اجباری است.")]
        [UIHint("tinymce_full"), AllowHtml]
        public string Text { get; set; }
        [Display(Name = "بازدیدکننده")]
        public int Visitor { get; set; }
        //[Display(Name = "نمایش در اسلایدر")]
        public bool ShowingInSlider { get; set; }
        [Display(Name = "تاریخ خبر")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss, yyyy/MM/dd}")]
        public DateTime CreateTime { get; set; }
        [MaxLength(100)]
        [Display(Name = "نویسنده خبر")]
        [Required(ErrorMessage = "فیلد {0} اجباری است.")]
        public string Author { get; set; }

        public virtual PageGroup PageGroup { get; set; }
        public virtual ICollection<PageComment> PageComment { get; set; }

        public Page()
        {

        }
    }
}
