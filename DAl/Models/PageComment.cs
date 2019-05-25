using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public class PageComment
    {
        [Key]
        public int CommentID { get; set; }
        [Display(Name = "عنوان خبر")]
        public int PageID { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد {0} اجباری است.")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "پست الکترونیکی")]
        [Required(ErrorMessage = "فیلد {0} اجباری است.")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "پست الکترونیک وارد شده معتبر نیست.")]
        public string Email { get; set; }
        [Display(Name = "وب سایت")]
        [MaxLength(100)]
        public string Website { get; set; }
        [Display(Name = "دیدگاه")]
        [Required(ErrorMessage = "فیلد {0} اجباری است.")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss, yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }

        public virtual Page Page { get; set; }

        public PageComment()
        {

        }
    }
}
