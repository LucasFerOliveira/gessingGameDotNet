using System.Collections.Generic;

namespace gessingGame.model
{
    class ListPratos
    {
        private List<Prato> pratos = new List<Prato>();

        public List<Prato> getPratos()
        {
            return pratos;
        }

        public void setPratos(List<Prato> pratos)
        {
            this.pratos = pratos;
        }

    }
}
