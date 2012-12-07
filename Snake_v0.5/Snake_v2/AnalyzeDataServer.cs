using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake_v2
{
    class AnalyzeDataServer
    {
        List<Point> wallList = new List<Point>();
        Point foodPoint;

        public void Matriz(string data)
        {

        }

        public List<Point> Wall(string data)
        {
            string removeBegin = data.Replace("Wall:", "");
            string[] posicoes = removeBegin.Split(';');
            string[] posWall = null;

            for (int i = 0; i < posicoes.Length - 1; i++)
            {
                posWall = posicoes[i].Split(',');

                int xWall = Convert.ToInt32(posWall[0]);
                int yWall = Convert.ToInt32(posWall[1]);

                wallList.Add(new Point(xWall, yWall));
            }

            return wallList;
        }

        public Point Food(string data)
        {
            string removeBegin = data.Replace("Food:", "");
            string[] posicoes = removeBegin.Split(',');

            foodPoint.X = Convert.ToInt32(posicoes[0]);
            foodPoint.Y = Convert.ToInt32(posicoes[1]);

            return foodPoint;
        }
    }
}
