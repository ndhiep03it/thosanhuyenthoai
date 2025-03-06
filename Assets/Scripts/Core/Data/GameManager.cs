using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    private string saveFilePath;
    public int intro = 0;
    public float x = 0;
    public float y = 0;
    public string map;
    public int thuxathu = 0;
    public int hp = 100;
    public int mp = 100;

    public int chimang = 0;
    public float necoban = 0.2f; // Xác suất né cơ bản (20%)
    public int hutmau = 0;
    public int hutki = 0;
    public int hpmax = 100;
    public int mpmax = 100;
    public int level = 1;
    public int theluc = 1000;
    public int levelcount = 0;
    public int levelcountNext = 0;
    public int dame = 10;
    public int diemkynang = 10;
    //public int damefake = 10;
    public int gold = 10000000;
    public int ruby = 10000000;
    public int slotChestMax = 30;
    public int currentSlotBuy = 0; // Số slot đã mở rộng (ban đầu là 0
    [Header("CHỈ SỐ TRANG BỊ HÀNH TRANG")]
    public int hpao = 100;
    public int mpao = 100;
    public int dameao = 100;
    public int chimangao = 0;
    public int hutkiao = 0;
    public int hutmauao = 0;
    public float neao = 0;


    public int leveldungluyen;
    public int levelcountdungluyen;
    public int leveldungluyencountNext ;
    public int dog;
    public int idnhiemvu = 0;




    // Hàm lấy EXP ngưỡng cho cấp độ dung luyện
    private int GetExpForLevelDungluyen(int level)
    {
        int[] expThresholds = { 0, 100, 300, 600, 1000, 1500, 2100, 2800, 3600, 4500 }; // Levels 1-10
        return (level - 1 >= 0 && level - 1 < expThresholds.Length) ? expThresholds[level - 1] : 0;
    }

    // Hàm cập nhật thanh trượt EXP và tiến độ
    public void UpdateExpDungluyen()
    {
        if (leveldungluyen >= 10)
        {
            GameManager.Singleton.leveldungluyen = 10;
            GameManager.Singleton.levelcountdungluyen = 4500;
            GameManager.Singleton.leveldungluyencountNext = 4500;
            Thongbao.Singleton.ShowThongbao("Dung luyện đạt cấp độ tối đa.");
            UImanager.Singleton.txtLevelCountDungluyen.text = $"100%";
            return;
        }

        // Cộng thêm EXP vào tổng levelcount
        levelcountdungluyen += 50;

        int nextLevelExp = GetExpForLevelDungluyen(leveldungluyen + 1);

        // Xử lý cấp độ và EXP dư
        while (levelcountdungluyen >= nextLevelExp && leveldungluyen < 10)
        {
            leveldungluyen++;
            levelcountdungluyen -= nextLevelExp;
            nextLevelExp = GetExpForLevelDungluyen(leveldungluyen + 1);

            Thongbao.Singleton.ShowThongbao($"Chúc mừng! Dung luyện lên cấp {leveldungluyen}.");
            SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Upgrade);
            hpmax += 50;
            mpmax += 50;
            dame += 10;
            necoban += 10;

            chimang += 1;
            hutki += 1;
            hutmau += 1;
        }

        // Cập nhật giá trị EXP thực tế sau khi tăng cấp
        int currentExp = levelcountdungluyen;
        leveldungluyencountNext = nextLevelExp;

        // Cập nhật slider
        UImanager.Singleton.sliderCountDungluyen.maxValue = nextLevelExp;
        UImanager.Singleton.sliderCountDungluyen.value = currentExp;

        // Hiển thị phần trăm tiến độ chính xác
        float progress = (float)currentExp / nextLevelExp;
        UImanager.Singleton.txtLevelCountDungluyen.text = $"+{Mathf.Clamp(Mathf.RoundToInt(progress * 100), 0, 100)}%";
    }

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //try
        //{
        //    await UnityServices.InitializeAsync();
        //}
        //catch (Exception e)
        //{
        //    Debug.LogException(e);
        //}
    }

    private  void Start()
    {
        // Đặt độ phân giải màn hình (1280x720 là ví dụ)
        Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        // Đường dẫn lưu file JSON
        saveFilePath = Application.persistentDataPath + "/playerData.json";
        LoadData();
        //damefake += dame;
        Debug.Log(saveFilePath);
        StartCoroutine(LoadDataTime());
        //InitializationOptions options = new InitializationOptions();
        //options.SetProfile("38xoPeukY1EbSTC88zViaeYg4vqy"); // Thay bằng Project ID thật của bạn
        //await UnityServices.InitializeAsync();


    }



    


    
    IEnumerator LoadDataTime()
    {
        yield return new WaitForSeconds(2f);
        //SaveData();
        LoadData();
        //Login();


    }
   
    // Lưu dữ liệu
    public void SaveData()
    {
        x = PlayerController.Singleton.x;
        y = PlayerController.Singleton.y;
        PlayerData data = new PlayerData { intro = intro,thuxathu = thuxathu, 
        health = hpmax, level = level, dame = dame,map = map ,levelcount = levelcount,gold = gold,ruby = ruby, X = x,Y =y,chimang = chimang,
        hutmau = hutmau,hutki = hutki,ne = necoban,mpmax = mpmax,currentSlotBuy = currentSlotBuy,theluc = theluc,levelcountdungluyen = levelcountdungluyen,leveldungluyen = leveldungluyen,leveldungluyencountNext = leveldungluyencountNext,dog = dog,
        idnhiemvu = idnhiemvu};
        string json = JsonUtility.ToJson(data);
        // Mã hóa dữ liệu trước khi lưu
        string encryptedJson = EncryptionUtility.Encrypt(json);
        File.WriteAllText(saveFilePath, encryptedJson);
        //File.WriteAllText(saveFilePath, json);
        Inventory.Singleton.SaveInventory();
        EquipmentManager.Singleton.SaveEquipment();
        
    }

    // Tải dữ liệu
    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);

            // Giải mã dữ liệu trước khi xử lý
            string jsonCode = EncryptionUtility.Decrypt(json);

            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonCode);
            intro = data.intro;
            x = data.X;
            y = data.Y;
            map = data.map;
            thuxathu = data.thuxathu;
            hpmax = data.health;
            mpmax = data.health;
            level = data.level;
            theluc = data.theluc;
            chimang = data.chimang;
            hutmau = data.hutmau;
            hutki = data.hutki;
            necoban = data.ne;
            levelcount = data.levelcount;
            dame = data.dame;
            gold = data.gold;
            ruby = data.ruby;
            currentSlotBuy = data.currentSlotBuy;
            levelcountdungluyen = data.levelcountdungluyen;
            leveldungluyen = data.leveldungluyen;
            leveldungluyencountNext = data.leveldungluyencountNext;
            dog = data.dog;
            idnhiemvu = data.idnhiemvu;
            Debug.Log("Dữ liệu đã được tải!");
        }
        else
        {
            Debug.LogWarning("Không tìm thấy file lưu!");
            intro = 0; // Giá trị mặc định
            x = 0; // Giá trị mặc định
            y = 0; // Giá trị mặc định
            map = "Map1"; // Giá trị mặc định
            thuxathu = 0; // Giá trị mặc định
            hp = 100; // Giá trị mặc định
            mp = 100; // Giá trị mặc định
            hpmax = 100; // Giá trị mặc định
            mpmax = 100; // Giá trị mặc định
            level = 1;
            theluc = 1000;
            chimang = 0;
            hutmau = 0;
            hutki = 0;
            levelcount = 0;
            dame = 10;
            gold = 10000000;
            ruby = 10000000;
            currentSlotBuy = 0;
            levelcountdungluyen = 0;
            leveldungluyen = 0;
            leveldungluyencountNext = 0;
            dog = 0;
            idnhiemvu = 0;
        }
    }

    
    
    // Class lưu trữ dữ liệu
    [System.Serializable]
    private class PlayerData
    {
        public int intro;
        public float X;
        public float Y;
        public int thuxathu;
        public int health;
        public int mpmax;
        public int hutmau;
        public int hutki = 0;
        public int chimang;
        public int level;
        public int theluc;
        public int levelcount = 0;
        public int dame;
        public float ne;
        public string map;
        public int gold;
        public int ruby;
        public int currentSlotBuy = 0; // Số slot đã mở rộng (ban đầu là 0


        public int leveldungluyen;
        public int levelcountdungluyen;
        public int leveldungluyencountNext;
        public int dog;
        public int idnhiemvu;
    }

    private int totalCoins = 0;
    public void AddCoins(int amount)
    {
        gold += amount;
        SaveData();
       
    }
    public void AddRuby(int amount)
    {
        ruby += amount;
        SaveData();

    }
    // Hàm lấy tổng số xu
    public int GetTotalCoins()
    {
        return totalCoins;
    }
    
}
