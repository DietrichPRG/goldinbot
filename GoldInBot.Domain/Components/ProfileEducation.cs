using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldInBot.Domain.Components
{
    public class ProfileEducation
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Period { get; set; }
        public IList<string> Skills { get; set; }
    }
}
