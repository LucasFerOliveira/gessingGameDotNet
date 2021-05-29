using System;

namespace gessingGame.model
{
    public class Prato
    {
        public String nome { get; set; }
        public String caracteristica { get; set; }
        public bool isMassa { get; set; }

        public Prato(String nome, String catacteristica, bool isMassa)
        {
            this.nome = nome;
            this.caracteristica = catacteristica;
            this.isMassa = isMassa;
        }

        public Prato()
        {
        }
    }
}
