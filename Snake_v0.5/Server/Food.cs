using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Food
    {
        public int x;
        public int y;
        public int valor;

        private Random rnd = new Random();

        public Food()
        {
            this.x = 0;
            this.y = 0;
            this.valor = 3;
        }

        public int[,] NewPosition(int[,] matrizJogo)
        {
            matrizJogo[this.x, this.y] = 0;

            int[,] matrizTemp = matrizJogo;

            this.x = rnd.Next(0, 50);
            this.y = rnd.Next(0, 50);

            //Enquanto o sorteio não der em uma posicao vazia.
            while (matrizTemp[this.x, this.y] != 0)
            {
                this.x = rnd.Next(0, 50);
                this.y = rnd.Next(0, 50);
            }

            matrizTemp[this.x, this.y] = this.valor;

            return matrizTemp;
        }

        public void SendToClient(int[,] matrizJogo, ClientProcessor[] players)
        {
            StringBuilder builder = new StringBuilder("");

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    //Se achar a comida: Coletar a posicao X e Y para logo após sair do for.
                    if (matrizJogo[x, y] == 3)
                    {
                        builder.Append(x.ToString() + "," + y.ToString());
                        break;
                    }
                }
            }

            foreach (ClientProcessor processor in players)
            {
                processor.SendFood(builder);
                Console.WriteLine("Food position sent: " + builder);
            }
        }
    }
}
