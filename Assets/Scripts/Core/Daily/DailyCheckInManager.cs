using System;
using System.Collections;
using System.IO;
using UnityEngine;

[Serializable]
public class DailyCheckInData
{
    public string lastCheckInDate = ""; // Để trống thay vì null
    public int consecutiveDays = 0;
    public float onlineHoursToday = 0;
    public bool rewardClaimed = false;
}


public class DailyCheckInManager : MonoBehaviour
{
    private string savePath;
    public DailyCheckInData checkInData;
    public GameObject P_KHONGKETNOI;

    void Start()
    {
        savePath = Application.persistentDataPath + "/daily_checkin.json";
        LoadData();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.LogError("Không có kết nối mạng. Vui lòng kiểm tra internet!");
            return;
        }
        StartCoroutine(CheckInWithServerTime());
    }

    public void CheckConnect()
    {
        StartCoroutine(CheckInWithServerTime());
    }

    IEnumerator CheckInWithServerTime()
    {
        yield return ServerTimeManager.Instance.FetchServerTime();

        // Nếu không lấy được thời gian từ máy chủ, dùng thời gian cục bộ
        if (ServerTimeManager.ServerTime == default || ServerTimeManager.ServerTime.Year < 2000)
        {
            Debug.LogWarning("Không thể lấy thời gian từ máy chủ! Dùng thời gian cục bộ.");
            ServerTimeManager.ServerTime = DateTime.Now;
        }

        if (IsNewDay())
        {
            checkInData.onlineHoursToday = 0;
            checkInData.lastCheckInDate = ServerTimeManager.ServerTime.ToString("yyyy-MM-dd");
            checkInData.consecutiveDays = (checkInData.consecutiveDays % 30) + 1;
            checkInData.rewardClaimed = false;
            SaveData();
        }
        else
        {
            Thongbao.Singleton.ShowThongbaoHistory("Hôm nay đã điểm danh rồi!");
        }
    }






    public void ClaimReward()
    {
        if (checkInData.rewardClaimed)
        {
            Thongbao.Singleton.ShowThongbao("Bạn đã nhận thưởng hôm nay rồi!");
            return;
        }

        /*int reward = */CalculateReward(checkInData.consecutiveDays);
        checkInData.rewardClaimed = true;
        SaveData();

        // Thêm phần thưởng vào game (ví dụ: vàng, kim cương, vật phẩm)
       

        //Thongbao.Singleton.ShowThongbao($"Nhận {reward}! Tổng ngày điểm danh: {checkInData.consecutiveDays}");
        ////Thongbao.Singleton.ShowThongbaoHistory($"Nhận {reward}! Tổng ngày điểm danh: {checkInData.consecutiveDays}");
    }

    private int CalculateReward(int dayStreak)
    {
        switch (dayStreak)
        {
            case 1:
                // phần thưởng ở đây
                PlayerInventory.AddGold(100000);
                Thongbao.Singleton.ShowThongbaoHistory($"Demo 1");

                break;
            case 2:
                Thongbao.Singleton.ShowThongbaoHistory($"Demo 2");
                // phần thưởng ở đây

                break;
            case 3:

                break;
            case 4: 
                
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
            case 21:
                break;
            case 22:
                break;
            case 23:
                break;
            case 24:
                break;
            case 25:
                break;
            case 26:
                break;
            case 27:
                break;
            case 28:
                break;
            case 29:
                break;
            case 30:
                break;
            default:
                break;
        }
        return 0;
    }

   



    private bool IsNewDay()
    {
        if (string.IsNullOrEmpty(checkInData.lastCheckInDate) || checkInData.lastCheckInDate == "0001-01-01")
            return true; // Nếu chưa có dữ liệu, coi như ngày mới

        DateTime lastDate;
        if (!DateTime.TryParse(checkInData.lastCheckInDate, out lastDate))
            return true; // Nếu lỗi khi parse, coi như ngày mới

        return (ServerTimeManager.ServerTime.Date > lastDate.Date);
    }

    private void LoadData()
    {
        if (File.Exists(savePath))
        {
            string encryptedJson = File.ReadAllText(savePath);

            // Giải mã dữ liệu trước khi sử dụng
            string decryptedJson = EncryptionUtility.Decrypt(encryptedJson);

            checkInData = JsonUtility.FromJson<DailyCheckInData>(decryptedJson);
        }
        else
        {
            checkInData = new DailyCheckInData();
        }
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(checkInData, true);

        // Mã hóa dữ liệu trước khi lưu
        string encryptedJson = EncryptionUtility.Encrypt(json);

        File.WriteAllText(savePath, encryptedJson);
    }

    public int GetStreak()
    {
        return checkInData.consecutiveDays;
    }

    public int GetReward()
    {
        return CalculateReward(checkInData.consecutiveDays);
    }

    public bool IsRewardClaimed()
    {
        return checkInData.rewardClaimed;
    }
    public TimeSpan GetTimeUntilNextCheckIn()
    {
        DateTime nextCheckInTime = ServerTimeManager.ServerTime.Date.AddDays(1);
        return nextCheckInTime - ServerTimeManager.ServerTime;
    }

}

// Lớp quản lý vàng (Giả sử game có hệ thống tài nguyên)
public static class PlayerInventory
{
    private static int gold = 0;

    public static void AddGold(int amount)
    {
        GameManager.Singleton.gold += amount;
        Thongbao.Singleton.ShowThongbaoHistory($"Bạn đã nhận được {amount} vàng từ điểm danh.");
        //Debug.Log($"Vàng hiện tại: {gold}");
    }
    public static void AddDiamond(int amount)
    {
        GameManager.Singleton.ruby += amount;
        //Debug.Log($"Vàng hiện tại: {gold}");
    }
}
