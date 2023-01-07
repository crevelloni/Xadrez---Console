using System;
using Tabuleiro;

namespace Xadrez___Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TabuleiroClass tab = new(8,8);

            Tela.ImprimirTabuleiro(tab);

            Console.WriteLine();
            



        }
    }
}