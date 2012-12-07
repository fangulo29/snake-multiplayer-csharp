using System;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Server
{
    class GameState
    {
        private const int MAX_PLAYERS = 2;
        private int lastID = 0;
        private int moveX = 0;
        private int moveY = 0;
        public int[,] matrizJogo;

        private bool ready = false;
        private bool goToPhaseTwo = false;
        private bool wentToPhase2 = false;

        private ClientProcessor[] players;
        private List<SnakePiece>[] snakesList = new List<SnakePiece>[2];
        private Food food;
        private Wall wall;
        private Placar placar;

        public bool IsReady()
        {
            return ready;
        }

        public GameState()
        {
            players = new ClientProcessor[MAX_PLAYERS];
            matrizJogo = new int[50, 50];
            food = new Food();
            wall = new Wall();
            placar = new Placar();

            //Criacao dos Jogadores.
            snakesList[0] = new List<SnakePiece>();
            snakesList[1] = new List<SnakePiece>();

            CreateSnake();
        }

        void CreateSnake()
        {
            //Inicializar a matriz.
            for (int x = 0; x < 50; x++)
                for (int y = 0; y < 50; y++)
                    matrizJogo[x, y] = 0;

            for (int i = 4; i > 1; i--)
            {
                //Cria a peça do Snake. Adiciona na lista e logo após na matriz.
                SnakePiece temp = new SnakePiece(i, 10, 1);
                snakesList[0].Add(temp);
                matrizJogo[i, 10] = 1;
            }

            for (int i = 44; i < 47; i++)
            {
                SnakePiece temp = new SnakePiece(i, 10, 2);
                snakesList[1].Add(temp);
                matrizJogo[i, 10] = 2;
            }
        }

        public void AddPlayer(TcpClient client)
        {
            //Caso já se tenha 02 jogadores, não pode mais entrar no servidor.
            if (ready)
                DisconnectPlayer(client);

            //Identificador do cliente
            int id = NextID();
            Console.WriteLine("Novo cliente entrou: " + id);

            //Cria Thread para cuidar do cliente.
            players[id] = new ClientProcessor(this, id, client);
            Thread thread = new Thread(players[id].Run);
            thread.Start();

            if (id + 1 >= MAX_PLAYERS)
            {
                ready = true;
                //Cria o contorno da parede e envia aos clientes.
                matrizJogo =  wall.CreateWallPhase01(matrizJogo);
                wall.SendToCLient(players, matrizJogo);
                
                // Cria a 1ª posicao da comida e envia ao cliente.
                matrizJogo = food.NewPosition(matrizJogo);
                food.SendToClient(matrizJogo, players);
            }
        }

        private void DisconnectPlayer(TcpClient client)
        {
            //Pega o canal de comunicação do cliente e servidor e escreve que o server está cheio.
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write("Server Cheio...");
            writer.WriteLine();
            writer.Flush();

            client.Close();
        }

        public int NextID()
        {
            return lastID++;
        }

        bool waiting = false;

        public void Play()
        {
            if (!waiting)
            {
                ReadyToPlay();
                waiting = true;
            }
        }

        private void ReadyToPlay()
        {
            foreach (ClientProcessor processor in players)
            {
                Console.WriteLine("Notify players: Play");
                processor.SendPlay();
            }
        }

        int posX = 0;
        int posY = 0;
        SnakePiece[] snakeArray;
        SnakePiece[] snakeArrayFinal;

        internal void AddMoveFromPlayer(int _id, string data)
        {
            Console.WriteLine("addmove from " + _id + " data=" + data);

            StringBuilder builder = new StringBuilder("");
            snakeArray = snakesList[_id].ToArray();

            switch (data)
            {
                case "Down": moveX = 0; moveY = 1; break;

                case "Up": moveX = 0; moveY = -1; break;

                case "Left": moveX = -1; moveY = 0; break;

                case "Right": moveX = 1; moveY = 0; break;
            }

            //Coleta a posicao da 1ª parte da cobra, soma a mesma com o movimento realizado pelo jogador para ser criado entao uma nova cabeça.
            posX = snakeArray[0].x + moveX;
            posY = snakeArray[0].y + moveY;

            if (matrizJogo[posX, posY] == 1 || matrizJogo[posX, posY] == 2 || matrizJogo[posX, posY] == 4)
            {
                wall.Death(players[_id]);
                placar.Morreu();
            }

            else if (matrizJogo[posX, posY] != 3)
            {
                // A posicao do ultimo pedaco da cobra será zerada e deletada da lista.
                matrizJogo[snakeArray[snakeArray.Length - 1].x, snakeArray[snakeArray.Length - 1].y] = 0;
                snakesList[_id].RemoveAt(snakeArray.Length - 1);
            }

            else
            {
                //Caso tenha colidido com a comida, trocar a posicao do alimento e nao deletar a ultima posicao da lista de pedacos de cobra.
                matrizJogo = food.NewPosition(matrizJogo);
                food.SendToClient(matrizJogo, players);
                
                //Enviar placar ao cliente e averiguar se os 02 podem passar de fase.
                placar.SendToClient(_id, players);

                if (!wentToPhase2)
                    goToPhaseTwo =  placar.VerificarPontos();
            }

            //Insere o novo pedaço da cobra na matriz e na lista (1ª posicao).
            SnakePiece snakeTemp = new SnakePiece(posX, posY, (_id + 1));
            snakesList[_id].Insert(0, snakeTemp);
            matrizJogo[posX, posY] = (_id + 1);

            snakeArrayFinal = snakesList[_id].ToArray();

            for (int i = 0; i < snakeArrayFinal.Length; i++)
            {
                builder.Append(snakeArrayFinal[i].x.ToString() + "," + snakeArrayFinal[i].y.ToString() + ";");
            }

            // notifica a todos o estado atual do jogo.
            foreach (ClientProcessor processor in players)
            {
                processor.SendMatriz(builder, (_id + 1));
                Console.WriteLine(builder);
            }

            if (goToPhaseTwo)
            {
                //Servidor escreve ao cliente que todas as snakes estão vivas.
                wall.Life(players);

                snakesList[0].Clear();
                snakesList[1].Clear();

                CreateSnake();

                matrizJogo = food.NewPosition(matrizJogo);
                food.SendToClient(matrizJogo, players);

                matrizJogo = wall.CreateWallPhase01(matrizJogo);
                matrizJogo = wall.CreateWallPhase02(matrizJogo);

                wall.SendToCLient(players, matrizJogo);

                goToPhaseTwo = false;
                wentToPhase2 = true;
            }
        }
    }
}
