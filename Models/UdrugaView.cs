using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adoptly.Models
{
    public class UdrugaView
    {
        public IEnumerable<Udruga> Udruge { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
