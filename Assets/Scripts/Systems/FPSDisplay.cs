using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private float deltaTime = 0.0f;

    void Update()
    {
        // Tính toán thời gian giữa các khung hình
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        // Đặt kiểu chữ và kích thước cho FPS
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.white;

        // Tính toán FPS
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;

        string text = string.Format("{0:0.0} ms ({1:0.} FPS)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
