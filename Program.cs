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
            Boolean exp =true;
            while (exp)
            {
                Console.WriteLine("Enter \n 1. Insert Data \n 2.Update Data \n 3.Delete Data \n 4.Display Data \n 5.Exit");
                Console.WriteLine("Enter Option");
                int option = int.Parse(Console.ReadLine());
                switch(option)
                {
                    case 1:
                        insertDataByStoredProcedure(conn);
                        break;
                    case 2:
                        updateStudentInfo(conn);
                        break;  
                    case 3:
                        deleteDataOfStudent(conn);
                        break;
                    case 4:
                        displaydatabyStoredProcedure(conn);
                        break;
                    case 5:
                       exp = false;
                       break;
                    default:
                        Console.WriteLine("invalid");
                        break;



                }

            }
            
          
        }

        private static void deleteDataOfStudent(SqlConnection conn)
        {
            Console.WriteLine("-------********* Data of   Deletation *********----");

            conn.Open();
            try
            {
                Console.WriteLine("Enter student id");
                int id = int.Parse(Console.ReadLine());
                string procedure = "DeleteStudentInfo";
                SqlCommand delete = new SqlCommand(procedure, conn);
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.AddWithValue("@id", id);
                int rowsAffected = delete.ExecuteNonQuery();
               
                if(rowsAffected > 0) 
                {
                    Console.WriteLine("Data deleted succesfully");
                    Console.WriteLine(rowsAffected.ToString() + "rows affected");
                }
                else
                {
                    Console.WriteLine("Data not Found!");

                }
                
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
            Console.WriteLine("-------********* DISPLAY TABLE DATA  *********----");
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
            Console.WriteLine("-------********* Data insertion  *********----");


            conn.Open();
            try
            {
                Console.WriteLine("Enter Name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Age");
                int age = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Mobile Number");
                long mobile = long.Parse(Console.ReadLine());

                SqlCommand insertCommand = new SqlCommand("spStudentdata", conn);
                insertCommand.CommandType = CommandType.StoredProcedure;

                insertCommand.Parameters.AddWithValue("@name", name);
                insertCommand.Parameters.AddWithValue("@age", age);
                insertCommand.Parameters.AddWithValue("@mobileNumber", mobile);
                int rowsAffected = insertCommand.ExecuteNonQuery();
                Console.WriteLine("Data Inserted Successfully");
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

            Console.WriteLine("-------********* Data updation  *********----");

            conn.Open();
            try { 
            Console.WriteLine("Enter Student Id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Student Mobile Number");
            long mobile = long.Parse(Console.ReadLine());
            SqlCommand updateCommand = new SqlCommand("UpdatestudentInfo", conn);
            updateCommand.CommandType = CommandType.StoredProcedure;
           

            SqlParameter param;

            param = updateCommand.Parameters.Add("@id", SqlDbType.Int);

            param.Value = id;



            param = updateCommand.Parameters.Add("@student_mobilenumber", SqlDbType.BigInt);

            param.Value = mobile;
            int rowsAffected =updateCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data Updated succesfully");
                    Console.WriteLine(rowsAffected.ToString() + " row(s) affected");
                }
                else
                {
                    Console.WriteLine("Data not Found!");

                }

                
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
