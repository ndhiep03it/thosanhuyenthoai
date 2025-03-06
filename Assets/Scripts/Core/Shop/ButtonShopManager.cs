using UnityEngine;
using UnityEngine.UI;

public class ButtonShopManager : MonoBehaviour
{
    public Button[] buttons; // Mảng chứa các button
    public Sprite selectedSprite; // Hình ảnh khi được chọn
    public Sprite defaultSprite;  // Hình ảnh mặc định
    private Button selectedButton; // Button hiện tại đang được chọn

    void Start()
    {
        // Gắn sự kiện nhấn cho từng button
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
        
    }

    // Xử lý khi một button được nhấn
    void OnButtonClicked(Button clickedButton)
    {
        if (selectedButton != null)
        {
            // Đổi lại hình ảnh của button cũ về mặc định
            selectedButton.image.sprite = defaultSprite;
        }

        // Cập nhật button mới và đổi hình ảnh
        selectedButton = clickedButton;
        selectedButton.image.sprite = selectedSprite;

        //Debug.Log("Button được chọn: " + clickedButton.name);
    }
}
