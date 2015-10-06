using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace pong
{
    class Player
    {
        public List<int> batPos;
       public int length=11;
        string icon;
        int speed = 50;
       public int x;
       public int y;
        int direction;
        Thread inputThread;

        public Player(int intX,int intY, string Icon)
        {
            
                x = intX;
            y = intY;
            icon = Icon;
            batPos = new List<int>();
            for (int i=0; i < length; i++)
            {
                batPos.Add(x + i);
            }
            foreach(int pos in batPos)
                Program.grid.set(pos, y, icon);
            inputThread = new Thread(input);
            inputThread.Start();
        }
        public void input()
        {
            while (true)
            {
                new System.Threading.ManualResetEvent(false).WaitOne(10);

                switch (Program.input.Key)
                {
                    case ConsoleKey.LeftArrow:
                        direction = -1;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = 1;
                        break;
                     
                }
            }
        }
        public void Update()
        {

            if (batPos[batPos.Count - 1] + direction < Program.grid.length && batPos[0] + direction >= 0)
            {
                
                if (direction == 1)
                {
                    
                    Program.grid.set(batPos[0], y, " ");
                    batPos.RemoveAt(0);
                    batPos.Add(batPos[batPos.Count - 1]+1);
                    Program.grid.set(batPos[batPos.Count - 1], y, icon);
                }
                if (direction == -1)
                {
                    Program.grid.set(batPos[batPos.Count - 1], y, " ");
                    batPos.RemoveAt(batPos.Count - 1);
                    batPos.Insert(0, batPos[0] - 1);
                    Program.grid.set(batPos[0], y, icon);
                }
               
            }
            direction = 0;
            Thread.Sleep(speed);

        }
    }
}
