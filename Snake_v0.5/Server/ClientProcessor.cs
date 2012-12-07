using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;

namespace Server
{
    class ClientProcessor
    {
        GameState gameState;
        TcpClient tcpClient;
        private int _id;
        StreamReader reader = null;
        StreamWriter writer = null;

        public int id { get { return _id; } }

        public ClientProcessor(GameState gameState, int id, TcpClient client)
        {
            this.gameState = gameState;
            this._id = id;
            this.tcpClient = client;

            //Pegar o canal de cominicação
            NetworkStream stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        public void Run()
        {
            String data = null;
            data = reader.ReadLine();

            //Enquanto o cliente existir...
            while (data != null)
            {
                Console.WriteLine(_id + ": Recebido = " + data);
                gameState.AddMoveFromPlayer(_id, data);
                data = reader.ReadLine();
            }

            // Shutdown and end connection
            tcpClient.Close();
        }

        /// <summary>
        /// Método que enviará os comandos a todos os jogadores.
        /// </summary>
        /// <param name="text">O texto que será enviado</param>
        private void send(String text)
        {
            writer.Write(text);
            writer.WriteLine();
            writer.Flush();
        }

        /// <summary>
        /// Pode comecar a jogar.
        /// </summary>
        public void SendPlay()
        {
            send("True");
        }

        /// <summary>
        /// Envia a posicao que ficará a cobra.
        /// </summary>
        /// <param name="builder">Todas as posicoes ocupadas pela cobra.</param>
        /// <param name="textMatriz">O valor que tem em cada posicao.</param>
        public void SendMatriz(StringBuilder builder, int textMatriz)
        {
            //A posicao que ficará o retângulo e seu respectivo player
            send("Matriz:" + builder + "." + textMatriz.ToString());
        }

        public void SendFood(StringBuilder builder)
        {
            send("Food:" + builder);
        }

        public void SendWall(StringBuilder builder)
        {
            send("Wall:" + builder);
        }

        public void SendScore(string score)
        {
            send("Placar:" + score);
        }

        public void SendDeath()
        {
            send("Death");
        }

        public void SendLife()
        {
            send("Life");
        }
    }
}
