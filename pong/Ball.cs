using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void Update()
        {
            x += xDirection;
            y += yDirection;
        }
    }
}
