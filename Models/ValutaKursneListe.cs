using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenjacnicaProjekat.Models
{
    public class ValutaKursneListe
    {
        public string valuta { get; set; }
        public string oznaka { get; set; }
        public float kupovniKurs { get; set; }
        public float srednjiKurs { get; set; }
        public float prodajniKurs { get; set; }

        public ValutaKursneListe() { }
        public ValutaKursneListe(string valuta, string oznaka, float kupovniKurs, float srednjiKurs, float prodajniKurs)
        {
            this.valuta = valuta;
            this.oznaka = oznaka;
            this.kupovniKurs = kupovniKurs;
            this.srednjiKurs = srednjiKurs;
            this.prodajniKurs = prodajniKurs;
        }
    }
}
