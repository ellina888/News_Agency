using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "فیلد {0} اجباری است.")]
        public string Username { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "فیلد {0} اجباری است.")]
        [StringLength(maximumLength: 20, ErrorMessage = "طول کلمه عبور بین 8 تا 20 کاراکتر است", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
