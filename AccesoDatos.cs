using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos
{
    class AccesoDatos
    {
        private SqlConnection conn = new SqlConnection("Server=DESKTOP-SLVKTQM;Database=Contactos;Integrated Security=true");

        public void InsertContact(Contacts contacts)
        {
            try
            {
                conn.Open();
                string query = @"INSERT INTO Contacts( [FirstName], [LastName], [Phone], [Address])
                                VALUES (@FirstName, @LastName, @Phone, @Address)";

                SqlParameter firstName = new SqlParameter();
                firstName.ParameterName = "@FirstName";
                firstName.Value = contacts.FirstName;
                firstName.DbType = System.Data.DbType.String;

                SqlParameter lastName = new SqlParameter("@LastName", contacts.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contacts.Phone);
                SqlParameter address = new SqlParameter("@Address", contacts.Address);


                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Contacts> GetContacts(string search = null)
        {
            List<Contacts> contacts = new List<Contacts>();
            try
            {
                conn.Open();
                string query = @"SELECT Id, FirstName, LastName, Phone, Address
                                FROM Contacts";

                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(search))
                {
                    query += @" WHERE FirstName LIKE @search OR LastName LIKE @search OR
                                Phone LIKE @search OR Address LIKE @search";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }

                command.CommandText = query;
                command.Connection = conn;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contacts.Add(new Contacts
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString()
                    }) ;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return contacts;

        }

        public void UpdateContact(Contacts contacts)
        {
            try
            {
                conn.Open();
                string query = @"UPDATE Contacts
                                SET FirstName = @FirstName,
                                    LastName = @LastName,
                                    Phone = @Phone,
                                    Address= @Address
                                WHERE Id = @Id";

                SqlParameter id = new SqlParameter("@Id", contacts.Id);
                SqlParameter firstName = new SqlParameter("@FirstName", contacts.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contacts.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contacts.Phone);
                SqlParameter address = new SqlParameter("@Address", contacts.Address);

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();



            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                conn.Close();
            }
        }

        public void DeleteContact(int Id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contacts WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", Id));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }

        }



    }
}
