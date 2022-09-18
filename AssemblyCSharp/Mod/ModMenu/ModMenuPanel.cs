﻿using Mod.Graphics;
using Mod.PickMob;
using Mod.Xmap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vietpad.InputMethod;

namespace Mod.ModMenu
{
    public class ModMenuPanel : IChatable
    {
        static ModMenuPanel _Instance;

        public static ModMenuPanel getInstance()
        {
            if (_Instance == null) _Instance = new ModMenuPanel();
            return _Instance;
        }

        public static void setTypeModMenu()
        {
            GameCanvas.panel.type = ModMenuMain.TYPE_MOD_MENU;
            GameCanvas.panel.tabName[ModMenuMain.TYPE_MOD_MENU] = new string[][]
            {
            new string[]{ "Bật/tắt", "" },
            new string[]{ "Điều", "chỉnh" },
            new string[]{ "Chức", "năng" },
            };
            GameCanvas.panel.setType(0);
            SoundMn.gI().getSoundOption();
            setTabModMenuMain();
            onModMenuBoolsValueChanged();
            onModMenuIntsValueChanged();
        }

        public static void setTabModMenuMain()
        {
            switch (GameCanvas.panel.type)
            {
                case ModMenuMain.TYPE_MOD_MENU:
                    setTabModMenu();
                    break;
                case TeleportMenu.TYPE_TELEPORT_LIST:
                    TeleportMenu.setTabTeleportListPanel();
                    break;
                case CustomBackground.TYPE_CUSTOM_BACKGROUND:
                    CustomBackground.setTabCustomBackgroundPanel();
                    break;
            }
        }

        static void setTabModMenu()
        {
            GameCanvas.panel.ITEM_HEIGHT = 24;
            if (GameCanvas.panel.currentTabIndex == 0) GameCanvas.panel.currentListLength = ModMenuMain.modMenuItemBools.Length;
            else if (GameCanvas.panel.currentTabIndex == 1) GameCanvas.panel.currentListLength = ModMenuMain.modMenuItemInts.Length;
            else GameCanvas.panel.currentListLength = ModMenuMain.modMenuItemFunctions.Length;
            GameCanvas.panel.selected = (GameCanvas.isTouch ? (-1) : 0);
            GameCanvas.panel.cmyLim = GameCanvas.panel.currentListLength * GameCanvas.panel.ITEM_HEIGHT - GameCanvas.panel.hScroll;
            if (GameCanvas.panel.cmyLim < 0) GameCanvas.panel.cmyLim = 0;
            GameCanvas.panel.cmy = GameCanvas.panel.cmtoY = GameCanvas.panel.cmyLast[GameCanvas.panel.currentTabIndex];
            if (GameCanvas.panel.cmy < 0) GameCanvas.panel.cmy = GameCanvas.panel.cmtoY = 0;
            if (GameCanvas.panel.cmy > GameCanvas.panel.cmyLim) GameCanvas.panel.cmy = GameCanvas.panel.cmtoY = GameCanvas.panel.cmyLim;
        }

        public static void doFireModMenuMain()
        {
            switch (GameCanvas.panel.type)
            {
                case ModMenuMain.TYPE_MOD_MENU:
                    doFireModMenu();
                    break;
                case TeleportMenu.TYPE_TELEPORT_LIST:
                    TeleportMenu.doFireTeleportListPanel();
                    break;
                case CustomBackground.TYPE_CUSTOM_BACKGROUND:
                    CustomBackground.doFireCustomBackgroundListPanel();
                    break;
            }
        }

        static void doFireModMenu()
        {
            if (GameCanvas.panel.currentTabIndex == 0) doFireModMenuBools();
            else if (GameCanvas.panel.currentTabIndex == 1) doFireModMenuInts();
            else doFireModMenuFunctions();
            notifySelectDisabledItem();
        }

        private static void doFireModMenuFunctions()
        {
            GameCanvas.panel.hideNow();
            switch (GameCanvas.panel.selected)
            {
                case 0:
                    XmapController.ShowXmapMenu();
                    break;
                case 1:
                    Pk9rPickMob.ShowMenu();
                    break;
                case 2:
                    TeleportMenu.ShowMenu();
                    break;
                case 3:
                    CustomBackground.ShowMenu();
                    break;
            }
        }

