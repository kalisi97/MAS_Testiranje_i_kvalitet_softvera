using MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static MAS_TestiranjeSoftvera_Projekat.Extensions.Enums;

namespace MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Implementations
{
    public class RepositoryOsoba:IRepositoryOsoba
    {
        DatabaseConnection broker;
        SqlTransaction transakcija;
        public RepositoryOsoba()
        {
            broker = DatabaseConnection.Sesion();
        }

        public string Delete(Osoba entity)
        {
            transakcija = null;
            string poruka = string.Empty;

            try
            {

                DatabaseConnection.broker.konekcija.Open();
                transakcija = broker.konekcija.BeginTransaction();
                broker.komanda =
                    new SqlCommand("", broker.konekcija, transakcija);
                broker.komanda.CommandText =
                    $"delete from Osoba where OsobaId = {entity.OsobaId}";

                broker.komanda.ExecuteNonQuery();

                Save();

                return "Uspesno!";
            }
            catch (Exception ex)
            {
                transakcija.Rollback();
                poruka = ex.Message.ToString();
                return poruka;
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }
        }

        public string Insert(Osoba entity)
        {
            transakcija = null;
            string poruka = string.Empty;

            try
            {
                entity.OsobaId = broker.VratiSifru("OsobaId", "Osoba");
                DatabaseConnection.broker.konekcija.Open();

                broker.komanda =
                  new SqlCommand("SELECT dbo.validEmail(@email)", broker.konekcija);
                broker.komanda.Parameters.AddWithValue("@email", entity.Email);
                broker.komanda.CommandType = CommandType.Text;
                int validEmail = Convert.ToInt32(broker.komanda.ExecuteScalar());
                if (validEmail == 0)
                    throw new Exception("Nevalidan unos email-a!");
                broker.komanda =
                    new SqlCommand("PROC_INSERT_OSOBA", broker.konekcija);
                broker.komanda.CommandType = CommandType.StoredProcedure;
                
                broker.komanda.Parameters.AddWithValue("OsobaId", Convert.ToInt32(entity.OsobaId));
                broker.komanda.Parameters.AddWithValue("MaticniBroj", SqlDbType.VarChar).Value = entity.MaticniBroj;
                broker.komanda.Parameters.AddWithValue("Ime", SqlDbType.NVarChar).Value = entity.Ime;
                broker.komanda.Parameters.AddWithValue("Prezime", SqlDbType.NVarChar).Value = entity.Prezime;
                broker.komanda.Parameters.AddWithValue("Visina", entity.Visina == 0 ? DBNull.Value : entity.Visina);
                broker.komanda.Parameters.AddWithValue("Tezina", entity.Tezina == 0 ? DBNull.Value : entity.Tezina);
                broker.komanda.Parameters.AddWithValue("BojaOciju", entity.BojaOciju == 0 ? DBNull.Value : (int)entity.BojaOciju);
                broker.komanda.Parameters.AddWithValue("Telefon", String.IsNullOrEmpty(entity.Telefon) ? DBNull.Value : entity.Telefon);
                broker.komanda.Parameters.AddWithValue("Email", SqlDbType.VarChar).Value = entity.Email;
                broker.komanda.Parameters.AddWithValue("DatumRodjenja", SqlDbType.Date).Value = entity.DatumRodjenja;
                broker.komanda.Parameters.AddWithValue("Adresa", String.IsNullOrEmpty(entity.Adresa) ? DBNull.Value : entity.Adresa);
                broker.komanda.Parameters.AddWithValue("Napomena", String.IsNullOrEmpty(entity.Napomena) ? DBNull.Value : entity.Napomena);
                broker.komanda.Parameters.AddWithValue("MestoId", SqlDbType.Int).Value = entity.MestoId;

                int result = broker.komanda.ExecuteNonQuery();

                if (result != -1)
                {
                    poruka = "Uspesno!";
                }
                else
                {
                    throw new Exception("Doslo je do greske!");
                }
                return poruka;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }
        }

        public void Save()
        {
            transakcija.Commit();
        }

