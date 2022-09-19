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
            string sql = "INSERT INTO tCustomer (";
            sql += " fName,";
            sql += " fPhone,";
            sql += " fEmail,";
            sql += " fAdress,";
            sql += " fPassword";
            sql += " )VALUES(";
            sql += " @K_name,";
            sql += " @K_phone,";
            sql += " @K_email,";
            sql += " @K_adress,";
            sql += " @K_password)";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_name", (object)p.fname));
            paras.Add(new SqlParameter("K_phone", (object)p.fphone));
            paras.Add(new SqlParameter("K_email", (object)p.fEmail));
            paras.Add(new SqlParameter("K_adress", (object)p.fAdress));
            paras.Add(new SqlParameter("K_password", (object)p.fPassword));

            executeSql(sql,paras);

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