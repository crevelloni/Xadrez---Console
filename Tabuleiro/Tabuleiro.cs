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

        public void ColocarPeca(Peca p, Posicao pos)
        {
            this.Peca[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;


        }
    }
}
