using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenjacnicaProjekat.Models
{
    public class Transakcija
    {
        public DateTime vremeTransakcije { get; set; }
        public string valutaOtkupa { get; set; }
        public string valutaProdaje { get; set; }
        public float iznosValuteOtkupa { get; set; }
        public float iznosValuteProdaje { get; set; }
        public float kursTransakcije { get; set; }

        public Transakcija()
        {
            vremeTransakcije = DateTime.Now;
        }
    }
}
