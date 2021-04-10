using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAS_TestiranjeSoftvera_Projekat.DataAccessLayer
{
    public class DatabaseConnection
    {
        public SqlCommand komanda { get; set; }
        public SqlConnection konekcija { get; set; }
        public SqlTransaction transakcija { get; set; }

        void konektujSe()
        {
            konekcija = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Baza;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        DatabaseConnection()
        {
            konektujSe();
        }

        public static DatabaseConnection broker;
        public static DatabaseConnection Sesion()
        {
            if (broker == null) broker = new DatabaseConnection();
            return broker;
        }

        public int VratiSifru(string nazivKolone, string nazivTabele)
        {
            try
            {
                konekcija.Open();
                komanda = new SqlCommand("", konekcija, transakcija);
                komanda.CommandText = $"Select max({nazivKolone}) from {nazivTabele}";
                int sifra = Convert.ToInt32(komanda.ExecuteScalar());
                return sifra + 1;
            }
            catch (Exception)
            {
                return 1;
            }
            finally { if (konekcija != null) konekcija.Close(); }
        }
    }
}