        static void doFireModMenuBools()
        {
            if (GameCanvas.panel.selected < 0) return;
            if (!ModMenuMain.modMenuItemBools[GameCanvas.panel.selected].isDisabled)
            {
                ModMenuMain.modMenuItemBools[GameCanvas.panel.selected].setValue(!ModMenuMain.modMenuItemBools[GameCanvas.panel.selected].Value);
                GameScr.info1.addInfo("Đã " + (ModMenuMain.modMenuItemBools[GameCanvas.panel.selected].Value ? "bật" : "tắt") + " " + ModMenuMain.modMenuItemBools[GameCanvas.panel.selected].Title + "!", 0);
            }
        }

        static void doFireModMenuInts()
        {
            if (GameCanvas.panel.selected < 0) return;
            int selected = GameCanvas.panel.selected;
            if (ModMenuMain.modMenuItemInts[selected].isDisabled) return;
            if (ModMenuMain.modMenuItemInts[selected].strValues != null) ModMenuMain.modMenuItemInts[selected].SwitchSelection();
            else
            {
                ChatTextField.gI().strChat = ModMenuMain.inputModMenuItemInts[selected][0];
                ChatTextField.gI().tfChat.name = ModMenuMain.inputModMenuItemInts[selected][1];
                ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
                ChatTextField.gI().startChat2(getInstance(), string.Empty);
                GameCanvas.panel.hide();
            }
        }

        public static void paintModMenuMain(mGraphics g)
        {
            switch (GameCanvas.panel.type)
            {
                case ModMenuMain.TYPE_MOD_MENU:
                    paintModMenu(g);
                    break;
                case TeleportMenu.TYPE_TELEPORT_LIST:
                    TeleportMenu.paintTeleportListPanel(g);
                    break;
                case CustomBackground.TYPE_CUSTOM_BACKGROUND:
                    CustomBackground.paintCustomBackgroundPanel(g);
                    break;
            }
        }

        static void paintModMenu(mGraphics g)
        {
            if (GameCanvas.panel.currentTabIndex == 0) paintModMenuBools(g);
            else if (GameCanvas.panel.currentTabIndex == 1) paintModMenuInts(g);
            else paintModMenuFunctions(g);
        }

        private static void paintModMenuFunctions(mGraphics g)
        {
            g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
            g.translate(0, -GameCanvas.panel.cmy);
            g.setColor(0);
            if (ModMenuMain.modMenuItemFunctions == null || ModMenuMain.modMenuItemFunctions.Length != GameCanvas.panel.currentListLength) return;
            bool isReset = true;
            string descriptionTextInfo = string.Empty;
            int x = 0, y = 0;
            for (int i = 0; i < GameCanvas.panel.currentListLength; i++)
            {
                int num = GameCanvas.panel.xScroll;
                int num2 = GameCanvas.panel.yScroll + i * GameCanvas.panel.ITEM_HEIGHT;
                int num3 = GameCanvas.panel.wScroll;
                int num4 = GameCanvas.panel.ITEM_HEIGHT - 1;
                ModMenuItemFunction modMenuItem = ModMenuMain.modMenuItemFunctions[i];
                if (!modMenuItem.isDisabled) g.setColor((i != GameCanvas.panel.selected) ? 15196114 : 16383818);
                else g.setColor((i != GameCanvas.panel.selected) ? new Color(0.54f, 0.51f, 0.46f) : new Color(0.61f, 0.63f, 0.18f));
                g.fillRect(num, num2, num3, num4);
                if (modMenuItem != null)
                {
                    mFont.tahoma_7_green2.drawString(g, i + 1 + ". " + modMenuItem.Title, num + 5, num2, 0);
                    string description = string.Empty;
                    if (mFont.tahoma_7_blue.getWidth(modMenuItem.Description) > 160)
                    {
                        string str = modMenuItem.Description;
                        while (mFont.tahoma_7_blue.getWidth(str + "...") > 160) str = str.Remove(str.Length - 1, 1);
                        description = str + "...";
                    }
                    else description = modMenuItem.Description;
                    //modMenuItem.Description.Length > 40 ? (modMenuItem.Description.Substring(0, 38) + "...") : modMenuItem.Description;
                    if (i == GameCanvas.panel.selected && mFont.tahoma_7_blue.getWidth(modMenuItem.Description) > 160 && !GameCanvas.panel.isClose)
                    {
                        isReset = false;
                        descriptionTextInfo = modMenuItem.Description;
                        x = num + 5;
                        y = num2 + 11;
                    }
                    else mFont.tahoma_7_blue.drawString(g, description, num + 5, num2 + 11, 0);
                }
            }
            if (isReset) TextInfo.reset();
            else
            {
                TextInfo.paint(g, descriptionTextInfo, x, y, 160, 15, mFont.tahoma_7_blue);
                g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
                g.translate(0, -GameCanvas.panel.cmy);
            }
            GameCanvas.panel.paintScrollArrow(g);
        }

