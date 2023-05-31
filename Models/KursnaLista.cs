using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenjacnicaProjekat.Models;

namespace MenjacnicaProjekat.Models
{
    public class KursnaLista : IEnumerable<ValutaKursneListe>
    {
        public List<ValutaKursneListe> kursnaLista;

        public KursnaLista() 
        { 
            kursnaLista = new List<ValutaKursneListe> ();
        }

        public void DodajValutu(ValutaKursneListe valuta)
        {
            kursnaLista.Add(valuta);
        }

        public ValutaKursneListe GetValutaAtIndex(int index)
        {
            return kursnaLista[index];
        }

        //Making list enumerable
        public IEnumerator<ValutaKursneListe> GetEnumerator()
        {
            return kursnaLista.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
