using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldInBot.Domain.Interfaces
{
    public interface IGoldInPage
    {
        public Task EnsurePageLoaded();

        public bool CurrentPage { get; }
    }
}
