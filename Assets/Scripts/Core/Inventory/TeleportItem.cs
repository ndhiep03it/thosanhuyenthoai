using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportItem : MonoBehaviour
{
    public Teleports[] teleportItem; // Danh sách các điểm dịch chuyển
    public Button[] buttonTeleport; // Danh sách các nút dịch chuyển

    private void Start()
    {
        // Gắn sự kiện cho từng nút dịch chuyển
        for (int i = 0; i < buttonTeleport.Length && i < teleportItem.Length; i++)
        {
            int index = i; // Lưu index cục bộ để tránh vấn đề delegate
            buttonTeleport[i].onClick.AddListener(() => TeleportClick(index));
        }
        
    }
    public void gohome()
    {
        TeleportClick(0);
        GameManager.Singleton.hp = GameManager.Singleton.hpao;
        GameManager.Singleton.mp = GameManager.Singleton.mpao;
        PlayerController.Singleton.moveSpeed = 7f;
    }

    // Xử lý dịch chuyển khi nhấn nút
    public void TeleportClick(int index)
    {
        if (index < 0 || index >= teleportItem.Length)
        {
            Debug.LogError("Index không hợp lệ!");
            return;
        }

        // Lưu vị trí spawn mới
        PlayerPrefs.SetFloat("SpawnX", teleportItem[index].x);
        PlayerPrefs.SetFloat("SpawnY", teleportItem[index].y);

        // Cập nhật thông tin map vào GameManager
        GameManager.Singleton.map = teleportItem[index].namePointMap;
        GameManager.Singleton.SaveData();

        // Kích hoạt giao diện loading (nếu có)
        if (UImanager.Singleton != null)
        {
            UImanager.Singleton.ClickPanelLoading();
        }

        // Chuyển sang map mới
        SceneManager.LoadScene(teleportItem[index].namePointMap);
        PlayerController.Singleton.LoadPositionPlayer();
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Teleports
{
    public string nameMap; // Tên map
    public string namePointMap; // Tên điểm trong map
    public float x; // Tọa độ X
    public float y; // Tọa độ Y
}
