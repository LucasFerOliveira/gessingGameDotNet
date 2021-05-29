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

namespace gessingGame
{
    public partial class View : Form
    {
        private Prato massa = new Prato("Lasanha", "");
        private Prato naoMassa = new Prato("Bolo de Chocolate", "");

        private ListPratos pratosMassa = new ListPratos();
        private ListPratos pratosNaoMassa = new ListPratos();

        private static int resposta;

        public View()
        {
            InitializeComponent();

            this.pratosMassa.getPratos().Add(massa);
            this.pratosNaoMassa.getPratos().Add(naoMassa);
        }

        private void InitializeComponent()
        {

        }
    }
}
