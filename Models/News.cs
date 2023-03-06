using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace IAAI.Models
{
    public class News
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "圖片")]
        public string Picture { get; set; }


        [Display(Name = "發布時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]//有加這個就是月曆格式//沒有的話就是input
        public DateTime? StartDate { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "主題")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(1000)]
        [Display(Name = "內容")]
        public string Content { get; set; }


        public static string SaveUpImage(HttpPostedFileBase Picture)
        {
            if (Picture == null)
            {
                return null;
            }
            else
            {
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Picture.FileName;
                var filePath = Path.Combine("~/Picture", fileName);
                var path = HttpContext.Current.Server.MapPath(filePath);

                Picture.SaveAs(path);

                return fileName;
            }

        }


        [Display(Name = "類別")]
        public int? ClassId { get; set; }


        [ForeignKey("ClassId")]  //綁關聯   //透過ClassId 查出MyCatalog
        public virtual NewsCatalog MyCatalog { get; set; }//希望可以直接操縱所屬類別  //虛擬的  //我必須知道我的所屬類別是誰




    }
}