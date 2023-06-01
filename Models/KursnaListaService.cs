using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents.DocumentStructures;
using Google.Cloud.Firestore;

namespace MenjacnicaProjekat.Models
{
    public class KursnaListaService
    {
        public FirestoreDb GetConnection()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"menjacnica-db-firebase-adminsdk-yffc6-f8e453e734.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            FirestoreDb dataBase = FirestoreDb.Create("menjacnica-db");
            return dataBase;
        }

        public void AddValutaWithCustomId(FirestoreDb db, KursnaLista kursnaLista)
        {
            foreach (ValutaKursneListe valuta in kursnaLista)
            {
                DocumentReference doc = db.Collection("KursnaLista").Document(valuta.valuta);
                Dictionary<string, object> vlauta = new Dictionary<string, object>()
                    {
                        { "Valuta", valuta.valuta },
                        { "Oznaka" , valuta.oznaka},
                        { "KupovniKurs" , valuta.kupovniKurs},
                        { "SrednjiKurs" , valuta.srednjiKurs},
                        { "ProdajniKurs" , valuta.prodajniKurs}
                    };

                doc.SetAsync(vlauta);
            }
        }
    }
}
