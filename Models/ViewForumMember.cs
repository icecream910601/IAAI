using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI.Models
{
    public class ViewForumMember
    {

        public string Account { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public MembershipType MembershipType { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool? IsCurrentMember { get; set; }
        public string Copy { get; set; }
        public string JobUnit { get; set; }
        public string JobTitle { get; set; }
        public string Education { get; set; }



        public string HistoryUnit { get; set; }
        public string HistoryJobTitle { get; set; }
        public int StartYear { get; set; }
        public int StartMonth { get; set; }
        public int EndYear { get; set; }
        public int EndMonth { get; set; }


    }
}