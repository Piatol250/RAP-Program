using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Controller
{ 
    internal class PublicationController
    {
        public static List<Entity.Publication> loadPublications(int id)
        {
            List<Entity.Publication> publications = new List<Entity.Publication>();
            MySqlConnection conn = Data.DBInterpreter.GetConnection();
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
                    publications.Add(new Entity.Publication
                    {
                        Doi = rdr.GetString(0),
                        Title = rdr.GetString(1),
                        Rank = Data.DBInterpreter.ParseEnum<Entity.RANKING>(rdr.GetString(2)),
                        Authors = rdr.GetString(3),
                        PublicationYear = rdr.GetInt32(4),
                        Type = Data.DBInterpreter.ParseEnum<Entity.PUBTYPE>(rdr.GetString(5)),
                        CiteAs = rdr.GetString(6),
                        AvailabilityDate = rdr.GetDateTime(7)
                    }) ;
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

        public List<Entity.Publication> sortPublications(Entity.Researcher researcher)
        {
            List<Entity.Publication> sorted = new List<Entity.Publication>();

            sorted = researcher.Publications.OrderBy(x => x.PublicationYear)
                                    .ThenBy(x => x.Title)
                                    .ToList();

            return sorted;
        }

        public List<Entity.Publication> GetPublications(Entity.Researcher Current)
        {
            return Current.Publications;
        }

    }
}
