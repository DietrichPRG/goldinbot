using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldInBot.Domain.Components
{
    public class ProfileAbout
    {
        public DateOnly Joined { get; set; }
        public string ContactInformation { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