        static void paintModMenuBools(mGraphics g)
        {
            g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
            g.translate(0, -GameCanvas.panel.cmy);
            g.setColor(0);
            if (ModMenuMain.modMenuItemBools == null || ModMenuMain.modMenuItemBools.Length != GameCanvas.panel.currentListLength) return;
            bool isReset = true;
            string descriptionTextInfo = string.Empty;
            int x = 0, y = 0;
            string str = (mResources.status + ": ") == "Trạng thái: " ? "Đang " : (mResources.status + ": ");
            for (int i = 0; i < GameCanvas.panel.currentListLength; i++)
            {
                int num = GameCanvas.panel.xScroll;
                int num2 = GameCanvas.panel.yScroll + i * GameCanvas.panel.ITEM_HEIGHT;
                int num3 = GameCanvas.panel.wScroll;
                int num4 = GameCanvas.panel.ITEM_HEIGHT - 1;
                ModMenuItemBoolean modMenuItem = ModMenuMain.modMenuItemBools[i];
                if (!modMenuItem.isDisabled) g.setColor((i != GameCanvas.panel.selected) ? 15196114 : 16383818);
                else g.setColor((i != GameCanvas.panel.selected) ? new Color(0.54f, 0.51f, 0.46f) : new Color(0.61f, 0.63f, 0.18f));
                g.fillRect(num, num2, num3, num4);
                if (modMenuItem != null)
                {
                    mFont.tahoma_7_green2.drawString(g, i + 1 + ". " + modMenuItem.Title, num + 5, num2, 0);
                    string description = string.Empty;
                    if (mFont.tahoma_7_blue.getWidth(modMenuItem.Description) > 145 - mFont.tahoma_7b_red.getWidth(str))
                    {
                        string str2 = modMenuItem.Description;
                        while (mFont.tahoma_7_blue.getWidth(str2 + "...") > 145 - mFont.tahoma_7b_red.getWidth(str)) str2 = str2.Remove(str2.Length - 1, 1);
                        description = str2 + "...";
                    }
                    else description = modMenuItem.Description;
                    //modMenuItem.Description.Length > 28 ? (modMenuItem.Description.Substring(0, 27) + "...") : modMenuItem.Description;
                    if (i == GameCanvas.panel.selected && mFont.tahoma_7_blue.getWidth(modMenuItem.Description) > 145 - mFont.tahoma_7b_red.getWidth(str) && !GameCanvas.panel.isClose)
                    {
                        isReset = false;
                        descriptionTextInfo = modMenuItem.Description;
                        x = num + 5;
                        y = num2 + 11;
                    }
                    else mFont.tahoma_7_blue.drawString(g, description, num + 5, num2 + 11, 0);
                    mFont mf = mFont.tahoma_7_grey;
                    if (modMenuItem.Value) mf = mFont.tahoma_7b_red;
                    mf.drawString(g, str + (modMenuItem.Value ? mResources.ON.ToLower() : mResources.OFF.ToLower()), num + num3 - 2, num2 + GameCanvas.panel.ITEM_HEIGHT - 14, mFont.RIGHT);
                }
            }
            if (isReset) TextInfo.reset();
            else
            {
                TextInfo.paint(g, descriptionTextInfo, x, y, 145 - mFont.tahoma_7b_red.getWidth(str), 15, mFont.tahoma_7_blue);
                g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
                g.translate(0, -GameCanvas.panel.cmy);
            }
            GameCanvas.panel.paintScrollArrow(g);
        }

