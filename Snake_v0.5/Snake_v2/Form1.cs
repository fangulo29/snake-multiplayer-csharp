using System;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using System.Reflection;

namespace Snake_v2
{
    public partial class SnakeForm : Form
    {
        TcpClient tcpClient;
        StreamReader reader = null;
        StreamWriter writer = null;

        bool left = false;
        bool right = false;
        bool up = false;
        bool down = false;
        bool podeJogar = false;
        bool updatePassou = false;
        bool snakeMorto = false;

        Graphics graphics;
        Image imagePieceSnake;
        Image imageFood;
        Image imageWall;

        Point foodPoint;
        public Point[] snakePointsArray1;
        public Point[] snakePointsArray2;
        Point[] wallPointArray;
        public List<Point> snakeList1 = new List<Point>();
        public List<Point> snakeList2 = new List<Point>();
        List<Point> wallList = new List<Point>();

        AnalyzeDataServer analyzeDataServer = new AnalyzeDataServer();

        public SnakeForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            imagePieceSnake = new Bitmap(@"images\SnakePiece.jpg");
            imageFood = new Bitmap(@"images\Food.png");
            imageWall = new Bitmap(@"images\Wall.png");

            wallPointArray = new Point[1];
            wallPointArray[0].X = 500;
            wallPointArray[0].Y = 500;

            snakePointsArray1 = new Point[3];
            snakePointsArray2 = new Point[3];

            snakeList1.Add(new Point(4, 10)); snakeList1.Add(new Point(3, 10)); snakeList1.Add(new Point(2, 10));
            snakeList2.Add(new Point(44, 10)); snakeList2.Add(new Point(45, 10)); snakeList2.Add(new Point(46, 10));

            snakePointsArray1 = snakeList1.ToArray();
            snakePointsArray2 = snakeList2.ToArray();
        }

        private void SnakeForm_Paint(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;

            //Cobras
            for (int j = 0; j < snakePointsArray1.Length; j++)
            {
                graphics.DrawImage(imagePieceSnake, snakePointsArray1[j].X * 10, snakePointsArray1[j].Y * 10);
            }

            for (int i = 0; i < snakePointsArray2.Length; i++)
            {
                graphics.DrawImage(imagePieceSnake, snakePointsArray2[i].X * 10, snakePointsArray2[i].Y * 10);
            }

            //Comida
            graphics.DrawImage(imageFood, foodPoint.X * 10, foodPoint.Y * 10);

            //Parede
            for (int k = 0; k < wallPointArray.Length; k++)
            {
                graphics.DrawImage(imageWall, wallPointArray[k].X * 10, wallPointArray[k].Y * 10);
            }
        }

        private void SnakeForm_KeyDown(object sender, KeyEventArgs e)
        {
            //Somente pode se mover caso ja tenha iniciado o jogo.
            if (podeJogar)
            {
                if (e.KeyData == Keys.Down && !up)
                {
                    up = false;
                    down = true;
                    left = false;
                    right = false;
                }

                if (e.KeyData == Keys.Up && !down)
                {
                    up = true;
                    down = false;
                    left = false;
                    right = false;
                }

                if (e.KeyData == Keys.Right && !left)
                {
                    up = false;
                    down = false;
                    left = false;
                    right = true;
                }

                if (e.KeyData == Keys.Left && !right)
                {
                    up = false;
                    down = false;
                    left = true;
                    right = false;
                }
            }
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            updatePassou = false;

            if (updatePassou == false && !snakeMorto)
            {
                if (down)
                {
                    updatePassou = true;
                    writer.Write("Down");
                    writer.WriteLine();
                    writer.Flush();
                }

                else if (up)
                {
                    updatePassou = true;
                    writer.Write("Up");
                    writer.WriteLine();
                    writer.Flush();
                }

                else if (left)
                {
                    updatePassou = true;
                    writer.Write("Left");
                    writer.WriteLine();
                    writer.Flush();
                }

                else if (right)
                {
                    updatePassou = true;
                    writer.Write("Right");
                    writer.WriteLine();
                    writer.Flush();
                }
            }
        }

        private void ConectarBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int port = Int32.Parse(textBoxPort.Text);
                tcpClient = new TcpClient(textBoxServer.Text, port);

                NetworkStream stream = tcpClient.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                Thread thread = new Thread(CheckMessage);
                thread.Start();
            }

            catch (Exception erro) { MessageBox.Show("Erro : " + erro.Message); }
        }

        void CheckMessage()
        {
            try
            {
                // faz leitura de uma linha do cliente
                String data = reader.ReadLine();

                while (data != null)
                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        ReactToMessage(data);
                    });

                    // aguarda proximos dados
                    data = reader.ReadLine();
                }

                // Shutdown and end connection
                tcpClient.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show("Erro lendo do servidor : " + err.Message);
            }
        }

        private void ReactToMessage(string data)
        {
            if (data.Equals("True"))
            {
                Painel.Enabled = false;
                Painel.Visible = false;
                UpdateTimer.Enabled = true;

                podeJogar = true;
            }

            if (data.StartsWith("Matriz:"))
            {
                MoverSnake(data);
            }

            if (data.StartsWith("Food:"))
            {
                foodPoint = analyzeDataServer.Food(data);
                Invalidate();
            }

            if (data.StartsWith("Wall:"))
            {
                wallList = analyzeDataServer.Wall(data);
                wallPointArray = wallList.ToArray();
                Invalidate();
            }

            if (data.StartsWith("Placar:"))
            {
                ExibirPlacar(data);
            }

            if (data.StartsWith("Death"))
                snakeMorto = true;

            if (data.StartsWith("Life"))
            {
                snakeMorto = false;
                down = true;
            }
        }

        private void MoverSnake(string data)
        {
            string removeBegin = data.Replace("Matriz:", "");
            string[] posicoes = removeBegin.Split(';', '.');
            string[] posCobra;

            //Coleta somente o ultimo numero (index de quem mandou o texto).
            int valorCobra = Convert.ToInt32(posicoes[posicoes.Length - 1]);

            snakeList1.Clear();
            snakeList2.Clear();

            for (int i = 0; i < posicoes.Length - 1; i++)
            {
                // Com isto conseguirei a posicao X e Y do pedaco que esta sendo analisado da cobra.
                posCobra = posicoes[i].Split(',');

                try
                {
                    Point pointTemp = new Point();

                    pointTemp.X = Convert.ToInt32(posCobra[0]);
                    pointTemp.Y = Convert.ToInt32(posCobra[1]);

                    if (valorCobra == 1)
                    {
                        snakeList1.Add(pointTemp);

                        // Converto a lista em array para ser atualizada no método Paint do Forms.
                        snakePointsArray1 = snakeList1.ToArray();
                    }
                    else
                    {
                        snakeList2.Add(pointTemp);

                        // Converto a lista em array para ser atualizada no método Paint do Forms.
                        snakePointsArray2 = snakeList2.ToArray();
                    }
                }

                catch { }
            }

            Invalidate();
        }

        private void ExibirPlacar(string data)
        {
            string removeBegin = data.Replace("Placar:", "");
            string[] dadosParaAnalisar = removeBegin.Split(',');

            if (dadosParaAnalisar[0] == "0")
                PlacarPlayer1.Text = dadosParaAnalisar[1];
            else
                PlacarPlayer2.Text = dadosParaAnalisar[1];
        }
    }
}
