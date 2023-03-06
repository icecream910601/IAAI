using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI.Models
{
    public class ViewLogin
    {
        

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }



        [Required(ErrorMessage = "{0}必填")]  //長度   密碼長度至少為 6
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)] //
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }


    }
}