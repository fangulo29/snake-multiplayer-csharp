using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class SnakePiece
    {
        public int x;
        public int y;
        public int valor;

        public SnakePiece(int x, int y, int valor)
        {
            this.x = x;
            this.y = y;
            this.valor = valor;
        }
    }
}