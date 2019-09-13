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


namespace Lifeware_Metro_UI
{
    public partial class DocViewer : ZeroitMaterialForm
    {

        OpenFileDialog DateiAuswahl = new OpenFileDialog();

        public DocViewer()
        {
            InitializeComponent();
        }


        public DocViewer(string Dateiname)
        {

        }
    }
}
