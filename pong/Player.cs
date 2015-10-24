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
        public int length = 11;
        string icon;
        int speed = 10;
        public int x;
        public int y;
        public int direction;
        int playerNum;
        Thread inputThread;
        Key leftKey, rightKey;

        Thread aiThread;
        public Player(int intX, int intY, string Icon, Key leftkey, Key rightkey, int playernum)
        {
            playerNum = playernum;

            aiThread = new Thread(Ai);
            aiThread.Start();

            leftKey = leftkey;
            rightKey = rightkey;
            x = intX;
            y = intY;
            icon = Icon;
            batPos = new List<int>();
            for (int i = 0; i < length; i++)
            {
                batPos.Add(x + i);
            }
            foreach (int pos in batPos)
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
                if (Keyboard.IsKeyDown(leftKey))
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

            Thread.Sleep(1);
            if (batPos[batPos.Count - 1] + direction < Program.grid.length && batPos[0] + direction >= 0)
            {

                if (direction == 1)
                {

                    Program.grid.set(batPos[0], y, "  ");
                    batPos.Add(batPos[batPos.Count - 1] + 1);

                    batPos.RemoveAt(0);
                    foreach (int pos in batPos)
                    {
                        Program.grid.set(pos, y, icon);
                    }

                }
                if (direction == -1)
                {
                    Program.grid.set(batPos[batPos.Count - 1], y, "  ");

                    batPos.Insert(0, batPos[0] - 1);
                    batPos.RemoveAt(batPos.Count - 1);
                    foreach (int pos in batPos)
                    {
                        Program.grid.set(pos, y, icon);
                    }
                }

            }

            direction = 0;

        }



        Random aiRand = new Random();
        int aiHitPos = 0;
        int targetX = 0;
        public bool targetChosen = false;
        public void Ai()
        {
            while (true)
            {
                Thread.Sleep(0);
                if (Program.balls.Count != 0)
                {

                    try
                    {
                        if (playerNum == 1)
                        {
                            if (Program.balls[0].yDirection == 1)
                            {

                                if (targetChosen == false)
                                {
                                    aiHitPos = aiRand.Next(0, batPos.Count - 1);

                                    targetX = Program.balls[0].x + (Program.balls[0].xDirection * (y - Program.balls[0].y));

                                    if (targetX < 0)
                                    {
                                        targetX *= -1;
                                    }
                                    if (targetX > Program.grid.length)
                                    {
                                        targetX = Program.grid.length - (targetX - Program.grid.length);

                                    }
                                    if (targetX < 0)
                                    {
                                        targetX *= -1;
                                    }



                                    targetChosen = true;
                                }
                            }
                        }
                        if (playerNum == 2)
                        {
                            if (Program.balls[0].yDirection == -1)
                            {

                                if (targetChosen == false)
                                {
                                    aiHitPos = aiRand.Next(0, batPos.Count - 1);

                                    targetX = Program.balls[0].x + (Program.balls[0].xDirection * (Program.balls[0].y - y));

                                    if (targetX < 0)
                                    {

                                        targetX *= -1;
                                    }
                                    if (targetX > Program.grid.length)
                                    {
                                        targetX = Program.grid.length - (targetX - Program.grid.length);

                                    }
                                    if (targetX < 0)
                                    {
                                        targetX *= -1;
                                    }



                                    targetChosen = true;
                                }
                            }
                        }


                        if (targetChosen == true)
                        {
                            if (targetX > batPos[aiHitPos] && batPos[batPos.Count - 1] != Program.grid.length - 1)
                            {
                                direction = 1;
                            }
                            else
                                if (targetX < batPos[aiHitPos] && batPos[0] != 0)
                                {

                                    direction = -1;
                                }
                                else
                                {

                                    direction = 0;
                                    if (playerNum == 1)
                                    {
                                        if (Program.balls[0].yDirection == -1)
                                        {
                                            targetChosen = false;
                                        }
                                    }
                                    if (playerNum == 2)
                                    {
                                        if (Program.balls[0].yDirection == 1)
                                        {
                                            targetChosen = false;
                                        }
                                    }
                                }


                        }
                        if (Program.balls[0].y < 0 || Program.balls[0].y > Program.grid.height)
                        {
                            targetChosen = false;
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
