using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace MenjacnicaProjekat.Models
{
    public class ScreapeService
    {
        string url = "https://www.kursna-lista.com/kursna-lista-nbs";

        public ScreapeService() { }

        public KursnaLista ScrapeFromNBS()
        {
            //Sending get request to kursna-lista.com
            HttpClient httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //Get all values of table
            var listElement = htmlDocument.DocumentNode.SelectNodes("//td");
            List<string> list = new List<string>();
            foreach ( var node in listElement)
            {
                //Debug.WriteLine(node.InnerText.Trim());
                list.Add(node.InnerText.Trim());
            }
            list.RemoveRange(list.Count-16, 16);

            //Organize list to models
            KursnaLista novaKursnaLista = new KursnaLista(); //Prazna kursna lista

            List<List<string>> listaValuta = SplitList(list, 7);//Lista chunkova od po 7 elemenata

            foreach (List<string> valuta in listaValuta)
            {
                ValutaKursneListe novaValuta = new ValutaKursneListe();

                novaValuta.valuta = valuta[0];
                novaValuta.oznaka = valuta[1];
                novaValuta.kupovniKurs = float.Parse(valuta[4]);
                novaValuta.srednjiKurs = float.Parse(valuta[5]);
                novaValuta.prodajniKurs = float.Parse(valuta[6]);

                novaKursnaLista.DodajValutu(novaValuta);
            }

            return novaKursnaLista;
        }

        //Funkcija koja sece listu na manje liste od po n clanova
        static List<List<T>> SplitList<T>(List<T> originalList, int sublistSize)
        {
            List<List<T>> smallerLists = new List<List<T>>();

            for (int i = 0; i < originalList.Count; i += sublistSize)
            {
                List<T> sublist = originalList
                    .Skip(i)
                    .Take(sublistSize)
                    .ToList();

                smallerLists.Add(sublist);
            }

            return smallerLists;
        }
    }
}
