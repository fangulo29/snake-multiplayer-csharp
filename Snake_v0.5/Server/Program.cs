using System;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpListener = null;
            int port = 9999;
            bool working = true;
            GameState gameState = new GameState();

            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                tcpListener = new TcpListener(ip, port);
                tcpListener.Start();

                while (working)
                {
                    //Se ainda nao se tem 02 jogadores, ficar aguardando.
                    if (!gameState.IsReady())
                    {
                        Console.WriteLine("Aguardando Conexões...");

                        //Trava até um cliente chegar
                        TcpClient tcpClient = tcpListener.AcceptTcpClient();

                        gameState.AddPlayer(tcpClient);
                    }
                    else
                    {
                        gameState.Play();
                    }
                }
            }

            catch (SocketException e) { Console.WriteLine("Erro: {0}", e); }

            finally { tcpListener.Stop(); }
        }
    }
}