using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        GameManager.Singleton.SaveData();
        ChestManager.Singleton.SaveChest();
        EquipmentManager.Singleton.SaveEquipment();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameManager.Singleton.SaveData();
            ChestManager.Singleton.SaveChest();
            EquipmentManager.Singleton.SaveEquipment();
            QuestManager.Instance.SaveQuests();

            //Inventory.Singleton.SaveInventory();
            Thongbao.Singleton.ShowThongbao("Luu data thanh cong.");
        }
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            GameManager.Singleton.SaveData();
            ChestManager.Singleton.SaveChest();
            EquipmentManager.Singleton.SaveEquipment();
            Inventory.Singleton.SaveInventory();
            QuestManager.Instance.SaveQuests();
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            GameManager.Singleton.SaveData();
            ChestManager.Singleton.SaveChest();
            EquipmentManager.Singleton.SaveEquipment();
            Inventory.Singleton.SaveInventory();
            QuestManager.Instance.SaveQuests();

        }
    }
    private void OnDestroy()
    {
       QuestManager.Instance.SaveQuests();

    }
}
