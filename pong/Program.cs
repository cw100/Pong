﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;

namespace pong
{
    class Program
    {
      
        public static Player playerStart;
        static List<Thread> playerThreads;
        public static Grid grid;
        public static List<Player> players;
        public static int playerOneScore = 0;
        public static int playerTwoScore = 0;
        public static List<Ball> balls;
        static Ball ball;
        static List<Thread> ballThreads;
        public static bool inUse=false;

        static void AddBall(int x, int y)
        {
            if (playerOneScore >= playerTwoScore)
            {
                ball = new Ball(x, y, -1, "O");
            }
            else
            {
                ball = new Ball(x, y, 1, "O");
            }
            balls.Add(ball);
            Thread ballThread = new Thread(() => BallUpdate(ball));


            ballThreads.Add(ballThread);

            ballThread.Start();

        }
        static void AddBall(int x)
        {
            if (playerOneScore >= playerTwoScore)
            {
                ball = new Ball(x,grid.height - 4 , -1, "O");
            }
            else
            {
                ball = new Ball(x, 3, 1, "O");
            }
            balls.Add(ball);
            Thread ballThread = new Thread(() => BallUpdate(ball));


            ballThreads.Add(ballThread);

            ballThread.Start();

        }
        static void BallUpdate(Ball ball)
        {
            while (ball.active)
            {
                ball.Update();
            }
            balls.Remove(ball);
            Thread.CurrentThread.Abort();
        }
        static void PlayerUpdate(int playerNum)
        {
            while (true)
            {
                players[playerNum].Update();
            }
            Thread.CurrentThread.Abort();
        }

        static void DrawScore()
        {
                         
                    Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight / 2);

                    Console.Write(playerOneScore + "-" + playerTwoScore);
                    
             }
        static bool active = true;
        static int width, height;
        static void Main(string[] args)
        {
            Console.Title = "Pong";
            height = 30;
            width = 90;
            Console.SetWindowSize(width, height);

            Console.SetBufferSize(width, height);
            grid = new Grid(Console.WindowWidth - 20, Console.WindowHeight);
            Console.CursorVisible = false;
            balls = new List<Ball>();
            ballThreads = new List<Thread>();
            players = new List<Player>();
            playerStart = new Player((grid.length - 10) / 2, grid.height - 3, "=", Key.Left, Key.Right, 1);
            players.Add(playerStart);
            playerStart = new Player((grid.length - 10) / 2, 2, "=", Key.A, Key.D, 2);

            players.Add(playerStart);
            playerThreads = new List<Thread>();

            Thread playerThread = new Thread(() => PlayerUpdate(0));
            playerThreads.Add(playerThread);
            playerThread = new Thread(() => PlayerUpdate(1));
            playerThreads.Add(playerThread);

            foreach (Thread plyerThread in playerThreads)
            {
                plyerThread.Start();
            }


            AddBall(grid.length / 2, grid.height / 2);
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - 19, i);
                Console.Write("|");
            }
            while (active)
            {
                DrawScore();
                grid.Update();

                if (balls.Count < 1)
                {
                   
                    AddBall(grid.length / 2 + balls.Count);
                }

                if (playerOneScore > 9 || playerTwoScore > 9)
                {
                    active = false;
                }
                
            }
            int winningPlayer;
            if (playerOneScore > playerTwoScore)
            {
                winningPlayer = 1;
            }
            else
            {
                winningPlayer = 2;
            }
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - 5) / 2, Console.WindowHeight / 2);
            Console.Write("Player " + winningPlayer + " wins!");
            Console.ReadKey(true);
        }
    }
}