        static void paintModMenuInts(mGraphics g)
        {
            g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
            g.translate(0, -GameCanvas.panel.cmy);
            g.setColor(0);
            if (ModMenuMain.modMenuItemInts == null || ModMenuMain.modMenuItemInts.Length != GameCanvas.panel.currentListLength) return;
            bool isReset = true;
            string descriptionTextInfo = string.Empty;
            int x = 0, y = 0, currSelectedValue = 0;
            for (int i = 0; i < GameCanvas.panel.currentListLength; i++)
            {
                int num = GameCanvas.panel.xScroll;
                int num2 = GameCanvas.panel.yScroll + i * GameCanvas.panel.ITEM_HEIGHT;
                int num3 = GameCanvas.panel.wScroll;
                int num4 = GameCanvas.panel.ITEM_HEIGHT - 1;
                ModMenuItemInt modMenuItem = ModMenuMain.modMenuItemInts[i];
                if (!modMenuItem.isDisabled) g.setColor((i != GameCanvas.panel.selected) ? 15196114 : 16383818);
                else g.setColor((i != GameCanvas.panel.selected) ? new Color(0.54f, 0.51f, 0.46f) : new Color(0.61f, 0.63f, 0.18f));
                g.fillRect(num, num2, num3, num4);
                if (modMenuItem != null)
                {
                    string description, str;
                    mFont.tahoma_7_green2.drawString(g, i + 1 + ". " + modMenuItem.Title, num + 5, num2, 0);
                    if (modMenuItem.strValues != null)
                    {
                        str = modMenuItem.getSelectedValue();
                        if (mFont.tahoma_7_blue.getWidth(str) > 160)
                        {
                            string str2 = str;
                            while (mFont.tahoma_7_blue.getWidth(str2 + "...") > 160) str2 = str2.Remove(str2.Length - 1, 1);
                            description = str2 + "...";
                        }
                        else description = str;
                        //description = str.Length > 28 ? (str.Substring(0, 27) + "...") : str;
                    }
                    else
                    {
                        str = modMenuItem.Description;
                        //description = str.Length > 35 ? (str.Substring(0, 34) + "...") : str;
                        if (mFont.tahoma_7b_red.getWidth(str) > 160 - mFont.tahoma_7_blue.getWidth(modMenuItem.SelectedValue.ToString()))
                        {
                            string str2 = str;
                            while (mFont.tahoma_7_blue.getWidth(str2 + "...") > 160 - mFont.tahoma_7_blue.getWidth(modMenuItem.SelectedValue.ToString())) str2 = str2.Remove(str2.Length - 1, 1);
                            description = str2 + "...";
                        }
                        else description = str;
                        mFont.tahoma_7b_red.drawString(g, modMenuItem.SelectedValue.ToString(), num + num3 - 2, num2 + GameCanvas.panel.ITEM_HEIGHT - 14, mFont.RIGHT);
                    }
                    if (i == GameCanvas.panel.selected && mFont.tahoma_7_blue.getWidth(str) > 160 - mFont.tahoma_7_blue.getWidth(modMenuItem.SelectedValue.ToString()) && !GameCanvas.panel.isClose)
                    {
                        isReset = false;
                        descriptionTextInfo = modMenuItem.Description;
                        currSelectedValue = modMenuItem.SelectedValue;
                        x = num + 5;
                        y = num2 + 11;
                    }
                    else mFont.tahoma_7_blue.drawString(g, description, num + 5, num2 + 11, 0);
                }
            }
            if (isReset) TextInfo.reset();
            else
            {
                TextInfo.paint(g, descriptionTextInfo, x, y, 160 - mFont.tahoma_7_blue.getWidth(currSelectedValue.ToString()), 15, mFont.tahoma_7_blue);
                g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
                g.translate(0, -GameCanvas.panel.cmy);
            }
            GameCanvas.panel.paintScrollArrow(g);
        }

