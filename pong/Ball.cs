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
        bool scoringActive = true;
        public int x;
        public int y;

        public int Speed;
        public int yDirection;
        public int xDirection;
        public string ballString;
        Random rand = new Random();
        public Ball(int intX, int intY, int ydirection, string ballstring)
        {
            x = intX;
            y = intY;
            Speed = 100;
            ballString = ballstring;
            xDirection = rand.Next(-3, 3);
            yDirection = ydirection;
        }
        bool inPlayer = false;
        int extraDistance=0;
        public bool WallCollision()
        {
            if (scoringActive)
            {
                if (y + yDirection < 0)
                {
                    Program.playerTwoScore += 1;
                    Program.grid.set(x, y, " ");
                    active = false;
                    foreach (Player player in Program.players)
                    {
                        player.targetChosen = false;
                    }
                    return true;
                }
            }
            else
            {
                if (y + yDirection < 0)
                {
                    yDirection *= -1;
                    y = 0;
                    return true;
                }
            }
            if (scoringActive)
            {
                if (y + yDirection >= Program.grid.height)
                {
                    Program.playerOneScore += 1;
                    Program.grid.set(x, y, " ");
                    active = false;
                    foreach (Player player in Program.players)
                    {
                        player.targetChosen = false;
                    }
                    return true;
                }
            }
            else
            {
                if (y + yDirection >= Program.grid.height)
                {
                    yDirection *= -1;
                    y = Program.grid.height;
                    y += yDirection;
                    return true;
                }
            }
            if (x + xDirection < 0)
            {

                extraDistance =  -(x + xDirection);
                xDirection *= -1;
                x = 0;
                x += extraDistance;
                
                
                
                return true;

            }
            if (x + xDirection >= Program.grid.length)
            {
                if (Program.grid.length - (x + xDirection) != 0)
                {
                    extraDistance = Program.grid.length - (x + xDirection) ;
                }
                else
                {
                    extraDistance = -1;
                }
                xDirection *= -1;
                x = Program.grid.length;
                x += extraDistance;
                
                return true;
            }

            return false;
        }
        public void PlayerCollision()
        {
            foreach (Player player in Program.players)
            {
                if (y + yDirection == player.y
                 && x <= player.batPos[player.batPos.Count - 1]
                 && x >= player.batPos[0]
                    || y + yDirection == player.y
                    && x + xDirection <= player.batPos[player.batPos.Count - 1]
                    && x + xDirection >= player.batPos[0]
                    || y  == player.y
                    && x  <= player.batPos[player.batPos.Count - 1]
                    && x  >= player.batPos[0]
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
                            xDirection = 0;
                        }
                    yDirection *= -1;

                }


            }
        }
        int preX;
        int preY;
        public void Update()
        {
            while (active)
            {
                preX = x;
                preY = y;
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
                
                   
                PlayerCollision();


                if (!WallCollision())
                {

                    x += xDirection;
                }

                y += yDirection;
                foreach (Player player in Program.players)
                {
                    if (y == player.y
                     && x <= player.batPos[player.batPos.Count - 1]
                     && x >= player.batPos[0]
                       )
                    {
                        PlayerCollision();

                        if (!WallCollision())
                        {

                            x += xDirection;
                        }

                        y += yDirection;
                    }
                }

                Program.grid.set(preX, preY, " ");
                if (!inPlayer && active)
                {
                  
                    Program.grid.set(x, y, ballString);
                }
                Thread.Sleep(10);
                
            }
            

        }

    }
}
