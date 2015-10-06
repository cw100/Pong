using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace pong
{
    class Program
    {
        public static Player playerOne;
        public static Grid grid = new Grid(40, 15);
        public static ConsoleKeyInfo input;
        static Thread inputThread= new Thread(Input);
        static Thread ballThread = new Thread(BallUpdate);
       static Ball ball;
        static void Input()
        {
            while (true)
            {
                input = Console.ReadKey(true);
            }
        }
        static void BallUpdate()
        {
            while (true)
            {
                ball.Update();
            }
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            inputThread.Start();
             ball = new Ball(5,5,"x");

             playerOne = new Player(5, 10, "T");

             ballThread.Start();
            while (true)
            {
                playerOne.Update();
                grid.Update();
            }
        }
    }
}
