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
            if (kursnaLista == null)
                return;

            foreach (ValutaKursneListe valuta in kursnaLista)
            {
                if (valuta.valuta == null)
                    continue;

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
        public async Task<KursnaLista> GetKursnaLista(FirestoreDb db)
        {
            Query kursnaListaRef = db.Collection("KursnaLista");
            QuerySnapshot snapshot = await kursnaListaRef.GetSnapshotAsync();

            KursnaLista kursnaLista = new KursnaLista();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    ValutaKursneListe valuta = new ValutaKursneListe();

                    Dictionary<string, object> documentData = document.ToDictionary();

                    valuta.valuta = documentData["Valuta"].ToString();
                    valuta.oznaka = documentData["Oznaka"].ToString();
                    valuta.kupovniKurs = float.Parse(documentData["KupovniKurs"].ToString());
                    valuta.srednjiKurs = float.Parse(documentData["SrednjiKurs"].ToString());
                    valuta.prodajniKurs = float.Parse(documentData["ProdajniKurs"].ToString());

                    kursnaLista.DodajValutu(valuta);
                }
                else
                {
                    MessageBox.Show("Document does not exist");
                }
            }

            return kursnaLista;
        }
    }
}
