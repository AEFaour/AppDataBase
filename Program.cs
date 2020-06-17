using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using databases;
using MySql.Data.MySqlClient;
using EmployesTrt;

namespace AppliDataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            //Employe employe = new Employe("CAMUS", "Albert", "Ecrivain");
            //Employe employeAjoute = EmployeDB.addEmploye(employe);
            //Console.WriteLine("Employé ajouté : " + employe.Id);

            //long id = 11L;
            //Employe employe = EmployeDB.getEmployeById(id);
            //if (employe != null)
            //{
            //    Console.WriteLine("Employe : "
            //                        + employe.Id + " "
            //                        + employe.Nom + " "
            //                        + employe.Prenom + " "
            //                        + employe.PosteOccupe);
            //} else
            //{
            //    Console.WriteLine("Il n'existe aucun employé avec l'id " + id);
            //}

            //IList<Employe> listeEmployes = EmployeDB.getEmployes();

            //if (listeEmployes.Count != 0) {
            //    Console.WriteLine("Liste des employes :");
            //    Console.WriteLine("--------------------");
            //    foreach (var e in listeEmployes)
            //    {
            //        Console.WriteLine(e.Id + " " + e.Nom + " " + e.Prenom);
            //    }
            //} else
            //{
            //    Console.WriteLine("Il n'existe aucun employé dans la base.");
            //}

            //Employe employeToUpdate = new Employe("", "", "");
            //employeToUpdate.Id = 3L;
            //bool isEmployeUpdated = EmployeDB.updateEmploye(employeToUpdate);
            //if (isEmployeUpdated)
            //{
            //    Console.WriteLine("Employé mis à jour avec succès ");
            //} else
            //{
            //    Console.WriteLine("L'employé avec id " + employeToUpdate.Id + " n'existe pas !");
            //}

            try
            {
                Adresse adresse = new Adresse(43, "Rue Victor Hugo", "Paris", 75000);
                Employe employe = new Employe("TABARLY", "Eric", "Libérateur", adresse);
                Employe employeAjoute = EmployeDB.addEmploye(employe);

                Console.WriteLine("Employe ajouté : " + employe.Id + " "
                                        + employe.Prenom + " "
                                        + employe.Nom + " "
                                        + employe.PosteOccupe + " "
                                        + "Adresse ["
                                        + employe.Adresse.Numero + " "
                                        + employe.Adresse.Rue + " "
                                        + employe.Adresse.Ville + " "
                                        + employe.Adresse.CodePostal + " "
                                        + "]");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ajout de l'adresse de l'employé");
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
