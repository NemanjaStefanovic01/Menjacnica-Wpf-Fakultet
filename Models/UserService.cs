using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Google.Cloud.Firestore;

namespace MenjacnicaProjekat.Models
{
    public class UserService
    {
        public UserService() { }

        public FirestoreDb GetConnection()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"menjacnica-db-firebase-adminsdk-yffc6-f8e453e734.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            FirestoreDb dataBase = FirestoreDb.Create("menjacnica-db");
            return dataBase;
        }
        public void AddUserWithAutoId(FirestoreDb db, UserModel newUser)
        {
            CollectionReference colRef = db.Collection("Users");
            Dictionary<string, object> user = new Dictionary<string, object>()
            {
                { "Ime", newUser.Ime },
                { "Prezime" , newUser.Prezime },
                { "UserName" , newUser.UserName },
                { "Password" , newUser.Password },
                { "Funkcija" , newUser.Funkcija },
                { "BtTelefona" , newUser.BrTelefona },
                { "DateCreated" , newUser.DateCreated },
                { "Status" , newUser.Status }
            };
            colRef.AddAsync(user);
        }
        
    }
}
