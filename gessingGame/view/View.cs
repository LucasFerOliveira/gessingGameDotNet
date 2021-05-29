using gessingGame.model;
using gessingGame.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gessingGame
{
    public partial class frmView : Form
    {
        private List<Prato> pratos = new List<Prato>();

        public frmView()
        {
            InitializeComponent();

            this.pratos.Add(new Prato("Lasanha", String.Empty, false));
            this.pratos.Add(new Prato("Bolo de Chocolate", String.Empty, false));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("O prato que você pensou é massa?", "", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                var isMassa = MessageBox.Show("O prato que você pensou é Lasanha?", "", MessageBoxButtons.YesNo);
                if (isMassa == DialogResult.Yes)
                    MessageBox.Show("Acertei de novo!", "", MessageBoxButtons.OK);
                else
                {
                    frmInput newForm = new frmInput(){ isPrato = true, pratos = pratos, pratoAnt =  pratos.FirstOrDefault().caracteristica, isMassa = true};
                    newForm.ShowDialog();
                }
            }
            else
            {
                var noMassa = MessageBox.Show("O prato que você pensou é Bolo de Chocolate?", "", MessageBoxButtons.YesNo);
                frmInput newForm = new frmInput() { isPrato = true, pratos = pratos, pratoAnt = pratos.FirstOrDefault().caracteristica, isMassa = false };
                newForm.ShowDialog();
            }

        }
    }
}