        public static bool paintTab(mGraphics g)
        {
            if (GameCanvas.panel.type == TeleportMenu.TYPE_TELEPORT_LIST)
            {
                g.setColor(13524492);
                g.fillRect(GameCanvas.panel.X + 1, 78, GameCanvas.panel.W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, "Danh sách nhân vật", GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2, 59, mFont.CENTER);
                return true;
            }
            else if (GameCanvas.panel.type == CustomBackground.TYPE_CUSTOM_BACKGROUND)
            {
                g.setColor(13524492);
                g.fillRect(GameCanvas.panel.X + 1, 78, GameCanvas.panel.W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, "Danh sách ảnh nền tùy chỉnh", GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2, 59, mFont.CENTER);
                return true;
            }
            return false;
        }

        public void onChatFromMe(string text, string to)
        {
            if (!string.IsNullOrEmpty(ChatTextField.gI().tfChat.getText()) && !string.IsNullOrEmpty(text))
            {
                string strChat = ChatTextField.gI().strChat;
                if (strChat == ModMenuMain.inputModMenuItemInts[0][0])
                {
                    try
                    {
                        int value = int.Parse(text);
                        if (value > 60 || value < 5) throw new Exception();
                        ModMenuMain.modMenuItemInts[0].setValue(value);
                        GameScr.info1.addInfo("Đã thay đổi mức FPS!", 0);
                    }
                    catch
                    {
                        GameCanvas.startOKDlg("Mức FPS không hợp lệ!");
                    }
                }
                else if (strChat == ModMenuMain.inputModMenuItemInts[6][0])
                {
                    try
                    {
                        int value = int.Parse(text);
                        if (value < 10) throw new Exception();
                        ModMenuMain.modMenuItemInts[6].setValue(value);
                        GameScr.info1.addInfo("Đã thay đổi thời gian đổi ảnh nền!", 0);
                    }
                    catch
                    {
                        GameCanvas.startOKDlg("Thời gian không hợp lệ!");
                    }
                }
            }
            else ChatTextField.gI().isShow = false;
            Utilities.ResetTF();
        }

        public void onCancelChat()
        {
            ChatTextField.gI().isShow = false;
            Utilities.ResetTF();
        }

        public static void onModMenuBoolsValueChanged()
        {
            QualitySettings.vSyncCount = ModMenuMain.modMenuItemBools[0].Value ? 1 : 0;
            CharEffect.isEnabled = ModMenuMain.modMenuItemBools[1].Value;
            AutoAttack.gI.toggle(ModMenuMain.modMenuItemBools[2].Value);
            ListCharsInMap.isEnabled = ModMenuMain.modMenuItemBools[3].Value;
            ListCharsInMap.isShowPet = ModMenuMain.modMenuItemBools[4].Value;
            AutoSS.isAutoSS = ModMenuMain.modMenuItemBools[5].Value;
            AutoT77.isAutoT77 = ModMenuMain.modMenuItemBools[6].Value;
            SuicideRange.isShowSuicideRange = ModMenuMain.modMenuItemBools[7].Value;
            CustomBackground.isEnabled = ModMenuMain.modMenuItemBools[8].Value;
            Pk9rPickMob.IsTanSat = ModMenuMain.modMenuItemBools[9].Value;
            Pk9rPickMob.IsNeSieuQuai = ModMenuMain.modMenuItemBools[10].Value;
            Pk9rPickMob.IsVuotDiaHinh = ModMenuMain.modMenuItemBools[11].Value;
            Pk9rPickMob.IsAutoPickItems = ModMenuMain.modMenuItemBools[12].Value;
            Pk9rPickMob.IsItemMe = ModMenuMain.modMenuItemBools[13].Value;
            Pk9rPickMob.IsLimitTimesPickItem = ModMenuMain.modMenuItemBools[14].Value;

            manageDisabledModMenuItems();
        }

