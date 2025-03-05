using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Vector2 hotspot = Vector2.zero; // Vị trí "tâm" của con trỏ
    public Texture2D defaultCursor;  // Con trỏ mặc định
    public Texture2D interactCursor; // Con trỏ khi nhấn chuột

    void Start()
    {
        // Đặt con trỏ chuột mặc định khi bắt đầu
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
        Cursor.visible = true;
    }

    void Update()
    {
        // Kiểm tra nếu chuột được nhấn (nhấn chuột trái)
        if (Input.GetMouseButtonDown(0)) // Button 0 là chuột trái
        {
            // Thay đổi con trỏ khi nhấn chuột
            Cursor.SetCursor(interactCursor, hotspot, CursorMode.Auto);
        }

        // Kiểm tra nếu chuột được thả (thả chuột trái)
        if (Input.GetMouseButtonUp(0)) // Button 0 là chuột trái
        {
            // Đặt lại con trỏ mặc định khi thả chuột
            Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
        }
    }
}
