﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace AcessoBD01
{
    public partial class frmAcesso : Form
    {
        public frmAcesso()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Método modifica (para DELETE, UPDATE e INSERT)
        private void modifica(String sql) 
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Conexao.abreConexao();
            try
            {
                //perguntar ao usuário se ele deseja executar essa ação
                if (MessageBox.Show("Deseja executar essa ação?","Atenção", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Ação realizada com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Falha ao realizar essa ação!");
                    }
                    cmd.Dispose();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.fechaConexao();
            }
        
    }
        #endregion
        #region Botão pesquisar
        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            
            cmd.CommandText = String.Format("SELECT * FROM estados WHERE codigo = {0}",
                        txtCodigo.Text);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Conexao.abreConexao();
            MySqlDataReader dr;
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtCodigo.Text = dr["codigo"].ToString();
                    txtNome.Text = dr["nome"].ToString();
                    txtUF.Text = dr["uf"].ToString();
                }
                dr.Close();
                cmd.Dispose();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.fechaConexao();
            }
        }
        #endregion
        #region Consulta
        private void pesquisa(string consulta) {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = consulta;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Conexao.abreConexao();
            MySqlDataReader dr;
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtCodigo.Text = dr["codigo"].ToString();
                    txtNome.Text = dr["nome"].ToString();
                    txtUF.Text = dr["uf"].ToString();
                }
                dr.Close();
                cmd.Dispose();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally {
                Conexao.fechaConexao();
            }
        }
        #endregion
        private void btnApagando_Click(object sender, EventArgs e)
        {
            string apaga = String.Format("DELETE FROM estados WHERE codigo = {0}", txtCodigo.Text); 
            modifica(apaga);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            String atualiza = String.Format("UPDATE estados SET nome='{0}',uf='{1}' WHERE codigo='{2}'",txtNome.Text,txtUF.Text,txtCodigo.Text);
            modifica(atualiza);
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            String terror = "INSERT INTO estados VALUES('(" + txtCodigo.Text + "','" + txtNome.Text + "','" + txtUF.Text + "')";
            String novo = String.Format("INSERT INTO estados VALUES ('{0}','{1}','{2}')", txtCodigo.Text, txtNome.Text, txtUF.Text);
            modifica(novo);
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            string primeiro = String.Format("SELECT * FROM espotados LIMIT 1");
            pesquisa(primeiro);
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            string ultimo = String.Format("SELECT * FROM estados ORDER BY codigo DESC LIMIT 1");
            pesquisa(ultimo);
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            string proximo = String.Format("SELECT * FROM estados WHERE codigo > {0}LIMIT1", txtCodigo.Text); 
            pesquisa(proximo);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            string anterior = String.Format("SELECT * FROM estados WHERE codigo < {0} ORDER BY codigo DESC LIMIT 1", txtCodigo.Text); 
            pesquisa(anterior);
        }
        private void frmAcesso_Load(object sender, EventArgs e)
        { 
            string primeiro =
                String.Format("SELECT * FROM estados LIMIT 1");
            pesquisa(primeiro); 
        }
    }
}
