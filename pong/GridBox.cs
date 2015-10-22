using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pong
{
    class GridBox
    {
        public string icon;
        public string lastIcon=" ";
        int x;
        int y;
        public bool updated=true;
        
        public GridBox(int xLocation, int yLocation)
       {
            icon = " ";
           x = xLocation;
           y = yLocation;
       }
        public void Update()
       {
           if (updated == true && Program.inUse == false)
           {
               do
               {
                   if (Program.inUse == false)
                   {
                       Program.inUse = true;
                       Console.SetCursorPosition(x, y);
                       Console.Write(icon);
                       Program.inUse = false;
                   }
               }
               while (Program.inUse);
               updated = false;
           }
       }

    }
}
