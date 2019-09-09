using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zeroit.Framework.MaterialDesign.Controls;
using XanderUI;
using System.Text.RegularExpressions;
using System.IO;


namespace Lifeware_Metro_UI
{
    public partial class Form_Dashboard : ZeroitMaterialForm
    {

        public int userID_H;
        public string FilePathUebergabe;
        // Hier wird der Schlüsselübergeben
        public string key_User_Cryt;




        public Form_Dashboard()
        {
            InitializeComponent();
        }

        public Form_Dashboard(int userID,string key)
        {
            InitializeComponent();
            this.userID_H = userID;
            this.key_User_Cryt = key;


            using (DatabaseConnectingHandler db = new DatabaseConnectingHandler())
            {
                // hier wird das ByteArray auf Null gesetzt !
                byte[] PictureLoad = null;



                foreach (var pat in db.Patients)
                {
                    if (pat.LoginId_Login == userID_H)
                    {
                        tbx_pd_anrede.Text = AesOperation.DecryptString(key_User_Cryt, pat.Anrede); 
                        tbx_pd_vorname.Text = AesOperation.DecryptString(key_User_Cryt, pat.Vorname); 
                        tbx_pd_nachname.Text = AesOperation.DecryptString(key_User_Cryt, pat.Nachname);
                        tbx_pd_strasse.Text = AesOperation.DecryptString(key_User_Cryt, pat.Straße); 
                        tbx_pd_nr.Text = pat.Nr.ToString(); 
                        tbx_pd_plz.Text = pat.PLZ.ToString(); 
                        tbx_pd_ort.Text = AesOperation.DecryptString(key_User_Cryt, pat.Ort);
                        tbx_pd_geburtstag.Text = AesOperation.DecryptString(key_User_Cryt, pat.geb); 
                        tbx_pd_geburtsort.Text = AesOperation.DecryptString(key_User_Cryt, pat.ort_geb); 
                        tbx_pd_telefonnummer.Text = AesOperation.DecryptString(key_User_Cryt, pat.ort_geb);
                        tbx_pd_krankenkasse.Text = AesOperation.DecryptString(key_User_Cryt, pat.Krankenkasse);
                        tbx_pd_kk_nummer.Text = AesOperation.DecryptString(key_User_Cryt, pat.KrankenkassenNR); 
                        tbx_pd_personalberater_nr.Text = AesOperation.DecryptString(key_User_Cryt, pat.PersonalausweisNR); 
                        tbx_pd_fuehrerschein_nr.Text = AesOperation.DecryptString(key_User_Cryt, pat.FührerscheinNR); 
                        
                        

                        // hier werden die Daten gespeichert 
                        PictureLoad = (byte[])(pat.Image);
                        
                        MemoryStream ms = new MemoryStream(PictureLoad);
                        picbox_pd_meinBild.SizeMode = PictureBoxSizeMode.StretchImage;
                        picbox_pd_meinBild.Image = Image.FromStream(ms);
                    }
                }
            }

        }

