using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI.Models
{
    public class Knowledge
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] //這樣才能顯示在月曆上
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]  // 資料庫雖然看不到時間 //但其實有期間  //不要顯示時分秒 //0:d 0000/00/00 0000-00-00
        [DataType(DataType.Date)]
        public DateTime? InitDate { get; set; }

        [MaxLength(200)]
        [Display(Name = "圖片")]
        public string Picture { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "主題")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "內容")]
        public string Content { get; set; }



    }
}