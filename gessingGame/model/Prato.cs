using System;

namespace gessingGame.model
{
    class Prato
    {
        public String descricao { get; set; }
        public String caracteristica { get; set; }

        public Prato(String descricao, String catacteristica)
        {
            this.descricao = descricao;
            this.caracteristica = catacteristica;
        }

        public Prato()
        {
        }
    }
}
