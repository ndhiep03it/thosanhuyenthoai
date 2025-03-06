using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public List<ItemSlot> items = new List<ItemSlot>(); // Danh sách các ô trong inventory

    private string saveFilePath;
    public string Itemid;
    public InventoryUI inventoryUI;
   

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
        saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
        LoadInventory(); // Tải dữ liệu Inventory khi bắt đầu
    }

    // Thêm item vào inventory
    // Thêm item vào inventory với xử lý lỗi
    // Phương thức tạo số sao ngẫu nhiên cho item
    public int RandomStars(int maxStars)
    {
        // Đảm bảo maxStars nằm trong khoảng hợp lệ
        maxStars = Mathf.Clamp(maxStars, 0, 7);

        // Các tỷ lệ phần trăm cho mỗi số sao (từ 0 đến 7)
        float[] probabilities = new float[maxStars + 1];

        // Thiết lập tỷ lệ cho từng sao
        probabilities[0] = 0.50f; // 0 sao: 50%
        probabilities[1] = 0.15f; // 1 sao: 15%
        probabilities[2] = 0.15f; // 2 sao: 15%
        probabilities[3] = 0.15f; // 3 sao: 15%
        probabilities[4] = 0.15f; // 4 sao: 15%
        probabilities[5] = 0.10f; // 5 sao: 10%
        probabilities[6] = 0.10f; // 6 sao: 10%
        probabilities[7] = 0.10f; // 7 sao: 10%

        // Chuẩn hóa xác suất nếu maxStars < 7
        float totalProbability = 0f;
        for (int i = 0; i <= maxStars; i++)
        {
            totalProbability += probabilities[i];
        }

        // Điều chỉnh xác suất để tổng bằng 1
        if (totalProbability > 0f)
        {
            for (int i = 0; i <= maxStars; i++)
            {
                probabilities[i] /= totalProbability;
            }
        }

        // Tạo một số ngẫu nhiên giữa 0 và 1 để chọn sao
        float randomValue = UnityEngine.Random.value;

        // Chọn số sao dựa trên tỷ lệ phần trăm
        float cumulativeProbability = 0f;
        for (int i = 0; i <= maxStars; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                return i; // Trả về số sao (từ 0 đến maxStars)
            }
        }

        // Trường hợp mặc định (nếu có gì đó sai sót)
        return maxStars;
    }




    public string AddItem(Item item, int quantity,string status)
    {
        if (item == null)
        {
            return "Item không hợp lệ!";
        }
        items.Add(new ItemSlot()); // Thêm slot trống mới
        // Tìm slot có thể cộng dồn
        foreach (ItemSlot slot in items)
        {
            if (slot.item == item && slot.quantity < item.maxStack)
            {

                int spaceLeft = item.maxStack - slot.quantity;
                int addQuantity = Mathf.Min(spaceLeft, quantity);

                slot.quantity += addQuantity;
                quantity -= addQuantity;
                slot.status = status;
                slot.doben = 100;

                if (quantity <= 0)
                {
                    SaveInventory(); // Lưu sau khi thêm item
                    LoadInventory();
                    return "Thêm thành công!";
                }
            }
        }

        // Tìm slot trống để thêm item mới
        foreach (ItemSlot slot in items)
        {
            if (slot.item == null)
            {
                items.Add(new ItemSlot()); // Thêm slot trống mới
                slot.item = item;
                slot.quantity = Mathf.Min(quantity, item.maxStack);

                // Nếu item là trang bị, áp dụng số sao ngẫu nhiên
                if (item.itemType == ItemType.Equipment)
                {
                    slot.stars = RandomStars(7); // Tạo số sao ngẫu nhiên cho trang bị
                }
                else
                {
                    slot.stars = 0; // Item không phải trang bị không cần số sao
                }
                slot.status = status;
                quantity -= slot.quantity;
                slot.doben = 1000;

                if (quantity <= 0)
                {
                    SaveInventory(); // Lưu sau khi thêm item
                    LoadInventory();
                    return "Thêm thành công!";
                }
            }
        }
        // Nếu tất cả slot đều đầy, kiểm tra nếu cần mở rộng inventory
        // Nếu không có slot trống, mở rộng inventory và thêm item vào
        if (quantity > 0)
        {
            //ExpandInventory(1);  // Mở rộng inventory
           
            return "Inventory đã đầy, không thể thêm item!";
        }
        InventoryUI.Singleton.UpdateItem();

       
        return "Thêm thành công!";
       
       
    }
    public string AddThaoItem(Item item, int quantity, string status, int star, int level, int damage, int hp, int mp, int chimang, int lifesteal, int manasteal, float ne, int solanepsao, int doben)
    {
        if (item == null)
        {
            return "Item không hợp lệ!";
        }

        List<ItemSlot> tempSlots = new List<ItemSlot>();

        // Xử lý item: cộng dồn hoặc thêm mới vào danh sách tạm thời
        while (quantity > 0)
        {
            ItemSlot existingSlot = tempSlots.FirstOrDefault(s => s.item == item && s.quantity < item.maxStack);
            if (existingSlot != null)
            {
                int spaceLeft = item.maxStack - existingSlot.quantity;
                int addQuantity = Mathf.Min(spaceLeft, quantity);
                existingSlot.quantity += addQuantity;
                quantity -= addQuantity;
                UpdateItemSlot(existingSlot, status, star, level, damage, hp, mp, chimang, lifesteal, manasteal, ne, solanepsao, doben);
            }
            else
            {
                int addQuantity = Mathf.Min(quantity, item.maxStack);
                ItemSlot newSlot = new ItemSlot
                {
                    item = item,
                    quantity = addQuantity
                };
                UpdateItemSlot(newSlot, status, star, level, damage, hp, mp, chimang, lifesteal, manasteal, ne, solanepsao, doben);
                tempSlots.Add(newSlot);
                quantity -= addQuantity;
            }
        }

        // Thêm các item đã xử lý vào inventory chính thức
        foreach (ItemSlot slot in tempSlots)
        {
            items.Add(slot);
        }

        SaveAndLoadInventory();
        inventoryUI.UpdateItem();
        return "Thêm thành công!";
    }


    // Hàm cập nhật thông tin cho slot item
    private void UpdateItemSlot(ItemSlot slot, string status, int star, int level, int damage, int hp, int mp, int chimang, int lifesteal, int manasteal, float ne, int solanepsao, int doben)
    {
        slot.status = status;
        slot.stars = star;
        slot.level = level;
        slot.dame = damage;
        slot.hp = hp;
        slot.mp = mp;
        slot.chimang = chimang;
        slot.hutki = lifesteal;
        slot.hutmau = manasteal;
        slot.ne = ne;
        slot.solanepsao = solanepsao;
        slot.doben = doben;
    }

    // Hàm lưu và tải inventory
    private void SaveAndLoadInventory()
    {
        SaveInventory();
        LoadInventory();
    }

    public string BuyItem(Item item, int quantity, string status,int level,int doben)
    {
        if (item == null)
        {
            return "Item không hợp lệ!";
        }

        // Tìm slot có thể cộng dồn
        foreach (ItemSlot slot in items)
        {
            if (slot.item == item && slot.quantity < item.maxStack)
            {
                int spaceLeft = item.maxStack - slot.quantity;
                int addQuantity = Mathf.Min(spaceLeft, quantity);

                slot.quantity += addQuantity;
                quantity -= addQuantity;
                slot.status = status;
                slot.level = level;
                slot.doben = doben;

                if (quantity <= 0)
                {
                    SaveInventory(); // Lưu sau khi thêm item
                    inventoryUI.UpdateItem();
                    return "Thêm thành công!";
                }
            }
        }

        // Nếu không thể cộng dồn, tìm slot trống để thêm item mới
        foreach (ItemSlot slot in items)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.quantity = Mathf.Min(quantity, item.maxStack);
                quantity -= slot.quantity;
                slot.status = status;
                slot.level = level;
                slot.doben = doben;
                if (quantity <= 0)
                {
                    SaveInventory(); // Lưu sau khi thêm item
                    inventoryUI.UpdateItem();
                    return "Thêm thành công!";
                }
            }
        }

        // Nếu không còn slot trống và cần thêm item, tạo slot mới
        while (quantity > 0)
        {
            int addQuantity = Mathf.Min(quantity, item.maxStack);

            ItemSlot newSlot = new ItemSlot
            {
                item = item,
                quantity = addQuantity,
                status = status,
                level = level,
                doben = doben
        };

            items.Add(newSlot);
            quantity -= addQuantity;
        }

        // Lưu lại và cập nhật giao diện
        SaveInventory();
        inventoryUI.UpdateItem();
        Debug.Log(item.itemName);
        return "Thêm thành công!";
    }
    public void ExpandInventory(int additionalSlots)
    {
        for (int i = 0; i < additionalSlots; i++)
        {
            items.Add(new ItemSlot()); // Thêm slot trống mới
        }

        SaveInventory(); // Lưu lại trạng thái Inventory
       
        LoadInventory();
       // Debug.Log($"Inventory đã được mở rộng thêm {additionalSlots} slot.");
    }
    public void SaveInventory()
    {
        try
        {
            List<InventoryData> dataToSave = new List<InventoryData>();

            foreach (ItemSlot slot in items)
            {
                if (slot.item != null)
                {
                    InventoryData data = new InventoryData
                    {
                        itemName = slot.item.name,
                        quantity = slot.quantity ,
                        level = slot.level,
                        dame = slot.dame,
                        hp = slot.hp,
                        mp = slot.mp,
                        chimang = slot.chimang,
                        hutmau = slot.hutmau,
                        hutki = slot.hutki,
                        ne = slot.ne,
                        stars = slot.stars ,
                        status = slot.status,
                        solanepsao = slot.solanepsao,
                        doben = slot.doben,

                    };
                    dataToSave.Add(data);
                }
            }

            string json = JsonUtility.ToJson(new InventorySaveData { inventory = dataToSave }, true);

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
    public string RemoveItem(int slotIndex, int quantity)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            return "Vị trí không hợp lệ!";
        }

        ItemSlot slot = items[slotIndex];

        if (slot.item == null)
        {
            return "Không có item nào trong slot này!";
        }
        
        if (slot.quantity >= quantity)
        {
            //slot.quantity -= quantity;
            slot.quantity = 0;

            // Nếu số lượng về 0, reset slot
            if (slot.quantity <= 0)
            {
                slot.item = null;
                slot.stars = 0;
               // slot.level = 1;
            }

            SaveInventory(); // Lưu lại trạng thái inventory
            Thongbao.Singleton.ShowThongbao($"Vứt thành công.");
            inventoryUI.UpdateItem();
            LoadInventory();
            return "Xóa item thành công!";
        }
        else
        {
            return "Số lượng không đủ để xóa!";
        }
    }
    public string NangcapItem(int slotIndex, int quantity)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            return "Vị trí không hợp lệ!";
        }

        ItemSlot slot = items[slotIndex];

        if (slot.item == null)
        {
            return "Không có item nào trong slot này!";
        }

        if (slot.quantity >= quantity)
        {
            //slot.quantity -= quantity;
            slot.quantity = 0;

            // Nếu số lượng về 0, reset slot
            if (slot.quantity <= 0)
            {
                slot.item = null;
                slot.stars = 0;
                // slot.level = 1;
            }

            SaveInventory(); // Lưu lại trạng thái inventory
            Thongbao.Singleton.ShowThongbao($"Nâng cấp thành công.");
            inventoryUI.UpdateItem();
            LoadInventory();
            return "Xóa item thành công!";
        }
        else
        {
            return "Số lượng không đủ để xóa!";
        }
    }
    public string RemoveItem1(int slotIndex, int quantity)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            return "Vị trí không hợp lệ!";
        }

        ItemSlot slot = items[slotIndex];

        if (slot.item == null)
        {
            return "Không có item nào trong slot này!";
        }

        if (slot.quantity >= quantity)
        {
            //slot.quantity -= quantity;
            slot.quantity = 0;
            quantity = 0;

            // Nếu số lượng về 0, reset slot
            if (slot.quantity == 0)
            {
                slot.item = null;
                slot.stars = 0;
              //  slot.level = 1;
            }

            SaveInventory(); // Lưu lại trạng thái inventory
            //Thongbao.Singleton.ShowThongbao($"Vứt thành công.");
            inventoryUI.UpdateItem();
            LoadInventory();
            return "Xóa item thành công!";
        }
        else
        {
            return "Số lượng không đủ để xóa!";
        }
    }
    public string UnRemoveItem(int slotIndex, int quantity)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            return "Vị trí không hợp lệ!";
        }

        ItemSlot slot = items[slotIndex];

        if (slot.item == null)
        {
            return "Không có item nào trong slot này!";
        }

        if (slot.quantity >= quantity)
        {
            //slot.quantity -= quantity;
            slot.quantity -= quantity;

            // Nếu số lượng về 0, reset slot
            if (slot.quantity == 0 )
            {
                slot.item = null;
                slot.stars = 0;
               // slot.level = level;
            }

            SaveInventory(); // Lưu lại trạng thái inventory         
            inventoryUI.UpdateItem();
            LoadInventory();
            return "Xóa item thành công!";
        }
        else
        {
            return "Số lượng không đủ để xóa!";
        }
    }
    public string SellItem(int slotIndex, int quantity,int giaban)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            return "Vị trí không hợp lệ!";
        }

        ItemSlot slot = items[slotIndex];

        if (slot.item == null)
        {
            return "Không có item nào trong slot này!";
        }

        if (slot.quantity >= quantity)
        {
            slot.quantity -= quantity;

            // Nếu số lượng về 0, reset slot
            if (slot.quantity == 0)
            {
                slot.item = null;
                slot.stars = 0;
            }

            SaveInventory(); // Lưu lại trạng thái inventory            
            //ShopInventory.Singleton.UpdateItem();
            LoadInventory();
            GameManager.Singleton.gold += giaban;
            Thongbao.Singleton.ShowThongbao($"Bán thành công.");
            return "Xóa item thành công!";
        }
        else
        {
            return "Số lượng không đủ để xóa!";
        }
    }
    //// Lưu dữ liệu Inventory vào file JSON
    //public void SaveInventory()
    //{
    //    try
    //    {
    //        List<InventoryData> dataToSave = new List<InventoryData>();

    //        foreach (ItemSlot slot in items)
    //        {
    //            if (slot.item != null)
    //            {
    //                InventoryData data = new InventoryData
    //                {
    //                    itemName = slot.item.name,
    //                    quantity = slot.quantity
    //                };
    //                dataToSave.Add(data);
    //            }
    //        }

    //        string json = JsonUtility.ToJson(new InventorySaveData { inventory = dataToSave }, true);
    //        File.WriteAllText(saveFilePath, json);

    //        Debug.Log("Inventory saved successfully to " + saveFilePath);
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.LogError("Failed to save inventory: " + e.Message);
    //    }
    //}

    public void LoadInventory()
    {
       // viewPaginationEquipment.ReloadItems();
        
        
        
        try
        {
            if (File.Exists(saveFilePath))
            {
                string encryptedJson = File.ReadAllText(saveFilePath);

                // Giải mã dữ liệu trước khi xử lý
                string json = EncryptionUtility.Decrypt(encryptedJson);

                InventorySaveData loadedData = JsonUtility.FromJson<InventorySaveData>(json);

                // Reset inventory
                items.Clear();

                foreach (InventoryData data in loadedData.inventory)
                {
                    // Tìm item từ Resources
                    Item item = Resources.Load<Item>($"Items/{data.itemName}");

                    if (item != null)
                    {
                        items.Add(new ItemSlot { item = item, quantity = data.quantity ,level = data.level, dame = data.dame, hp = data.hp, mp = data.mp
                        ,chimang = data.chimang,hutmau = data.hutmau,hutki = data.hutki, ne = data.ne, stars = data.stars,status = data.status,solanepsao = data.solanepsao, doben = data.doben});
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

    // Tải dữ liệu Inventory từ file JSON
    //public void LoadInventory()
    //{
    //    try
    //    {
    //        if (File.Exists(saveFilePath))
    //        {
    //            string json = File.ReadAllText(saveFilePath);
    //            InventorySaveData loadedData = JsonUtility.FromJson<InventorySaveData>(json);

    //            // Reset inventory
    //            items.Clear();

    //            foreach (InventoryData data in loadedData.inventory)
    //            {
    //                // Tìm item từ Resources
    //                Item item = Resources.Load<Item>($"Items/{data.itemName}");

    //                if (item != null)
    //                {
    //                    items.Add(new ItemSlot { item = item, quantity = data.quantity });
    //                }
    //                else
    //                {
    //                    Debug.LogWarning($"Item not found: {data.itemName}");
    //                }
    //            }

    //            Debug.Log("Inventory loaded successfully from " + saveFilePath);
    //        }
    //        else
    //        {
    //            Debug.Log("No save file found. Creating a new inventory.");
    //        }
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.LogError("Failed to load inventory: " + e.Message);
    //    }
    //}

    // Phương thức tìm item trong inventory theo tên
    public Item GetItemByName(string itemName)
    {
        // Kiểm tra nếu itemName hợp lệ
        if (string.IsNullOrEmpty(itemName)) return null;

        // Duyệt qua tất cả các ô trong inventory
        foreach (ItemSlot slot in items)
        {
            // Kiểm tra xem item trong slot có tên giống với itemName không
            if (slot.item != null && slot.item.name == itemName)
            {
                return slot.item; // Trả về item tìm thấy
            }
        }

        // Nếu không tìm thấy item, trả về null
       // Debug.LogWarning($"Item with name {itemName} not found in inventory.");
        return null;
    }

}

// Dữ liệu lưu trữ cho một item
[System.Serializable]
public class InventoryData
{
    public string itemName;
    public int quantity;
    public int level; // cấp trang bị
    public int dame; // cấp trang bị
    public int hp; // cấp trang bị
    public int mp; // cấp trang bị
    public int chimang; // cấp trang bị
    public int hutmau; // cấp trang bị
    public int hutki; // cấp trang bị
    public float ne; // cấp trang bị
    public int stars; // số sao sở hữu
    public string status; // số sao sở hữu
    public int solanepsao; // số sao sở hữu
    public int doben; //độ bền


}

// Dữ liệu lưu trữ toàn bộ inventory
[System.Serializable]
public class InventorySaveData
{
    public List<InventoryData> inventory = new List<InventoryData>();
}

// Dữ liệu của một slot trong inventory
[System.Serializable]
public class ItemSlot
{
    public Item item;
    public int quantity;
    public int level; // cấp trang bị
    public int dame; // cấp trang bị
    public int hp; // cấp trang bị
    public int mp; // cấp trang bị
    public int chimang; // cấp trang bị
    public int hutmau; // cấp trang bị
    public int hutki; // cấp trang bị
    public float ne; // né trang bị
    public int stars; // số sao sở hữu
    public int doben; //độ bền của vật phẩm
    public string status; // số sao sở hữu
    public int solanepsao; // số sao sở hữu


}


