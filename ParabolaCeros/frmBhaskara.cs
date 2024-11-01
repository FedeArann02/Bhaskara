using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParabolaCeros
{
    public partial class frmBhaskara : Form
    {
        public frmBhaskara()
        {
            InitializeComponent();
            initFade();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            llamarCalcular();
        }

        private void llamarCalcular()
        {
            if (double.TryParse(txtA.Text, out double A) && double.TryParse(txtB.Text, out double B) && double.TryParse(txtC.Text, out double C))
            {
                if (A != 0)
                {
                    calcularCeros(A, B, C);
                }
                else
                {
                    MessageBox.Show("A debe ser distinto de cero (A != 0)", "A", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ingrese datos válidos (números enteros o decimales)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void calcularCeros(double A, double B, double C)
        {
            double B2, division, op2, result, x1, x2, v1, v2;

            try
            {
                B2 = Math.Pow(B, 2);
                division = 2 * A;
                op2 = 4 * A * C;
                result = B2 - op2;

                if (result < 0)
                {
                    txtX1.Text = "SIN RAÍZ";
                    txtX2.Text = "SIN RAÍZ";
                }
                else
                {
                    result = Math.Sqrt(result);
                    x1 = (-B + result) / division;
                    x2 = (-B - result) / division;
                    txtX1.Text = Convert.ToString(decimal.Round(decimal.Parse(x1.ToString()), 2));
                    txtX2.Text = Convert.ToString(decimal.Round(decimal.Parse(x2.ToString()), 2));
                }

                v1 = -(B / division);
                v2 = A * (Math.Pow(v1, 2)) + B * v1 + C;
                txtV1.Text = Convert.ToString(decimal.Round(decimal.Parse(v1.ToString()), 2));
                txtV2.Text = Convert.ToString(decimal.Round(decimal.Parse(v2.ToString()), 2));
            }
            catch(Exception ex)
            {
                clear();
                MessageBox.Show("Error: " + ex.Message + "", "Ingrese valores válidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            txtA.Text = string.Empty;
            txtB.Text = string.Empty;
            txtC.Text = string.Empty;
            txtV1.Text = string.Empty;
            txtV2.Text = string.Empty;
            txtX1.Text = string.Empty;
            txtX2.Text = string.Empty;
        }

        private void pbxSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void initFade()
        {
            fadeOutTimer = new Timer();
            fadeOutTimer.Interval = 30; // Tiempo en milisegundos (ajustable para una velocidad de difuminado)
            fadeOutTimer.Tick += FadeOutTimer_Tick;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!isClosing)
            {
                e.Cancel = true;
                fadeOutTimer.Start(); // Inicia el difuminado
            }
        }

        private void FadeOutTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.15; // Disminuir la opacidad gradualmente
            }
            else
            {
                fadeOutTimer.Stop(); // Detener el temporizador
                isClosing = true;
                this.Close(); // Cerrar el formulario una vez que la opacidad llega a cero
            }
        }

        private bool isClosing = false;
        private Timer fadeOutTimer;
    }
}
