using Xadrez___Console.Tabuleiro;

namespace Tabuleiro
{
    public class Pecas
    {
        public Posicao? Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; };

        public Pecas(Posicao? posicao, Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = posicao;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QtdMovimentos = 0;

        }
    }
}
