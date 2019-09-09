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
        int MaxVersuche = 0;

        DatabaseConnectingHandler db = new DatabaseConnectingHandler();
        Logins login = new Logins();

        public int userID;
        public string key;



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
                    key = user.AES_ID;

                    Form_Dashboard dboard = new Form_Dashboard(userID,key);
                    // dboard.Dispose();
                    dboard.Show();

                    
                    // Hier wird die Hauptform versteckt.
                    Form_Verstecken();

                    return;
                }


               
               
            }

            MaxVersuche++;

            lbl_Login_false.Text = "Login/Password falsch";

            // Hier werden die Versuche gezählt für die Falscheingabe der Daten.
            if (MaxVersuche == 3)
            {
                MessageBox.Show("Sie habe 3 mal sich Falsch angemeldet, das System ist in 60. Sekunden wieder verfügbar");
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
                login.Password = Epass;
                // Hier wird Verschlüsselungs_Key_generiert
                login.AES_ID = AesOperation.GenerateCoupon(32);

                tbx_reg_login.Text = String.Empty;
                tbx_reg_passwd.Text = String.Empty;
                tbx_reg_confirm_passwd.Text = String.Empty;

                db.Logins.Add(login);
                db.SaveChanges();


                MessageBox.Show("Registrierung ist angeschlossen.");


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

        private void Login_Load(object sender, EventArgs e)
        {
            tbx_Passwd.UseSystemPasswordChar = true;
            tbx_reg_passwd.UseSystemPasswordChar = true;
            tbx_reg_confirm_passwd.UseSystemPasswordChar = true;
        }

        private void Form_Verstecken()
        {
            this.Hide();
            //this.Visible = false;
            //this.ShowInTaskbar = false;
        }

        public void Form_Show()
        {
            this.Show();
        }

        private void Btn_registrieren_Click(object sender, EventArgs e)
        {
            AddUser(tbx_reg_login.Text, tbx_reg_passwd.Text, tbx_reg_confirm_passwd.Text);
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
    }
}
