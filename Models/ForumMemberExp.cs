using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using IAAI.Filter;
using Newtonsoft.Json;

namespace IAAI.Models
{
    public class ForumMemberExp
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "服務單位")]
        public string HistoryUnit { get; set; }


        [MaxLength(100)]
        [Display(Name = "職稱")]
        public string HistoryJobTitle { get; set; }

        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "請輸入有效的年份")]
        [Display(Name = "起年")]
        public int StartYear { get; set; }

        [Range(1, 12, ErrorMessage = "月份必須介於 1 到 12 之間。")]
        [Display(Name = "起月")]
        public int StartMonth { get; set; }

        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "請輸入有效的年份")]
        [Display(Name = "迄年")]
        public int EndYear { get; set; }

        [Range(1, 12, ErrorMessage = "月份必須介於 1 到 12 之間。")]
        [Display(Name = "迄月")]
        public int EndMonth { get; set; }



        [Display(Name = "討論區會員")]
        public int? ForumMemberId { get; set; }

        [JsonIgnore]
        [ForeignKey("ForumMemberId")]  //綁關聯   //透過ClassId 查出MyCatalog
        public virtual ForumMember ForumMember { get; set; }//希望可以直接操縱所屬類別  //虛擬的  //我必須知道我的所屬類別是誰


        //我們建立了一個自訂的驗證方法 ValidateDateRange，這個方法會在驗證時自動呼叫。在這個方法中，我們先假設驗證結果是成功的(ValidationResult.Success)，然後判斷「迄年迄月」是否大於或等於「起年起月」，如果不是，就建立一個 ValidationResult 物件，把錯誤訊息和錯誤欄位名稱傳入。最後返回 ValidationResult 物件即可。

        //您可以在需要驗證的地方呼叫這個方法，例如在 controller 的 Create 和 Edit 方法中呼叫 ModelState.IsValid，這樣就會自動呼叫 ValidateDateRange 驗證方法，並把錯誤訊息加入 ModelState 中，方便在 View 中顯示錯誤訊息。

        // 自訂的驗證方法
        public ValidationResult ValidateDateRange(ValidationContext validationContext)
        {
            var result = ValidationResult.Success;
            if (EndYear < StartYear || (EndYear == StartYear && EndMonth < StartMonth))
            {
                result = new ValidationResult("迄年迄月必須大於等於起年起月。", new[] { "EndYear", "EndMonth" });
            }
            return result;
        }
    }
}