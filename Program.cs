﻿using System;
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
                tab.ColocarPeca(new Rei(tab, Cor.Preto), new Posicao(0, 0));

                Tela.ImprimirTabuleiro(tab);

            }
            catch (TabuleiroException te)
            {
                Console.WriteLine(te.Message);
            }
        }
    }
}