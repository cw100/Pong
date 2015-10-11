using System;
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
        static  List<Thread> playerThreads;
        public static Grid grid;
        public static List<Player> players;
        
        static List<Ball> balls;
         static Ball ball;
         static List<Thread> ballThreads;
        static void AddBall(int x, int y)
         {

             ball = new Ball(x, y, "O");
             balls.Add(ball);
             int ballnum = balls.Count - 1;
             Thread ballThread = new Thread(() => BallUpdate(ballnum));
             
             
             ballThreads.Add(ballThread);

             ballThread.Start();
            
         }
        static void BallUpdate(int ballNum)
        {
            while (true)
            {
                balls[ballNum].Update();
            }
        }
        static void PlayerUpdate(int playerNum)
        {
            while (true)
            {
                players[playerNum].Update();
            }
        }
        static void Main(string[] args)
        {
            grid = new Grid(Console.WindowWidth - 1, Console.WindowHeight);
            Console.CursorVisible = false;
            balls = new List<Ball>();
            ballThreads = new List<Thread>();

            AddBall(2, Console.WindowHeight / 2);
            AddBall(Console.WindowWidth / 2, Console.WindowHeight / 2);

             players = new List<Player>();
             playerStart = new Player((Console.WindowWidth -10)/ 2, Console.WindowHeight - 3, "=", Key.Left, Key.Right);
             players.Add(playerStart);
             playerStart = new Player((Console.WindowWidth -10)/ 2, 2, "=", Key.A, Key.D);
             
             players.Add(playerStart);
             playerThreads = new List<Thread>();

             Thread playerThread = new Thread(() => PlayerUpdate(0));
             playerThreads.Add(playerThread);
              playerThread = new Thread(() => PlayerUpdate(1));
              playerThreads.Add(playerThread);
            foreach(Thread plyerThread in playerThreads)
            {
                plyerThread.Start();
            }
       
            
            while (true)
            {
                
                
                grid.Update();
            }
        }
    }
}
