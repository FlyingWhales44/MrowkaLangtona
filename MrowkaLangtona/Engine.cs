﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrowkaLangtona
{
    public class Ant
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Direction { get; set; } //N E S W (kierunki)
    }

    class Engine : Ant
    {
        public List<List<bool>> board { get; set; }
        public Ant ant;  
        public int boardX,boardY;

        public void game()
        {
            if (board[ant.Y][ant.X] == true)//true-biale pole
            {
                board[ant.Y][ant.X] = false;
                switch (ant.Direction)
                {
                    case 'N':
                        ant.Direction = 'W';
                        ant.X = ant.X - 1;
                        if (ant.X == -1)
                            ant.X = boardX - 1;
                        break;
                    case 'E':
                        ant.Direction = 'N';
                        ant.Y = (ant.Y + 1) % boardY;
                        break;
                    case 'S':
                        ant.Direction = 'E';
                        ant.X = (ant.X + 1) % boardX;
                        break;
                    case 'W':
                        ant.Direction = 'S';
                        ant.Y = ant.Y - 1;
                        if (ant.Y == -1)
                            ant.Y = boardY - 1;
                        break;
                }
            }
            else
            {
                board[ant.Y][ant.X] = true;
                switch (ant.Direction)
                {
                    case 'N':
                        ant.Direction = 'E';
                        ant.X = (ant.X + 1) % boardX;
                        break;
                    case 'E':
                        ant.Direction = 'S';
                        ant.Y = ant.Y - 1;
                        if (ant.Y == -1)
                            ant.Y = boardY - 1;
                        break;
                    case 'S':
                        ant.Direction = 'W';
                        ant.X = ant.X - 1;
                        if (ant.X == -1)
                            ant.X = boardX - 1;
                        break;
                    case 'W':
                        ant.Direction = 'N';
                        ant.Y = (ant.Y + 1) % boardY;
                        break;
                }
            }
        }

        public void antSet(int x,int y,char dir)
        {
            ant.X = x;
            ant.Y = y;
            ant.Direction = dir;
        }

        public void set(int bX,int bY)
        {
            boardX = bX;
            boardY = bY;

            ant = new Ant();

            board = new List<List<bool>>();

            for (int i = 0; i < boardY; i++)
            {
                board.Add(new List<bool>());
                for (int j = 0; j < boardX; j++)
                    board[i].Add(true);
            }
        }

    }
}
