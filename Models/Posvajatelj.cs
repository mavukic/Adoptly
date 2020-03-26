using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Adoptly.Models
{
    public class Posvajatelj
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String Ime { get; set; }

        public String Prezime { get; set; }

        public String Adresa { get; set; }

        public String OIB { get; set; }

        public String BrMob { get; set; }

        public int LjubimacId { get; set; }
        public Ljubimac Ljubimac { get; set; }
    }
}
