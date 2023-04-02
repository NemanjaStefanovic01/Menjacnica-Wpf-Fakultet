using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserModel
{
    private string ime;
    private string prezime;
    private string userName;
    private string password;
    private string funkcija;
    private string brTelefona;
    private string dateCreated;
    private string status;

    public UserModel()
    {
    }
    public UserModel(string ime, string prezime, string userName, string password, string funkcija, string brTelefona, string dateCreated, string status)
    {
        this.ime = ime;
        this.prezime = prezime;
        this.userName = userName;
        this.password = password;
        this.funkcija = funkcija;
        this.brTelefona = brTelefona;
        this.dateCreated = dateCreated;
        this.status = status;
    }

    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Funkcija { get; set; }    
    public string BrTelefona { get; set; }
    public string DateCreated { get; set; }
    public string Status { get; set; }
}

