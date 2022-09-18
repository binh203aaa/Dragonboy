using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mod.ModMenu
{
    public class ModMenuItemBoolean : ModMenuItem
    {
        public string Title { get; set; }

        public string description { get; set; }

        public bool value { get; set; }

        public string RMSName { get; set; }

        public bool isDisabled { get; set; }

        public string DisabledReason { get; set; }

        public ModMenuItemBoolean(string title, string description, bool defaultValue = false, string rmsName = "", bool isDisabled = false, string disabledReason = "")
        {
            Title = title;
            this.description = description;
            value = defaultValue;
            RMSName = rmsName;
            this.isDisabled = isDisabled;
            DisabledReason = disabledReason;
        }

        public override bool Equals(object obj)
        {
            if (obj is ModMenuItemBoolean modMenuItem)
            {
                return modMenuItem.Title == Title && modMenuItem.description == description && modMenuItem.value == value && modMenuItem.RMSName == RMSName && modMenuItem.isDisabled == isDisabled;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = -1012648466;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(description);
            hashCode = hashCode * -1521134295 + value.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RMSName);
            return hashCode;
        }

        public void setValue(bool value)
        {
            this.value = value;
            ModMenuPanel.onModMenuBoolsValueChanged();
        }

        public override void paint(mGraphics g)
        {
            this.w = GameCanvas.panel.wScroll;
            this.h = GameCanvas.panel.ITEM_HEIGHT - 1;
            
            string str = mResources.status == "Trạng thái" ? "Đang " : (mResources.status + ": ");
            int widthStrStatus = mFont.tahoma_7b_red.getWidth(str);

            if (!this.isDisabled)
                g.setColor(this.isSelected ? 0xF9FF4A : 0xE7DFD2);
            else
                g.setColor(this.isSelected ? new Color(0.61f, 0.63f, 0.18f) : new Color(0.54f, 0.51f, 0.46f));
            g.fillRect(this.x, this.y, this.w, this.h);

            // paint Title
            mFont.tahoma_7_green2.drawString(g, this.index + 1 + ". " + this.Title, this.x + 5, this.y, 0);

            // paint description
            //modMenuItem.Description.Length > 28 ? (modMenuItem.Description.Substring(0, 27) + "...") : modMenuItem.Description;
            if (this.isSelected && mFont.tahoma_7_blue.getWidth(this.description) > 145 - widthStrStatus && !GameCanvas.panel.isClose)
            {
                TextInfo.paint(g, this.description, this.x + 5, this.y + 11, 145 - mFont.tahoma_7b_red.getWidth(str), 15, mFont.tahoma_7_blue);
                //g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
                //g.translate(0, -GameCanvas.panel.cmy);
            }
            else
            {
                string description = this.description;
                if (mFont.tahoma_7_blue.getWidth(this.description) > 145 - widthStrStatus)
                {
                    while (mFont.tahoma_7_blue.getWidth(description + "...") > 145 - widthStrStatus)
                        description = description.Remove(description.Length - 1, 1);
                    description = description + "...";
                }
                mFont.tahoma_7_blue.drawString(g, description, this.x + 5, this.y + 11, 0);
            }

            // Paint status
            mFont mf = mFont.tahoma_7_grey;
            if (this.value)
                mf = mFont.tahoma_7b_red;
            mf.drawString(g, str + (this.value ? mResources.ON.ToLower() : mResources.OFF.ToLower()), this.x + this.w - 2, this.y + GameCanvas.panel.ITEM_HEIGHT - 14, mFont.RIGHT);
        }
    }
}