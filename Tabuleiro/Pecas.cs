namespace Tabuleiro
{
    public class Peca
    {
        public Posicao? Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public TabuleiroClass Tabuleiro { get; protected   set; }

        public Peca(TabuleiroClass tabuleiro, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QtdMovimentos = 0;

        }
    }
}
