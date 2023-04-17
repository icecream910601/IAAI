using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI.Models
{
    public class LandingPageContent
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        //[Required(ErrorMessage = "{0}必填")]
        //[Display(Name = "BarText")]
        //public string BarText { get; set; }

        //[Required(ErrorMessage = "{0}必填")]
        //[MaxLength(100)]
        //[Display(Name = "BarPic")]
        //public string BarPic { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "Picture1")]
        public string Picture1 { get; set; }



        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "Picture2")]
        public string Picture2 { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "Picture3")]
        public string Picture3 { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "Picture4")]
        public string Picture4 { get; set; }

    }
}