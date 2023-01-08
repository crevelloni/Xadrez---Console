using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace Xadrez
{
    public class Bispo : Peca
    {
        public Bispo(TabuleiroClass tab, Cor cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "B";
        }
        public override bool[,] MovimentosPossiveis()
        {
            throw new NotImplementedException();
        }
    }
}
