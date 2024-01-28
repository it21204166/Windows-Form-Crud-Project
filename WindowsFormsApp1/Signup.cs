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
    public partial class Signup : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=Thisera\SQLEXPRESS;Initial Catalog=Student;Persist Security Info=True;User ID=sa;Password=geeshan2001");

        private string gender;
        public Signup()
        {
            InitializeComponent();
            LoadDataIntoComboBox();
        }



        private void LoadDataIntoComboBox()
        {
            try
            {
                connect.Open();

                
                string query = "SELECT regNo FROM Registration";
                SqlCommand cmd = new SqlCommand(query, connect);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    regNoCombox.Items.Add(reader["regNo"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure, Do you really want to Exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();

                    
                    if (regNoCombox.SelectedItem != null)
                    {
                        string selectedRegNo = regNoCombox.SelectedItem.ToString();  
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        
                        if (result == DialogResult.Yes)
                        {
                            
                            string deleteQuery = "DELETE FROM Registration WHERE regNo = @RegNo";

                            using (SqlCommand cmd = new SqlCommand(deleteQuery, connect))
                            {
                                cmd.Parameters.AddWithValue("@RegNo", selectedRegNo);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Record Deleted Successfully", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    
                                    ClearInputFields();

                                    
                                    BindData();
                                }
                                else
                                {
                                    MessageBox.Show("No records deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            {
                firstnameTxt.Clear();
                lastnameTxt.Clear();
                dateofbirth.Value = DateTime.Now;
                gender = "";
                AdressTxt.Clear();
                EmailTxt.Clear();
                MobilePhoneTxt.Clear();
                HomePhoneTxt.Clear();
                ParentNameTxt.Clear();
                ParentNICTxt.Clear();
                ParentContact.Clear();
            }

            firstnameTxt.Focus();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    
                    if (regNoCombox.SelectedItem != null)
                    {
                        string selectedRegNo = regNoCombox.SelectedItem.ToString();  

                        string updateQuery = "UPDATE Registration SET " +
                            "firstName = @FirstName, " +
                            "lastName = @LastName, " +
                            "dateOfBirth = @DateOfBirth, " +
                            "gender = @Gender, " +
                            "address = @Address, " +
                            "email = @Email, " +
                            "mobilePhone = @MobilePhone, " +
                            "homePhone = @HomePhone, " +
                            "parentName = @ParentName, " +
                            "nic = @Nic, " +
                            "contactNo = @ContactNo " +
                            "WHERE regNo = @RegNo";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@RegNo", selectedRegNo);
                            cmd.Parameters.AddWithValue("@FirstName", firstnameTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@LastName", lastnameTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@DateOfBirth", dateofbirth.Value.Date);
                            cmd.Parameters.AddWithValue("@Gender", radioButtonMale.Checked ? "Male" : "Female");
                            cmd.Parameters.AddWithValue("@Address", AdressTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email", EmailTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@MobilePhone", int.Parse(MobilePhoneTxt.Text.Trim()));
                            cmd.Parameters.AddWithValue("@HomePhone", int.Parse(HomePhoneTxt.Text.Trim()));
                            cmd.Parameters.AddWithValue("@ParentName", ParentNameTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Nic", ParentNICTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@ContactNo", int.Parse(ParentContact.Text.Trim()));

                            

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record Updated Successfully", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                
                                DisplayDetails(selectedRegNo);

                                
                                BindData();
                            }
                            else
                            {
                                MessageBox.Show("No records updated", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying details: " + ex.Message);
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    String insertData = "INSERT INTO registration (firstName, lastName,dateOfBirth, gender ,address, email ,mobilePhone, homePhone ,parentName ,nic,contactNo)" +
                        "VALUES(@firstName, @lastName, @dateOfBirth, @gender ,@address, @email ,@mobilePhone, @homePhone ,@parentName ,@nic, @contactNo)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstnameTxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@lastName", lastnameTxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@dateOfBirth", dateofbirth.Value.Date.ToString().Trim());
                        cmd.Parameters.AddWithValue("@gender", gender.Trim());
                        cmd.Parameters.AddWithValue("@address", AdressTxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", EmailTxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@mobilePhone", int.Parse(MobilePhoneTxt.Text.Trim()));
                        cmd.Parameters.AddWithValue("@homePhone", int.Parse(HomePhoneTxt.Text.Trim()));
                        cmd.Parameters.AddWithValue("@parentName", ParentNameTxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@nic", ParentNICTxt.Text.Trim());
                        cmd.Parameters.AddWithValue("@contactNo", int.Parse(ParentContact.Text.Trim()));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record Added Succesfully", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataIntoComboBox();
                        BindData();
                        ClearInputFields();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connection DB: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }

        void BindData()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM registration", connect);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dc = new DataTable();
            da.Fill(dc);
            dataGridView1.DataSource = dc;
        }



        
        private void ClearInputFields()
        {

            firstnameTxt.Text = "";
            lastnameTxt.Text = "";
            dateofbirth.Value = DateTime.Now;
            gender = "";
            AdressTxt.Text = "";
            EmailTxt.Text = "";
            MobilePhoneTxt.Text = "";
            HomePhoneTxt.Text = "";
            ParentNameTxt.Text = "";
            ParentNICTxt.Text = "";
            ParentContact.Text = "";


            firstnameTxt.Focus();
        }

        private void ParentContact_TextChanged(object sender, EventArgs e)
        {

        }

        private void ParentNICTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void ParentNameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void HomePhoneTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void MobilePhoneTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void EmailTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdressTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFemale.Checked)
            {
                gender = "Female";
            }
        }

        private void radioButtonMale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMale.Checked)
            {
                gender = "Male";
            }
        }

        private void dateofbirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lastnameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void firstnameTxt_TextChanged(object sender, EventArgs e)
        {

        }



        
        private void DisplayDetails(string regNo)
        {
            try
            {
                connect.Open();

                string query = "SELECT * FROM Registration WHERE regNo = @RegNo";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@RegNo", regNo);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    firstnameTxt.Text = reader["firstName"].ToString();
                    lastnameTxt.Text = reader["lastName"].ToString();
                    dateofbirth.Text = reader["dateOfBirth"].ToString();
                    AdressTxt.Text = reader["address"].ToString();
                    EmailTxt.Text = reader["email"].ToString();
                    MobilePhoneTxt.Text = reader["mobilePhone"].ToString();
                    HomePhoneTxt.Text = reader["homePhone"].ToString();
                    ParentNameTxt.Text = reader["parentName"].ToString();
                    ParentNICTxt.Text = reader["nic"].ToString();
                    ParentContact.Text = reader["contactNo"].ToString();

                    
                    string genderValue = reader["gender"].ToString();
                    radioButtonMale.Checked = (genderValue == "Male");
                    radioButtonFemale.Checked = (genderValue == "Female");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying details: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }
    





    

            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Signup_Load(object sender, EventArgs e)
        {
            
            this.registrationTableAdapter.Fill(this.studentDataSet.registration);
            BindData();
            
        }

        private void regNoDown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void regNoDown_DropDown(object sender, EventArgs e)
        {
            
        }

        private void regNoDown_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void regNoDown_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Form1 sForm = new Form1();
            sForm.Show();
            this.Hide();
        }

        private void regNoCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRegNo = regNoCombox.SelectedItem.ToString();
            DisplayDetails(selectedRegNo);
        }
    }
}
