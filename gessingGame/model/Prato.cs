using System;

namespace gessingGame.model
{
    public class Prato
    {
        public String descricao { get; set; }
        public String caracteristica { get; set; }
        public bool isMassa { get; set; }

        public Prato(String descricao, String catacteristica, bool isMassa)
        {
            this.descricao = descricao;
            this.caracteristica = catacteristica;
            this.isMassa = isMassa;
        }

        public Prato()
        {
        }
    }
}
