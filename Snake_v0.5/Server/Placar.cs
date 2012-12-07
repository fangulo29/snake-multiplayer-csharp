using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Placar
    {
        int valorComida;
        int[] placares;

        private bool morreu;

        public Placar()
        {
            placares = new int[2];
            valorComida = 10;
        }

        public void SendToClient(int id, ClientProcessor[] players)
        {
            placares[id] += valorComida;

            foreach (ClientProcessor processor in players)
            {
                processor.SendScore(id.ToString() + "," + placares[id]);
            }
        }

        public void Morreu()
        {
            morreu = true;
        }

        public bool VerificarPontos()
        {
            if (!morreu)
            {
                if (placares[0] >= 200 && placares[1] >= 200)
                    return true;

                else
                    return false;

            }
            else
            {
                if (placares[0] >= 400 || placares[1] >= 400)
                    return true;
                else
                    return false;
            }
        }
    }
}
