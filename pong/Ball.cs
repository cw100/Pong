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
        public int x;
        public int y;
       
        public int Speed;
        public int yDirection;
        public int xDirection;
        public string ballString;
        public Ball(int intX, int intY, string ballstring)
    {
        x = intX;
        y = intY;
        Speed = 100;
        ballString = ballstring;
        xDirection = 1;
        yDirection = 1;
    }
        public void Update()
        {
            Program.grid.set(x, y, " ");
            
             if (y + yDirection == Program.playerOne.y
                 && x  <= Program.playerOne.batPos[Program.playerOne.batPos.Count-1]
                 && x  >= Program.playerOne.batPos[0])
            {
                if (   x >= Program.playerOne.batPos[((Program.playerOne.batPos.Count - 1)*3 / 4)])
                {
                    xDirection = 1;
                }
                if ( x <= Program.playerOne.batPos[((Program.playerOne.batPos.Count - 1) / 4)])
                {
                    xDirection = -1;
                }
                yDirection *= -1;
                
            }
            
            x += xDirection;
            y += yDirection;
            
            if(y < 0)
            {
                yDirection *= -1;
                y = 0 + 1;
            }
            if (y >= Program.grid.height)
            {
                yDirection *= -1;
                y = Program.grid.height - 1;
            }
            if (x < 0)
            {
                xDirection *= -1;
                x = 0 + 1;
            }
            if (x >= Program.grid.length)
            {
                xDirection *= -1;
                x = Program.grid.length - 1;
            }
            if (y == Program.playerOne.y
                  && x+xDirection <= Program.playerOne.batPos[Program.playerOne.batPos.Count - 1]
                  && x+xDirection >= Program.playerOne.batPos[0])
            {

                y -= yDirection;

                yDirection *= -1;
            }
            Program.grid.set(x, y, ballString);

            Thread.Sleep(Speed);
        }
        
    }
}
