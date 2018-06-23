using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fx.Core
{
    class SqlAction
    {
        private static SQLiteConnection conn = new SQLiteConnection(config.DataSource);
        private static SQLiteCommand cmd = new SQLiteCommand();

        public static bool AddH(Dictionary<string, object> dic)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);                
                sh.Insert("fcjlk3", dic);
                return true;       
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public static DataTable SelectH(string searchStr)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);
                string sql = "select * from fcjlk3";
                DataTable dt = sh.Select(sql);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                DataTable dt = new DataTable();
                return dt;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
