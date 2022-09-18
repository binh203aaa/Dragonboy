using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod.ModMenu
{
    public abstract class ModMenuItem
    {
        public int x;
        public int y;
        public int w;
        public int h;

        public int index = -1;
        
        public bool isSelected => this.index == GameCanvas.panel.selected;

        public abstract void paint(mGraphics g);
    }
}
