using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI.Models
{
    public class LandingPageSlider
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }



        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "SliderPicture")]
        public string SliderPicture { get; set; }



    }
}