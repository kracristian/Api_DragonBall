using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Models
{
    public class DragonBallResponse
    {
        public List<Character> Items { get; set; }
        public Meta Meta { get; set; }
        public Links Links { get; set; }
    }
}
