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

            this.pratos.Add(new Prato() { nome = "Lasanha", éMassa = true, nivel = 0, vinculo = Guid.NewGuid() });
            this.pratos.Add(new Prato() { nome = "Bolo de Chocolate", éMassa = false, nivel = 0, vinculo = Guid.NewGuid() });
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            var resposta = MessageBox.Show("O prato que você pensou é massa?", "Confirm", MessageBoxButtons.YesNo);
            var éMassa = resposta == DialogResult.Yes;
            var lst = pratos.Where(p => p.éMassa == éMassa && p.nivel != 0);
            if (lst.Count() == 0)
                PratoInicial(éMassa, 1);
            else
                ValidaResposta(lst, lst, 0);
        }
        public void ValidaResposta(IEnumerable<Prato> lst, IEnumerable<Prato> lstOriginal, int qtdSim)
        {
            foreach (var item in lst)
            {
                var resposta = MessageBox.Show("O prato que você pensou é " + item.descricao + "?", "Confirm", MessageBoxButtons.YesNo);
                var lstSub = lst.Where(p => p.nivel == item.nivel && p.uid != item.uid);
                var countCategoria = lstOriginal.Where(p => p.nivel == item.nivel).Count();
                var catMax = lstOriginal.Max(p => p.nivel);
                var nivelVinculo = lstOriginal.First(p => p.uid == item.vinculo).nome;
                var nomeUltimo = lstOriginal.Last(p => p.nivel == item.nivel).nome;
                var lstMaisCat = lstOriginal.Where(p => p.vinculo == item.uid && p.uid != p.vinculo);
                qtdSim = resposta == DialogResult.Yes ? qtdSim + 1 : qtdSim;
                if (resposta == DialogResult.Yes || qtdSim > 0)
                {
                    // Valida se a lista possui sub categorias e passa a subcategoria filtrada
                    if (lstMaisCat.Count() > 0 && resposta == DialogResult.Yes)
                    {
                        ValidaResposta(lstMaisCat, lstOriginal, qtdSim);
                        return;
                    }
                    // Valida se falei sim se não tem mais itens na categoria e valida o prato pelo nome
                    if ((qtdSim > 1 && lstSub.Count() > 0) || (qtdSim == 1 && countCategoria == 1) || (lstSub.Count() == 0 && countCategoria > 1 && resposta == DialogResult.Yes))
                    {
                        ValidaPratoNome(item);
                        return;
                    }
                    else
                    {
                        // Valida se estou na última categoria para validar o prato pai
                        if (lstSub.Count() == 0)
                        {
                            var respostaPrato = MessageBox.Show("O prato que você pensou é " + nivelVinculo + "?", "Confirm", MessageBoxButtons.YesNo);
                            if (respostaPrato == DialogResult.Yes)
                            {
                                MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
                                return;
                            }
                            else
                            {
                                InsereList(item.éMassa, item.nivel, lstOriginal.First(p => p.uid == item.vinculo).nome, lstOriginal.First(p => p.uid == item.vinculo).uid);
                                return;
                            }
                        }
                        // Se não for o último da categoria ele passa a lista para retirar esse item da categoria
                        else
                        {
                            ValidaResposta(lstSub, lstOriginal, qtdSim);
                            return;
                        }
                    }
                }
                else
                {
                    // Verifica se tem mais itens na categoria, se não entrei em nenhuma categoria pai, para passar a lista só com as categorias que não foram validadas
                    if (item.nivel != catMax && qtdSim == 0)
                    {
                        var lstLimpa = lstOriginal.Where(p => p.nivel > item.nivel);
                        ValidaResposta(lstLimpa, lstOriginal, qtdSim);
                        return;
                    }
                    else
                    {
                        // No final valida o prato inicial se não entrei em nenhuma categoria
                        PratoInicial(item.éMassa, item.nivel + 1);
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
                InsereList(prato.éMassa, prato.nivel, prato.nome, prato.uid);
                return;
            }
        }
        public void PratoInicial(bool éMassa, int nivel)
        {
            var lstPratoInicial = pratos.Where(p => p.éMassa == éMassa && p.nivel == 0);
            var tentativa = MessageBox.Show("O prato que você pensou é " + lstPratoInicial.First().nome + "?", "Confirm", MessageBoxButtons.YesNo);
            if (tentativa == DialogResult.Yes)
            {
                MessageBox.Show("Acertei de novo!", "Jogo Gourmet", MessageBoxButtons.OK);
            }
            else
                InsereList(éMassa, nivel, lstPratoInicial.ElementAt(0).nome, Guid.Empty);
        }

        public void InsereList(bool éMassa, int nivel, String pratAnt, Guid vinculo)
        {
            frmInput newForm = new frmInput() { éPrato = true, pratos = pratos, pratoAnt = pratAnt, éMassa = éMassa, level = nivel, vinculo = vinculo };
            newForm.ShowDialog();
        }

    }
}
