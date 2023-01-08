using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tabuleiro
{
    public class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Posição: ");
            sb.Append(Linha);
            sb.Append(", ");
            sb.Append(Coluna);

            return sb.ToString();
        }

        public void DefinirValores(int linha, int coluna)
        {
            Linha = (int)linha;
            Coluna = (int)coluna;
        }

    }

}
