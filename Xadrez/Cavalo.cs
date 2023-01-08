using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace Xadrez
{
    public class Cavalo : Peca
    {
        public Cavalo(TabuleiroClass tab, Cor cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "C";
        }
        public override bool[,] MovimentosPossiveis()
        {
            throw new NotImplementedException();
        }
    }
}
