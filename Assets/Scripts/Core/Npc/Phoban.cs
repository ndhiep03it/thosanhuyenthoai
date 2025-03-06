using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Phoban : MonoBehaviour
{
    private const int bobienquangninh = 15;
    public Button[] buttonTeleport; // Danh sách các nút dịch chuyển
    public phoban[] phobans;



    private void Start()
    {
        // Gắn sự kiện cho từng nút dịch chuyển
        for (int i = 0; i < buttonTeleport.Length && i < phobans.Length; i++)
        {
            int index = i; // Lưu index cục bộ để tránh vấn đề delegate
            buttonTeleport[i].onClick.AddListener(() => JoinMao(index));
        }

    }

    public void JoinMao(int select)
    {
        switch (select)
        {
            case 0:
                if (GameManager.Singleton.level < bobienquangninh)
                {
                    Thongbao.Singleton.ShowThongbao("Bạn chưa đủ cấp độ để vào map Bờ Biển Quảng Ninh.");
                }
                else 
                {
                    Thongbao.Singleton.ShowThongbao("Đang tải...");
                    PlayerPrefs.SetFloat("SpawnX", phobans[select].x);
                    PlayerPrefs.SetFloat("SpawnY", phobans[select].y);

                    // Cập nhật thông tin map vào GameManager
                    GameManager.Singleton.map = phobans[select].namePointMap;
                    GameManager.Singleton.SaveData();

                    // Kích hoạt giao diện loading (nếu có)
                    if (UImanager.Singleton != null)
                    {
                        UImanager.Singleton.ClickPanelLoading();
                    }

                    // Chuyển sang map mới
                    SceneManager.LoadScene(phobans[select].namePointMap);
                    PlayerController.Singleton.LoadPositionPlayer();
                    

                } 

                break;

        }
       

    }
}

[System.Serializable]
public class phoban
{
    public string nameMap; // Tên map
    public string namePointMap; // Tên điểm trong map
    public float x; // Tọa độ X
    public float y; // Tọa độ Y
}
