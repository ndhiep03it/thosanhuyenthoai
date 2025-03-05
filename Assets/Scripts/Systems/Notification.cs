using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public static Notification Instance { get; private set; } // Singleton instance
    public GameObject NotificationUI;
    public Text txtNotification;

    private void Awake()
    {
        // Kiểm tra nếu đã có instance
        if (Instance != null && Instance != this)
        {          
            return;
        }
        Instance = this; // Gán instance       
    }

    public void ShowNotification(string message)
    {
        if (NotificationUI != null && txtNotification != null)
        {
            txtNotification.text = message; // Cập nhật nội dung thông báo
            NotificationUI.SetActive(true); // Hiển thị UI thông báo
        }
    }

    public void HideNotification()
    {
        if (NotificationUI != null)
        {
            NotificationUI.SetActive(false); // Ẩn UI thông báo
        }
    }
}
