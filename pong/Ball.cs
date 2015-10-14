using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace pong
{
    class Ball
    {
        public bool active = true;
        bool scoringActive =true;
        public int x;
        public int y;
       
        public int Speed;
        public int yDirection;
        public int xDirection;
        public string ballString;
        Random rand = new Random();
        public Ball(int intX, int intY,int ydirection, string ballstring)
    {
        x = intX;
        y = intY;
        Speed = 100;
        ballString = ballstring;
        xDirection = rand.Next(-1,2);
        yDirection = ydirection;
    }
        bool inPlayer=false;
        public void Update()
        {
            while (active)
            {
                inPlayer = false;
                foreach (Player player in Program.players)
                {
                    if (y == player.y
                     && x <= player.batPos[player.batPos.Count - 1]
                     && x >= player.batPos[0]
                       )
                    {
                        inPlayer = true;
                    }
                }
                if (!inPlayer)
                {
                    Program.grid.set(x, y, " ");
                }
                foreach (Player player in Program.players)
                {
                    if (y + yDirection == player.y
                     && x + xDirection <= player.batPos[player.batPos.Count - 1]
                     && x + xDirection >= player.batPos[0]
                       )
                    {
                        
                        if (x >= player.batPos[((player.batPos.Count - 1) * 6 / 10)])
                        {
                            xDirection = 1;
                            if (x >= player.batPos[((player.batPos.Count - 1) * 8 / 10)])
                            {
                                xDirection = 2;
                                if (x >= player.batPos[((player.batPos.Count - 1))])
                                {

                                    xDirection = 3;

                                }
                            }
                        }
                        else
                        if (x <= player.batPos[((player.batPos.Count - 1) * 4 / 10)])
                        {
                            xDirection = -1;
                            if (x <= player.batPos[((player.batPos.Count - 1) * 4 / 10)])
                            {
                                xDirection = -2;
                                if (x <= player.batPos[((player.batPos.Count - 1) / 10)])
                                {

                                    xDirection = -3;

                                }
                            }
                        }
                        else
                        {
                            xDirection =0;
                        }
                        yDirection *= -1;

                    }
                    else
                        if (y + yDirection == player.y
                        && x <= player.batPos[player.batPos.Count - 1]
                        && x >= player.batPos[0]
                          )
                        {
                            if (x >= player.batPos[((player.batPos.Count - 1) * 3 / 4)])
                            {
                                if (1 > xDirection)
                                {
                                    xDirection += 1;
                                }
                            }
                            if (x <= player.batPos[((player.batPos.Count - 1) / 4)])
                            {
                                if (xDirection > -1)
                                {
                                    xDirection -= 1;
                                }
                            }
                            yDirection *= -1;

                        }
                        else
                            if (y == player.y
                               && x + xDirection <= player.batPos[player.batPos.Count - 1]
                               && x + xDirection >= player.batPos[0]
                                 )
                            {
                                if (x >= player.batPos[((player.batPos.Count - 1) * 3 / 4)])
                                {
                                    if (1 > xDirection)
                                    {
                                        xDirection += 1;
                                    }
                                }
                                if (x <= player.batPos[((player.batPos.Count - 1) / 4)])
                                {
                                    if (xDirection > -1)
                                    {
                                        xDirection -= 1;
                                    }
                                }

                            }

                }
                if (scoringActive)
                {
                    if (y + yDirection < 0)
                    {
                        Program.playerTwoScore += 1;
                        Program.grid.set(x, y, " ");
                        active = false;

                    }
                }
                else
                {
                    if (y + yDirection < 0)
                    {
                        yDirection *= -1;
                        y = 0;

                    }
                }
                if (scoringActive)
                {
                    if (y + yDirection >= Program.grid.height)
                    {
                        Program.playerOneScore += 1;
                        Program.grid.set(x, y, " ");
                        active = false;
                    }
                }
                else
                {
                    if (y + yDirection >= Program.grid.height)
                    {
                        yDirection *= -1;
                        y = Program.grid.height;
                        y += yDirection;
                    }
                }
                if (x + xDirection < 0)
                {

                    xDirection *= -1;
                    x = 0;

                }
                if (x + xDirection >= Program.grid.length)
                {
                    xDirection *= -1;
                    x = Program.grid.length;
                    x += xDirection;
                }

                x += xDirection;
                y += yDirection;
                foreach (Player player in Program.players)
                {
                    if (y == player.y
                     && x <= player.batPos[player.batPos.Count - 1]
                     && x >= player.batPos[0]
                       )
                    {
                        inPlayer = true;
                    }
                }
                inPlayer = false;
                if (!inPlayer&&active)
                {
                    Program.grid.set(x, y, ballString);
                }

                Thread.Sleep(50);
            }
            
        }
        
    }
}
