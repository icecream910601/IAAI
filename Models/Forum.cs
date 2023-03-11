using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace IAAI.Models
{
    public class Forum
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "標題")]
        public string Header { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "內容")]
        public string Main { get; set; }


        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]  // 資料庫雖然看不到時間 //但其實有期間  //不要顯示時分秒 //0:d 0000/00/00 0000-00-00
        [DataType(DataType.Date)]

        public DateTime? InitDate { get; set; }


        [JsonIgnore]
        [Display(Name = "姓名")]
        public int? ForumMemberId { get; set; }


        [ForeignKey("ForumMemberId")]  //綁關聯   //透過ClassId 查出MyCatalog
        public virtual ForumMember ForumMember { get; set; }//希望可以直接操縱所屬類別  //虛擬的  //我必須知道我的所屬類別是誰

        [JsonIgnore]
        public virtual ICollection<ForumReply> ForumReplys { get; set; } //virtual 虛擬的 //一個類別裡面有很多個消息



    }
}