using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Singleton;
    public GameObject ItemShop;
    public Transform contentShop;
    public Shop shop;
    [Header("Shop Profile")]
    public GameObject panelShopProfile;
    public Image IconItemShop;
    public Text txtNameItemShop;
    public Text txtDescriptions;
    public Text txtParama;
    public Text txtleveyc;
    public Text txtGia;
    public Text txtGiaVatpham;
    public int gia;
    public Item[] item; // Mảng Item
    private GameObject targetItem;
    public Button[] categoryButtons;        // Mảng các button danh mục
    public SlotName slotName;
    private void OnEnable()
    {
        FilterItemsBySlot(SlotName.ao);
    }

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
           
        }
        
    }
    private void Start()
    {
        // Gắn sự kiện cho các button danh mục
        foreach (var button in categoryButtons)
        {
            var slotNameFilter = (SlotName)System.Enum.Parse(typeof(SlotName), button.name); // Lấy tên nút làm SlotName
            button.onClick.AddListener(() => FilterItemsBySlot(slotNameFilter));
        }

        FilterItemsBySlot(SlotName.ao);
    }

    private void LoadItemShop()
    {
        ClearContent(contentShop);

        foreach (var shopItem in shop.item)
        {
            AddItemToContent(shopItem, contentShop);
        }
    }
    

    private void AddItemToContent(Item shopItem, Transform content)
    {
        GameObject obj = Instantiate(ItemShop, content, false);
        ShopProfile shopProfile = obj.GetComponent<ShopProfile>();
        shopProfile.IconItem.sprite = shopItem.icon;
        shopProfile.txtNameItemShop.text = shopItem.itemName;
        shopProfile.txtParama.text = shopItem.GetItemEffect();
        shopProfile.txtGia.text = shopItem.giaMua.ToString();
        shopProfile.description = shopItem.description;
        shopProfile.giamua = shopItem.giaMua;
        shopProfile.itemMoney = shopItem.itemMoney;
        shopProfile.item.Add(shopItem);

        // Kiểm tra cấp độ yêu cầu
        if (GameManager.Singleton.level < shopItem.level)
        {
            shopProfile.levelyc = $"<color=red>Cấp độ yêu cầu {shopItem.level}</color>";
        }
        else
        {
            shopProfile.levelyc = $"<color=black>Đạt cấp độ {shopItem.level} yêu cầu</color>";
        }

        // Gắn sự kiện nhấn vào vật phẩm
        if (shopProfile.buttonCheck != null)
        {
            shopProfile.buttonCheck.onClick.AddListener(() => shopProfile.OnItemShopClicked());
        }
    }

    private void ClearContent(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    // Lọc vật phẩm theo SlotName
    private void FilterItemsBySlot(SlotName filterSlot)
    {
        ClearContent(contentShop);

        var filteredItems = shop.item.Where(item => item.slotName == filterSlot).ToList();

        foreach (var shopItem in filteredItems)
        {
            AddItemToContent(shopItem, contentShop);
        }

        Debug.Log($"Đã lọc các vật phẩm theo danh mục: {filterSlot}");
    }


    public void SetTarget(GameObject target)
    {
        if (targetItem != null)
        {
            ShopProfile itemContent1 = targetItem.GetComponent<ShopProfile>();
            if (itemContent1 != null)
            {
                itemContent1.HideArrow();
            }
        }

        targetItem = target;
        ShopProfile itemContent2 = target.GetComponent<ShopProfile>();
        if (itemContent2 != null)
        {
            itemContent2.ShowArrow();
        }
    }
    public void BuyItem()
    {
        // Kiểm tra loại tiền tệ
        if (item[0].itemMoney == ItemMoney.GOLD)
        {
            if (GameManager.Singleton.gold < gia)
            {
                Thongbao.Singleton.ShowThongbao("Bạn không đủ vàng để mua.");
                return;
            }

            // Mua vật phẩm với vàng
            GameManager.Singleton.gold -= gia;
            Thongbao.Singleton.ShowThongbao($"Mua thành công {item[0].itemName} với giá {gia} vàng.");
        }
        else if (item[0].itemMoney == ItemMoney.RUBY)
        {
            if (GameManager.Singleton.ruby < gia)
            {
                Thongbao.Singleton.ShowThongbao("Bạn không đủ Ruby để mua.");
                return;
            }

            // Mua vật phẩm với Ruby
            GameManager.Singleton.ruby -= gia;
            Thongbao.Singleton.ShowThongbao($"Mua thành công {item[0].itemName} với giá {gia} Ruby.");
        }

        // Mua vật phẩm thành công, thêm vào kho
        Inventory.Singleton.BuyItem(item[0], 1, "Vật phẩm được mua từ cửa hàng.", 1,1000);
        PlayerController.Singleton.DisableEventSystem();
        PlayerController.Singleton.EnableEventSystem();
        ShopInventory.Singleton.UpdateItem();

    }

}
