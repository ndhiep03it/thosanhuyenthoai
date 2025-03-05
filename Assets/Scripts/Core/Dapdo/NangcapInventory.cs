using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NangcapInventory : MonoBehaviour
{
    public static NangcapInventory Singleton;
    public GameObject slotPrefab;        // Prefab cho mỗi ô trong inventory
    public Transform content;            // Gốc chứa các slot (dùng ScrollView Content)
    public bool[] slot;                  // Nơi chứa trạng thái các slot (true = đã có item, false = trống)
    private GameObject targetItem;
    public Item[] item; // Mảng Item
    public int giaban = 0;
    public int vitriitem = 0;
    public int level = 0;
    public Text txtStatus;
    public EpchisoUI epchisoUI;


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
        slot = new bool[Inventory.Singleton.items.Count]; // Đặt kích thước mảng bằng số item trong inventory
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
        if (Inventory.Singleton == null || Inventory.Singleton.items == null)
        {
            Debug.LogWarning("Inventory hoặc danh sách items chưa được khởi tạo!");
            return;
        }

        // Đảm bảo kích thước mảng slot khớp với số lượng items
        if (slot == null || slot.Length != Inventory.Singleton.items.Count)
        {
            slot = new bool[Inventory.Singleton.items.Count];
        }

        // Xóa tất cả slot cũ trong content
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // Tạo lại toàn bộ slot dựa trên inventory.items
        for (int i = 0; i < Inventory.Singleton.items.Count; i++)
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
            if (Inventory.Singleton.items[i].item != null)
            {
                itemProfile.Icon.sprite = Inventory.Singleton.items[i].item.icon;
                itemProfile.Icon.enabled = true;

                itemProfile.itemType = Inventory.Singleton.items[i].item.itemType;
                itemProfile.description = Inventory.Singleton.items[i].item.description;
                itemProfile.itemType = Inventory.Singleton.items[i].item.itemType;
                itemProfile.status = Inventory.Singleton.items[i].status;
                itemProfile.level = Inventory.Singleton.items[i].level;
                itemProfile.hp = Inventory.Singleton.items[i].hp;
                itemProfile.mp = Inventory.Singleton.items[i].mp;
                itemProfile.dame = Inventory.Singleton.items[i].dame;
                itemProfile.chimang = Inventory.Singleton.items[i].chimang;
                itemProfile.hutki = Inventory.Singleton.items[i].hutki;
                itemProfile.hutmau = Inventory.Singleton.items[i].hutmau;
                itemProfile.solanepsao = Inventory.Singleton.items[i].solanepsao;
                itemProfile.doben = Inventory.Singleton.items[i].doben;
                itemProfile.giaban = Inventory.Singleton.items[i].item.giaBan;

                itemProfile.LoaiItemType = Inventory.Singleton.items[i].item.GetItemChiso();
                itemProfile.LoaiItemType = Inventory.Singleton.items[i].item.GetItemEffect();
                itemProfile.slotName = Inventory.Singleton.items[i].item.slotName;
                itemProfile.item.Add(Inventory.Singleton.items[i].item);
                // Cập nhật vị trí item từ inventory (chỉ mục của item trong inventory)
                itemProfile.vitriitem = i;
                // Hiển thị số lượng nếu > 1
                itemProfile.txtQuantity.text = Inventory.Singleton.items[i].quantity > 1
                    ? Inventory.Singleton.items[i].quantity.ToString()
                    : "";


                if (GameManager.Singleton.level < Inventory.Singleton.items[i].item.level)
                {

                    itemProfile.levelyc = $"<color=red>Cấp độ yêu cầu {Inventory.Singleton.items[i].item.level}</color>";

                }
                else
                {
                    itemProfile.levelyc = $"<color=black>Đạt cấp độ {Inventory.Singleton.items[i].item.level} yêu cầu</color>";


                }
                // Gắn chức năng khi nhấn vào slot
                if (itemProfile.Click != null)
                {
                    int index = i; // Lưu giá trị index để sử dụng trong lambda
                    itemProfile.Click.onClick.AddListener(() => itemProfile.OnItemClickedNangcapcs(Inventory.Singleton.items[index]));
                    //epchisoUI.buttonsNangcap[0].onClick.AddListener(() => itemProfile.OnItemClickedNangcapcs(Inventory.Singleton.items[index]));
                    //epchisoUI.buttonsNangcap[1].onClick.AddListener(() => itemProfile.OnItemClickedNangcapcs(Inventory.Singleton.items[index]));
                }

                // Đánh dấu slot là đã có item
                slot[i] = true;

                // Hiển thị số sao nếu item là trang bị
                if (Inventory.Singleton.items[i].item.itemType == ItemType.Equipment)
                {
                    int numberOfStars = Inventory.Singleton.items[i].stars; // Giả sử bạn có thuộc tính "stars" trong item
                    itemProfile.stars = Inventory.Singleton.items[i].stars;
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

}
