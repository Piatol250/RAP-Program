using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_Program
{
    abstract class DBInterpreter
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

        public static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        /// <summary>
        /// Iterates over all researcher information in the database, generates a researcher object, and adds this object to a list that is returned.
        /// </summary>
        /// <returns>List of researcher objects</returns>
        public static List<Researcher> loadResearchers()
        {
            List<Researcher> researchers = new List<Researcher>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            MySqlCommand getInfo = new MySqlCommand("select id, type, given_name, family_name, title, unit, " +
                                                    "campus, email, photo, degree, supervisor_id, level, utas_start, " +
                                                    "current_start from researcher", conn);

            try
            {
                conn.Open();
                rdr = getInfo.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetString(1) == "Staff")
                    {
                        researchers.Add(new Staff
                        {
                            Id = rdr.GetInt32(0),
                            researcherType = TYPE.Employee,
                            Given_Name = rdr.GetString(2),
                            Family_Name = rdr.GetString(3),
                            Title = rdr.GetString(4),
                            Unit = rdr.GetString(5),
                            Campus = rdr.GetString(6),
                            Email = rdr.GetString(7),
                            Photo = rdr.GetString(8),
                            Level = ParseEnum<LEVEL>(rdr.GetString(11)),
                            Tenure = (float)(DateTime.Today - rdr.GetDateTime(12)).TotalDays/365
                        });
                    }
                    else if (rdr.GetString(1) == "Student")
                    {
                        researchers.Add(new Student {
                            Id = rdr.GetInt32(0),
                            researcherType = TYPE.Student,
                            Given_Name = rdr.GetString(2),
                            Family_Name = rdr.GetString(3),
                            Title = rdr.GetString(4),
                            Unit = rdr.GetString(5),
                            Campus = rdr.GetString(6),
                            Email = rdr.GetString(7),
                            Photo = rdr.GetString(8),
                            Degree = rdr.GetString(9),
                            SupervisorID = rdr.GetInt32(10),
                            Tenure = (float)(DateTime.Today - rdr.GetDateTime(12)).TotalDays / 365
                        });
                    }
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

            return researchers;
        }

        /// <summary>
        /// Iterates over all positions in the database, checks the researcher id of the position against the given researcher id and adds all 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Position> loadPositions(int id)
        {
            List<Position> positions = new List<Position>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            MySqlCommand getPub = new MySqlCommand("select id, level, start, end from position as pos where pos.id=?id", conn);
            getPub.Parameters.AddWithValue("id", id);

            try
            {
                conn.Open();
                rdr = getPub.ExecuteReader();

                while (rdr.Read())
                {
                    positions.Add(new Position {    
                                                Level= ParseEnum<EMPLOYMENTlEVEL>(rdr.GetString(1)),
                                                start = rdr.GetDateTime(2),
                                                                             
                    });

                    if(rdr.GetValue(3) == null)
                    {
                        positions.Last().end = rdr.GetDateTime(3);
                    }
                    else
                    {
                        positions.Last().end = DateTime.Now;
                    }
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

            return positions;

        }

        /* public static List<Researcher> randomizeData(int numResearchers, int numPubs)
         {

         }
        */
    }
}

   

