using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Security.Cryptography;


namespace Lifeware_Metro_UI
{
    public partial class Login : Zeroit.Framework.MaterialDesign.Controls.ZeroitMaterialForm
    {

        DatabaseConnectingHandler db = new DatabaseConnectingHandler();
        Logins login = new Logins();

        public int userID;


        public Login()
        {
            InitializeComponent();

        }

        private void Btn_anmelden_Click(object sender, EventArgs e)
        {
            string username = tbx_Login.Text;
            string password = HPass(tbx_Passwd.Text);


            foreach (var user in db.Logins)
            {

                if (user.Username == username && user.Password == password)
                {
                    tbx_Login.Text = string.Empty;
                    tbx_Passwd.Text = string.Empty;

                    userID = user.Id_Login;

                    MessageBox.Show(userID.ToString());

                }
            }
        }

        private void AddUser(string username, string password, string confirmPass)
        {
            foreach (var user in db.Logins)
            {
                if (user.Username.Equals(username))
                {
                    MessageBox.Show("Dieser Nutzer exsistiert !");
                    return;

                }
            }

            if (password != confirmPass)
            {
                MessageBox.Show("Password stimmt nicht überein!");
            }

            else if (password.Length < 8)
            {
                MessageBox.Show("Das Passwort muss mindestens 8 Zeichen haben.");
            }
            else
            {
                string Epass = HPass(password);
                login.Username = username;

            }
        }

        public string HPass(string password)
        {
            SHA512 sha = new SHA512CryptoServiceProvider();
            sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] result = sha.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
