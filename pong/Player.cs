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
        int speed = 10;
       public int x;
       public int y;
        public int direction;
        int playerNum;
        Thread inputThread;
        Key leftKey, rightKey;

        Thread aiThread;
        public Player(int intX, int intY, string Icon, Key leftkey, Key rightkey,int playernum)
        {
            playerNum = playernum;
            if(playerNum  ==2)
            {
                aiThread = new Thread(Ai);
                aiThread.Start();
            }
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

                        Program.grid.set(batPos[0], y, " ");
                        batPos.Add(batPos[batPos.Count - 1] + 1);

                        batPos.RemoveAt(0);
                        foreach (int pos in batPos)
                        {
                            Program.grid.set(pos, y, icon);
                        }

                    }
                    if (direction == -1)
                    {
                        Program.grid.set(batPos[batPos.Count - 1], y, " ");

                        batPos.Insert(0, batPos[0] - 1);
                        batPos.RemoveAt(batPos.Count - 1);
                        foreach (int pos in batPos)
                        {
                            Program.grid.set(pos, y, icon);
                        }
                    }

                }

               
                

                if (playerNum == 1)
                {

                    direction = 0;
                    Thread.Sleep(20);
                }
                else
                {
                    Thread.Sleep(19);
                }


        }
        Random aiRand = new Random();
        int aiHitPos=0;
        public void Ai()
        {
            while (true)
            {
                if (Program.balls.Count != 0)
                {
                    try
                    {

                        if (Program.balls[0].x > batPos[batPos.Count - (1+aiHitPos)])
                        {
                            Thread.Sleep(50 );
                            direction = 1;
                        }
                        else
                            if (Program.balls[0].x < batPos[aiHitPos])
                            {
                                Thread.Sleep(50);
                                direction = -1;
                            }
                            else
                            {
                                aiHitPos = 4;
                                direction = 0;
                            }
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
