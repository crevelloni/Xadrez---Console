using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace Xadrez
{
    public class Rei : Peca
    {
        public Rei(TabuleiroClass tab, Cor cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "R"; 
        }

    }
}
