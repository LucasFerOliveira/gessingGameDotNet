using System;

namespace gessingGame.model
{
    public class Prato
    {
        public String nome { get; set; }
        public String descricao { get; set; }
        public bool isMassa { get; set; }
        public int categoria { get; set; }
        public Guid uid { get; set; }
        public Guid fk_uid { get; set; }

        public Prato(String nome, String descricao, bool isMassa, int categoria, Guid fk_uid)
        {
            this.uid = Guid.NewGuid();
            this.nome = nome;
            this.descricao = descricao;
            this.isMassa = isMassa;
            this.categoria = categoria;
            this.fk_uid = fk_uid;
        }

        public Prato()
        {
        }
    }
}
