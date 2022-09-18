using System;
using System.Collections.Generic;

namespace Mod.ModMenu
{
    public class ModMenuMain
    {
        public const int TYPE_MOD_MENU = 26;

        public static ModMenuItemBoolean vsync = 
            new ModMenuItemBoolean("Vsync", "Tắt Vsync nếu bạn muốn điều chỉnh FPS!",
                defaultValue: true, rmsName: "isvsync");

        public static ModMenuItemBoolean showInfoChar = 
            new ModMenuItemBoolean("Hiện thông tin nhân vật", "Hiện gần chính xác thời gian NRD, khiên, khỉ, huýt sáo... của nhân vật đang focus",
                defaultValue: true, rmsName: "isshowinfochar");

        public static ModMenuItemBoolean autoAttack = new ModMenuItemBoolean("Tự đánh", "Bật/tắt tự đánh");

        public static ModMenuItemBoolean showListChar =
            new ModMenuItemBoolean("Hiện danh sách nhân vật", "Hiện danh sách nhân vật trong map",
                rmsName: "isshowlistchar");

        public static ModMenuItemBoolean showListPet =
            new ModMenuItemBoolean("Hiện đệ tử trong danh sách", "Hiện đệ tử trong danh sách nhân vật trong map (đệ tử không có sư phụ trong map không được hiển thị)",
                rmsName: "isshowlistpet", isDisabled: true, disabledReason: "Bạn chưa bật chức năng \"Hiện danh sách nhân vật\"!");

        public static ModMenuItemBoolean autoSS =
            new ModMenuItemBoolean("Auto up SS", "Auto up acc sơ sinh đến nhiệm vụ vào bang",
                isDisabled: true, disabledReason: "Bạn đã qua nhiệm vụ sơ sinh!");

        public static ModMenuItemBoolean autoT77 =
            new ModMenuItemBoolean("Auto T77", "Auto up Tàu Pảy Pảy",
                isDisabled: true, disabledReason: "Bạn không thể vào map Đông Karin!");

        public static ModMenuItemBoolean showCideRange = 
            new ModMenuItemBoolean("Hiện khoảng cách bom", "Hiển thị người, quái, boss... trong tầm bom",
                rmsName: "isshowsuiciderange");

        public static ModMenuItemBoolean customBackground =
            new ModMenuItemBoolean("Ảnh nền tùy chỉnh", "Thay thế nền của game bằng ảnh tùy chỉnh (ảnh sẽ được tự động điều chỉnh cho vừa kích thước màn hình)",
                rmsName: "iscustombackground", isDisabled: false, disabledReason: "Bạn cần tắt chức năng \"Giảm đồ họa\"!");

        public static ModMenuItemBoolean tanSat =
            new ModMenuItemBoolean("Tàn sát", "Bật/tắt tự động đánh quái",
                isDisabled: false, disabledReason: "Bạn đang bật auto T77 hoặc auto up SS!");

        public static ModMenuItemBoolean neSieuQuai =
            new ModMenuItemBoolean("Né siêu quái khi tàn sát", "Tự động né siêu quái khi tàn sát",
                defaultValue: true, rmsName: "isnesieuquaits");

        public static ModMenuItemBoolean vuotDiaHinh =
            new ModMenuItemBoolean("Vượt địa hình khi tàn sát", "Bật/tắt tự động vượt địa hình khi đang tàn sát",
                defaultValue: true, rmsName: "isvuotdiahinh");

        public static ModMenuItemBoolean autoPickItem =
            new ModMenuItemBoolean("Tự động nhặt vật phẩm", "Bật/tắt tự động nhặt vật phẩm",
                defaultValue: true, rmsName: "isautopick", isDisabled: false, disabledReason: "Bạn đang bật auto T77 hoặc auto up SS!");

        public static ModMenuItemBoolean justPickMyItem =
            new ModMenuItemBoolean("Không nhặt đồ của người khác", "Bật/tắt lọc không nhặt vật phẩm của người khác",
                defaultValue: true, rmsName: "ispickmyitemonly");

        public static ModMenuItemBoolean limitPickItemTimes = 
            new ModMenuItemBoolean("Giới hạn số lần nhặt", "Bật/tắt giới hạn số lần tự động nhặt một vật phẩm",
                defaultValue: true, rmsName: "islimitpicktimes");

        public static ModMenuItemBoolean[] modMenuItemBools = new ModMenuItemBoolean[]
        {
            vsync, showInfoChar, autoAttack, showListChar, showListPet, autoSS, autoT77, showCideRange, customBackground,
            tanSat, neSieuQuai, vuotDiaHinh, autoPickItem, justPickMyItem, limitPickItemTimes
        };

