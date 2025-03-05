using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Thongbao : MonoBehaviour
{
    public static Thongbao Singleton;
    public GameObject THONGBAOOBJ;
    public Transform HistoryContent;
    public GameObject HistoryPrefabs;
    public TextMeshProUGUI txtTrangthai;

    public GameObject UIBar;
    public Slider sliderHpEnemy;
    public Text txtHPenemy;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
          
        }
        else
        {
           
        }
    }
    public void ShowThongbao(string message)
    {
        GameObject obj = Instantiate(THONGBAOOBJ, GameObject.Find("CanvasTB").transform, false);
        Text text = obj.GetComponentInChildren<Text>();
        text.text = message;
    }

    public void ShowThongbaoHistory(string message)
    {
        // Kiểm tra số lượng con hiện tại của HistoryContent
        if (HistoryContent.childCount >= 10)
        {
            // Xóa object đầu tiên (con đầu tiên)
            Destroy(HistoryContent.GetChild(0).gameObject);
        }

        // Tạo mới thông báo
        GameObject obj = Instantiate(HistoryPrefabs, HistoryContent, false);

        // Lấy component Text và gán nội dung thông báo
        Text text = obj.GetComponent<Text>();

        if (text != null)
        {
            text.text = message;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy component Text trong prefab thông báo.");
        }
    }



}
