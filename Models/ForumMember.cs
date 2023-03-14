using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace IAAI.Models
{
    public class ForumMember 
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Compare(nameof(Password), ErrorMessage = "密碼和確認密碼不相符")]
        [Required(ErrorMessage = "{0}必填")] //長度最多到100   密碼長度至少為 6
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)] //
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]


        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "密碼和確認密碼不相符")]
        [Required(ErrorMessage = "{0}必填")] //長度最多到100   密碼長度至少為 6
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)] //
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]


        public string ConfirmedPassword { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "性別")]
        public Gender Gender { get; set; }



        [Display(Name = "生日")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyyy-MM-dd}")] // 資料庫雖然看不到時間 //但其實有期間  //不要顯示時分秒 //0:d 0000/00/00 0000-00-00
        [DataType(DataType.Date)]

        public DateTime? Birthday { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "會員申請類別")]
        public MembershipType MembershipType { get; set; }

        [RegularExpression(@"^\(?\d{2,4}\)?[\s\-]?\d{3,4}\-?\d{4}$", ErrorMessage = "請輸入正確的電話號碼格式")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "電話")]
        public string Telephone { get; set; }

        [RegularExpression(@"^\(?\d{2,4}\)?[\s\-]?\d{3,4}\-?\d{4}$", ErrorMessage = "請輸入正確的電話號碼格式")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "手機")]
        public string Mobile { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0}必填")]     //
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]    //  
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]   //
        [Display(Name = "E-Mail")]
        public string Email { get; set; }



        [Display(Name = "是否為當年度有效會員")]
        public bool? IsCurrentMember { get; set; }


        [MaxLength(100)]
        [Display(Name = "會員影本")]
        public string Copy { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "現職單位")]
        public string JobUnit { get; set; }



        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "職稱")]
        public string JobTitle { get; set; }



        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "最高學歷")]
        public string Education { get; set; }


        [JsonIgnore]
        public virtual ICollection<ForumMemberExp> ForumMemberExps { get; set; } //virtual 虛擬的 //一個類別裡面有很多個消息
        [JsonIgnore]
        public virtual ICollection<Forum> Forums { get; set; } //virtual 虛擬的 //一個類別裡面有很多個消息
        [JsonIgnore]
        public virtual ICollection<ForumReply> ForumReplys { get; set; } //virtual 虛擬的 //一個類別裡面有很多個消息


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

        public void HashPassword()
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Password));
                Password = Convert.ToBase64String(hash);
            }
        }

        public void HashConfirmedPassword()
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(ConfirmedPassword));
                ConfirmedPassword = Convert.ToBase64String(hash);
            }
        }




    }
    public enum Gender
    {
        [Display(Name = "男")]
        Male,

        [Display(Name = "女")]
        Female
    }

    public enum MembershipType
    {
        [Display(Name = "正式會員")]
        FullMember,

        [Display(Name = "準會員")]
        AssociateMember,

        [Display(Name = "個人贊助會員")]
        SponsorMember,

        [Display(Name = "學生會員")]
        StudentMember
    }
}