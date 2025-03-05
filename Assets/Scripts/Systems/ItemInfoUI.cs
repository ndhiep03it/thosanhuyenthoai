using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public GameObject infoPanel; // Panel nhỏ hiển thị thuộc tính vật phẩm
    //public Text itemNameText; // Text hiển thị tên vật phẩm
    //public Text itemInfoText; // Text hiển thị thông tin vật phẩm
    public ItemProfile itemProfile;
    // public string itemName; // Tên của vật phẩm
    // public string itemDescription; // Mô tả vật phẩm hoặc thuộc tính của vật phẩm
    //public Image IconItem;
    public Transform UiTarget;
    public GameObject PanelCanvas; // chỉ cho phép   EquipmentManager.Singleton.panelMove đi trong phạm vi không qua

    private void Start()
    {
        // Ẩn Panel khi không cần thiết
        EquipmentManager.Singleton.panelMove.SetActive(false);
    }

    // Khi chuột di vào vật phẩm
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Lấy panel UI
        GameObject panel = EquipmentManager.Singleton.panelMove;
        PanelTarget panelTarget = panel.GetComponent<PanelTarget>();     
        if (panelTarget.Icon != null && panelTarget.txtName != null)
        {
            // Cập nhật thông tin từ item vào panel
            panelTarget.Icon.sprite = itemProfile.Icon.sprite; // Gán icon từ item hiện tại
            panelTarget.txtName.text = itemProfile.item[0].itemName; // Gán thông tin từ item hiện tại
        }

        // Hiển thị panel
        panel.SetActive(true);

        // Cập nhật vị trí của panel
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f; // Đảm bảo tọa độ Z nằm trong không gian 2D
        panel.transform.position = UiTarget.transform.position; // Điều chỉnh vị trí dựa trên UiTarget
    }
    // Khi người dùng nhấn vào vật phẩm (click)
    
    private void Update()
    {
       
    }
    // Khi chuột ra khỏi vật phẩm
    public void OnPointerExit(PointerEventData eventData)
    {
        // Ẩn panel khi chuột rời khỏi vật phẩm
        EquipmentManager.Singleton.panelMove.SetActive(false);
    }

    // Thêm thông tin vào các item (Item Name, Description)
    public void SetItemInfo(string name, string description)
    {
        //itemName = name;
        //itemDescription = description;
    }
}
