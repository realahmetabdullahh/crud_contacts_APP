using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        public static bool GetContactInfoByID(int ID,ref string FirstName,ref string LastName,ref string Email,ref string Phone,ref string Address,ref DateTime BirthDate,ref int CountryID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.Connectionstring);

            string query = "select *from Contacts where ContactID = @ContactID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("ContactID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                   BirthDate = (DateTime)reader["BirthDate"];
                        
                    CountryID = (int)reader["CountryID"];
                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
                
            }
            catch (Exception ex) { IsFound = false; //Console.WriteLine("Error " + ex.Message);
            }
            finally { connection.Close(); }
            return IsFound;
        }

        public static int AddNewContact(string FirstName, string LastName, string Email, string Phone, string Address, DateTime BirthDate, int CountryID)
        {
            int ContactID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.Connectionstring);
            string query = @"Insert Into Contacts (FirstName,LastName,Email,Phone,Address,BirthDate,CountryID) 

                    Values (@FirstName,@LastName,@Email,@Phone,@Address,@BirthDate,@CountryID)
                        SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName",FirstName);
            command.Parameters.AddWithValue("@LastName",LastName);
            command.Parameters.AddWithValue("@Email",Email);
            command.Parameters.AddWithValue("@Phone",Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@BirthDate", BirthDate);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();    
                
                if (result != null && int.TryParse(result.ToString(), out int InsertedID) )
                    
                { 
                    ContactID = InsertedID;
                }

            }catch(Exception ex) { 
                
               // Console.WriteLine( "ERROR " +ex.Message);
                                  }
            finally { connection.Close(); }
            return ContactID;
        }

        public static bool UpdateContact(int ID, string FirstName, string LastName, string Email, string Phone, string Address, DateTime BirthDate, int CountryID)
        {
            int rowsEffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.Connectionstring);
            string query = "Update Contacts set FirstName = @FirstName, LastName =@LastName, Email = @Email, Phone = @Phone, Address = @Address, BirthDate = @BirthDate, CountryID = @CountryID where ContactID = @ContactID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("FirstName", FirstName);
            command.Parameters.AddWithValue("LastName",LastName);
            command.Parameters.AddWithValue("Email", Email);
            command.Parameters.AddWithValue("Phone", Phone);
            command.Parameters.AddWithValue("Address", Address);
            command.Parameters.AddWithValue("BirthDate",BirthDate);
            command.Parameters.AddWithValue("CountryID", CountryID);
            command.Parameters.AddWithValue("ContactID", ID);
            try
            {
                connection.Open();
                rowsEffected = command.ExecuteNonQuery();

            }catch(Exception ex) { //Console.WriteLine("ERROR "+ ex.Message); 
            }
            finally { connection.Close(); }
                
            return rowsEffected > 0;
        }

        public static bool DeleteContact(int ID)
        {
            int RowsEffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.Connectionstring);
            string query = @"Delete Contacts where ContactID = @ContactID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("ContactID", ID);
        
            try
            {
                connection.Open();
                RowsEffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) { //Console.WriteLine("ERROR "+ ex.Message);
            }
            finally { connection.Close(); }
            return RowsEffected > 0;
        }

        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.Connectionstring);
            string queery = "select *from Contacts";
            SqlCommand command = new SqlCommand(queery, connection);
            try
            {
                connection.Open();
                
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
               // Console.WriteLine(ex.Message);
               
            }
            finally { connection.Close(); }
            return dt;
        }

        public static bool IsContactExists(int ID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.Connectionstring);
            string query = "select Found=1 from Contacts where contactID = @ContactID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("ContactID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { 
                    IsFound = true;
                   
                }
                reader.Close ();
            }
            catch (Exception ex) { //Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
                return IsFound;
        }
    }
}
