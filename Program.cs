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
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.WriteLine();
                        Console.WriteLine("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.RecuperaPeca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.WriteLine("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                        partida.ValidarPosicaoDestino(origem, destino);
                        partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                        //178
                    }
                  
                }

            }
            catch (TabuleiroException te)
            {
                Console.WriteLine(te.Message);
                Console.ReadLine();
            }
        }
    }
}