using System;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public bool[] isLoad; // Mảng để theo dõi trạng thái tải
    private void Start()
    {
        // Khởi tạo mảng isLoad với kích thước 5 (hoặc số lượng phần tải cụ thể)
        isLoad = new bool[5];
        Load();
    }

    private void Load()
    {
        //Debug.Log("Bắt đầu tải dữ liệu...");

        

        // Tải dữ liệu game
        try
        {
            if (GameManager.Singleton != null)
            {
                GameManager.Singleton.LoadData();
                isLoad[1] = true; // Đánh dấu hoàn thành
                //Debug.Log("Dữ liệu GameManager đã được tải thành công.");
            }
            else
            {
                Debug.LogWarning("GameManager chưa được khởi tạo.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Lỗi khi tải dữ liệu GameManager: {e.Message}");
        }

        // Tải dữ liệu rương (Chest)
        try
        {
            if (ChestManager.Singleton != null)
            {
                ChestManager.Singleton.LoadChest();
                isLoad[2] = true; // Đánh dấu hoàn thành
                //Debug.Log("Dữ liệu rương đã được tải thành công.");
            }
            else
            {
                Debug.LogWarning("ChestManager chưa được khởi tạo.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Lỗi khi tải dữ liệu rương: {e.Message}");
        }

        // Tải dữ liệu kỹ năng
        try
        {
            if (SkillController.Singleton != null)
            {
                SkillController.Singleton.LoadSkill();
                isLoad[3] = true; // Đánh dấu hoàn thành
                //Debug.Log("Dữ liệu kỹ năng đã được tải thành công.");
            }
            else
            {
                Debug.LogWarning("SkillController chưa được khởi tạo.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Lỗi khi tải dữ liệu kỹ năng: {e.Message}");
        }

        // Tải dữ liệu kho đồ (Inventory)
        try
        {
            if (Inventory.Singleton != null)
            {
                Inventory.Singleton.LoadInventory();
                isLoad[4] = true; // Đánh dấu hoàn thành
                //Debug.Log("Dữ liệu kho đồ đã được tải thành công.");
            }
            else
            {
                Debug.LogWarning("Inventory chưa được khởi tạo.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Lỗi khi tải dữ liệu kho đồ: {e.Message}");
        }
        // Tải dữ liệu trang bị
        try
        {
            if (EquipmentManager.Singleton != null)
            {
                EquipmentManager.Singleton.LoadEquipment();
                isLoad[0] = true; // Đánh dấu hoàn thành
                                  // Debug.Log("Dữ liệu trang bị đã được tải thành công.");
            }
            else
            {
                Debug.LogWarning("EquipmentManager chưa được khởi tạo.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Lỗi khi tải dữ liệu trang bị: {e.Message}");
        }
        SkillController.Singleton.LoadSkill();
        Debug.Log("Hoàn thành tải dữ liệu.");
        CheckLoadStatus(); // Gọi hàm kiểm tra trạng thái tải
    }

    private void CheckLoadStatus()
    {
        for (int i = 0; i < isLoad.Length; i++)
        {
            //Debug.Log($"isLoad[{i}]: {isLoad[i]}");
        }
    }
}
