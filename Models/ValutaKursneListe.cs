using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenjacnicaProjekat.Models
{
    public class ValutaKursneListe
    {
        public string valuta;
        public string oznaka;
        public float kupovniKurs;
        public float srednjiKurs;
        public float prodajniKurs;

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
