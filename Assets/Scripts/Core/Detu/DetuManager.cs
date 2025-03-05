using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetuManager : MonoBehaviour
{
    public static DetuManager singleton;
    public int enableDetu = 0;
    [Header("Thong tiin de tu")]
    public TextMeshProUGUI txttHONGTIN;

    public GameObject P_DETU;
    public int hp = 100;
    public int mp = 100;
    public int chimang = 0;
    public int level = 1;
    public int theluc = 1000;
    public int levelcount = 0;
    public int levelcountNext = 0;
    public int dame = 10;

    private void Awake()
    {
        if (singleton == null) singleton = this;
    }



    public void Showdetu()
    {
        if (enableDetu == 1)
        {
            P_DETU.SetActive(true);
            ShowUI();
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("Bạn chưa có đệ tử không thể xem.");
            P_DETU.SetActive(false);

        }
    }

    void ShowUI()
    {
        txttHONGTIN.text = "Thông tin\n" +
        $"Đệ tử\n Tấn công:{dame}\n HP:{hp} \n MP:{mp} \n Chí mạng:{chimang} \n Cấp:{level} \n Tiến trình:{levelcount} \n Thể lực:{theluc} \n";
    }

}
