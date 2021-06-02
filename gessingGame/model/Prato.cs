using System;

namespace gessingGame.model
{
    public class Prato
    {
        public String nome { get; set; }
        public String descricao { get; set; }
        public bool éMassa { get; set; }
        public int nivel { get; set; }
        public Guid uid { get; set; }
        public Guid vinculo { get; set; }
    }
}
