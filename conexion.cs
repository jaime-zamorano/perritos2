using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace perritos2
{
    public class conexion
    {
        public List<DataRow> CallBaseSqlParmetros(string sp, string[,] parametros)
        {
            DataTable dt = new DataTable("DataOt");

            try
            {

                SqlConnection cnx = new SqlConnection();
                string Strconexion = "Data Source = 192.168.200.35; Initial Catalog = Test_Dog; Persist Security Info = True; User ID = rentway; Password = euro2801";
                
                SqlCommand cmd = new SqlCommand();
                SqlParameter param = new SqlParameter();

                for (int i = 0; i <= parametros.GetUpperBound(0); i++)
                {
                    param.ParameterName = parametros[i, 0];
                    param.Value = parametros[i, 1];
                    cmd.Parameters.Add(param);
                    param = new SqlParameter();
                }

                SqlDataReader dtr = default(SqlDataReader);


                DataSet ds = new DataSet();

                cnx = new SqlConnection(Strconexion);
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                cnx.Open();
                dtr = cmd.ExecuteReader();

                dt.Load(dtr);





                cnx.Close();
                dtr.Close();
                dtr.Dispose();


                List<DataRow> dr = dt.AsEnumerable().ToList();


                return dr;

            }
            catch (Exception ex)
            {
                //logfiles Erra = new logfiles();
                //Erra.ErrorLog(HttpContext.Current.Server.MapPath("/Logs/ErrorLog"), ex.Message + "1 " + sp + "par " + parametros.ToString());
                string msj = ex.Message.ToString();
                List<DataRow> dr = dt.AsEnumerable().ToList();
                return dr;

            }


        }
    }
}
