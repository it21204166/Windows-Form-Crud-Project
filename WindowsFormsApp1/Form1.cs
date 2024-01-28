using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {

        SqlConnection connect = new SqlConnection(@"Data Source=Thisera\SQLEXPRESS;Initial Catalog=Student;Persist Security Info=True;User ID=sa;Password=geeshan2001");
       
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure, Do you really want to Exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            { 
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {




            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM login WHERE username = @username AND password = @password";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@username", NameTxt.Text);
                        cmd.Parameters.AddWithValue("@password", passwordTxt.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count >= 1)
                        {
                            
                            if (IsPasswordComplex(passwordTxt.Text))
                            {
                                Signup sForm = new Signup();
                                sForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid Login credentials, please check Username and Password and try again", "Invalid login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Login credentials, please check Username and Password and try again", "Invalid login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connection DB: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }

            
             bool IsPasswordComplex(string password)
            {
                
                return password.Any(char.IsUpper) && password.Any(char.IsLower);
            }





        }

        private void NameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShowPassword_CheckedChanged(object sender, EventArgs e)
        {

            if (ShowPassword.Checked)
            {
                passwordTxt.PasswordChar = '\0';
            }
            else
            {
                passwordTxt.PasswordChar = '*';
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            
            {
                passwordTxt.Clear();
                NameTxt.Clear();
            }

            NameTxt.Focus();


        }

        private void passwordTxt_TextChanged(object sender, EventArgs e)
        {
            passwordTxt.PasswordChar = '*';
        }

        

        
    }
}
