using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace IAAI.Models
{
    public class CertifiedMember
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "Picture")]
        public string Picture { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "Company")]
        public string Company { get; set; }


        //value="2023/3/3 上午 12:00:00"
        /*DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]*/

        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]  // 資料庫雖然看不到時間 //但其實有期間  //不要顯示時分秒 //0:d 0000/00/00 0000-00-00
        [DataType(DataType.Date)]

        public DateTime? InitDate { get; set; }


        // 其他方法

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
    }
}