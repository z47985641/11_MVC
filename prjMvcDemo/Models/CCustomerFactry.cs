using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjMvcDemo.Models
{
    public class CCustomerFactry
    {
        public void delete(int fId)
        {
            string sql = "DELETE FROM tCustomer where fId = @K_fId";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_fId", (object)fId));
            executeSql(sql, paras);
        }
        public void create(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "INSERT INTO tCustomer (";
            if(!string.IsNullOrEmpty(p.fname))
                sql += " fName,";
            if (!string.IsNullOrEmpty(p.fphone))
                sql += " fPhone,";
            if (!string.IsNullOrEmpty(p.fEmail))
                sql += " fEmail,";
            if (!string.IsNullOrEmpty(p.fAdress))
                sql += " fAdress,";
            if (!string.IsNullOrEmpty(p.fPassword))
                sql += " fPassword,";
            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += " )VALUES(";
            if (!string.IsNullOrEmpty(p.fname))
            {
            sql += " @K_name,"; 
            paras.Add(new SqlParameter("K_name", (object)p.fname));
            }
                
            if (!string.IsNullOrEmpty(p.fphone))
            {
                sql += " @K_phone,";
                paras.Add(new SqlParameter("K_phone", (object)p.fphone));
            }
            
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += " @K_email,";
                paras.Add(new SqlParameter("K_email", (object)p.fEmail));
            }
            
            if (!string.IsNullOrEmpty(p.fAdress))
            {
                sql += " @K_adress,";
                paras.Add(new SqlParameter("K_adress", (object)p.fAdress));
            }
            
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += " @K_password,";
                paras.Add(new SqlParameter("K_password", (object)p.fPassword));
            }

            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1) + ")";

            executeSql(sql,paras);

        }

        internal List<CCustomer> queryByKeyword(string Keyword)
        {
            string sql = "SELECT * FROM tCustomer WHERE fName LIKE @K_Keyword";
            sql += " OR fPhone LIKE @K_Keyword";
            sql += " OR fEmail LIKE @K_Keyword";
            sql += " OR fAdress LIKE @K_Keyword";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_Keyword", (object)Keyword));
            return querybysql(sql, paras);
        }

        public void update(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "update tCustomer SET";
           if (!string.IsNullOrEmpty(p.fname))
            {
                sql += " fName = @K_name,";
                paras.Add(new SqlParameter("K_name", (object)p.fname));
            }

            if (!string.IsNullOrEmpty(p.fphone))
            {
                sql += " fPhone = @K_phone,";
                paras.Add(new SqlParameter("K_phone", (object)p.fphone));
            }

            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += " fEmail = @K_email,";
                paras.Add(new SqlParameter("K_email", (object)p.fEmail));
            }

            if (!string.IsNullOrEmpty(p.fAdress))
            {
                sql += " fAdress =  @K_adress,";
                paras.Add(new SqlParameter("K_adress", (object)p.fAdress));
            }

            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += " fPassword = @K_password,";
                paras.Add(new SqlParameter("K_password", (object)p.fPassword));
            }

            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);

            sql += " WHERE fid = @K_id";
            paras.Add(new SqlParameter("K_id", (object)p.fid));

            executeSql(sql, paras);

        }

        public CCustomer querybyId(int fid)
        {
            string sql = "SELECT * FROM tCustomer WHERE fId = @K_fid";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_fid", (object)fid));
            List<CCustomer> list = querybysql(sql, paras);
            if (list.Count == 0)
                return null;

            return list[0];
        }
        public List<CCustomer> queryALL()
        {
            string sql = "  select * from tCustomer";
            return querybysql(sql, null);
        }

        private  List<CCustomer> querybysql(string sql, List<SqlParameter> paras)
        {
            List<CCustomer> list = new List<CCustomer>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbdemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            if (paras != null)
                cmd.Parameters.AddRange(paras.ToArray());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CCustomer X = new CCustomer();
                X.fid = (int)reader["fid"];
                X.fname = reader["fname"].ToString();
                X.fphone = reader["fphone"].ToString();
                X.fEmail = reader["fEmail"].ToString();
                X.fAdress = reader["fAdress"].ToString();
                X.fPassword = reader["fPassword"].ToString();
                list.Add(X);
            }
            con.Close();
            return list;
        }

        private static void executeSql(string sql,List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbdemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            if (paras != null)
                cmd.Parameters.AddRange(paras.ToArray());
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}