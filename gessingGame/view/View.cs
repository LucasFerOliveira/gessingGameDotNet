using gessingGame.model;
using gessingGame.view;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace gessingGame
{
    public partial class frmView : Form
    {
        private List<Prato> pratos = new List<Prato>();
        public frmView()
        {
            InitializeComponent();

            this.pratos.Add(new Prato("Lasanha", String.Empty, true, 0));
            this.pratos.Add(new Prato("Bolo de Chocolate", String.Empty, false, 0));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var resposta = MessageBox.Show("O prato que você pensou é massa?", "Confirm", MessageBoxButtons.YesNo);
            var isMassa = resposta == DialogResult.Yes;
            var lst = pratos.Where(p => p.isMassa == isMassa && p.categoria != 0);
            if (lst.Count() == 0)
                pratoInicial(isMassa);
            else
                validaResposta(lst);
        }
        public void validaResposta(IEnumerable<Prato> lst)
        {
            foreach (var item in lst)
            {
                var resposta = MessageBox.Show("O prato que você pensou é " + item.descricao + "?", "Confirm", MessageBoxButtons.YesNo);
                if (resposta == DialogResult.Yes)
                {
                    var lstSub = lst.Where(p => p.categoria == item.categoria && p.uid == item.uid);
                    if (lstSub.Count() == 0)
                    {
                        var respostaPrato = MessageBox.Show("O prato que você pensou é " + item.nome + "?", "Confirm", MessageBoxButtons.YesNo);
                        if (respostaPrato == DialogResult.Yes)
                        {
                            MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
                            return;
                        }
                        else
                        {
                            insereList(item.isMassa);
                            return;
                        }
                    }
                    else
                    {
                        validaResposta(lstSub);
                    }
                }
            }

        }
        public void pratoInicial(bool isMassa)
        {
            var lstPratoInicial = pratos.Where(p => isMassa).Where(p => p.categoria == 0);
            var tentativa = MessageBox.Show("O prato que você pensou é " + lstPratoInicial.First().nome + "?", "Confirm", MessageBoxButtons.YesNo);
            if (tentativa == DialogResult.Yes)
            {
                MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
            }
            else
                insereList(isMassa);
        }

        public void insereList(bool isMassa)
        {
            int auxMassa = 0;
            int auxNMassa = 0;
            if (isMassa)
                auxMassa = ++auxMassa;
            else
                auxNMassa = --auxNMassa;
            frmInput newForm = new frmInput() { isPrato = true, pratos = pratos, pratoAnt = pratos.Where(p => p.isMassa).Reverse().Last().nome, isMassa = isMassa, auxMassa = auxMassa, auxNMassa = auxNMassa };
            newForm.ShowDialog();
        }

    }
}
