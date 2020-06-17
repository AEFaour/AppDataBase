using System;
using MySql.Data.MySqlClient;

namespace databases {
	public class MySqlDataBase
	{
		private readonly string urlConnection = "SERVER=127.0.0.1; DATABASE=doranco; UID=root; PWD=root";

		public MySqlDataBase()
		{
		}

		public MySqlConnection getConnection()
		{
			MySqlConnection connection = null;
			try
			{
				connection = new MySqlConnection(urlConnection);
			} catch (Exception ex)
			{
				Console.WriteLine("Erreur lors de la récupération d'une connection à la BDD");
				Console.WriteLine(ex.Message);
			}
			return connection;
		}
	}
}
