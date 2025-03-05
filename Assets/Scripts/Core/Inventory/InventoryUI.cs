using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Singleton;
    public Inventory inventory;          // Tham chiếu tới inventory
    public GameObject slotPrefab;        // Prefab cho mỗi ô trong inventory
    public Transform content;            // Gốc chứa các slot (dùng ScrollView Content)
    public bool[] slot;                  // Nơi chứa trạng thái các slot (true = đã có item, false = trống)
    public GameObject targetItem;
    public Item[] item; // Mảng Item
    public int vitriitem = 0;
    public int star = 0;
    public SlotName slotName;
    public string slotname;
    public EquipmentManager equipmentManager;
    public EquipmentUIManager equipmentUIManager;
    public Button[] categoryButtons;        // Mảng các button danh mục
    public Text txtNhacnho;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }
    
    private void Start()
    {
        //FilterItemsBySlot(SlotName.ao);
    }
    private void OnEnable()
    {
        //Gắn sự kiện cho các button danh mục
        foreach (var button in categoryButtons)
        {
            if (System.Enum.TryParse(button.name, out SlotName slotNameFilter))
            {
                button.onClick.AddListener(() => FilterItemsBySlot(slotNameFilter));
            }
            else
            {
                Debug.LogError($"Tên nút không khớp với SlotName: {button.name}");
            }
        }

        InitializeSlots();
        UpdateUIAll();
        //UpdateItem();
    }
    // Lọc vật phẩm theo SlotName
    private void FilterItemsBySlot(SlotName filterSlot)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // Lọc item theo slotName và kiểm tra null
        var filteredItems = inventory.items
            .Select((item, index) => new { Item = item, Index = index }) // Đính kèm index gốc
            .Where(pair => pair.Item != null && pair.Item.item != null && pair.Item.item.slotName == filterSlot) // Kiểm tra null
            .ToList();
        
        foreach (var pair in filteredItems)
        {
            AddItemToContent(pair.Item, content, pair.Index); // Truyền cả item và index gốc
        }

        //Debug.Log($"Đã lọc các vật phẩm theo danh mục: {filterSlot}");
    }

    public void Tatca()
    {
        InitializeSlots();
        UpdateUIAll();
    }


    // Thêm item vào content
    private void AddItemToContent(ItemSlot inventoryItem, Transform content, int index)
    {
        if (inventoryItem == null || inventoryItem.item == null)
        {
            Debug.LogWarning($"Bỏ qua ItemSlot null hoặc không có item tại vị trí {index}.");
            return;
        }

        GameObject newSlot = Instantiate(slotPrefab, content);
        ItemProfile itemProfile = newSlot.GetComponent<ItemProfile>();

        if (itemProfile == null)
        {
            Debug.LogError($"Slot prefab thiếu component ItemProfile. Kiểm tra prefab của bạn!");
            return;
        }

        SetupItemProfile(itemProfile, inventoryItem, index);
    }



    // Cài đặt thông tin item
    private void SetupItemProfile(ItemProfile itemProfile, ItemSlot currentItem, int index)
    {
        itemProfile.Icon.sprite = currentItem.item.icon;
        itemProfile.Icon.enabled = true;

        itemProfile.itemType = currentItem.item.itemType;
        itemProfile.description = currentItem.item.description;
        itemProfile.status = currentItem.status;

        // Gán đúng chỉ số của item trong danh sách inventory
        itemProfile.vitriitem = index;

        itemProfile.level = currentItem.level;
        itemProfile.dame = currentItem.dame;
        itemProfile.hp = currentItem.hp;
        itemProfile.mp = currentItem.mp;
        itemProfile.chimang = currentItem.chimang;
        itemProfile.hutki = currentItem.hutki;
        itemProfile.hutmau = currentItem.hutmau;
        itemProfile.ne = currentItem.ne;
        itemProfile.doben = currentItem.doben;
        itemProfile.solanepsao = currentItem.solanepsao;
        itemProfile.slotName = currentItem.item.slotName;

        itemProfile.LoaiItemType = currentItem.item.GetItemEffect();
        itemProfile.item.Add(currentItem.item);

        SetupRewards(itemProfile, currentItem.item);
        UpdateSlotState(itemProfile, currentItem);
    }



    // Thiết lập phần thưởng
    private void SetupRewards(ItemProfile itemProfile, Item item)
    {
        if (item.itemReward == null || item.itemReward.Length == 0) return;

        foreach (var reward in item.itemReward)
        {
            if (reward != null)
            {
                itemProfile.itemBoxReward.Add(reward);
            }
        }
    }

    // Cập nhật trạng thái slot
    private void UpdateSlotState(ItemProfile itemProfile, ItemSlot currentItem)
    {
        // Hiển thị số lượng
        itemProfile.txtQuantity.text = currentItem.quantity > 1
            ? currentItem.quantity.ToString()
            : "";
        itemProfile.quantity = currentItem.quantity;

        // Kiểm tra cấp độ yêu cầu
        itemProfile.levelyc = GameManager.Singleton.level < currentItem.item.level
            ? $"<color=red>Cấp độ yêu cầu {currentItem.item.level}</color>"
            : $"<color=black>Đạt cấp độ {currentItem.item.level} yêu cầu</color>";

        // Gắn sự kiện nhấn vào slot
        itemProfile.Click?.onClick.RemoveAllListeners();
        itemProfile.Click?.onClick.AddListener(() => itemProfile.OnItemClicked(currentItem));

        // Hiển thị số sao nếu là trang bị
        UpdateItemStars(itemProfile, currentItem);
    }

    // Hiển thị sao của item
    private void UpdateItemStars(ItemProfile itemProfile, ItemSlot currentItem)
    {
        if (currentItem.item.itemType == ItemType.Equipment)
        {
            int numberOfStars = currentItem.stars;
            itemProfile.stars = numberOfStars;

            for (int j = 0; j < itemProfile.starImages.Length; j++)
            {
                itemProfile.starImages[j].SetActive(j < numberOfStars);
            }
        }
        else
        {
            foreach (var starImage in itemProfile.starImages)
            {
                starImage.SetActive(false);
            }
        }
    }

    // Xử lý slot trống
    private void SetupEmptySlot(ItemProfile itemProfile,int index)
    {
        itemProfile.Icon.enabled = false;
        itemProfile.txtQuantity.text = "";
        itemProfile.vitriitem =index;

        foreach (var starImage in itemProfile.starImages)
        {
            starImage.SetActive(false);
        }
    }





    // Khởi tạo trạng thái của các slot
    private void InitializeSlots()
    {
        slot = new bool[inventory.items.Count]; // Đặt kích thước mảng bằng số item trong inventory
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i] = false; // Ban đầu, tất cả slot được đánh dấu là trống
        }
    }
   
    public void UpdateItem()
    {
        // Chức năng khác nếu cần
        //slot[0] = false;
        InitializeSlots();
       
        //UpdateUI();
        //FilterItemsBySlot(slotName);
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
    public void UpdateUIAll()
    {
        // Kiểm tra nếu inventory hoặc items chưa được khởi tạo
        if (inventory == null || inventory.items == null)
        {
            Debug.LogWarning("Inventory hoặc danh sách items chưa được khởi tạo!");
            return;
        }

        // Đảm bảo kích thước mảng slot khớp với số lượng items
        if (slot == null || slot.Length != inventory.items.Count)
        {
            slot = new bool[inventory.items.Count];
        }

        // Xóa tất cả slot cũ trong content
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        //foreach (var child in content)
        //{
        //    foreach (Transform subChild in child)
        //    {
        //        Destroy(subChild.gameObject);
        //    }
        //}


        // Tạo lại toàn bộ slot dựa trên inventory.items
        for (int i = 0; i < inventory.items.Count; i++)
        {
            // Tạo slot trong content
            GameObject newSlot = Instantiate(slotPrefab, content);
            ItemProfile itemProfile = newSlot.GetComponent<ItemProfile>();

            if (itemProfile == null)
            {
                Debug.LogError($"Slot prefab thiếu component ItemProfile. Kiểm tra prefab của bạn!");
                continue;
            }

            // Hiển thị thông tin item nếu slot có item
            if (inventory.items[i].item != null)
            {
                itemProfile.Icon.sprite = inventory.items[i].item.icon;
                itemProfile.Icon.enabled = true;

                itemProfile.itemType = inventory.items[i].item.itemType;
                itemProfile.description = inventory.items[i].item.description;
                itemProfile.itemType = inventory.items[i].item.itemType;
                itemProfile.status = inventory.items[i].status;
                itemProfile.level = inventory.items[i].level;
                itemProfile.dame = inventory.items[i].dame;
                itemProfile.hp = inventory.items[i].hp;
                itemProfile.mp = inventory.items[i].mp;
                itemProfile.chimang = inventory.items[i].chimang;
                itemProfile.hutki = inventory.items[i].hutki;
                itemProfile.hutmau = inventory.items[i].hutmau;
                itemProfile.ne = inventory.items[i].ne;
                itemProfile.solanepsao = inventory.items[i].solanepsao;
                itemProfile.doben = inventory.items[i].doben;
                itemProfile.slotName = inventory.items[i].item.slotName;

                itemProfile.LoaiItemType = inventory.items[i].item.GetItemEffect();
                itemProfile.item.Add(inventory.items[i].item);
                if (inventory.items[i].item.itemReward != null && inventory.items[i].item.itemReward.Length > 0) // Kiểm tra itemReward không null và có phần tử
                {
                    foreach (var reward in inventory.items[i].item.itemReward) // Lặp qua từng phần tử trong itemReward
                    {
                        if (reward != null) // Kiểm tra phần tử không null
                        {
                            itemProfile.itemBoxReward.Add(reward); // Thêm phần tử vào danh sách
                        }
                    }
                }
                else
                {
                    // Debug.Log($"Danh sách phần thưởng rỗng hoặc không tồn tại cho item tại index {i}.");
                }

                // Cập nhật vị trí item từ inventory (chỉ mục của item trong inventory)
                itemProfile.vitriitem = i;
                // Hiển thị số lượng nếu > 1
                itemProfile.txtQuantity.text = inventory.items[i].quantity > 1
                    ? inventory.items[i].quantity.ToString()
                    : "";
                itemProfile.quantity = inventory.items[i].quantity;

                if (GameManager.Singleton.level < inventory.items[i].item.level)
                {

                    itemProfile.levelyc = $"<color=red>Cấp độ yêu cầu {inventory.items[i].item.level}</color>";

                }
                else
                {
                    itemProfile.levelyc = $"<color=black>Đạt cấp độ {inventory.items[i].item.level} yêu cầu</color>";


                }
                // Gắn chức năng khi nhấn vào slot
                if (itemProfile.Click != null)
                {
                    int index = i; // Lưu giá trị index để sử dụng trong lambda
                    itemProfile.Click.onClick.AddListener(() => itemProfile.OnItemClicked(inventory.items[index]));
                }

                // Đánh dấu slot là đã có item
                slot[i] = true;

                // Hiển thị số sao nếu item là trang bị
                if (inventory.items[i].item.itemType == ItemType.Equipment)
                {
                    int numberOfStars = inventory.items[i].stars; // Giả sử bạn có thuộc tính "stars" trong item
                    itemProfile.stars = inventory.items[i].stars;
                    // Kích hoạt sao tương ứng với số sao của item
                    for (int j = 0; j < itemProfile.starImages.Length; j++)
                    {
                        itemProfile.starImages[j].SetActive(j < numberOfStars); // Kích hoạt số sao
                    }
                }
                else
                {
                    // Nếu không phải trang bị, ẩn tất cả các sao
                    foreach (var starImage in itemProfile.starImages)
                    {
                        starImage.SetActive(false);
                    }
                }
            }
            else
            {
                // Trường hợp slot trống
                itemProfile.Icon.enabled = false;
                itemProfile.txtQuantity.text = "";
                foreach (var starImage in itemProfile.starImages)
                {
                    starImage.SetActive(false); // Ẩn các sao khi không có item
                }
                slot[i] = false;
            }
        }

        //Debug.Log("Giao diện inventory đã được cập nhật.");
    }



    // Hàm xác định container phù hợp (nếu cần sử dụng nhiều loại inventory)
    private int GetSlotParentIndex(ItemSlot itemSlot)
    {
        // Xác định loại item và trả về index container phù hợp
        if (itemSlot.item != null)
        {
            if (itemSlot.item.itemType == ItemType.Consumable) return 1;
            if (itemSlot.item.itemType == ItemType.Equipment) return 2;
        }
        return 0; // Mặc định trả về main inventory
    }

    public void Hienthivut()
    {
        txtNhacnho.text = $"Bạn có chắc muốn vứt <color=red> {item[0].itemName} hay không?</color>";
    }
    public void RemoveItem()
    {
        inventory.RemoveItem(vitriitem,1);
        UpdateUIAll();
        Thongbao.Singleton.ShowThongbao($"Bạn đã vứt thành công <color=red>{item[0].itemName}</color>");
       
    }
    public void SellItem()
    {
        inventory.SellItem(vitriitem, 1, item[0].giaBan);
        UpdateUIAll();
        Thongbao.Singleton.ShowThongbao($"Bán thành công <color=yellow>{item[0].itemName}</color>");

    }
    public void EquipItem()
    {
        equipmentManager.EquipItem(item[0],slotName.ToString());
        equipmentUIManager.UpdateEquipmentUI(); // Làm mới UI
        
        
    }

}                                                     
