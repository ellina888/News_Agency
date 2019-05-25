using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DAl
{
    public class PageGroup
    {
        [Key]
        public int GroupID { get; set; }
        [Display(Name ="گروه خبری")]
        [Required(ErrorMessage ="فیلد {0} اجباری است.")]
        [MaxLength(100)]
        //[Remote("PageGroups", "Edit", AdditionalFields = "Id",
        //        ErrorMessage = "Product name already exists")]
        public string GroupTitle { get; set; }

        public virtual ICollection<Page> Page { get; set; }

        public PageGroup()
        {

        }
    }
}
