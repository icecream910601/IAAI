using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI.Models
{
    public class Master
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "照片")]
        public string Picture { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "現職")]
        public string PresentJob { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "最高學歷")]
        public string Education { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "字數超過500")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(500)]
        [Display(Name = "基本介紹")]
        public string Introduction { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "字數超過500")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(500)]
        [Display(Name = "經歷")]
        public string Experience { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [AllowHtml]
        [Display(Name = "詳細說明")]
        public string Description { get; set; }


        //public static string SaveUpImage(HttpPostedFileBase Picture)
        //{
        //    if (Picture == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Picture.FileName;
        //        var filePath = Path.Combine("~/Picture", fileName);
        //        var path = HttpContext.Current.Server.MapPath(filePath);

        //        Picture.SaveAs(path);

        //        return fileName;
        //    }
        //}



    }
}