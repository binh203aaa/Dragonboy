using Mod.ModHelper;
using System;

namespace Mod
{
    public class AutoAttack : ThreadActionUpdate<AutoAttack>
    {
        public override int Interval => 100;

        protected override void update()
        {
            if (getMyFocus(out MyVector vMob, out MyVector vChar))
            {
                var myskill = Char.myCharz().myskill;
                long currentTimeMillis = mSystem.currentTimeMillis();

                if (currentTimeMillis - myskill.lastTimeUseThisSkill > myskill.coolDown)
                {
                    Service.gI().sendPlayerAttack(vMob, vChar, -1); // type = -1 -> auto
                    myskill.lastTimeUseThisSkill = currentTimeMillis;
                }
            }
        }

        private bool getMyFocus(out MyVector vMob, out MyVector vChar)
        {
            vMob = new MyVector();
            vChar = new MyVector();

            var myChar = Char.myCharz();
            if (myChar.mobFocus != null)
                vMob.addElement(myChar.mobFocus);
            else if (myChar.charFocus != null)
                vChar.addElement(myChar.charFocus);
            else
                return false;

            return true;
        }
    }
}
