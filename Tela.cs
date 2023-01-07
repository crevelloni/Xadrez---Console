using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace Xadrez___Console
{
    public class Tela
    {

        public static void ImprimirTabuleiro(TabuleiroClass tab)
        {

            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.RecuperaPeca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.RecuperaPeca(i,j) + " ");
                    }

                }
                Console.WriteLine();

            }

        }

    }
}
