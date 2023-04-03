using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace MenjacnicaProjekat.Models
{
    [FirestoreData]
    public class UserFD
    {
        [FirestoreProperty]
        public string Ime { get; set; }
        [FirestoreProperty]
        public string Prezime { get; set; }
        [FirestoreProperty]
        public string UserName { get; set; }
        [FirestoreProperty]
        public string Password { get; set; }
        [FirestoreProperty]
        public string Funkcija { get; set; }
        [FirestoreProperty]
        public string BrTelefona { get; set; }
        [FirestoreProperty]
        public string DateCreated { get; set; }
        [FirestoreProperty]
        public string Status { get; set; }
    }
}
