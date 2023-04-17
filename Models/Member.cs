using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace IAAI.Models
{
    public class Member
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }



        [Required(ErrorMessage = "{0}必填")]  //長度最多到100   密碼長度至少為 6
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)] //
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        public void HashPassword()
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Password));
                Password = Convert.ToBase64String(hash);
            }
        }


        [MaxLength(50)] 
        public string Salt { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]     //
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]    //  
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]   //
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "電話")]
        public string Telephone { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "地址")]
        public string Address { get; set; }


        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] //這樣才能顯示在月曆上
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]  // 資料庫雖然看不到時間 //但其實有期間  //不要顯示時分秒 //0:d 0000/00/00 0000-00-00
        [DataType(DataType.Date)]

        public DateTime? InitDate { get; set; }


        [MaxLength(500)]
        [Display(Name = "權限")]
        public string Permission { get; set; }

    }
}