using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAAI.Models
{
    public class LandingPageViewModel
    {
        public List<LandingPageSlider> Sliders { get; set; }
        public List<LandingPageLink> Links { get; set; }

        public List<News> News { get; set; }

    }
}