using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
    public static ChestManager Singleton;
    public List<ChestSlot> items = new List<ChestSlot>(); // Danh sách các ô trong inventory

    private string saveFilePath;
    public string Itemid;
   // public int level = 0;
    public int vitriitem = 0;
    public int quantity = 0;
    public int star = 0;
    public int level;
    public int hp;
    public int mp;
    public int hutmau;
    public int hutki;
    public float ne;
    public int chimang;
    public int dame;
    public int stars; // số sao sở hữu
    public int solanepsao; // số sao sở hữu
    public int doben; // số sao sở hữu
    public string status;

    public Item[] item; // Mảng Item
    public ChestUI chestUI;
    private GameObject targetItem;

    public GameObject p_ruongdo;
    public GameObject p_thao;
    public GameObject p_cat;
    public GameObject p_buychest;
    public Text txtSlotBuychest;
    public const int slotbuyMax = 30; // Giới hạn số slot có thể mua







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

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "chest.json");
        LoadChest(); // Tải dữ liệu Inventory khi bắt đầu
        txtSlotBuychest.text = $"Slot rương hiện tại: {GameManager.Singleton.currentSlotBuy}/{slotbuyMax}";
    }

  

    public void SaveChest()
    {
        try
        {
            List<ChestData> dataToSave = new List<ChestData>();

            foreach (ChestSlot slot in items)
            {
                if (slot.item != null)
                {
                    ChestData data = new ChestData
                    {
                        itemName = slot.item.name,
                        quantity = slot.quantity,
                        stars = slot.stars,
                        status = slot.status,
                        level = slot.level,
                        dame = slot.dame,
                        hp = slot.hp,
                        mp = slot.mp,
                        hutki = slot.hutki,
                        hutmau = slot.hutmau,
                        chimang = slot.chimang,
                        ne = slot.ne,
                        doben = slot.doben,
                        solanepsao = slot.solanepsao,

                    };
                    dataToSave.Add(data);
                }
            }

            string json = JsonUtility.ToJson(new ChestSaveData { inventory = dataToSave }, true);

            // Mã hóa dữ liệu trước khi lưu
            string encryptedJson = EncryptionUtility.Encrypt(json);
            File.WriteAllText(saveFilePath, encryptedJson);

            //Debug.Log("Inventory saved successfully with encryption.");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save inventory: " + e.Message);
        }
    }
    public void SetTarget(GameObject target)
    {
        if (targetItem != null)
        {
            ItemProfile itemContent1 = targetItem.GetComponent<ItemProfile>();
            if (itemContent1 != null)
            {
                itemContent1.HideArrow();
            }
        }

        targetItem = target;
        ItemProfile itemContent2 = target.GetComponent<ItemProfile>();
        if (itemContent2 != null)
        {
            itemContent2.ShowArrow();
        }
    }
    public void LoadChest()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                string encryptedJson = File.ReadAllText(saveFilePath);

                // Giải mã dữ liệu trước khi xử lý
                string json = EncryptionUtility.Decrypt(encryptedJson);

                ChestSaveData loadedData = JsonUtility.FromJson<ChestSaveData>(json);

                // Reset inventory
                items.Clear();

                foreach (ChestData data in loadedData.inventory)
                {
                    // Tìm item từ Resources
                    Item item = Resources.Load<Item>($"Items/{data.itemName}");

                    if (item != null)
                    {
                        items.Add(new ChestSlot { item = item, quantity = data.quantity, level = data.level, stars = data.stars, status = data.status ,dame = dame,hp = hp,
                        mp = mp,chimang = chimang,hutki = hutki,hutmau = hutmau,ne = data.ne,solanepsao = solanepsao,doben = data.doben});
                    }
                    else
                    {
                        Debug.LogWarning($"Item not found: {data.itemName}");
                    }
                }

                // Debug.Log("Inventory loaded successfully with decryption.");
            }
            else
            {
                //Debug.Log("No save file found. Creating a new inventory.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load inventory: " + e.Message);
        }
    }
    public void AddChest()
    {
        if (items.Count >= GameManager.Singleton.slotChestMax)
        {
            Thongbao.Singleton.ShowThongbao("Không đủ chỗ trống để thêm rương hiện tại: " + GameManager.Singleton.slotChestMax);
            return;
        }

        if (item == null || item.Length == 0 || item[0] == null)
        {
            Thongbao.Singleton.ShowThongbao("Không có item hợp lệ để thêm rương!");
            return;
        }

        if (quantity <= 0)
        {
            Thongbao.Singleton.ShowThongbao("Số lượng phải lớn hơn 0.");
            return;
        }

        if (star < 0)
        {
            Thongbao.Singleton.ShowThongbao("Số sao không hợp lệ.");
            return;
        }

        AddItemChest(item[0], quantity, status, star,level,dame,hp,mp,chimang,hutki,hutmau,ne,solanepsao,doben);
        ChestInventory.Singleton.UpdateItem();
        chestUI.UpdateItem();
        //try
        //{
            

        //}
        //catch (Exception ex)
        //{
        //    Debug.LogError("Lỗi khi thêm rương: " + ex.Message);
        //}
    }

    public void LayChest()
    {
        Inventory.Singleton.AddThaoItem(item[0], quantity, status, star,level,dame,hp,mp,chimang,hutki,hutmau,ne,solanepsao,doben);
        RemoveItemChest(vitriitem,quantity);
        //chestUI.UpdateItem();
        ChestInventory.Singleton.UpdateItem();
        chestUI.UpdateItem();
        SaveChanges();
    }

    public string AddItemChest(Item item, int quantity, string status, int star,int level,int dame,int hp,int mp,int chimang,int hutki,int hutmau,float ne,int solanepsao,int doben)
    {
        if (item == null)
        {
            return "Item không hợp lệ!";
        }

        // Tìm slot có thể cộng dồn chỉ khi quantity >= 2
        if (quantity >= 2)
        {
            foreach (ChestSlot slot in items)
            {
                if (slot.item == item && slot.quantity < item.maxStack)
                {
                    int spaceLeft = item.maxStack - slot.quantity;
                    int addQuantity = Mathf.Min(spaceLeft, quantity);

                    slot.quantity += addQuantity;
                    quantity -= addQuantity;
                    slot.status = status;
                    slot.dame = dame;
                    slot.level = level;
                    slot.hp = hp;
                    slot.mp = mp;
                    slot.hutki = hutki;
                    slot.hutmau = hutmau;
                    slot.chimang = chimang;
                    slot.ne = ne;
                    slot.doben = doben;
                    slot.solanepsao = solanepsao;

                    Inventory.Singleton.RemoveItem1(vitriitem, addQuantity);

                    if (quantity <= 0)
                    {
                        SaveChanges();
                        return "Thêm thành công!";
                    }
                }
            }
        }

        // Tìm slot trống để thêm item mới
        foreach (ChestSlot slot in items)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.quantity = Mathf.Min(quantity, item.maxStack);
                slot.status = status;
                slot.dame = dame;
                slot.level = level;
                slot.hp = hp;
                slot.mp = mp;
                slot.hutki = hutki;
                slot.hutmau = hutmau;
                slot.chimang = chimang;
                slot.ne = ne;
                slot.doben = doben;
                slot.solanepsao = solanepsao;
                quantity -= slot.quantity;
                Inventory.Singleton.RemoveItem1(vitriitem, slot.quantity);

                if (quantity <= 0)
                {
                    SaveChanges();
                    return "Thêm thành công!";
                }
            }
        }

        // Nếu không còn slot phù hợp, tạo slot mới cho item
        while (quantity > 0)
        {
            int addQuantity = Mathf.Min(quantity, item.maxStack);

            ChestSlot newSlot = new ChestSlot
            {
                item = item,
                quantity = addQuantity,
                level = level,
                status = status,
                stars = star,
                dame = dame,
                hp = hp,
                mp = mp,
                hutki = hutki,
                hutmau = hutmau,
                chimang = chimang,
                ne = ne,
                doben = doben,
                solanepsao = solanepsao,

            };

            items.Add(newSlot);
            quantity -= addQuantity;
            Inventory.Singleton.RemoveItem1(vitriitem, addQuantity);
        }

        // Lưu thay đổi và cập nhật giao diện
        SaveChanges();
        return "Thêm thành công!";
    }

    // Phương thức lưu và tải dữ liệu
    private void SaveChanges()
    {
        Inventory.Singleton.SaveInventory();
        Inventory.Singleton.LoadInventory();
        SaveChest();
        LoadChest();
    }
    public string RemoveItemChest(int slotIndex, int quantity)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            return "Vị trí không hợp lệ!";
        }

        ChestSlot slot = items[slotIndex];

        if (slot.item == null)
        {
            return "Không có item nào trong slot này!";
        }

        if (slot.quantity >= quantity)
        {
            //slot.quantity -= quantity;
            slot.quantity = 0;

            // Nếu số lượng về 0, reset slot
            if (slot.quantity == 0)
            {
                slot.item = null;
                slot.stars = 0;
                
            }

            SaveChanges(); // Lưu lại trạng thái inventory
            //Thongbao.Singleton.ShowThongbao($"Vứt thành công.");
            ChestInventory.Singleton.UpdateItem();
            LoadChest();
            return "Xóa item thành công!";
        }
        else
        {
            return "Số lượng không đủ để xóa!";
        }
    }
    public void BuySlotChest()
    {
        // Kiểm tra tham chiếu cần thiết
        if (GameManager.Singleton == null || Thongbao.Singleton == null || txtSlotBuychest == null)
        {
            Debug.LogError("Tham chiếu bị thiếu! Kiểm tra GameManager, Thongbao hoặc txtSlotBuychest.");
            return;
        }

        // Kiểm tra nếu đã đạt giới hạn
        if (GameManager.Singleton.currentSlotBuy >= GameManager.Singleton.slotChestMax)
        {
            Thongbao.Singleton.ShowThongbao("Bạn đã đạt giới hạn mở rộng tối đa!");
            return;
        }

        // Tính toán chi phí và kiểm tra vàng
        int cost = CalculateSlotCost();
        if (GameManager.Singleton.gold < cost)
        {
            Thongbao.Singleton.ShowThongbao("Không đủ vàng để mua thêm slot!");
            return;
        }

        // Trừ vàng và tăng số slot
        GameManager.Singleton.gold -= cost;
        GameManager.Singleton.currentSlotBuy++;

        // Cập nhật UI
        txtSlotBuychest.text = $"Slot rương hiện tại: {GameManager.Singleton.currentSlotBuy}/{slotbuyMax}";

        // Lưu trạng thái sau khi mua
        SaveChanges();

        // Thông báo thành công
        Thongbao.Singleton.ShowThongbao("Đã mua thành công thêm 1 slot!");
    }
    
    private int CalculateSlotCost()
    {
        // Công thức tính giá: slot đầu 100 vàng, tăng thêm 50 mỗi slot
        return 100 + (GameManager.Singleton.currentSlotBuy * 50);
    }
}




