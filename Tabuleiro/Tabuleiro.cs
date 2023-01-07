using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabuleiro
{
    public class TabuleiroClass
    {

        public int Linhas { get; set; }
        public int Colunas { get; set; }

        private Peca[,] Peca;
        public TabuleiroClass(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Peca = new Peca[linhas, colunas];
        }

        public Peca RecuperaPeca(int linha, int coluna)
        {
            return Peca[linha, coluna];

        }


    }
}
