using System;
using Tabuleiro;
using Xadrez;

namespace Xadrez___Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                TabuleiroClass tab = new(8, 8);

                tab.ColocarPeca(new Torre(tab, Cor.Preto), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(tab, Cor.Preto), new Posicao(1, 3));
                tab.ColocarPeca(new Rei(tab, Cor.Preto), new Posicao(2, 4));


                tab.ColocarPeca(new Torre(tab, Cor.Branco), new Posicao(5, 1));
                tab.ColocarPeca(new Torre(tab, Cor.Branco), new Posicao(6, 3));
                tab.ColocarPeca(new Rei(tab, Cor.Branco), new Posicao(7, 0));

                Tela.ImprimirTabuleiro(tab);

                //PosicaoXadrez px = new('a', 1);
                //Console.WriteLine(px);


            }
            catch (TabuleiroException te)
            {
                Console.WriteLine(te.Message);
            }
        }
    }
}