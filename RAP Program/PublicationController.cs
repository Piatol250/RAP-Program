using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_Program
{
    
    internal class PublicationController
    {
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";
        private static MySqlConnection conn = null;

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        public static List<Publication> loadPublications(int id)
        {
            List<Publication> publications = new List<Publication>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            MySqlCommand getPub = new MySqlCommand("select pub.doi, title, ranking, authors, year, type, cite_as, available from publication as pub, researcher_publication as respub " +
                                                    "where pub.doi=respub.doi and researcher_id=?id", conn);
            getPub.Parameters.AddWithValue("id", id);

            try
            {
                conn.Open();
                rdr = getPub.ExecuteReader();

                while (rdr.Read())
                {
                    publications.Add(new Publication
                    {
                        Doi = rdr.GetString(0),
                        Title = rdr.GetString(1),
                        Rank = ParseEnum<RANKING>(rdr.GetString(2)),
                        Authors = rdr.GetString(3).Replace(" ", string.Empty).Split(','),
                        PublicationDate = rdr.GetInt32(4),
                        Type = ParseEnum<PUBTYPE>(rdr.GetString(5)),
                        CiteAs = rdr.GetString(6),
                        AvailabilityDate = rdr.GetDateTime(7)
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return publications;

        }

    }
}
