using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using HWOOP.Properties;
using System.Windows.Forms;

namespace HWOOP
{
    class MyMember
    {
        public string m_username { get; set; }
        public string m_Password { get; set; }
        public string m_email { get; set; }
       
        public void CreateUser(out bool logon)
        {
            try
            {
                string connString = Settings.Default.Northwind;
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connString;
                    conn.Open();
                    using (SqlCommand com = new SqlCommand())
                    {
                        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(this.m_Password));
                        StringBuilder sBuilder = new StringBuilder();
                        for (int i = 0; i < data.Length; i++)
                        {
                            sBuilder.Append(data[i].ToString("x2"));
                        }
                        com.CommandText = "Insert into Member (Name, Password, Email) Values('" + this.m_username + "','" + sBuilder + "','" + this.m_email + "')";
                        com.Connection = conn;
                        com.ExecuteNonQuery();
                        logon = true;
                    }
                }//auto call command.dispose();
            }//auto call conn.close()=>conn.dispose();
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
                logon = false;
            }
        }

        public void CreateUser()
        {
        }

        public void ValidateUser(out bool checkresult)
        {
            //try
            //{
            string connString = Settings.Default.Northwind;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (SqlCommand com = new SqlCommand())
                {
                    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(this.m_Password));
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    com.CommandText = $"Select * from Member where name='{this.m_username}'  and password='{sBuilder}'";
                    com.Connection = conn;
                    SqlDataReader dataReader = com.ExecuteReader();
                    checkresult = dataReader.HasRows;
                }
            }//auto call command.dispose();
            //}//auto call conn.close()=>conn.dispose();
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}/
        }

        public void ChangePassword(string OldPassword, string NewPassword)
        {

        }
        public string FindUsersByEmail(string email)
        {
            string username = "";
            string connString = Settings.Default.Northwind;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = $"Select name from Member where email='{email}'";
                    com.Connection = conn;
                    SqlDataReader dataReader = com.ExecuteReader();
                    while (dataReader.Read())
                    {
                        username = dataReader[0].ToString();
                    }
                }
            }//auto call command.dispose();
            return username;
        }
        public void DeleteUser(string username)
        {

        }
        public string FindPasswordByName(string username)
        {
            string password = "";
            string connString = Settings.Default.Northwind;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = $"Select password from Member where name='{username}'";
                    com.Connection = conn;
                    SqlDataReader dataReader = com.ExecuteReader();
                    while (dataReader.Read())
                    {
                        password = dataReader[0].ToString();
                    }
                }
            }//auto call command.dispose();
            return password;
        }
    }


}

