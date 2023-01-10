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
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Hpecas;
        private HashSet<Peca> Hcapturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant;
        public PartidaXadrez()
        {
            tab = new TabuleiroClass(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            Terminada = false;
            VulneravelEnPassant = null;
            Hpecas = new HashSet<Peca>();
            Hcapturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQuantidade();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Hcapturadas.Add(pecaCapturada);
            }

            // #JogadasEspeciais roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarQuantidade();
                tab.ColocarPeca(T, destinoT);

            }
            // #JogadasEspeciais roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarQuantidade();
                tab.ColocarPeca(T, destinoT);

            }
            // #JogadasEspeciais en Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branco)
                    {
                        posP = new(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.RetirarPeca(posP);
                    Hcapturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (ReiEmXeque(jogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colcoar em xeque!");
            }
            if (ReiEmXeque(Adversaria(jogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (XequeMate(Adversaria(jogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                turno++;
                MudaJogador();
            }

            Peca p = tab.RecuperaPeca(destino);

            //#JogadasEspeciais en Passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }
        }
        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecrementarQuantidade();
            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                Hcapturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);

            // #JogadasEspeciais desfaz roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.IncrementarQuantidade();
                tab.ColocarPeca(T, origemT);

            }
            // #JogadasEspeciais desfaz roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.IncrementarQuantidade();
                tab.ColocarPeca(T, origemT);

            }
            // #JogadasEspeciais en Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = tab.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branco)
                    {
                        posP = new(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new(4, destino.Coluna);
                    }
                    tab.ColocarPeca(peao,posP);
                }
            }
        }
        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (tab.RecuperaPeca(pos) == null)
            {
                throw new TabuleiroException("Não exite peça na posição escolhida");
            }
            if (jogadorAtual != tab.RecuperaPeca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.RecuperaPeca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida");
            }
        }
        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.RecuperaPeca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        private void MudaJogador()
        {
            if (jogadorAtual == Cor.Branco)
            {
                jogadorAtual = Cor.Preto;
            }
            else
            {
                jogadorAtual = Cor.Branco;
            }
        }
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Hpecas.Add(peca);
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Hcapturadas)
            {
                if (x.Cor == cor)
                    aux.Add(x);
            }
            return aux;
        }
        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool ReiEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool XequeMate(Cor cor)
        {
            if (!ReiEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < tab.Linhas; i++)
                {
                    for (int j = 0; j < tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = ReiEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }

                }

            }
            return true;

        }
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Hpecas)
            {
                if (x.Cor == cor)
                    aux.Add(x);
            }
            aux.ExceptWith(PecasCapturadas(cor));

            return aux;
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 8, new Torre(tab, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(tab, Cor.Preto));
            ColocarNovaPeca('d', 8, new Rainha(tab, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(tab, Cor.Preto, this));
            ColocarNovaPeca('f', 8, new Bispo(tab, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(tab, Cor.Preto));
            ColocarNovaPeca('a', 7, new Peao(tab, Cor.Preto, this));
            ColocarNovaPeca('b', 7, new Peao(tab, Cor.Preto, this));
            ColocarNovaPeca('c', 7, new Peao(tab, Cor.Preto, this));
            ColocarNovaPeca('d', 7, new Peao(tab, Cor.Preto, this));
            ColocarNovaPeca('e', 7, new Peao(tab, Cor.Preto, this));
            ColocarNovaPeca('f', 7, new Peao(tab, Cor.Preto, this));
            ColocarNovaPeca('g', 7, new Peao(tab, Cor.Preto, this));
            ColocarNovaPeca('h', 7, new Peao(tab, Cor.Preto, this));


            ColocarNovaPeca('a', 1, new Torre(tab, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Rainha(tab, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(tab, Cor.Branco, this));
            ColocarNovaPeca('f', 1, new Bispo(tab, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(tab, Cor.Branco));
            ColocarNovaPeca('a', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('b', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('c', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('d', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('e', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('f', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('g', 2, new Peao(tab, Cor.Branco, this));
            ColocarNovaPeca('h', 2, new Peao(tab, Cor.Branco, this));


        }
    }
}
