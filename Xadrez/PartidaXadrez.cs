using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace Xadrez
{
    public class PartidaXadrez
    {
        public TabuleiroClass tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaXadrez()
        {
            tab = new TabuleiroClass(8,8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            Terminada = false;
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQuantidade();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
        }
        private void ColocarPecas()
        {
            tab.ColocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('c', 1).ToPosicao());

        }
       
    }
}
