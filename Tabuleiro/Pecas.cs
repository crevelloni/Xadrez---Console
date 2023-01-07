using Xadrez___Console.Tabuleiro;

namespace Tabuleiro
{
    public class Peca
    {
        public Posicao? Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public TabuleiroClass Tabuleiro { get; protected   set; }

        public Peca(Posicao? posicao, Cor cor, TabuleiroClass tabuleiro)
        {
            Posicao = posicao;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QtdMovimentos = 0;

        }
    }
}
