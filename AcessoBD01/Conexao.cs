using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace AcessoBD01
{
    class Conexao
    {
        private static string strConexao = 
            @"Server = localhost; Database = acessobd;
            Uid = astrogildo; Pwd = 123456";
        private static MySqlConnection cn = new MySqlConnection(strConexao);

        public static MySqlConnection abreConexao() {
            try
            {
                if (cn.State.ToString() == "Closed")
                {
                    cn.Open();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return cn;
        }

        public static void fechaConexao()
        {
            try
            {
                if (cn.State.ToString() == "Open")
                {
                    cn.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
