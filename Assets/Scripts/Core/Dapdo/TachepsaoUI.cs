using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TachepsaoUI : MonoBehaviour
{
    public Image Icon;         // Icon đại diện cho item
    public Text txtNameItem;   // Số lượng item
    public Text txtParama;   // Chỉ số item
    public Text txtQuantity;   // Số lượng item
    public Text txtDescription; // Mô tả chi tiết
    public Text txtLevelyeucau; // Cấp yêu cầu chi tiết
    public Text txtItemType;   // Loại item (Tiêu hao, Trang bị, Nguyên liệu)
    public GameObject PanelProerties;
    public GameObject[] starImages;     // Danh sách các GameObject hình sao
    public ItemType itemTypeSelect;
    public Scrollbar scrollRect;
   



    private void Start()
    {

    }

    // Hiển thị chi tiết của item
    public void ShowItemDetails(Item item, int level, int quantity, int stars, ItemType itemType, string status, string levelRequired, int damage, int hp, int mp, int chimang, int lifesteal, int manasteal,float ne,
        int solanepsao)
    {
        if (item == null)
        {
            Debug.LogWarning("Item không hợp lệ!");
            return;
        }

        PanelProerties.SetActive(true);

        // Cập nhật icon
        if (Icon != null && item.icon != null)
        {
            Icon.sprite = item.icon;
            Icon.enabled = true;
        }

        // Hiển thị tên vật phẩm
        txtNameItem.text = item.itemName;
        txtLevelyeucau.text = levelRequired;

        // Cập nhật số lượng
        txtQuantity.text = quantity.ToString();

        // Cập nhật loại item
        txtItemType.text = $"Loại: {item.GetItemTypeName()}";

        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        itemTypeSelect = itemType;
        if (level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (hp > 0) statsDescription += $"\nHP: +{hp}";
        if (mp > 0) statsDescription += $"\nMP: +{mp}";
        if (damage > 0) statsDescription += $"\nTấn công: +{damage}";
        if (chimang > 0) statsDescription += $"\nChí mạng: +{chimang}";
        if (lifesteal > 0) statsDescription += $"\nHút ki: +{lifesteal}";
        if (manasteal > 0) statsDescription += $"\nHút máu: +{manasteal}";
        if (ne > 0) statsDescription += $"\nNé: +{ne}";
        if (itemTypeSelect == ItemType.Material)
        {

        }
        else if (itemTypeSelect == ItemType.Equipment)
        {
            if (solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{solanepsao}/7";
        }
        else
        {

        }
        // Cập nhật mô tả
        //txtDescription.text = $"{item.GetItemEffect()}\n<color=black>{item.description}</color>\n{status}{statsDescription}";
        txtDescription.text = $"<color=black>{item.description}</color>\n{status}{statsDescription}";
        txtParama.text = $"{item.GetItemEffect()}";
        // Hiển thị sao
        UpdateStarImages(stars, itemType == ItemType.Equipment);
    }

    // Cập nhật trạng thái hình sao
    private void UpdateStarImages(int stars, bool showStars)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i]?.SetActive(showStars && i < stars);
        }
    }
    // Ẩn thông tin item
    public void HideItemDetails()
    {
        // Sử dụng Coroutine để đặt giá trị scrollbar sau một khoảng thời gian
        if (scrollRect != null)
        {
            // Reset vị trí scroll về đầu trang
            scrollRect.value = 1;
        }
        Icon.enabled = false;
        txtNameItem.text = "";
        txtQuantity.text = "";
        txtItemType.text = "";
        txtDescription.text = "";
        PanelProerties.SetActive(false);


    }

}
