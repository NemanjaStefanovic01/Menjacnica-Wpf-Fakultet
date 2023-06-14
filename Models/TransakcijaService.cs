using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents.DocumentStructures;
using Google.Cloud.Firestore;

namespace MenjacnicaProjekat.Models
{
    public class TransakcijaService
    {
        public FirestoreDb GetConnection()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"menjacnica-db-firebase-adminsdk-yffc6-f8e453e734.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            FirestoreDb dataBase = FirestoreDb.Create("menjacnica-db");
            return dataBase;
        }

        public async Task AddTransakcija(FirestoreDb db, Transakcija transakcija)
        {
            CollectionReference transakcijeCollection = db.Collection("Transakcije");

            Dictionary<string, object> tr = new Dictionary<string, object>()
            {
                { "VremeTransakcije", transakcija.vremeTransakcije },
                { "ValutaOtkupa", transakcija.valutaOtkupa },
                { "ValutaProdaje", transakcija.valutaProdaje },
                { "IznosValuteOtkupa", transakcija.iznosValuteOtkupa },
                { "IznosValuteProdaje", transakcija.iznosValuteProdaje },
                { "KursTransakcije", transakcija.kursTransakcije },
            };

            await transakcijeCollection.AddAsync(tr);
        }
    }
}
