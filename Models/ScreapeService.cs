using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Diagnostics;

namespace MenjacnicaProjekat.Models
{
    public class ScreapeService
    {
        string url = "https://www.kursna-lista.com/kursna-lista-nbs";

        public ScreapeService() { }

        public void ScrapeFromNBS()
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
                Debug.WriteLine(node.InnerText.Trim());
                list.Add(node.InnerText.Trim());
            }

            //Organize values in model
        }
    }
}
