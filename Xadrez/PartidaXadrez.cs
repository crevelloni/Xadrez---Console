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
            tab.ColocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('c', 2).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('d', 2).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('e', 2).ToPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Preto), new PosicaoXadrez('d', 1).ToPosicao());
            tab.ColocarPeca(new Rainha(tab, Cor.Preto), new PosicaoXadrez('a', 6).ToPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.Branco), new PosicaoXadrez('h', 5).ToPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.Branco), new PosicaoXadrez('g', 1).ToPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.Branco), new PosicaoXadrez('f', 4).ToPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Branco), new PosicaoXadrez('c', 7).ToPosicao());
            tab.ColocarPeca(new Rainha(tab, Cor.Branco), new PosicaoXadrez('e', 3).ToPosicao());


        }
    }
}
