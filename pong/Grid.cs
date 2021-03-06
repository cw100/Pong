﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace pong
{
    class Grid

    {
     public int length, height;
    public List<GridBox> grid ;
        public Grid(int l, int h)
        {
            length = l;
            height = h;
            grid = new List<GridBox>();
            grid = new List<GridBox>();
            for(int i=0; i <height;i++)
            {
                for (int j = 0; j < length; j++)
                {
                    grid.Add(new GridBox(j,i));

                }
            }
        }
        public void set(int x, int y, string str)
        {
            if (0 <= x && x < length && 0 <= y && y < height)
            {
             
                grid[x + (y * length)].lastIcon = grid[x + (y * length)].icon;
                grid[x + (y * length)].icon = str;
                grid[x + (y * length)].updated = true;
                
            }
            else
            {
                throw Exception("");
        }
        }
        public void set(int x, int y)
        {
            if (0 <= x && x < length && 0 <= y && y < height)
            {
                grid[x + (y * length)].icon = grid[x + (y * length)].lastIcon;
                grid[x + (y * length)].lastIcon = grid[x + (y * length)].icon;
                grid[x + (y * length)].updated = true;
            }
            else
            {
                throw Exception("");
            }
        }

        private Exception Exception(string p)
        {
            throw new NotImplementedException();
        }
        public void Update()
        {
            Thread.Sleep(1);
            foreach(GridBox gB in grid)
            {
                gB.Update();
            }
        }
    }
}