        public IEnumerable<Osoba> SelectAll()
        {
           
            try
            {
                List<Osoba> osobe = new List<Osoba>();

                broker.konekcija.Open();
                broker.komanda = new SqlCommand("", broker.konekcija, transakcija);
                broker.komanda.CommandText = $"Select * from Osoba o join Mesto m on (o.MestoId = m.MestoId)";

                SqlDataReader citac = broker.komanda.ExecuteReader();

                while (citac.Read())
                {
                    Osoba osoba = new Osoba
                    {
                        OsobaId = citac.GetInt32(0),
                        MaticniBroj = citac.GetString(1),
                        Ime = citac.GetString(2),
                        Prezime = citac.GetString(3),
                        Visina = GetIntValueOrDefault(citac, "Visina"),
                        Tezina = GetIntValueOrDefault(citac, "Tezina"),
                        BojaOciju = (BojaOciju)GetIntValueOrDefault(citac, "BojaOciju"),
                        Telefon = GetStringValueOrDefault(citac, "Telefon"),
                        Email = citac.GetString(8),
                        DatumRodjenja = citac.GetDateTime(9),
                        Adresa = GetStringValueOrDefault(citac, "Adresa"),
                        Napomena = GetStringValueOrDefault(citac, "Napomena"),
                        Mesto = new Mesto
                        {
                            MestoId = citac.GetInt32(12),
                            Naziv = citac.GetString(14),
                            PttBroj = citac.GetInt32(15),
                            BrojStanovnika = GetIntValueOrDefault(citac, "BrojStanovnika")
                        }

                    };
                    osobe.Add(osoba);
                }
                citac.Close();

                return osobe;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }
        }

        public Osoba SelectById(int? id)
        {
            Osoba o = null;
            try
            {
                broker.konekcija.Open();                  
                broker.komanda = new SqlCommand("", broker.konekcija, transakcija);
                broker.komanda.CommandText = $"Select * from Osoba o join Mesto m on (o.MestoId = m.MestoId) where OsobaId = {id}";
                SqlDataReader citac = broker.komanda.ExecuteReader();

                while (citac.Read())
                {
                    o = new Osoba
                    {
                        OsobaId = citac.GetInt32(0),
                        MaticniBroj = citac.GetString(1),
                        Ime = citac.GetString(2),
                        Prezime = citac.GetString(3),
                        Visina = GetIntValueOrDefault(citac, "Visina"),
                        Tezina = GetIntValueOrDefault(citac, "Tezina"),
                        BojaOciju = (BojaOciju)GetIntValueOrDefault(citac, "BojaOciju"),
                        Telefon = GetStringValueOrDefault(citac, "Telefon"),
                        Email = citac.GetString(8),
                        DatumRodjenja = citac.GetDateTime(9),
                        Adresa = GetStringValueOrDefault(citac, "Adresa"),
                        Napomena = GetStringValueOrDefault(citac, "Napomena"),
                        Mesto = new Mesto
                        {
                            MestoId = citac.GetInt32(12),
                            Naziv = citac.GetString(14),
                            PttBroj = citac.GetInt32(15),
                            BrojStanovnika = GetIntValueOrDefault(citac,"BrojStanovnika")
                        }
                    };
                   
                }
                citac.Close();
                o.MestoId = (int)o.Mesto.MestoId;
                return o;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }
        }

