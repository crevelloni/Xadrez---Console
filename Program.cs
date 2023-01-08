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
                PartidaXadrez partida = new();


                while (!partida.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);

                    Console.WriteLine("Orgigem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                    Console.WriteLine("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.ExecutaMovimento(origem, destino);
                }


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