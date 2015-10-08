using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;

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
        public int direction;

        Thread inputThread;
        Key leftKey, rightKey;


        public Player(int intX, int intY, string Icon, Key leftkey, Key rightkey)
        {
            leftKey = leftkey;
            rightKey = rightkey;
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
            inputThread.SetApartmentState(ApartmentState.STA);
            inputThread.Start();
        }
        
        public void input()
        {
            while (true)
            {

                Thread.Sleep(10);
                if (Keyboard.IsKeyDown(leftKey) )
                     {
                        direction = -1;
                    }

                else if (Keyboard.IsKeyDown(rightKey))
                     {
                        direction = 1;
                      }
                     
                
            }
        }
        public void Update()
        {

            if (batPos[batPos.Count - 1] + direction < Program.grid.length && batPos[0] + direction >= 0)
            {
               
                if (direction == 1)
                {
                    
                    Program.grid.set(batPos[0], y," ");
                    batPos.RemoveAt(0);
                    batPos.Add(batPos[batPos.Count - 1]+1);
                    foreach (int pos in batPos)
                    {
                        Program.grid.set(pos, y, icon);
                    }
                   
                }
                if (direction == -1)
                {
                    Program.grid.set(batPos[batPos.Count - 1], y," ");
                    batPos.RemoveAt(batPos.Count - 1);
                    batPos.Insert(0, batPos[0] - 1);
                    foreach (int pos in batPos)
                    {
                        Program.grid.set(pos, y, icon);
                    }
                }
               
            }
           
            
            direction = 0;
            Thread.Sleep(speed);

        }
    }
}