        private void Tbx_pd_geburtstag_Validating(object sender, CancelEventArgs e)
        {
            Regex reg = new Regex(@"^(\d{1,2}).(\d{1,2}).(\d{4})$");
            Match m = reg.Match(tbx_pd_geburtstag.Text);
            if (m.Success)
            {
                int dd = int.Parse(m.Groups[1].Value);
                int mm = int.Parse(m.Groups[2].Value);
                int yyyy = int.Parse(m.Groups[3].Value);
                e.Cancel = dd < 1 || dd > 31 || mm < 1 || mm > 12 || yyyy > 2019;
            }
            else e.Cancel = true;
            if (e.Cancel)
            {
                if (MessageBox.Show("Das Datenformat ist leider Flasch.Das Korrekteformat dd/mm/yyyy\n+ dd Biite geben Sie Zahlen zwischen 1 and 31.\n" +
                    "+ mm und den 1 and 12.\n+ yyyy vor dem Jahr 2019 ein.",
                    "Invalid date", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                    e.Cancel = false;
            }
        }

        private void Tbx_pd_telefonnummer_Format(object sender, EventArgs e)
        {
                                                                                            //string format;
                                                                                            //string ausgabe;
                                                                                            //format = "{0:(###) ###-####}";
                                                                                            //ausgabe = String.Format(format, tbx_pd_telefonnummer.Text);
                                                                                            //tbx_pd_ort.Text = ausgabe;
                                                                                            //TelefonnummerFormat telefonformat = new TelefonnummerFormat(tbx_pd_telefonnummer.Text, "+##########");
                                                                                            //tbx_pd_telefonnummer.Text = telefonformat.ToString();

            NummerFormat format = new NummerFormat(tbx_pd_telefonnummer.Text);
            tbx_pd_telefonnummer.Text = format.ToString();



        }

        private void Btn_picbox_speichern_Click(object sender, EventArgs e)
        {
            OpenFileDialog DateiAuswahl = new OpenFileDialog();
            DateiAuswahl.Filter = "image files (*.png)| *.png | All files (*.*) | *.*";
            DateiAuswahl.InitialDirectory = "";
            DateiAuswahl.Title = "Profilbild wählen";



            if (DateiAuswahl.ShowDialog() == DialogResult.OK)
            {

                picbox_pd_meinBild.SizeMode = PictureBoxSizeMode.StretchImage;
                picbox_pd_meinBild.ImageLocation = DateiAuswahl.FileName;

                                                                                            //--------------------------------------------------------------------    
                                                                                            // FilePathUebergabe = DateiAuswahl.FileName;
                                                                                            //Image image = Image.FromFile(DateiAuswahl.FileName);
                                                                                            //picbox_pd_eigenesBild.
                                                                                            //picbox_pd_eigenesBild.Image = image;
                                                                                            /// Hier wurde die PictureBox vom Zeroit verwendet.
                                                                                            //picbox_zeroit.BackgroundImageLayout = ImageLayout.Stretch;
                                                                                            //picbox_zeroit.Image = image;
                                                                                            //---------------------------------------------------------------------


            }
        }

        private void Form_Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login gb = new Login();
            gb.Form_Show();
        }

        private void Btn_speichern_Click(object sender, EventArgs e)
        {
            DatabaseConnectingHandler db = new DatabaseConnectingHandler();

            /// Table Persönliche Daten
            Patients pat = new Patients();

            if (picbox_pd_meinBild.ImageLocation != null)
            {
                byte[] imgData;
                imgData = File.ReadAllBytes(picbox_pd_meinBild.ImageLocation);
                pat.Image = imgData;
            }
            else
            {
                byte [] PictureLoad = (byte[])(pat.Image);
                pat.Image = PictureLoad;
            }
            {
                pat.LoginId_Login = userID_H;
                pat.Anrede = AesOperation.EncryptString(key_User_Cryt, tbx_pd_anrede.Text); 
                pat.Vorname = AesOperation.EncryptString(key_User_Cryt, tbx_pd_vorname.Text);
                pat.Nachname = AesOperation.EncryptString(key_User_Cryt, tbx_pd_nachname.Text);
                pat.Straße = AesOperation.EncryptString(key_User_Cryt, tbx_pd_strasse.Text);
                pat.Nr = Int16.Parse(tbx_pd_nr.Text);
                pat.PLZ = Int16.Parse(tbx_pd_plz.Text);
                pat.Ort = AesOperation.EncryptString(key_User_Cryt, tbx_pd_ort.Text);
                pat.geb = AesOperation.EncryptString(key_User_Cryt, tbx_pd_geburtstag.Text);
                pat.ort_geb = AesOperation.EncryptString(key_User_Cryt, tbx_pd_geburtsort.Text);
                pat.Telefon = AesOperation.EncryptString(key_User_Cryt, tbx_pd_telefonnummer.Text);
                pat.Krankenkasse = AesOperation.EncryptString(key_User_Cryt, tbx_pd_krankenkasse.Text);
                pat.KrankenkassenNR = AesOperation.EncryptString(key_User_Cryt, tbx_pd_kk_nummer.Text);
                pat.PersonalausweisNR = AesOperation.EncryptString(key_User_Cryt, tbx_pd_personalberater_nr.Text);
                pat.FührerscheinNR = AesOperation.EncryptString(key_User_Cryt, tbx_pd_fuehrerschein_nr.Text);
                //pat.Image = imgData;
            };


            db.Patients.Add(pat);
            db.SaveChanges();

            lbl_Dashboard_Status.ForeColor = Color.Red;
            lbl_Dashboard_Status.Text = "GESPEICHERT";


            /// Table Notfallkontakte
            Notfallkontakte notk = new Notfallkontakte();

            /// Notfallkontakte
            // {
            //notk.PatientsId_Patient = userID_H;
            //notk.Vorname = tbx.Text;
            //notk.Name = NotfallName.Text;
            //notk.Strasse = NotfallStraße.Text;
            //notk.PLZ = NotfallPLZ.Text;
            //notk.Ort = NotfallOrt.Text;
            // };

            //db.Notfallkontakte.Add(notk);
            //db.SaveChanges();

        }

        private void Form_Dashboard_Load(object sender, EventArgs e)
        {
            lbl_Dashboard_Status.Text = "OPEN";
        }

        private void Tbx_pd_anrede_TextChanged(object sender, EventArgs e)
        {
            lbl_Dashboard_Status.Text = "In Bearbeitung";
        }
    }
}
