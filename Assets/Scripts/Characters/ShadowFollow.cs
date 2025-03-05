using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    public Transform player; // Gắn Player vào đây
    public LayerMask groundLayer; // Layer của mặt đất hoặc vật thể cần bóng
    public float maxRayDistance = 10f; // Khoảng cách tối đa của tia raycast
    public float groundY = 0f; // Vị trí Y của mặt đất

    void Update()
    {
        if (player != null)
        {
            // Lấy vị trí hiện tại của bóng
            Vector3 shadowPosition = transform.position;

            // Đặt vị trí X của bóng theo vị trí Player
            shadowPosition.x = player.position.x;

            // Sử dụng Raycast để kiểm tra vị trí tiếp xúc dưới Player
            RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, maxRayDistance, groundLayer);

            if (hit.collider != null)
            {
                // Nếu tia Raycast chạm một collider, đặt vị trí Y của bóng tại điểm tiếp xúc
                shadowPosition.y = hit.point.y;
            }
            else
            {
                // Nếu không chạm gì, đặt bóng ở vị trí mặt đất mặc định (groundY)
                shadowPosition.y = groundY;
            }

            // Cập nhật vị trí của bóng
            transform.position = shadowPosition;
        }
    }
}