// Dữ liệu lưu trữ cho một item
[System.Serializable]
public class ChestData
{
    public string itemName;
    public int quantity;
    public int level;
    public int dame; // cấp trang bị
    public int hp; // cấp trang bị
    public int mp; // cấp trang bị
    public int chimang; // cấp trang bị
    public int hutmau; // cấp trang bị
    public int hutki; // cấp trang bị
    public float ne; // cấp trang bị
    public int stars; // số sao sở hữu
    public int doben; // độ bền sở hữu
    public int solanepsao; // số sao sở hữu
    public string status; // số sao sở hữu

}


// Dữ liệu lưu trữ toàn bộ chest
[System.Serializable]
public class ChestSaveData
{
    public List<ChestData> inventory = new List<ChestData>();
}

// Dữ liệu của một slot trong chest
[System.Serializable]
public class ChestSlot
{
    public Item item;
    public int quantity;
    public int level; // số sao sở hữu
    public int dame; // cấp trang bị
    public int hp; // cấp trang bị
    public int mp; // cấp trang bị
    public int chimang; // cấp trang bị
    public int hutmau; // cấp trang bị
    public int hutki; // cấp trang bị
    public float ne; // cấp trang bị
    public int stars; // số sao sở hữu
    public int doben; // độ bền sở hữu
    public int solanepsao; // số sao sở hữu
    public string status; // số sao sở hữu


}
