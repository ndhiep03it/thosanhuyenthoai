using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager Singleton;
    public Text txtNameBar;
    public Slider sliderHpBar;
    public Slider sliderMpBar;
    public Slider sliderCountBar;
    public Text txtHpbar;
    public Text txtMpbar;
    public Text txtLevel;
    public Text txtLevelCount;
    public Text txttoado;
    public Text txtgold;
    public Text txtruby;

    private string playerName;
    private int lastLevel = -1;
    private float lastLevelCount = -1f;
    private Vector2 lastPosition;

    [Header("Panel Đào đất")]
    public GameObject PanelDaodat;



    [Header("LOADING")]
    public GameObject PanelLoadingNextScenes;

    [Header("THONGTIN")]
    public Text txtThongtin;
    [Header("THONGTIN 2")]
    public Text txtTancong;
    public Slider sliderTancong;
    public Text txtHp;
    public Slider sliderHp;
    public Text txtMp;
    public Slider sliderMp;
    public Text txtTheluc;
    public Slider sliderTheluc;
    public Text txtLevel2;
    public Slider sliderLevel;

    public Text txtChimang;
    public Slider sliderChimang;
    [Header("DUNG LUYEN")]
    public Text txtCountQuantity;
    public Slider sliderCountDungluyen;
    public Text txtLevelCountDungluyen;
    public Text txtLevelCountDungluyenAll;
    public bool isTansatActive; 




    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
           
        }
       
    }
    void Start()
    {
        // Lấy tên người chơi một lần khi khởi động
        playerName = PlayerPrefs.GetString("PlayerName", "Player");
        txtNameBar.text = playerName;

        // Khởi tạo vị trí để tránh cập nhật UI ngay lần đầu
        lastPosition = new Vector2(float.MinValue, float.MinValue);
        SetValue();
        UImanager.Singleton.txtLevelCountDungluyen.text = $"???%";
    }

    void Update()
    {
        UpdateLevelUI();
        UpdatePositionUI();
    }

    private void UpdateLevelUI()
    {
        // Chỉ cập nhật level nếu có sự thay đổi
        if (GameManager.Singleton.level != lastLevel)
        {
            lastLevel = GameManager.Singleton.level;
            txtLevel.text = "LV." + lastLevel;
        }

        // Chỉ cập nhật level count nếu có sự thay đổi
        if (Math.Abs(GameManager.Singleton.levelcount - lastLevelCount) > 0.01f)
        {
            //lastLevelCount = GameManager.Singleton.levelcount;
           // txtLevelCount.text = "+ " + lastLevelCount + "%";
           // sliderCountBar.value = lastLevelCount;
        }
        txtHpbar.text = GameManager.Singleton.hp.ToString()/* + "/" + GameManager.Singleton.hpmax*/;
        txtMpbar.text = GameManager.Singleton.mp.ToString() /*+ "/" + GameManager.Singleton.mpmax*/;
        sliderHpBar.maxValue = GameManager.Singleton.hpao;
        sliderMpBar.maxValue = GameManager.Singleton.mpao;
        txtgold.text = FormatNumber(GameManager.Singleton.gold);
        txtruby.text = FormatNumber(GameManager.Singleton.ruby);
        txtThongtin.text = "[Thông tin cơ bản]\n" +
     $"Tấn công: <color=yellow>{GameManager.Singleton.dame} - {GameManager.Singleton.dameao}</color>\n" +  // Tấn công: vàng
     $"HP: <color=red>{GameManager.Singleton.hp}</color> / <color=red>{GameManager.Singleton.hpao}</color>\n" +  // HP: đỏ cho hiện tại, xanh cho tối đa
     $"MP: <color=blue>{GameManager.Singleton.mp}</color> / <color=blue>{GameManager.Singleton.mpao}</color>\n" +  // MP: đỏ cho hiện tại, xanh cho tối đa
     "Lv. <color=blue>" + GameManager.Singleton.level + "</color>\n" +  // Level: xanh lam
     $"Tiến độ: <color=orange>{GameManager.Singleton.levelcount}%</color> /{GameManager.Singleton.levelcountNext}\n" +  // Tiến độ: cam
     $"Chí mạng: <color=purple>{GameManager.Singleton.chimangao}%</color>\n" +  // Chí mạng: tím
     $"Hút máu: <color=red>{GameManager.Singleton.hutmauao}</color>%\n" +  // Hút máu: đỏ
     $"Hút ki: <color=blue>{GameManager.Singleton.hutkiao}</color>%\n" +  // Hút ki: xanh
     $"Né: <color=lime>{GameManager.Singleton.neao}</color>\n" +  // Né: xanh lá
     $"Thể lực: <color=lime>{GameManager.Singleton.theluc}/1000</color>\n";  // Né: xanh lá


        DataUI();

    }
    public void DataUI()
    {
        //GameManager.Singleton.hp = EquipmentManager.Singleton.hp;
        //GameManager.Singleton.mp = EquipmentManager.Singleton.mp;
        //GameManager.Singleton.dame = EquipmentManager.Singleton.dame;
        //GameManager.Singleton.chimang = EquipmentManager.Singleton.chimang;
        //txtTancong.text = "Tấn công " + GameManager.Singleton.dameao;
        //txtHp.text = "HP " + GameManager.Singleton.hpao;
        //txtMp.text = "MP " + GameManager.Singleton.mpao;
        //txtTheluc.text = "Thể lực " + GameManager.Singleton.theluc;
        sliderTancong.value=  GameManager.Singleton.dameao;
        sliderTancong.maxValue=  10000;

        sliderHp.maxValue=  GameManager.Singleton.hpao;
        sliderHp.value=  GameManager.Singleton.hp;

        sliderMp.maxValue = GameManager.Singleton.mpao;
        sliderMp.value = GameManager.Singleton.mp;

 
        sliderLevel.value = GameManager.Singleton.level;

        sliderChimang.value = GameManager.Singleton.chimangao;

        sliderTheluc.value=  GameManager.Singleton.theluc;
        //txtLevel2.text = "LV." + GameManager.Singleton.level;
        //txtChimang.text = "Chí mạng " + GameManager.Singleton.chimangao;

        // Tìm item có tên "XENG" trong kho đồ
        var NUOCTHANH = Inventory.Singleton.items.Find(item => item.item.name == "NUOCTHANHDUNGLUYEN");

        if (NUOCTHANH != null)
        {
            txtCountQuantity.text = NUOCTHANH.quantity.ToString(); 
            
        }
        else
        {
            txtCountQuantity.text = "0";
        }

        txtLevelCountDungluyenAll.text = GameManager.Singleton.levelcountdungluyen + "/" + GameManager.Singleton.leveldungluyencountNext;
        
    }

    private Coroutine autoNangCoroutine; // Lưu trữ coroutine đang chạy
    public ParticleSystem prc; // Lưu trữ coroutine đang chạy

    public void Nangcapdungluyen()
    {
        // Tìm vị trí của item trong kho đồ
        int index = Inventory.Singleton.items.FindIndex(item => item.item.name == "NUOCTHANHDUNGLUYEN");

        if (index != -1) // Nếu vật phẩm tồn tại
        {
            var NUOCTHANH = Inventory.Singleton.items[index];

            // Cập nhật hiển thị số lượng vật phẩm
            txtCountQuantity.text = NUOCTHANH.quantity.ToString();

            // Kiểm tra cấp độ tối đa
            if (GameManager.Singleton.leveldungluyen >= 10)
            {
                Thongbao.Singleton.ShowThongbao("Dung luyện đạt cấp độ tối đa.");
                GameManager.Singleton.leveldungluyen = 10;
                GameManager.Singleton.levelcountdungluyen = 4500;
                GameManager.Singleton.leveldungluyencountNext = 4500;
                sliderCountDungluyen.maxValue = 4500;
                sliderCountDungluyen.value = 4500;
                return;
            }

            if (NUOCTHANH.quantity >= 1)
            {
                

                // Kiểm tra lại nếu đạt cấp tối đa sau khi nâng cấp
                if (GameManager.Singleton.leveldungluyen >= 10)
                {
                    //Thongbao.Singleton.ShowThongbao("Dung luyện đạt cấp độ tối đa.");
                    GameManager.Singleton.leveldungluyen = 10;
                    GameManager.Singleton.levelcountdungluyen = 4500;
                    GameManager.Singleton.leveldungluyencountNext = 4500;
                    // Tắt tự động nâng cấp nếu đang bật
                    if (autoNangCoroutine != null)
                    {
                        StopCoroutine(autoNangCoroutine);
                        autoNangCoroutine = null;
                        isTansatActive = false;
                    }
                }
                else
                {
                    // Cập nhật EXP trước khi xóa vật phẩm
                    GameManager.Singleton.UpdateExpDungluyen();
                    Thongbao.Singleton.ShowThongbao("Dung luyện thành công.");

                    // Hiển thị hiệu ứng
                    prc.gameObject.SetActive(true);
                    prc.Play();
                    SoundManager.Instance.PlaySound(SoundManager.SoundEffect.UpgradeDungluyen);

                    // Xóa 1 item
                    Inventory.Singleton.UnRemoveItem(index, 1);
                }  
            }
        }
        else
        {
            // Không có vật phẩm
            Thongbao.Singleton.ShowThongbao("Không đủ nước thánh dung luyện cần 1.");
            txtCountQuantity.text = "0";
            prc.gameObject.SetActive(false);

            // Dừng tự động nâng cấp khi hết vật phẩm
            if (autoNangCoroutine != null)
            {
                StopCoroutine(autoNangCoroutine);
                autoNangCoroutine = null;
                isTansatActive = false;
            }
        }
    }


    public void AutoNang()
    {
        // Bật/tắt chế độ tự động nâng cấp
        isTansatActive = !isTansatActive;

        if (isTansatActive)
        {
            Thongbao.Singleton.ShowThongbao("Tự động nâng cấp bật.");

            // Kiểm tra nếu chưa có coroutine nào đang chạy, tránh trùng lặp
            if (autoNangCoroutine == null)
            {
                autoNangCoroutine = StartCoroutine(AutoNangCap());
            }
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("Tự động nâng cấp tắt.");
            prc.gameObject.SetActive(false);
            // Dừng coroutine đúng cách
            if (autoNangCoroutine != null)
            {
                StopCoroutine(autoNangCoroutine);
                autoNangCoroutine = null;
            }
        }
    }

    IEnumerator AutoNangCap()
    {
        while (isTansatActive)
        {
            yield return new WaitForSeconds(0.5f);
            Nangcapdungluyen();

            // Nếu đã tắt tự động hoặc hết vật phẩm, dừng coroutine
            if (!isTansatActive)
            {
                autoNangCoroutine = null;
                yield break; // Dừng coroutine
            }
        }
    }

    private void UpdatePositionUI()
    {
        // Lấy tọa độ hiện tại
        Vector2 currentPosition = new Vector2(PlayerController.Singleton.x, PlayerController.Singleton.y);

        // Chỉ cập nhật nếu vị trí thay đổi
        if (currentPosition != lastPosition)
        {
            lastPosition = currentPosition;
            txttoado.text = $"X:{currentPosition.x} - Y:{currentPosition.y}";
        }
    }

    public void ClickPanelLoading()
    {
        PanelLoadingNextScenes.SetActive(true);
        StartCoroutine(CloseLoading(2));
    }
    IEnumerator CloseLoading(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        PanelLoadingNextScenes.SetActive(false);

    }
    public void SetValue()
    {
        sliderMpBar.value = GameManager.Singleton.mp;
        sliderHpBar.value = GameManager.Singleton.hp;

        sliderHpBar.maxValue = GameManager.Singleton.hpao;
        sliderMpBar.maxValue = GameManager.Singleton.mpao;
        txtHpbar.text = GameManager.Singleton.hp.ToString()/* + "/" + GameManager.Singleton.hpmax*/;
        txtMpbar.text = GameManager.Singleton.mp.ToString() /*+ "/" + GameManager.Singleton.mpmax*/;
        PlayerController.Singleton.UpdateExpUI(GameManager.Singleton.level, GameManager.Singleton.levelcount);

        sliderCountDungluyen.maxValue = GameManager.Singleton.levelcountdungluyen;
        sliderCountDungluyen.value = GameManager.Singleton.levelcountdungluyen;
    }
    string FormatNumber(int value)
    {
        return value.ToString("N0", CultureInfo.InvariantCulture);
    }
}
