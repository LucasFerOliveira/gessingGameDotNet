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
        public String caracteristica;
        public bool isMassa;
        public frmInput()
        {
            InitializeComponent();

        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            if (isPrato)
            {
                lblText.Text = "Qual prato você pensou?";

            }
            else
            {
                lblText.Text = addPrato.caracteristica + "é _______ mas " + pratoAnt;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Retorno();
            this.Close();
        }

        public List<Prato> Retorno()
        {
            Prato newPrato = new Prato();
            if (isPrato)
            {
                isPrato = false;
                frmInput newForm = new frmInput() { caracteristica = txtInput.Text, isPrato = isPrato };
                newForm.ShowDialog();
            }
            newPrato.caracteristica = caracteristica;
            newPrato.isMassa = isMassa;
            newPrato.descricao = txtInput.Text;
            pratos.Add(newPrato);
            return pratos;

        }
    }
}
