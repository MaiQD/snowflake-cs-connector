using Snowflake.Data.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowflakeConnector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Boolean testStatus = true;
            try
            {
                using (IDbConnection conn = new SnowflakeDbConnection())
                {
                    conn.ConnectionString = "account=<accountname>;user=.<xxxxxx>;password=<xxxxxx>;ROLE=ACCOUNTADMIN;db=<DBNAME>;schema=<schemaname>";
                    conn.Open();
                    Console.WriteLine("Connection successful!");
                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "USE WAREHOUSE XXXX_WAREHOUSE";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "select * from TABLE1";   // sql opertion fetching 
                        //data from an existing table
                        IDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0));
                        }
                        conn.Close();
                    }
                }
            }
            catch (DbException exc)
            {
                Console.WriteLine("Error Message: {0}", exc.Message);
                testStatus = false;
            }
            Console.WriteLine(testStatus);
            Console.ReadKey();
        }
    }
}
