using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Adoptly.Models
{
    public class Ljubimac
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Ime { get; set; }
        [System.Web.Mvc.AllowHtml]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]

        public int? SklonisteId { get; set; }
        public Skloniste Skloniste { get; set; }

        public String Vrsta { get; set; }
        public String Opis { get; set; }
        public int Godine { get; set; }

        public int? PosvajteljId { get; set; }
        public Posvajatelj posvajatelj { get; set; }
    }
}
