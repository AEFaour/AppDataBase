using System;
using System.Data;
using databases;
using MySql.Data.MySqlClient;

public class Employe
{
	public long Id { get; set; }
	public string Nom { get; set; }
	public string Prenom { get; set; }
	public string PosteOccupe { get; set; }
	public Adresse Adresse;

	public Employe()
	{
	}

    public Employe(string nom, string prenom, string posteOccupe, Adresse adresse)
    {
        Nom = nom;
        Prenom = prenom;
        PosteOccupe = posteOccupe;
        Adresse = adresse;
    }
}
