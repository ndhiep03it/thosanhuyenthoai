using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemProfileUI : MonoBehaviour
{
    public Image Icon;         // Icon đại diện cho item
    public Image AnhBoderItemMac;         // Icon đại diện cho item
    public Image ExpireditemsIcon;         // Icon đại diện cho hạn sử dùng đô bền item
    public GameObject ButtonGiahan;
    public GameObject ButtonSudung;
    public Text txtNameItem;   // Số lượng item
    public Text txtParamaItem;   // Chỉ số item
    public Text txtQuantity;   // Số lượng item
    public Text txtDescription; // Mô tả chi tiết
    public Text txtLevelyeucau; // Cấp yêu cầu chi tiết
    public Text txtItemType;   // Loại item (Tiêu hao, Trang bị, Nguyên liệu)
    public GameObject PanelProerties;
    public GameObject[] starImages;     // Danh sách các GameObject hình sao
    public ItemType itemTypeSelect;
    public Scrollbar scrollRect;
    public Text txtGiaban;
    public Text txtLevelYC;



    private void Start()
    {
        
    }

    // Hiển thị chi tiết của item
    public void ShowItemDetails(Item item,Image boderanhcap,int level, int quantity, int stars, ItemType itemType, string status, string levelRequired, int damage, int hp, int mp,int chimang, int lifesteal,int manasteal,float ne,
    int solanepsao,int doben,string boxmessage)
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
            AnhBoderItemMac.sprite = boderanhcap.sprite;
            Icon.enabled = true;
            AnhBoderItemMac.enabled = true;

        }
        itemTypeSelect = itemType;
        // Hiển thị tên vật phẩm
        txtNameItem.text = item.itemName;
        txtLevelyeucau.text = levelRequired;
        txtLevelYC.text = "Cấp yêu cầu sử dụng:" + item.level;
        txtGiaban.text = "Giá bán:" + item.giaBan;
        // Cập nhật số lượng
        if(itemTypeSelect == ItemType.Equipment)
        {
            txtQuantity.text = "";
        }
        else
        {
            txtQuantity.text = quantity.ToString();
        }
        

        // Cập nhật loại item
        txtItemType.text = $"Loại: {item.GetItemTypeName()}";

        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";

        if (level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (hp > 0) statsDescription += $"\n\u2764 HP: +{hp}";
        if (mp > 0) statsDescription += $"\n\u2764 MP: +{mp}";
        if (damage > 0) statsDescription += $"\n\u2764 Tấn công: +{damage}";
        if (chimang > 0) statsDescription += $"\n\u2764 Chí mạng: +{chimang}";
        if (lifesteal > 0) statsDescription += $"\n\u2764 Hút ki: +{lifesteal}";
        if (manasteal > 0) statsDescription += $"\n\u2764 Hút máu: +{manasteal}";
        if (ne > 0) statsDescription += $"\n\u2764 Né: +{ne}";
        if (itemTypeSelect == ItemType.Material)
        {

        }
        else if (itemTypeSelect == ItemType.Equipment)
        {
            if (solanepsao >= 0) statsDescription += $"\n\u2764 Số lần ép sao tối đa:{solanepsao}/7";
            statsDescription += $"\n\u2764 Độ bền:{doben}/100";
        }
        else
        {

        }
       
        // Cập nhật mô tả
        txtDescription.text = $"\n\u2764 <color=black>{item.description}</color>\n{status}{statsDescription} \n{boxmessage}";
        txtParamaItem.text = $"{item.GetItemEffect()}";
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
