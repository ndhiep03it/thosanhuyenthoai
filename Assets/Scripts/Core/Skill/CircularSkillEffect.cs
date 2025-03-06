using UnityEngine;
using System.Collections;  // Cần dùng để sử dụng Coroutine
using System.Collections.Generic;

public class CircularSkillEffect : MonoBehaviour
{
    public float timdeHide = 10f;
    public int segments = 36;  // Số lượng phân đoạn của vòng tròn
    public float radius = 5f;  // Bán kính vòng tròn
    public float rotationSpeed = 50f;  // Tốc độ xoay vòng tròn
    public float glowSpeed = 1f;  // Tốc độ thay đổi độ sáng (nếu dùng)
    public int damage = 10;  // Sát thương gây ra khi đứng trong phạm vi
    public float damageInterval = 1f;  // Thời gian giữa các lần gây sát thương (1 giây)
    public float damageDuration = 5f;  // Thời gian tối đa để tiếp tục trừ máu

    //public LineRenderer lineRenderer;
    private Color startColor = Color.green;  // Màu bắt đầu của vòng tròn
    private Color endColor = Color.red;  // Màu kết thúc của vòng tròn
    private float time = 0f;  // Biến thời gian dùng để thay đổi màu sắc
    private CircleCollider2D circleCollider;  // Collider cho vòng tròn

    private Coroutine damageCoroutine;  // Coroutine dùng để gây sát thương từ từ

    void Start()
    {
        // Thêm LineRenderer vào GameObject này
        //lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Thêm CircleCollider2D để phát hiện phạm vi va chạm
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;  // Đảm bảo collider chỉ dùng để phát hiện va chạm mà không gây ra vật lý

        // Cấu hình LineRenderer
        //lineRenderer.positionCount = segments + 1;  // Thêm 1 để tạo điểm khép kín
       // lineRenderer.loop = true;  // Cho phép vòng tròn khép kín
       // lineRenderer.startWidth = 1f;  // Độ rộng của vòng tròn
        //lineRenderer.endWidth = 1f;
        //lineRenderer.useWorldSpace = false;  // Để vẽ trong không gian local

        // Vẽ vòng tròn
        CreateCircle();
    }

    void Update()
    {
        // Xoay vòng tròn theo thời gian
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Thay đổi độ sáng vòng tròn theo thời gian (hiệu ứng luyện)
        //time += Time.deltaTime * glowSpeed;
        //lineRenderer.startColor = Color.Lerp(startColor, endColor, Mathf.PingPong(time, 1));  // Lấy màu gradient từ xanh sang đỏ
        //lineRenderer.endColor = lineRenderer.startColor;
    }
    private void OnEnable()
    {
        //StartCoroutine(HideActive());
    }
    private void OnDisable()
    {
        timdeHide = 10f;
    }
   
    IEnumerator HideActive()
    {
        yield return new WaitForSeconds(timdeHide);
        gameObject.SetActive(false);
    }
   
    void CreateCircle()
    {
        float angleStep = 360f / segments;  // Góc giữa các điểm trên vòng tròn

        for (int i = 0; i < segments; i++)
        {
            // Tính toán vị trí các điểm trên vòng tròn
            float angle = i * angleStep * Mathf.Deg2Rad;  // Chuyển độ sang radian
            Vector3 point = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            //lineRenderer.SetPosition(i, point);  // Đặt vị trí của điểm
        }
    }

    private HashSet<Collider2D> currentlyDamagedEnemies = new HashSet<Collider2D>();

    private void OnTriggerStay2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng có phải là enemy hay không
        if (other.CompareTag("Enemy"))
        {
            // Nếu đối tượng chưa có trong danh sách đang nhận sát thương
            if (!currentlyDamagedEnemies.Contains(other))
            {
                // Thêm đối tượng vào danh sách
                currentlyDamagedEnemies.Add(other);
                // Bắt đầu coroutine cho đối tượng này
                StartCoroutine(ApplyDamageOverTime(other));
            }
        }
    }

    // Coroutine để gây sát thương từ từ cho đối tượng
    private IEnumerator ApplyDamageOverTime(Collider2D enemy)
    {
        float elapsedTime = 0f;

        // Trong thời gian damageDuration, liên tục gây sát thương mỗi damageInterval giây
        while (elapsedTime < damageDuration)
        {
            // Kiểm tra lại nếu đối tượng vẫn còn sống
            if (enemy != null)
            {
                // Gọi hàm nhận sát thương của enemy
                enemy.GetComponent<EnemyController>().TakeDamage(damage, Color.red);  // Cần có script EnemyController với hàm TakeDamage
            }

            // Chờ trong damageInterval giây trước khi gây sát thương lần tiếp theo
            yield return new WaitForSeconds(damageInterval);

            // Cập nhật thời gian đã trôi qua
            elapsedTime += damageInterval;
        }

        // Sau khi kết thúc coroutine, xóa đối tượng khỏi danh sách đang nhận sát thương
        currentlyDamagedEnemies.Remove(enemy);
    }


}
