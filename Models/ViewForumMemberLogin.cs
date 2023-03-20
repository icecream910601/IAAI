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
    public class ViewForumMemberLogin
    {
      

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


        public void HashPassword()
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Password));
                Password = Convert.ToBase64String(hash);
            }
        }

        //public string ConfirmedPassword { get; set; }
        //public string Name { get; set; }
        //public Gender Gender { get; set; }
        //public DateTime? Birthday { get; set; }
        //public MembershipType MembershipType { get; set; }
        //public string Telephone { get; set; }
        //public string Mobile { get; set; }
        //public string Address { get; set; }
        //public string Email { get; set; }
        //public bool? IsCurrentMember { get; set; }
        //public string Copy { get; set; }
        //public string JobUnit { get; set; }
        //public string JobTitle { get; set; }
        //public string Education { get; set; }



        //public string HistoryUnit { get; set; }
        //public string HistoryJobTitle { get; set; }
        //public int StartYear { get; set; }
        //public int StartMonth { get; set; }
        //public int EndYear { get; set; }
        //public int EndMonth { get; set; }


    }
}