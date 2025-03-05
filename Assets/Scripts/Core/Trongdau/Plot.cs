using System;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
public class Plot : MonoBehaviour
{
    public Button actionButton;
    public Text buttonText;
    public SpriteRenderer plotSpriteLon; // Cây non
    public GameObject plotSpriteMini; // Cây trưởng thành
    public GameObject plotSpriteVua; // Cây trưởng thành
    public Text timerText;
    public Button waterButton; // Nút tưới nước
    public Text waterButtonText;
    public Text TextgrowthPercentage;
    public ParticleSystem particleWater;

    private bool isPlanted = false;
    public long plantTime;
    private const int growTime = 1200; // Cây lớn sau 20 P
    private bool isReady = false;

    private bool needsWater = false;
    private long lastWateredTime;
    private int waterInterval = 15; // Phải tưới nước mỗi 10 giây

    private string plotID;

    public GameObject uiEffectPrefab; // Hiệu ứng trên UI để báo cây trưởng thành
    public Transform uiCanvas; // Tham chiếu đến Canvas UI

    void Start()
    {
        plotID = "Dau_" + transform.position.x + "_" + transform.position.y;
        LoadPlotData();

        actionButton.onClick.AddListener(OnButtonClick);
        waterButton.onClick.AddListener(WaterPlant);


        if (isReady)
        {
            //Debug.Log("🌱 Cây đã trưởng thành khi vào game!");
            FarmManager.singleton.ActivePanelDau();

            // Hiển thị UI thông báo
            if (uiEffectPrefab != null && uiCanvas != null)
            {
                GameObject obj = Instantiate(uiEffectPrefab, uiCanvas, false);
                DauContent dauContent = obj.GetComponent<DauContent>();
                dauContent.txtTendau.text = plotID;
                //Debug.Log("📢 Thông báo UI: Cây đã trưởng thành!");
            }
        }

        UpdateUI();
    }

