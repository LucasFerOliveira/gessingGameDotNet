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
                var lstMassa = pratos.Where(p => p.isMassa);
                if (lstMassa.Count() == 1)
                    pratoInicial(isMassa);
                else
                {
                    for (int i = 1; i < lstMassa.Count(); i++)
                    {
                        var tentativa = MessageBox.Show("O prato que você pensou é " + lstMassa.ElementAt(i).descricao + "?", "Confirm", MessageBoxButtons.YesNo);
                        if (tentativa == DialogResult.Yes)
                        {
                            foreach (var item in pratos.Where(p => p.isMassa && p.categoria == lstMassa.ElementAt(i).categoria))
                            {
                                if (pratos.Where(p => p.isMassa && p.categoria == lstMassa.ElementAt(i).categoria).Count() == 1)
                                {
                                    var tentativaPrato = MessageBox.Show("O prato que você pensou é " + lstMassa.ElementAt(i).nome + "?", "Confirm", MessageBoxButtons.YesNo);
                                    if (tentativaPrato == DialogResult.Yes)
                                    {
                                        MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
                                        break;
                                    }
                                }
                                else
                                {
                                    var tentativaSub = MessageBox.Show("O prato que você pensou é " + lstMassa.ElementAt(i).descricao + "?", "Confirm", MessageBoxButtons.YesNo);
                                    if (tentativaSub == DialogResult.Yes)
                                    {
                                        foreach (var itemPrato in pratos.Where(p => p.isMassa && p.categoria == lstMassa.ElementAt(i).categoria && p.descricao.Equals(lstMassa.ElementAt(i).descricao)))
                                        {
                                            var tentativaPrato = MessageBox.Show("O prato que você pensou é " + lstMassa.ElementAt(i).nome + "?", "Confirm", MessageBoxButtons.YesNo);
                                            if (tentativaSub == DialogResult.Yes)
                                            {
                                                MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
                                                break;
                                            }
                                            ++i;
                                        }
                                    }
                                    else
                                        ++i;
                                }

                            }

                        }
                        insereList(isMassa);
                    }
                }
            }
            else
            {


                //foreach (var lst in pratos.Where(p => !p.isMassa).Reverse())
                //{
                //    var tentativa = MessageBox.Show("O prato que você pensou é " + lst.nome + "?", "Confirm", MessageBoxButtons.YesNo);
                //    if (tentativa == DialogResult.Yes)
                //    {
                //        MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
                //        break;
                //    }
                //}
                //insereList(isMassa);
            }
        }
        public void pratoInicial(bool isMassa)
        {
            var lstPratoInicial = pratos.Where(p => isMassa && p.categoria == 0);
            var tentativa = MessageBox.Show("O prato que você pensou é " + lstPratoInicial.First().nome + "?", "Confirm", MessageBoxButtons.YesNo);
            if (tentativa == DialogResult.Yes)
            {
                MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
            } else
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
