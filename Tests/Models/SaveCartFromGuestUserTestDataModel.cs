using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Models
{
    public class SaveCartFromGuestUserTestDataModel
    {
        public User LoginData { get; set; }
        public IEnumerable<string> Links { get; set; }
    }
}
