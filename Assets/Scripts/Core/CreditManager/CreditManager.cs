using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform content;           // Scroll View Content
    public GameObject creditPrefab;     // Prefab hiển thị từng mục Credit


    public List<Credit> credits = new List<Credit>();

  
    private void OnEnable()
    {
        // Tải danh sách Credit từ JSON
        LoadCredits();

        // Hiển thị danh sách
        DisplayCredits();
        // Lấy kích thước của content
        
    }

    private void Update()
    {
        // Cuộn danh sách tự động
       
    }
    
    // Hiển thị danh sách Credit
    public void DisplayCredits()
    {
        // Xóa nội dung cũ
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // Tạo mục mới cho từng Credit
        foreach (var credit in credits)
        {
            GameObject creditObject = Instantiate(creditPrefab, content);
            Text[] texts = creditObject.GetComponentsInChildren<Text>();

            // Gán dữ liệu tên và vai trò
            texts[0].text = credit.name;
            texts[1].text = credit.role;
        }
    }

  

    // Tải danh sách từ JSON
    private void LoadCredits()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "credits.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CreditList creditList = JsonUtility.FromJson<CreditList>(json);
            credits = creditList.credits;
            Debug.Log(filePath);
        }
        else
        {
            //Debug.LogError("Không tìm thấy file credits.json tại " + filePath);
        }
    }
}
