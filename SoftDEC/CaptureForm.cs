using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftDEC
{
    public partial class CaptureForm : Form
    {
        public CaptureForm(string Title, string Description)
        {
            InitializeComponent();
            try
            {
                if (string.IsNullOrWhiteSpace(Title))
                {
                    TitleText.Text = "CAUTION / PRECAUCIÓN";
                }
                else TitleText.Text = Title.Truncate(15).ToUpper();

                if (string.IsNullOrWhiteSpace(Description))
                {
                    DescriptionText.Text = "The alert that we have doesn't have any message information to display right now. If you see this happening more than once, please contact the owner of this content.\n\n" +
                        "La alerta que tenemos no tiene ninguna información de mensaje para mostrar en este momento. Si ve que esto sucede más de una vez, comuníquese con el propietario de este contenido.";
                }
                else DescriptionText.Text = Description.Truncate(500).ToUpper();
            }
            catch (Exception)
            {

            }
        }

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
