using System.Text;

namespace Server
{
    class Wall
    {
        public int x;
        public int y;
        public int valor;

        public Wall()
        {
            this.x = 0;
            this.y = 0;
            this.valor = 4;
        }

        public void SendToCLient(ClientProcessor[] players, int[,] matrizJogo)
        {
            StringBuilder builder = new StringBuilder("");

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    //Coletar todas a posicao de cada parede.
                    if (matrizJogo[x, y] == 4)
                    {
                        builder.Append(x.ToString() + "," + y.ToString() + ";");
                    }
                }
            }

            foreach (ClientProcessor processor in players)
            {
                processor.SendWall(builder);
            }
        }

        public int[,] CreateWallPhase01(int[,] matrizJogo)
        {
            int[,] matrizTemp = matrizJogo;

            for (int i = 0; i < 50; i++)
            {
                matrizTemp[i, 0] = this.valor;
                matrizTemp[0, i] = this.valor;
                matrizTemp[49, i] = this.valor;
                matrizTemp[i, 49] = this.valor;
            }

            return matrizTemp;
        }

        public int[,] CreateWallPhase02(int[,] matrizJogo)
        {
            int[,] matrizTemp = matrizJogo;

            //Bloco central
            for (int x = 22; x < 28; x++)
                for (int y = 22; y < 28; y++)
                    matrizTemp[x, y] = this.valor;

            matrizTemp = LFormat(matrizTemp);

            return matrizTemp;
        }

        public void Death(ClientProcessor player)
        {
            ClientProcessor processor = player;

            processor.SendDeath();
        }

        public void Life(ClientProcessor[] players)
        {
            foreach (ClientProcessor processor in players)
            {
                processor.SendLife();
            }
        }

        private int[,] LFormat(int[,] matrizJogo)
        {
            int[,] matrizTemp = matrizJogo;

            for (int x = 3; x < 8; x++)
                matrizTemp[x, 3] = this.valor;

            for (int y = 3; y < 10; y++)
                matrizTemp[3, y] = this.valor;

            for (int x = 3; x < 8; x++)
                matrizTemp[x, 46] = this.valor;

            for (int y = 46; y > 39; y--)
                matrizTemp[3, y] = this.valor;


            for (int x = 46; x > 39; x--)
                matrizTemp[x, 46] = this.valor;

            for (int y = 46; y > 39; y--)
                matrizTemp[46, y] = this.valor;

            for (int x = 46; x > 39; x--)
                matrizTemp[x, 3] = this.valor;

            for (int y = 3; y < 10; y++)
                matrizTemp[46, y] = this.valor;

            return matrizTemp;
        }
    }
}
