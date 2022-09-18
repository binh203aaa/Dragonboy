using Mod.Xmap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Mod.ModMenu;
using Mod.ModHelper;
using System.Threading;

namespace Mod
{
    public class AutoGoback : ThreadActionUpdate<AutoGoback>
    {
        public static InfoGoback infoGoback = new InfoGoback();

        bool isGobacking = false;

        long lastTimeGoback;

        public override int Interval => 1000;

        //public static void update()
        //{
        //    if (ModMenuMain.getStatusInt(2) == 0) return;
        //    if (GameCanvas.gameTick % (30 * Time.timeScale) == 0)
        //    {
        //        if (!isGobacking)
        //        {
        //            if (Char.myCharz().cHP <= 0 || Char.myCharz().isDie)
        //            {
        //                if (mSystem.currentTimeMillis() - lastTimeGoback > 4000) lastTimeGoback = mSystem.currentTimeMillis();
        //                if (mSystem.currentTimeMillis() - lastTimeGoback > 3000)
        //                {
        //                    if (ModMenuMain.getStatusInt(2) == 1) infoGoback = new InfoGoback(TileMap.mapID, TileMap.zoneID, Char.myCharz());
        //                    Service.gI().returnTownFromDead();
        //                    isGobacking = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (TileMap.mapID == Char.myCharz().cgender + 21)
        //            {
        //                if (GameScr.vItemMap.size() > 0) Service.gI().pickItem(-1);
        //                else if (Char.myCharz().cHP <= 1) GameScr.gI().doUseHP();
        //                else if (!Pk9rXmap.IsXmapRunning) XmapController.StartRunToMapId(infoGoback.mapID);
        //            }
        //            else if (TileMap.mapID == infoGoback.mapID)
        //            {
        //                if (TileMap.zoneID != infoGoback.zoneID && GameCanvas.gameTick % (60 * Time.timeScale) == 0) Service.gI().requestChangeZone(infoGoback.zoneID, 0);
        //                if (TileMap.zoneID == infoGoback.zoneID)
        //                {
        //                    if (Char.myCharz().cx != infoGoback.x || Char.myCharz().cy != infoGoback.y) Utilities.teleportMyChar(infoGoback.x, infoGoback.y);
        //                    else isGobacking = false;
        //                }
        //            }
        //        }
        //    }
        //}

        protected override void update()
        {
            if (ModMenuMain.getStatusInt(2) == 0)
                return;

            var myChar = Char.myCharz();
            if (isGobacking)
            {
                if (TileMap.mapID == myChar.cgender + 21)
                {
                    if (GameScr.vItemMap.size() > 0)
                        Service.gI().pickItem(-1);
                    else if (myChar.cHP <= 1)
                        GameScr.gI().doUseHP();
                    else if (!Pk9rXmap.IsXmapRunning)
                        XmapController.StartRunToMapId(infoGoback.mapID);
                    return;
                }

                if (TileMap.mapID == infoGoback.mapID)
                {
                    if (TileMap.zoneID != infoGoback.zoneID)
                    {
                        Service.gI().requestChangeZone(infoGoback.zoneID, 0);
                        return;
                    }
                    if (TileMap.zoneID == infoGoback.zoneID)
                    {
                        if (myChar.cx != infoGoback.x || myChar.cy != infoGoback.y)
                            Utilities.teleportMyChar(infoGoback.x, infoGoback.y);
                        else
                            isGobacking = false;
                    }
                }
                return;
            }

            if (myChar.cHP <= 0 || myChar.isDie)
            {
                //if (mSystem.currentTimeMillis() - lastTimeGoback > 4000)
                //    lastTimeGoback = mSystem.currentTimeMillis();
                //if (mSystem.currentTimeMillis() - lastTimeGoback > 3000)
                //{
                if (ModMenuMain.getStatusInt(2) == 1)
                    infoGoback = InfoGoback.getCurrenInfoGoback();

                Service.gI().returnTownFromDead();
                isGobacking = true;
                //}
            }
        }

        public struct InfoGoback
        {
            public int mapID;
            public int zoneID;
            public int x;
            public int y;

            public InfoGoback(int mapId, int zoneId, int x, int y)
            {
                mapID = mapId;
                zoneID = zoneId;
                this.x = x;
                this.y = TileMap.tileTypeAt(x, y, 2) ? y : Utilities.getYGround(x);
            }

            public InfoGoback(int mapId, int zoneId, IMapObject mapObject)
            {
                mapID = mapId;
                zoneID = zoneId;
                this.x = mapObject.getX();
                this.y = TileMap.tileTypeAt(x, mapObject.getY(), 2) ? mapObject.getY() : Utilities.getYGround(x);
            }

            public static InfoGoback getCurrenInfoGoback()
            {
                var myChar = Char.myCharz();
                if (myChar == null)
                    throw new NullReferenceException("myChar is null");

                return new InfoGoback
                {
                    mapID = TileMap.mapID,
                    zoneID = TileMap.zoneID,
                    x = myChar.cx,
                    y = myChar.cy
                };
            }
        }

    }
}