        public string Update(Osoba entity)
        {
            transakcija = null;
            string poruka = string.Empty;

            try
            {

                DatabaseConnection.broker.konekcija.Open();
                transakcija = broker.konekcija.BeginTransaction();

                broker.komanda =
                new SqlCommand("SELECT dbo.validEmail(@email)", broker.konekcija);
                broker.komanda.Parameters.AddWithValue("@email", entity.Email);
                broker.komanda.CommandType = CommandType.Text;
                int validEmail = Convert.ToInt32(broker.komanda.ExecuteScalar());
                if (validEmail == 0)
                    throw new Exception("Nevalidan unos email-a!");

                broker.komanda =
                    new SqlCommand("", broker.konekcija, transakcija);
                broker.komanda.CommandText =
                    $"update Osoba set MaticniBroj ='{entity.MaticniBroj}', Ime = '{entity.Ime}', Prezime = '{entity.Prezime}', DatumRodjenja = '{entity.DatumRodjenja}', Napomena = @Napomena, Adresa = @Adresa, Visina = @Visina, Tezina = @Tezina, BojaOciju = @BojaOciju, Telefon = @Telefon, Email = '{entity.Email}', MestoId = {entity.MestoId} where OsobaId = {entity.OsobaId}";
                broker.komanda.Parameters.AddWithValue("@Napomena", String.IsNullOrEmpty(entity.Napomena) ? DBNull.Value : entity.Napomena);
                broker.komanda.Parameters.AddWithValue("@Adresa", String.IsNullOrEmpty(entity.Adresa) ? DBNull.Value : entity.Adresa);
                broker.komanda.Parameters.AddWithValue("@Visina", entity.Visina == 0 ? DBNull.Value : entity.Visina);
                broker.komanda.Parameters.AddWithValue("@Tezina", entity.Tezina == 0 ? DBNull.Value : entity.Tezina);
                broker.komanda.Parameters.AddWithValue("@Telefon", String.IsNullOrEmpty(entity.Telefon) ? DBNull.Value : entity.Telefon);
                broker.komanda.Parameters.AddWithValue("@BojaOciju", entity.BojaOciju == 0 ? DBNull.Value : entity.BojaOciju);

                broker.komanda.ExecuteNonQuery();

                Save();

                poruka = "Uspesno!";
                return poruka;
            }
            catch (Exception ex)
            {
                transakcija.Rollback();
                poruka = ex.Message.ToString();
                return poruka;
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }
        }

        private string GetStringValueOrDefault(SqlDataReader citac, string nazivKolone)
        {
            string data = (citac.IsDBNull(citac.GetOrdinal(nazivKolone)))
                      ? "" : citac[nazivKolone].ToString();
            return data;
        }

        public int GetIntValueOrDefault(SqlDataReader citac, string nazivKolone)
        {
            int data = (citac.IsDBNull(citac.GetOrdinal(nazivKolone)))
                        ? 0 : int.Parse(citac[nazivKolone].ToString());
            return data;
        }
        public IEnumerable<Osoba> SelectAllAdults()
        {

            try
            {
                List<Osoba> osobe = new List<Osoba>();

                broker.konekcija.Open();
                broker.komanda = new SqlCommand("", broker.konekcija, transakcija);
                broker.komanda.CommandText = $"Select * from PunoletneOsobe";

                SqlDataReader citac = broker.komanda.ExecuteReader();

                while (citac.Read())
                {
                    Osoba osoba = new Osoba
                    {
                        OsobaId = citac.GetInt32(0),
                        MaticniBroj = citac.GetString(1),
                        Ime = citac.GetString(2),
                        Prezime = citac.GetString(3),
                        Visina = GetIntValueOrDefault(citac, "visina"),
                        Tezina = GetIntValueOrDefault(citac, "tezina"),
                        BojaOciju = (BojaOciju)GetIntValueOrDefault(citac, "boja_ociju"),
                        Telefon = GetStringValueOrDefault(citac, "kontakt"),
                        Email = citac.GetString(8),
                        DatumRodjenja = citac.GetDateTime(9),
                        Adresa = GetStringValueOrDefault(citac, "adresa"),
                        Napomena = GetStringValueOrDefault(citac, "napomena"),
                        Mesto = new Mesto
                        {
                            MestoId = citac.GetInt32(12),
                            Naziv = citac.GetString(13),
                          //  PttBroj = citac.GetInt32(15),
                            //BrojStanovnika = GetIntValueOrDefault(citac, "BrojStanovnika")
                        }

                    };
                    osobe.Add(osoba);
                }
                citac.Close();

                return osobe;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (broker.konekcija != null)
                    broker.konekcija.Close();
            }
        }
    }
}
