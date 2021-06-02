using gessingGame.model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace gessingGame.view
{
    public partial class frmInput : Form
    {
        private Prato addPrato = new Prato();

        public List<Prato> pratos = new List<Prato>();
        public bool éPrato;
        public String pratoAnt;
        public String nome;
        public bool éMassa;
        public int level;
        public Guid vinculo;
        public frmInput()
        {
            InitializeComponent();
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            if (éPrato)
                lblText.Text = "Qual prato você pensou?";
            else
                lblText.Text = "" + nome + " é _______ mas " + pratoAnt;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            InsereNome();
            this.Close();
        }

        public void InsereNome()
        {
            if (éPrato)
            {
                éPrato = false;
                frmInput newForm = new frmInput() { nome = txtInput.Text, éPrato = éPrato, pratoAnt = pratoAnt, éMassa = éMassa, pratos = pratos, level = level, vinculo = vinculo };
                newForm.ShowDialog();
            }
            else if (addPrato.nome != nome)
                InsereLista();
        }
        public List<Prato> InsereLista()
        {
            addPrato.nome = nome;
            addPrato.descricao = txtInput.Text;
            addPrato.éMassa = éMassa;
            addPrato.nivel = level;
            addPrato.uid = Guid.NewGuid();
            addPrato.vinculo = vinculo == Guid.Empty ? addPrato.uid : vinculo;
            pratos.Add(addPrato);
            return pratos;
        }
    }
}
