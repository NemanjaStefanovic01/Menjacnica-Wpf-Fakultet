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
        public void AddUserWithCustomId(FirestoreDb db, UserModel newUser)
        {
            DocumentReference DOC = db.Collection("Users").Document(newUser.UserName);
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
            DOC.SetAsync(user);
        }

        public async Task<List<UserModel>> GetUsersList(FirestoreDb db)
        {
            Query Qref = db.Collection("Users");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            List<UserModel> usersList = new List<UserModel>();

            foreach (DocumentSnapshot docsnap in snap)
            {
                UserFD userFd = docsnap.ConvertTo<UserFD>();

                if(docsnap.Exists)
                {
                    UserModel user = new UserModel();

                    user.Ime = userFd.Ime;
                    user.Prezime = userFd.Prezime;
                    user.UserName = userFd.UserName;
                    user.Password = userFd.Password;
                    user.Funkcija = userFd.Funkcija;
                    user.BrTelefona = userFd.BrTelefona;
                    user.DateCreated = userFd.DateCreated;
                    user.Status = userFd.Status;

                    usersList.Add(user);
                }
                else
                {
                    MessageBox.Show("Didn't find any users");
                }
            }

            Debug.WriteLine("Borj u listi koja se vraca: " + usersList.Count);
            return usersList;
        }
    }
}
