using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_By_Storedprocedure
{
    internal class Program
    {
     
        static void Main(string[] args)
        {
            string connectionString = @"server = HP\SQLEXPRESS;
                            database= student; 
                            integrated security = sspi; 
                           Trusted_Connection = true";
            SqlConnection conn = new SqlConnection(connectionString);
            //insertDataByStoredProcedure(conn);
            displaydatabyStoredProcedure(conn);
           // updateStudentInfo(conn);
            deleteDataOfStudent(conn);


            displaydatabyStoredProcedure(conn); 
        }

        private static void deleteDataOfStudent(SqlConnection conn)
        {
            conn.Open();
            try
            {
                Console.WriteLine("enter student id");
                int id = int.Parse(Console.ReadLine());
                string procedure = "DeleteStudentInfo";
                SqlCommand delete = new SqlCommand(procedure, conn);
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.AddWithValue("@id", id);
                int rowsAffected = delete.ExecuteNonQuery();
                Console.WriteLine(rowsAffected.ToString() + "rows affected");
                Console.WriteLine("data deleted succesfully");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private static void displaydatabyStoredProcedure(SqlConnection conn)
        {
            conn.Open();
            string procedure = "spReadData";
            try
            {
                SqlCommand insertCommand = new SqlCommand(procedure, conn);
                insertCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader sdr = insertCommand.ExecuteReader();

                while (sdr.Read())
                {

                    Console.WriteLine(sdr["Id"] + ",  " + sdr["firstName"] + ",  " + sdr["age"] + ",  " + sdr["MobileNumber"]);
                }
                sdr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();


                Console.ReadLine();
            }
        }

        public static void insertDataByStoredProcedure(SqlConnection conn)
        {
            
            conn.Open();
            try
            {
                Console.WriteLine("enter name");
                string name = Console.ReadLine();
                Console.WriteLine("enter age");
                int age = int.Parse(Console.ReadLine());
                Console.WriteLine("enter mobile number");
                long mobile = long.Parse(Console.ReadLine());

                SqlCommand insertCommand = new SqlCommand("spStudentdata", conn);
                insertCommand.CommandType = CommandType.StoredProcedure;

                insertCommand.Parameters.AddWithValue("@name", name);
                insertCommand.Parameters.AddWithValue("@age", age);
                insertCommand.Parameters.AddWithValue("@mobileNumber", mobile);
                int rowsAffected = insertCommand.ExecuteNonQuery();
                Console.WriteLine("data inserted successfully");
                Console.WriteLine(rowsAffected.ToString() + " row(s) affected");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        public static void updateStudentInfo(SqlConnection conn)
        {
            conn.Open();
            try { 
            Console.WriteLine("enter student id");
           int id = int.Parse(Console.ReadLine());
            Console.WriteLine("enter student mobileNumber");
           long mobile = long.Parse(Console.ReadLine());
            SqlCommand updateCommand = new SqlCommand("UpdatestudentInfo", conn);
            updateCommand.CommandType = CommandType.StoredProcedure;
           

            SqlParameter param;

            param = updateCommand.Parameters.Add("@id", SqlDbType.Int);

            param.Value = id;



            param = updateCommand.Parameters.Add("@student_mobilenumber", SqlDbType.BigInt);

            param.Value = mobile;
            int rowsAffected =updateCommand.ExecuteNonQuery();
           
            Console.WriteLine(rowsAffected.ToString() + " row(s) affected");
            Console.WriteLine("data updated successfully");
            }
            catch(SqlException ex) 
            {
                Console.WriteLine(ex.Message);
            }finally
            { 
                conn.Close(); 
            }  
                  
        }
    }
}
