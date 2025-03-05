using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestUI : MonoBehaviour
{
    public static ChestUI Singleton;
    public ChestManager chestManager;          // Tham chiếu tới inventory
    public GameObject slotPrefab;        // Prefab cho mỗi ô trong inventory
    public Transform content;            // Gốc chứa các slot (dùng ScrollView Content)
    public bool[] slot;                  // Nơi chứa trạng thái các slot (true = đã có item, false = trống)
    public GameObject targetItem;
    public Item[] item; // Mảng Item
    public int vitriitem = 0;
    public int star = 0;
    public SlotName slotName;
    public EquipmentManager equipmentManager;
    public EquipmentUIManager equipmentUIManager;
    public ScrollViewPagination scrollViewPaginationInventory;
    public Text txtslotChest; // Text hiển thị thông tin rương đồ
    public Text txtslotInventory; // Text hiển thị thông tin túi đồ
    public Color normalColor = Color.white; // Màu chữ mặc định
    public Color warningColor = Color.red; // Màu chữ cảnh báo

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    private void OnEnable()
    {
        InitializeSlots();
        UpdateUI();
    }

    // Khởi tạo trạng thái của các slot
    private void InitializeSlots()
    {
        slot = new bool[chestManager.items.Count]; // Đặt kích thước mảng bằng số item trong inventory
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i] = false; // Ban đầu, tất cả slot được đánh dấu là trống
        }
    }

    public void UpdateItem()
    {
        // Chức năng khác nếu cần
       // slot[0] = false;
        InitializeSlots();

        UpdateUI();
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
    public void UpdateUI()
    {
        // Kiểm tra nếu inventory hoặc items chưa được khởi tạo
        if (chestManager == null || chestManager.items == null)
        {
            Debug.LogWarning("Inventory hoặc danh sách items chưa được khởi tạo!");
            return;
        }

        // Đảm bảo kích thước mảng slot khớp với số lượng items
        if (slot == null || slot.Length != chestManager.items.Count)
        {
            slot = new bool[chestManager.items.Count];
        }

        // Xóa tất cả slot cũ trong content
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }


        // Tạo lại toàn bộ slot dựa trên inventory.items
        for (int i = 0; i < chestManager.items.Count; i++)
        {
            // Tạo slot trong content
            GameObject newSlot = Instantiate(slotPrefab, content);
            ItemProfile itemProfile = newSlot.GetComponent<ItemProfile>();
            ChestInfo chest = newSlot.GetComponent<ChestInfo>();

            if (itemProfile == null)
            {
                Debug.LogError($"Slot prefab thiếu component ItemProfile. Kiểm tra prefab của bạn!");
                continue;
            }

            // Hiển thị thông tin item nếu slot có item
            if (chestManager.items[i].item != null)
            {
                itemProfile.Icon.sprite = chestManager.items[i].item.icon;
                itemProfile.Icon.enabled = true;
                chest.txtNamechest.text = chestManager.items[i].item.itemName;
                chest.txtParama.text = chestManager.items[i].item.GetItemEffect() + chestManager.items[i].item.description;

                itemProfile.itemType = chestManager.items[i].item.itemType;
                itemProfile.description = chestManager.items[i].item.description;
                itemProfile.itemType = chestManager.items[i].item.itemType;
                itemProfile.status = chestManager.items[i].status;
                itemProfile.stars = chestManager.items[i].stars;
                itemProfile.level = chestManager.items[i].level;
                itemProfile.dame = chestManager.items[i].dame;
                itemProfile.hp = chestManager.items[i].hp;
                itemProfile.mp = chestManager.items[i].mp;
                itemProfile.chimang = chestManager.items[i].chimang;
                itemProfile.hutki = chestManager.items[i].hutki;
                itemProfile.hutmau = chestManager.items[i].hutmau;
                itemProfile.ne = chestManager.items[i].ne;
                itemProfile.solanepsao = chestManager.items[i].solanepsao;
                itemProfile.doben = chestManager.items[i].doben;
                itemProfile.slotName = chestManager.items[i].item.slotName;

                itemProfile.LoaiItemType = chestManager.items[i].item.GetItemEffect();
                itemProfile.item.Add(chestManager.items[i].item);
                // Cập nhật vị trí item từ inventory (chỉ mục của item trong inventory)
                itemProfile.vitriitem = i;
                itemProfile.quantity = chestManager.items[i].quantity;
                // Hiển thị số lượng nếu > 1
                itemProfile.txtQuantity.text = chestManager.items[i].quantity > 1
                    ? chestManager.items[i].quantity.ToString()
                    : "";

                if (GameManager.Singleton.level < chestManager.items[i].item.level)
                {

                    itemProfile.levelyc = $"<color=red>\u26A1 Cấp độ yêu cầu {chestManager.items[i].item.level}</color>";

                }
                else
                {
                    itemProfile.levelyc = $"\u26A1 <color=black>Đạt cấp độ {chestManager.items[i].item.level} yêu cầu</color>";


                }
                // Gắn chức năng khi nhấn vào slot
                if (itemProfile.Click != null)
                {
                    int index = i; // Lưu giá trị index để sử dụng trong lambda
                    itemProfile.Click.onClick.AddListener(() => itemProfile.OnItemClickedLayChest(chestManager.items[index]));
                }

                // Đánh dấu slot là đã có item
                slot[i] = true;

                // Hiển thị số sao nếu item là trang bị
                if (chestManager.items[i].item.itemType == ItemType.Equipment)
                {
                    int numberOfStars = chestManager.items[i].stars; // Giả sử bạn có thuộc tính "stars" trong item
                    itemProfile.stars = chestManager.items[i].stars;
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
        scrollViewPaginationInventory.ReloadItems();
        scrollViewPaginationInventory.UpdatePagination();
        scrollViewPaginationInventory.PreviousPage();
        UpdateChestUI();
    }

    public void UpdateChestUI()
    {
        // Kiểm tra nếu các tham chiếu cần thiết chưa được gán
        if (txtslotChest == null || chestManager == null || GameManager.Singleton == null)
        {
            Debug.LogError("Tham chiếu chưa được gán đầy đủ!");
            return;
        }

        // Lấy số lượng rương hiện tại và giới hạn
        int currentCount = chestManager.items.Count;
        int maxCount = GameManager.Singleton.slotChestMax + GameManager.Singleton.currentSlotBuy;

        // Cập nhật text hiển thị
        txtslotChest.text = $"[Rương đồ] Chỗ chứa hiện tại: {currentCount}/{maxCount} ";
        txtslotInventory.text = $"[Túi đồ] đang sở hữu vật phẩm hiện tại: {Inventory.Singleton.items.Count}";

        // Nếu số lượng rương gần đạt giới hạn, chuyển màu sang đỏ
        if (currentCount >= maxCount)
        {
            txtslotChest.color = warningColor;

            // Hiển thị cảnh báo hoặc gợi ý nâng cấp
            Thongbao.Singleton.ShowThongbao("Chỗ chứa đã đầy! Hãy nâng cấp rương đồ để chứa thêm.");
        }
        else if (currentCount >= maxCount * 0.8f) // Nếu đạt 80% giới hạn, cảnh báo trước
        {
            txtslotChest.color = warningColor;
            Thongbao.Singleton.ShowThongbao("Sắp hết chỗ chứa, hãy nâng cấp rương đồ!");
        }
        else
        {
            txtslotChest.color = normalColor; // Trở về màu bình thường
        }
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

    //public void RemoveItem()
    //{
    //    chestManager.RemoveItem(vitriitem, 1);
    //    UpdateItem();
    //    scrollViewPaginationInventory.ReloadItems();
    //    scrollViewPaginationInventory.UpdatePagination();
    //}
    public void EquipItem()
    {
        //equipmentManager.EquipItem(item[0], slotName.ToString());
        //equipmentUIManager.UpdateEquipmentUI(); // Làm mới UI
        //scrollViewPaginationInventory.ReloadItems();
       // scrollViewPaginationInventory.UpdatePagination();

    }
}
