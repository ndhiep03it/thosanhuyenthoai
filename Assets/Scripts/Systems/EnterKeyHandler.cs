using UnityEngine;
using UnityEngine.UI;

public class EnterKeyHandler : MonoBehaviour
{
    // Danh sách các nút
    public Button[] targetButtons;

    // Sprites để sử dụng
    public Sprite defaultSprite; // Sprite mặc định
    public Sprite highlightedSprite; // Sprite khi được chọn

    // Chỉ số hiện tại của nút được chọn
    private int currentIndex = 0;

    void Start()
    {
        // Đặt trạng thái cho nút đầu tiên khi bắt đầu
        if (targetButtons.Length > 0)
        {
            HighlightButton(currentIndex);
        }
    }

    void Update()
    {
        // Di chuyển sang trái
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelection(-1);
        }
        // Di chuyển sang phải
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelection(1);
        }
        // Nhấn Enter để kích hoạt sự kiện của nút hiện tại
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) // Tất cả các nút Enter
        {
            if (targetButtons.Length > 0 && targetButtons[currentIndex] != null)
            {
                targetButtons[currentIndex].onClick.Invoke();
            }
        }

    }

    // Hàm di chuyển giữa các nút
    void MoveSelection(int direction)
    {
        if (targetButtons.Length == 0) return;

        // Bỏ highlight nút hiện tại
        UnhighlightButton(currentIndex);

        // Tính toán nút tiếp theo
        currentIndex = (currentIndex + direction + targetButtons.Length) % targetButtons.Length;

        // Đặt highlight cho nút mới
        HighlightButton(currentIndex);
    }

    // Hàm làm nổi bật nút được chọn
    void HighlightButton(int index)
    {
        if (targetButtons[index] != null)
        {
            // Thay đổi sprite của nút thành sprite được chọn
            Image buttonImage = targetButtons[index].GetComponent<Image>();
            if (buttonImage != null && highlightedSprite != null)
            {
                buttonImage.sprite = highlightedSprite;
            }
        }
    }

    // Hàm bỏ làm nổi bật nút
    void UnhighlightButton(int index)
    {
        if (targetButtons[index] != null)
        {
            // Thay đổi sprite của nút thành sprite mặc định
            Image buttonImage = targetButtons[index].GetComponent<Image>();
            if (buttonImage != null && defaultSprite != null)
            {
                buttonImage.sprite = defaultSprite;
            }
        }
    }
}
