using System;
using MySql.Data.MySqlClient;
using databases;
using System.Data;
using System.Collections.Generic;

namespace EmployesTrt
{
    public static class EmployeDB
    {

        public static Employe addEmploye(Employe employe)
        {
            if (employe == null || employe.Adresse == null)
            {
                Console.WriteLine("l'objet employe et/ou l'adresse de l'employe ne doivent pas être nuls !");
                return null;
            }
            MySqlConnection connection = null;
            MySqlTransaction transaction = null;
            try
            {
                MySqlDataBase dataBase = new MySqlDataBase();
                Console.WriteLine("Récupération d'une connexion de la BDD en cours...");
                connection = dataBase.getConnection();
                Console.WriteLine("Ouverture de la connexion à la BDD en cours...");
                connection.Open();
                Console.WriteLine("Ouverture de la connexion à la BDD réussie.");

                transaction = connection.BeginTransaction();

                Console.WriteLine("Préparation de la requête d'ajout de l'employe");
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.Transaction = transaction;

                cmd.CommandText = "INSERT INTO employe(nom, prenom, poste_occupe) VALUES(@nom, @prenom, @posteOccupe)";

                cmd.Parameters.AddWithValue("@nom", employe.Nom);
                cmd.Parameters.AddWithValue("@prenom", employe.Prenom);
                cmd.Parameters.AddWithValue("@posteOccupe", employe.PosteOccupe);

                Console.WriteLine("Exécution de la requête");
                int nbLignesEmploye = cmd.ExecuteNonQuery();
                Console.WriteLine("Nombre de lignes ajoutées dans la table 'employe' : " + nbLignesEmploye);

                // récupérer l'id de l'employé qui vient d'être ajouté puis de l'injecter dans l'objet employé.
                employe.Id = cmd.LastInsertedId;

                Console.WriteLine("Préparation de la requête d'ajout de l'adresse de l'employe");
                cmd.CommandText = "INSERT INTO adresse(numero, rue, ville, code_postal, employe_id)" +
                                        "VALUES(@numero, @rue, @ville, @codePostal, @employe_id)";

                cmd.Parameters.AddWithValue("@numero", employe.Adresse.Numero);
                cmd.Parameters.AddWithValue("@rue", employe.Adresse.Rue);
                cmd.Parameters.AddWithValue("@ville", employe.Adresse.Ville);
                cmd.Parameters.AddWithValue("@codePostal", employe.Adresse.CodePostal);
                cmd.Parameters.AddWithValue("@employe_id", employe.Id);

                Console.WriteLine("Exécution de la requête");
                int nbLignesAdresse = cmd.ExecuteNonQuery();

                transaction.Commit();

                Console.WriteLine("Nombre de lignes ajoutées dans la table 'adresse' : " + nbLignesAdresse);

                // récupérer l'id de l'employé qui vient d'être ajouté puis de l'injecter dans l'objet employé.
                employe.Adresse.Id = cmd.LastInsertedId;

                connection.Close();

            }
            catch (Exception ex)
            {
                // RollBack sur l'employé ajouté
                try
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                }
                catch (MySqlException except)
                {
                    if (transaction.Connection != null)
                    {
                        Console.WriteLine("Une exception du type " + except.GetType() +
                        " a été rencontrée lors d'une tentative d'annulation de la transaction.");
                        Console.WriteLine(except.Message);
                    }
                }
                Console.WriteLine("Une erreur du type " + ex.GetType() +
                    " a été rencontrée lors de l'insertion de données dans la BDD !");
                Console.WriteLine(ex.Message);

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return employe;
        }

        public static Employe getEmployeById(long id)
        {
            Employe employe = null;
            MySqlConnection connection = null;

            try
            {
                MySqlDataBase dataBase = new MySqlDataBase();
                Console.WriteLine("Récupération d'une connexion de la BDD en cours...");
                connection = dataBase.getConnection();
                Console.WriteLine("Ouverture de la connexion à la BDD en cours...");
                connection.Open();
                Console.WriteLine("Ouverture de la connexion à la BDD réussie.");

                Console.WriteLine("Préparation de la requête");
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM employe WHERE id = @id";

                cmd.Parameters.AddWithValue("@id", id);

                Console.WriteLine("Exécution de la requête");
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    employe = new Employe();
                    employe.Id = reader.GetInt32(0);
                    employe.Nom = reader.GetString(1);
                    employe.Prenom = reader.GetString(2);
                    employe.PosteOccupe = reader.GetString(3);
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ajout d'un employé");
                Console.WriteLine(ex.Message);

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return employe;
        }

        public static IList<Employe> getEmployes()
        {
            IList<Employe> listeEmployes = new List<Employe>();
            MySqlConnection connection = null;

            try
            {
                MySqlDataBase dataBase = new MySqlDataBase();
                Console.WriteLine("Récupération d'une connexion de la BDD en cours...");
                connection = dataBase.getConnection();
                Console.WriteLine("Ouverture de la connexion à la BDD en cours...");
                connection.Open();
                Console.WriteLine("Ouverture de la connexion à la BDD réussie.");

                Console.WriteLine("Préparation de la requête");
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM employe";

                Console.WriteLine("Exécution de la requête");
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employe employe = new Employe();
                    employe.Id = reader.GetInt32(0);
                    employe.Nom = reader.GetString(1);
                    employe.Prenom = reader.GetString(2);
                    employe.PosteOccupe = reader.GetString(3);
                    listeEmployes.Add(employe);
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ajout d'un employé");
                Console.WriteLine(ex.Message);

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return listeEmployes;
        }

        public static bool updateEmploye(Employe employe)
        {
            Employe employeRecupere = getEmployeById(employe.Id);

            if (employeRecupere == null)
            {
                return false;
            }

            /*
             * 
             * mettre à jour l'employe 
             * UPDATE employe SET nom = @nom, prenom = @prenom, poste_occupe = @poste_occupe WHERE id = @id
             */

            return true;
        }
    }
}

