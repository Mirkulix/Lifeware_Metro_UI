using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zeroit.Framework.MaterialDesign.Controls;
using XanderUI;
using System.Text.RegularExpressions;

namespace Lifeware_Metro_UI
{
    public partial class Form_Dashboard : ZeroitMaterialForm
    {

        public int userID_H;
        public Form_Dashboard()
        {
            InitializeComponent();
        }

        public Form_Dashboard(int userID)
        {
            InitializeComponent();
            this.userID_H = userID;

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
    }
}
