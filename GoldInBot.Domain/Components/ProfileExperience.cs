using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldInBot.Domain.Components
{
    public class ProfileExperience
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Period { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public IList<string> Skills { get; set; }
        public bool LinkedinHelpedGetJob { get; set; }
    }
}