        /// <summary>
        /// Thêm điều chỉnh chỉ số chức năng mod ở đây
        /// </summary>
        public static ModMenuItemInt[] modMenuItemInts = new ModMenuItemInt[]
        {
            new ModMenuItemInt("FPS", 
                description: "FPS mục tiêu (cần tắt Vsync để thay đổi có hiệu lực)",
                defaultValue: 60, rmsName: "targetfps", isDisabled: false, disabledReason: "Bạn chưa tắt Vsync!"),
            new ModMenuItemInt("Giảm đồ họa",
                strValues: new string[]{ "Đang tắt", "Đang bật mức 1", "Đang bật mức 2", "Đang bật mức 3"},
                defaultValue: 0, rmsName: "levelreducegraphics"),
            new ModMenuItemInt("Goback",
                strValues: new string[]{ "Đang tắt", "Đang bật (goback tới chỗ cũ khi chết)", "Đang bật (goback tới map cố định)" }),
            new ModMenuItemInt("Gõ tiếng Việt",
                strValues: new string[]{ "Đang tắt", "Đang bật kiểu gõ TELEX", "Đang bật kiểu gõ VIQR", "Đang bật kiểu gõ VNI"},
                defaultValue: 0, rmsName: "vietmode", isDisabled: false, disabledReason: "Bạn không biết gõ tiếng Việt!"),
            new ModMenuItemInt("Auto up đệ tử",
                strValues: new string[]{ "Đang tắt", "Đang bật up đệ thường", "Đang bật up đệ né siêu quái", "Đang bật up đệ kaioken"},
                defaultValue: 0, rmsName: "", isDisabled: false, disabledReason: "Bạn không có đệ tử!"),
            new ModMenuItemInt("Đánh khi đệ cần",
                strValues: new string[]{ "Đánh quái gần nhất", "Đánh đệ (tự động bật cờ xám)", "Đánh bản thân (tự động bật cờ xám)" }, 
                defaultValue: 0, rmsName: "modeautopet", isDisabled: true, disabledReason: "Bạn chưa bật chức năng \"Auto up đệ tử\"!"),
            new ModMenuItemInt("Thời gian đổi ảnh nền", 
                description: "Điều chỉnh thời gian thay đổi ảnh nền (giây)", 
                defaultValue: 30, rmsName: "backgroundinveral", isDisabled: false, disabledReason: "Bạn chưa bật chức năng \"Ảnh nền tùy chỉnh\"!"),
        };

        public static ModMenuItemFunction[] modMenuItemFunctions = new ModMenuItemFunction[]
        {
            new ModMenuItemFunction("Menu Xmap", "Mở menu Xmap (chat \"xmp\" hoặc bấm nút x)"),
            new ModMenuItemFunction("Menu PickMob", "Mở menu PickMob (chat \"pickmob\")"),
            new ModMenuItemFunction("Menu Teleport", "Mở menu dịch chuyển (chat \"tele\" hoặc bấm nút z)"),
            new ModMenuItemFunction("Menu Custom Background", "Mở menu ảnh nền tùy chỉnh"),
        };

        public static Dictionary<int, string[]> inputModMenuItemInts = new Dictionary<int, string[]>()
        {
            { 0, new string[]{"Nhập mức FPS", "FPS"} },
            { 6, new string[]{"Nhập thời gian thay đổi ảnh nền", "giây"} },
        };

        public static void SaveData()
        {
            foreach (ModMenuItemBoolean modMenuItem in modMenuItemBools) 
                if (!string.IsNullOrEmpty(modMenuItem.RMSName)) 
                    Utilities.saveRMSBool(modMenuItem.RMSName, modMenuItem.Value);
            foreach (ModMenuItemInt modMenuItem in modMenuItemInts) 
                if (!string.IsNullOrEmpty(modMenuItem.RMSName)) 
                    Utilities.saveRMSInt(modMenuItem.RMSName, modMenuItem.SelectedValue);
        }

        public static void LoadData()
        {
            foreach (ModMenuItemBoolean modMenuItem in modMenuItemBools)
            {
                try
                {
                    if (!string.IsNullOrEmpty(modMenuItem.RMSName)) modMenuItem.setValue(Utilities.loadRMSBool(modMenuItem.RMSName));
                }
                catch { }
            }
            foreach (ModMenuItemInt modMenuItem in modMenuItemInts)
            {
                try
                {
                    int data = Utilities.loadRMSInt(modMenuItem.RMSName);
                    modMenuItem.setValue(data == -1 ? 0 : data);
                }
                catch { }
            }
        }

        public static bool getStatusBool(string rmsName)
        {
            foreach (ModMenuItemBoolean modMenuItem in modMenuItemBools)
            {
                if (modMenuItem.RMSName == rmsName) return modMenuItem.Value;
            }
            throw new Exception("Not found any ModMenuItemBoolean with RMSName \"" + rmsName + "\"!");
        }

        public static bool getStatusBool(int index)
        {
            return modMenuItemBools[index].Value;
        }

        public static int getStatusInt(string rmsName)
        {
            foreach (ModMenuItemInt modMenuItem in modMenuItemInts)
            {
                if (modMenuItem.RMSName == rmsName) return modMenuItem.SelectedValue;
            }
            throw new Exception("Not found any ModMenuItemOther with RMSName \"" + rmsName + "\"!");
        }

        public static int getStatusInt(int index)
        {
            return modMenuItemInts[index].SelectedValue;
        }
    }
}