    void Update()
    {
        if (isPlanted && !isReady)
        {
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            long timeElapsed = currentTime - plantTime;
            long timeLeft = growTime - timeElapsed;

            // Kiểm tra xem có cần tưới nước không
            long timeSinceLastWatered = currentTime - lastWateredTime;
            if (timeSinceLastWatered >= waterInterval)
            {
                needsWater = true;
                waterButton.gameObject.SetActive(true);
                waterButtonText.text = "Tưới nước!";
                actionButton.gameObject.SetActive(true);

            }
            else
            {
                needsWater = false;
                waterButton.gameObject.SetActive(false);
            }

            // Nếu cần tưới nước thì dừng đếm thời gian trưởng thành
            if (needsWater)
            {
                timerText.text = "Cần nước!";
            }
            else if (timeLeft <= 0)
            {
                isReady = true;
                buttonText.text = "Thu hoạch";
                timerText.text = "Sẵn sàng!";
            }
            else
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
                timerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            }

            UpdatePlotAppearance(); // Cập nhật hình ảnh cây theo trạng thái
        }
    }

    void OnButtonClick()
    {
        if (!isPlanted)
        {
            PlantBean();
        }
        else if (isReady)
        {
            HarvestBean();
        }
        else if (needsWater)
        {
            //Debug.Log("Hãy tưới nước trước!");
            Thongbao.Singleton.ShowThongbao("Hãy tưới nước trước!");
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("Đậu đang lớn...");

        }
        UpdateUI();
    }

    void PlantBean()
    {
        // Kiểm tra xem có hạt giống đậu trong kho đồ không
        var seedItem = Inventory.Singleton.items.Find(item => item.item.name == "HATGIONGDAU");

        if (seedItem != null)
        {
            // Ghi nhận thời gian trồng và trạng thái
            isPlanted = true;
            plantTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            lastWateredTime = plantTime;
            isReady = false;
            needsWater = false;

            // Lưu trạng thái mới vào bộ nhớ
            SavePlotData();

            // Hiển thị thông báo
            Debug.Log("Đậu được trồng!");
            Thongbao.Singleton.ShowThongbao($"🌱 Đậu được trồng tại ô {plotID}!");
            Thongbao.Singleton.ShowThongbaoHistory($"📜 Đậu được trồng tại ô {plotID}!");

            // Trừ đi 1 hạt giống từ kho
            Inventory.Singleton.UnRemoveItem(Inventory.Singleton.items.IndexOf(seedItem), 1);

            // Cập nhật giao diện
            UpdatePlotAppearance();
            UpdateUI();
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("❌ Bạn không có hạt giống đậu để trồng.");
        }
    }


    void HarvestBean()
    {
        string itemName = null;
        Item item = null;
        isPlanted = false;
        isReady = false;
        needsWater = false;
        buttonText.text = "Trồng";
        timerText.text = "";
        SavePlotData();
        Thongbao.Singleton.ShowThongbao("Đã thu hoạch đậu!");
        itemName = "DAUTHANCAP1";
        item = Resources.Load<Item>("Items/" + itemName);
        if (item != null)
        {
            Inventory.Singleton.BuyItem(item, 1, "Từ trông cây", 0, 100);
            Thongbao.Singleton.ShowThongbao("Bạn nhận được Đậu thần.");
            Thongbao.Singleton.ShowThongbaoHistory("Bạn nhận được Đậu thần.");
        }
        else
        {
            //Debug.LogWarning($"Item '{itemName}' không tồn tại trong Resources!");
        }
        UpdatePlotAppearance();





        UpdatePlotAppearance(); // Cập nhật giao diện

        
    }

    void WaterPlant()
    {
        lastWateredTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        needsWater = false;
        waterButton.gameObject.SetActive(false);
        particleWater.gameObject.SetActive(true);
        particleWater.Play();
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Water);
        SavePlotData();
        Thongbao.Singleton.ShowThongbao($"Đã tưới nước cây {plotID}!");
        Thongbao.Singleton.ShowThongbaoHistory($"Đã tưới nước cây {plotID}!");
        StartCoroutine(timeHidePartica());
    }

    IEnumerator timeHidePartica()
    {
        yield return new WaitForSeconds(1f);
        particleWater.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        if (!isPlanted)
        {
            buttonText.text = "Trồng";
            timerText.text = "";
            waterButton.gameObject.SetActive(false);
        }
        else if (isReady)
        {
            buttonText.text = "Thu hoạch";
            timerText.text = "Sẵn sàng!";
            waterButton.gameObject.SetActive(false);
        }
        else if (needsWater)
        {
            buttonText.text = "Cần tưới nước!";
            timerText.text = "Héo úa!";
            waterButton.gameObject.SetActive(true);
            actionButton.gameObject.SetActive(false);
        }
        else
        {
            buttonText.text = "Đang lớn...";
            waterButton.gameObject.SetActive(false);
            actionButton.gameObject.SetActive(true);
        }

        UpdatePlotAppearance();
    }

    void UpdatePlotAppearance()
    {
        //Debug.Log($"UpdatePlotAppearance: isPlanted={isPlanted}, isReady={isReady}");

        if (!isPlanted)
        {
            //Debug.Log("Cây chưa được trồng!");
            plotSpriteLon.enabled = false;
            plotSpriteMini.SetActive(false);
            plotSpriteVua.SetActive(false);
            return;
        }

        long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long timeElapsed = currentTime - plantTime;
        float growthPercentage = (float)timeElapsed / growTime;

        //Debug.Log($"Cây đang phát triển: {growthPercentage * 100}%");
        TextgrowthPercentage.text = $"[{growthPercentage * 100}%]";
        // Reset tất cả sprite trước khi thiết lập lại
        //plotSpriteLon.enabled = false;
        // plotSpriteMini.SetActive(false);
        //plotSpriteVua.SetActive(false);

        if (growthPercentage >= 1f)
        {
            isReady = true;
            plotSpriteLon.enabled = true;
            Debug.Log("Cây đã trưởng thành!");
            TextgrowthPercentage.text = "";
        }
        else if (growthPercentage >= 0.7f)
        {
            plotSpriteVua.SetActive(true);
            plotSpriteMini.SetActive(false);
            //Debug.Log("Cây gần trưởng thành!");
        }
        else if (growthPercentage >= 0.4f)
        {
            //plotSpriteLon.enabled = true;
            //plotSpriteLon.color = new Color(1f, 1f, 1f, 0.7f);
            plotSpriteMini.SetActive(true);
            //Debug.Log("Cây đang lớn (cỡ trung)!");
        }
        else
        {
            plotSpriteLon.enabled = false;
            plotSpriteLon.color = new Color(1f, 1f, 1f, 0.5f);
            plotSpriteMini.SetActive(true);
            //Debug.Log("Cây mới trồng!");
        }
    }




    void SavePlotData()
    {

        PlayerPrefs.SetInt(plotID + "_isPlanted", isPlanted ? 1 : 0);
        PlayerPrefs.SetString(plotID + "_plantTime", plantTime.ToString());
        PlayerPrefs.SetInt(plotID + "_isReady", isReady ? 1 : 0);
        PlayerPrefs.SetInt(plotID + "_needsWater", needsWater ? 1 : 0);
        PlayerPrefs.SetString(plotID + "_lastWateredTime", lastWateredTime.ToString());
        PlayerPrefs.Save();
        
       
    }

    void LoadPlotData()
    {
        isPlanted = PlayerPrefs.GetInt(plotID + "_isPlanted", 0) == 1;
        plantTime = long.Parse(PlayerPrefs.GetString(plotID + "_plantTime", "0"));
        isReady = PlayerPrefs.GetInt(plotID + "_isReady", 0) == 1;
        needsWater = PlayerPrefs.GetInt(plotID + "_needsWater", 0) == 1;
        lastWateredTime = long.Parse(PlayerPrefs.GetString(plotID + "_lastWateredTime", "0"));

        UpdatePlotAppearance();
    }
}
