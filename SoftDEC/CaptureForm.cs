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
        public bool ConfigurationFinished = true;

        /// <summary>
        /// CaptureForm can be used to generate a text screen, along with one image.
        /// </summary>
        /// <param name="Title">Specifies the title to use in the alert.</param>
        /// <param name="Description">Specifies the description to use in the alert.</param>
        /// <param name="ImageURL">Specifies the location to use for the image (local and online). Can be left unspecified for none.</param>
        /// <param name="BackValueR">Specifies the background red value for the title. Can be left unspecified for the default.</param>
        /// <param name="BackValueG">Specifies the background green value for the title. Can be left unspecified for the default.</param>
        /// <param name="BackValueB">Specifies the background blue value for the title. Can be left unspecified for the default.</param>
        /// <param name="ForeValueR">Specifies the foreground red value for the title. Can be left unspecified for the default.</param>
        /// <param name="ForeValueG">Specifies the foreground green value for the title. Can be left unspecified for the default.</param>
        /// <param name="ForeValueB">Specifies the foreground blue value for the title. Can be left unspecified for the default.</param>
        public CaptureForm(string Title, string Description, string ImageURL = null,
            int BackValueR = 188, int BackValueG = 0, int BackValueB = 0,
            int ForeValueR = 255, int ForeValueG = 255, int ForeValueB = 0
            )
        {
            InitializeComponent();
            try
            {
                // /// <summary>
                // /// Going beyond the number specified will revert to the number in this variable.
                // /// </summary>
                int maxLength = 255;

                if (BackValueR > maxLength) BackValueR = maxLength;
                if (BackValueG > maxLength) BackValueG = maxLength;
                if (BackValueB > maxLength) BackValueB = maxLength;
                if (ForeValueR > maxLength) ForeValueR = maxLength;
                if (ForeValueG > maxLength) ForeValueG = maxLength;
                if (ForeValueB > maxLength) ForeValueB = maxLength;

                TitleHolder.BackColor = Color.FromArgb(255, BackValueR, BackValueG, BackValueB);
                TitleHolder.ForeColor = Color.FromArgb(255, ForeValueR, ForeValueG, ForeValueB);

                if (string.IsNullOrWhiteSpace(Title))
                {
                    TitleText.Text = "CAUTION / PRECAUCIÓN";
                }
                else TitleText.Text = Title.Truncate(40).ToUpper();

                if (string.IsNullOrWhiteSpace(Description))
                {
                    DescriptionText.Text = "The alert that we have doesn't have any message information to display right now. If you see this happening more than once, please contact the owner of this content.\n\n" +
                        "La alerta que tenemos no tiene ninguna información de mensaje para mostrar en este momento. Si ve que esto sucede más de una vez, comuníquese con el propietario de este contenido.";
                }
                else DescriptionText.Text = Description.Truncate(500);

                if (string.IsNullOrWhiteSpace(ImageURL))
                {
                    this.Controls.Remove(AlertImage);
                    AlertImage.Dispose();
                }
                else
                {
                    try
                    {
                        AlertImage.Load(ImageURL);
                    }
                    catch (Exception)
                    {
                        this.Controls.Remove(AlertImage);
                        AlertImage.Dispose();
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                ConfigurationFinished = true;
            }
        }

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
