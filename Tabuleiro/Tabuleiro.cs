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
        public Peca RecuperaPeca(Posicao pos)
        {
            return Peca[pos.Linha, pos.Coluna];
        }
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return RecuperaPeca(pos) != null;
        }
        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça na posição!");
            }
            this.Peca[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }
        public Peca RetirarPeca(Posicao pos)
        {
            if (RecuperaPeca(pos) == null)
            {
                return null;
            }
            Peca aux = RecuperaPeca(pos);
            aux.Posicao = null;
            Peca[pos.Linha, pos.Coluna] = null;
            return aux;
        }
        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
                return false;
            return true;
        }
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
                throw new TabuleiroException("Posição Inválida");
        }

    }
}
