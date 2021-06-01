﻿using gessingGame.model;
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
                PratoInicial(isMassa, 1);
            else
                ValidaResposta(lst, lst, 0);
        }
        public void ValidaResposta(IEnumerable<Prato> lst, IEnumerable<Prato> lstOriginal, int categoria)
        {
            foreach (var item in lst)
            {
                var resposta = MessageBox.Show("O prato que você pensou é " + item.descricao + "?", "Confirm", MessageBoxButtons.YesNo);
                var lstSub = lst.Where(p => p.categoria == item.categoria && p.uid != item.uid);
                var countCategoria = lstOriginal.Where(p => p.categoria == item.categoria).Count();
                var catMax = lstOriginal.Max(p => p.categoria);
                var nomePai = lstOriginal.First(p => p.categoria == item.categoria).nome;
                var nomeUltimo = lstOriginal.Last(p => p.categoria == item.categoria).nome;
                var catCorrente = categoria;
                if (resposta == DialogResult.Yes || catCorrente > 0)
                {
                    catCorrente = resposta == DialogResult.Yes ? catCorrente+1 : catCorrente;
                    if ((catCorrente > 1 && lstSub.Count() > 0) || (catCorrente == 1 && countCategoria == 1))
                    {
                        ValidaPratoNome(item);
                        return;
                    }
                    else
                    {
                        if (lstSub.Count() == 0)
                        {
                            var respostaPrato = MessageBox.Show("O prato que você pensou é " + nomePai + "?", "Confirm", MessageBoxButtons.YesNo);
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
                        else
                        {
                            ValidaResposta(lstSub, lstOriginal, catCorrente);
                            return;
                        }
                    }
                }
                else
                {
                    if (item.categoria != catMax && catCorrente == 0)
                    {
                        var lstLimpa = lstOriginal.Where(p => p.categoria > item.categoria);
                        ValidaResposta(lstLimpa, lstOriginal, catCorrente);
                        return;
                    }
                    else
                    {
                        PratoInicial(item.isMassa, item.categoria + 1);
                        return;
                    }

                }

            }
        }
        public void ValidaPratoNome(Prato prato)
        {
            var respostaPrato = MessageBox.Show("O prato que você pensou é " + prato.nome + "?", "Confirm", MessageBoxButtons.YesNo);
            if (respostaPrato == DialogResult.Yes)
            {
                MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
                return;
            }
            else
            {
                InsereList(prato.isMassa, prato.categoria, prato.nome);
                return;
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
