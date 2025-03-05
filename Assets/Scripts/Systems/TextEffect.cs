using UnityEngine;

public class TextEffect : MonoBehaviour
{
    private TextMesh textMesh;
    public float moveSpeed = 0.1f;  // Tốc độ di chuyển lên trên
    public float maxHeight = 1.0f;  // Chiều cao tối đa mà chữ có thể di chuyển lên

    private Vector3 originalPosition;

    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        originalPosition = transform.position;  // Lưu vị trí gốc
    }

    void Update()
    {
        // Di chuyển lên trên mà không quay lại vị trí ban đầu
        if (transform.position.y < originalPosition.y + maxHeight)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }
}
