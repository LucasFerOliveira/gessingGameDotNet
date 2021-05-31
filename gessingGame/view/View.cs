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
            var teste = pratos;
            var lst = pratos.Where(p => p.isMassa == isMassa && p.categoria != 0);
            if (lst.Count() == 0)
                PratoInicial(isMassa, 1);
            else
                ValidaResposta(lst, lst);
        }
        public void ValidaResposta(IEnumerable<Prato> lst, IEnumerable<Prato> lstOriginal)
        {
            foreach (var item in lst)
            {
                var resposta = MessageBox.Show("O prato que você pensou é " + item.descricao + "?", "Confirm", MessageBoxButtons.YesNo);
                var lstSub = lst.Where(p => p.categoria == item.categoria && p.uid != item.uid);
                var maisCategoria = lstOriginal.Where(p => p.categoria == item.categoria).Count() > 1 ? true : false;
                if (resposta == DialogResult.Yes)
                {
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
                            InsereList(item.isMassa, item.categoria, item.nome);
                            return;
                        }
                    }
                    else if (lstSub.Count() == 1)
                    {
                        ValidaResposta(lstSub, lstOriginal);
                        return;
                    }
                    else
                    {
                        ValidaResposta(lstSub, lstOriginal);
                        return;
                    }
                }
                else
                {
                    if (lstOriginal.ElementAt(0).uid == item.uid)
                    {
                        var lstLimpa = lstOriginal.Where(p => p.categoria != item.categoria);
                        if (lstLimpa.Count() == 0)
                        {
                            PratoInicial(item.isMassa, item.categoria + 1);
                            return;
                        }
                        else
                        {
                            ValidaResposta(lstLimpa, lstOriginal);
                            return;
                        }
                    }
                    var teste = lstOriginal.Max(p => p.categoria);
                    if (lstSub.Count() == 0 && item.categoria == teste)
                    {
                        PratoInicial(item.isMassa, item.categoria + 1);
                        return;
                    }
                    else if (lstSub.Count() == 0 && !maisCategoria)
                    {
                        ValidaResposta(lstSub, lstOriginal);
                        return;
                    }
                    if (lstSub.Count() > 0)
                    {
                        ValidaResposta(lstSub, lstOriginal);
                        return;
                    }
                    else
                    {
                        var nomePai = lstOriginal.First(p => p.categoria == item.categoria).nome;
                        var respostaPrato = MessageBox.Show("O prato que você pensou é " + nomePai + "?", "Confirm", MessageBoxButtons.YesNo);
                        if (respostaPrato == DialogResult.Yes)
                        {
                            MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
                            return;
                        }
                        else
                        {
                            InsereList(item.isMassa, item.categoria, nomePai);
                            return;
                        }
                    }

                }
            }

        }
        public void PratoInicial(bool isMassa, int categoria)
        {
            var lstPratoInicial = pratos.Where(p => p.isMassa == isMassa && p.categoria == 0);
            var tentativa = MessageBox.Show("O prato que você pensou é " + lstPratoInicial.First().nome + "?", "Confirm", MessageBoxButtons.YesNo);
            if (tentativa == DialogResult.Yes)
            {
                MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
            }
            else
                InsereList(isMassa, categoria, lstPratoInicial.ElementAt(0).nome);
        }

        public void InsereList(bool isMassa, int categoria, String pratAnt)
        {
            frmInput newForm = new frmInput() { isPrato = true, pratos = pratos, pratoAnt = pratAnt, isMassa = isMassa, categoria = categoria };
            newForm.ShowDialog();
        }

    }
}
