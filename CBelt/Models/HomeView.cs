using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBelt.Models
{
    public class HomeView
    {
        public List<Event> events { get; set; }

        public User LoggedIn { get; set; }
    }
}
