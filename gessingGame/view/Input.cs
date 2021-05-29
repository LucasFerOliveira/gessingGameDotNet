using gessingGame.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gessingGame.view
{
    public partial class frmInput : Form
    {
        private Prato addPrato = new Prato();

        public List<Prato> pratos = new List<Prato>();
        public bool isPrato;
        public String pratoAnt;
        public String nome;
        public bool isMassa;
        public frmInput()
        {
            InitializeComponent();
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            if (isPrato)
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
            if (isPrato)
            {
                isPrato = false;
                frmInput newForm = new frmInput() { nome = txtInput.Text, isPrato = isPrato, pratoAnt = pratoAnt, isMassa = isMassa, pratos = pratos };
                newForm.ShowDialog();
            }
            else if (addPrato.nome != nome)
                InsereLista();
        }
        public List<Prato> InsereLista()
        {
            addPrato.nome = nome;
            addPrato.caracteristica = txtInput.Text;
            addPrato.isMassa = isMassa;
            pratos.Add(addPrato);
            return pratos;
        }
    }
}
