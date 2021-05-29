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

            this.pratos.Add(new Prato("Lasanha", String.Empty, true));
            this.pratos.Add(new Prato("Bolo de Chocolate", String.Empty, false));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var isMassa = MessageBox.Show("O prato que você pensou é massa?", "Confirm", MessageBoxButtons.YesNo);

            if (isMassa == DialogResult.Yes)
            {
                validaResposta(true);
            }
            else
            {
                validaResposta(false);
            }

        }
        public void validaResposta(bool isMassa)
        {
            if (isMassa)
            {
                foreach (var lst in pratos.Where(p => p.isMassa))
                {
                    var tentativa = MessageBox.Show("O prato que você pensou é Lasanha?", "Desisto", MessageBoxButtons.YesNo);
                    if (tentativa == DialogResult.Yes)
                        MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
                    else
                        insereList(isMassa);
                }
            }
            else
            {
                foreach (var lst in pratos.Where(p => !p.isMassa))
                {

                    var tentativa = MessageBox.Show("O prato que você pensou é Lasanha?", "Desisto", MessageBoxButtons.YesNo);
                    if (tentativa == DialogResult.Yes)
                        MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
                    else
                        insereList(isMassa);
                }
            }
        }
        public void insereList(bool isMassa)
        {
            frmInput newForm = new frmInput() { isPrato = true, pratos = pratos, pratoAnt = pratos.Where(p => p.isMassa).Last().nome, isMassa = isMassa };
            newForm.ShowDialog();
        }

    }
}
