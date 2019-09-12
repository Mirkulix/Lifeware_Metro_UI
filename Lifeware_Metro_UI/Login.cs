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

    /// <summary>
    /// Mit dieser Form wird das Login und die Registrierung bereit gestellt.
    /// 
    /// </summary>
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
                /////// Wichtiger Block /////// 
                // Hier wird Verschlüsselungs_Key_generiert 
                // Der Schlüssel wird nur ein einziges mal erzeugt. Wenn dieser Schlüsselverloren geht 
                // könnne die Daten nicht wieder hergestellt werden.
                login.AES_ID = AesOperation.GenerateCoupon(32);


                // Ausgabe des Schlüssels in eine MessageBox
                MessageBox.Show("Der Security-Key Lautet:", login.AES_ID);

                tbx_reg_login.Text = String.Empty;
                tbx_reg_passwd.Text = String.Empty;
                tbx_reg_confirm_passwd.Text = String.Empty;

                db.Logins.Add(login);
                db.SaveChanges();

                //Hier wird gezeigt das die Registrierung abgeschlossen ist.
                MessageBox.Show("Registrierung ist angeschlossen.");


            }
        }


        // Hier wird das Password verschlüsselt über ein SHA512 

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

        // Hier wird die Form geladen und weiter Attribute aktiviert.
        // Dazu gehört die das Einbilden des Logos und die Aktivierung der PasswordChar.

        private void Login_Load(object sender, EventArgs e)
        {
            PicLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            PicLogo.Image = Lifeware_Metro_UI.Properties.Resources.Xsaver;

            tbx_Passwd.UseSystemPasswordChar = true;
            tbx_reg_passwd.UseSystemPasswordChar = true;
            tbx_reg_confirm_passwd.UseSystemPasswordChar = true;
        }

        //Hier wird die Login Form ausgeblendet.

        private void Form_Verstecken()
        {
            this.Hide();
            //this.Visible = false;
            //this.ShowInTaskbar = false;
        }

        // Hier wird Sie wieder Aktiviert

        public void Form_Show()
        {
            this.Show();
        }

        // Mit der Methode wird ein User angelegt.

        private void Btn_registrieren_Click(object sender, EventArgs e)
        {
            AddUser(tbx_reg_login.Text, tbx_reg_passwd.Text, tbx_reg_confirm_passwd.Text);
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