        public static void onModMenuIntsValueChanged()
        {
            if (ModMenuMain.modMenuItemInts[0].SelectedValue < 5 || ModMenuMain.modMenuItemInts[0].SelectedValue > 60)
                ModMenuMain.modMenuItemInts[0].SelectedValue = 60;
            Application.targetFrameRate = ModMenuMain.modMenuItemInts[0].SelectedValue;
            
            if (ModMenuMain.modMenuItemInts[2].SelectedValue == 2)
            {
                AutoGoback.infoGoback = new AutoGoback.InfoGoback(TileMap.mapID, TileMap.zoneID, Char.myCharz().cx, Char.myCharz().cy);
                GameScr.info1.addInfo($"Goback đến map: {TileMap.mapName}, khu: {TileMap.zoneID}, tọa độ: ({AutoGoback.infoGoback.x}, {AutoGoback.infoGoback.y})!", 0);
            }
            if (ModMenuMain.modMenuItemInts[3].SelectedValue == 0) VietKeyHandler.VietModeEnabled = false;
            else
            {
                VietKeyHandler.VietModeEnabled = true;
                VietKeyHandler.InputMethod = (InputMethods)(ModMenuMain.modMenuItemInts[3].SelectedValue - 1);
            }
            CustomBackground.inveralDrawImages = ModMenuMain.modMenuItemInts[6].SelectedValue * 1000;

            manageDisabledModMenuItems();
        }

        public static void manageDisabledModMenuItems()
        {
            ModMenuMain.modMenuItemBools[4].isDisabled = !ModMenuMain.modMenuItemBools[3].Value;
            if (Char.myCharz().taskMaint != null) ModMenuMain.modMenuItemBools[5].isDisabled = Char.myCharz().taskMaint.taskId > 11;
            if (Char.myCharz().cPower > 2000000 || (Char.myCharz().cPower > 1500000 && TileMap.mapID != 111) || (Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId < 9)) ModMenuMain.modMenuItemBools[6].isDisabled = true;
            else ModMenuMain.modMenuItemBools[6].isDisabled = false;
            ModMenuMain.modMenuItemBools[8].isDisabled = ModMenuMain.modMenuItemInts[1].SelectedValue > 0;
            ModMenuMain.modMenuItemBools[9].isDisabled = AutoSS.isAutoSS || AutoT77.isAutoT77;

            ModMenuMain.modMenuItemInts[0].isDisabled = ModMenuMain.modMenuItemBools[0].Value;
            ModMenuMain.modMenuItemInts[2].isDisabled = ModMenuMain.modMenuItemBools[5].Value || ModMenuMain.modMenuItemBools[6].Value;
            if (ModMenuMain.modMenuItemInts[2].isDisabled) ModMenuMain.modMenuItemInts[2].SelectedValue = 0;
            ModMenuMain.modMenuItemInts[4].isDisabled = !Char.myCharz().havePet || ModMenuMain.modMenuItemBools[5].Value || ModMenuMain.modMenuItemBools[6].Value;
            if (ModMenuMain.modMenuItemInts[4].isDisabled) ModMenuMain.modMenuItemInts[4].SelectedValue = 0;
            ModMenuMain.modMenuItemInts[5].isDisabled = ModMenuMain.modMenuItemInts[4].SelectedValue == 0;
        }

        static void notifySelectDisabledItem()
        {
            int selected = GameCanvas.panel.selected;
            if (GameCanvas.panel.currentTabIndex == 0)
            {
                if (!ModMenuMain.modMenuItemBools[selected].isDisabled) return;
                GameScr.info1.addInfo(ModMenuMain.modMenuItemBools[selected].DisabledReason, 0);
            }
            else if (GameCanvas.panel.currentTabIndex == 1)
            {
                if (!ModMenuMain.modMenuItemInts[selected].isDisabled) return;
                GameScr.info1.addInfo(ModMenuMain.modMenuItemInts[selected].DisabledReason, 0);
            }
            else
            {
                if (!ModMenuMain.modMenuItemFunctions[selected].isDisabled) return;
                GameScr.info1.addInfo(ModMenuMain.modMenuItemFunctions[selected].DisabledReason, 0);
            }
        }
    }
}