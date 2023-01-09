﻿using System;
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

        public PartidaXadrez()
        {
            tab = new TabuleiroClass(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            Terminada = false;
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
            turno++;
            MudaJogador();
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
            if (!tab.RecuperaPeca(origem).PodeMoverPara(destino))
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
            if(R == null)
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
            ColocarNovaPeca('c', 8, new Torre(tab, Cor.Preto));
            ColocarNovaPeca('d', 7, new Torre(tab, Cor.Preto));
            ColocarNovaPeca('e', 8, new Torre(tab, Cor.Preto));
            ColocarNovaPeca('c', 7, new Torre(tab, Cor.Preto));
            ColocarNovaPeca('d', 8, new Rei(tab, Cor.Preto));
            ColocarNovaPeca('e', 7, new Torre(tab, Cor.Preto));

            ColocarNovaPeca('c', 2, new Torre(tab, Cor.Branco));
            ColocarNovaPeca('d', 2, new Torre(tab, Cor.Branco));
            ColocarNovaPeca('e', 2, new Torre(tab, Cor.Branco));
            ColocarNovaPeca('c', 1, new Torre(tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branco));
            ColocarNovaPeca('e', 1, new Torre(tab, Cor.Branco));

        }
    }
}
