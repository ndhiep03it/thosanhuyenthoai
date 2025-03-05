using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // Thêm thư viện cho sự kiện

public class Skill5 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Joystick joystick;             // Joystick ảo
    public LineRenderer lineRenderer;     // LineRenderer để vẽ đường thẳng
    public GameObject skillPrefab;        // Prefab chiêu thức
    public float maxLineLength = 5f;      // Độ dài tối đa của đường thẳng
    public float travelTime = 0.5f;       // Thời gian để viên đạn bay đến targetPosition
    public float minLineWidth = 0.05f;    // Độ dày nhỏ nhất của LineRenderer
    public float maxLineWidth = 0.2f;     // Độ dày lớn nhất của LineRenderer
    private bool isCharging = false;
    public Vector2 targetPosition;        // Vị trí cuối cùng của đường LineRenderer
    public Transform player;
    public GameObject SoundSkill5;
    public Button cancelButton;           // Nút hủy chiêu (Button hoặc bất kỳ GameObject UI nào)

    private bool isPointerOver = false;   // Kiểm tra xem chuột có đang ở trong vùng của nút hủy hay không

    void Update()
    {
        // Kiểm tra nếu người chơi nhấn nút hủy chiêu (nút "X" hoặc button UI)
        if (cancelButton.gameObject.activeSelf && Input.GetButtonDown("Fire1"))  // "Fire1" là input mặc định, hoặc có thể là nút "X"
        {
            CancelSkill(); // Hủy chiêu khi nhấn nút
           
        }

        if (isPointerOver)  // Nếu chuột đang ở trên nút hủy
        {
            CancelSkill();  // Tự động hủy chiêu
            cancelButton.gameObject.SetActive(true);  // Hiển thị nút hủy khi đang kéo joystick
           

        }

        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (direction.magnitude > 0.1f)  // Nếu joystick đang được kéo
        {
            isCharging = true;
            targetPosition = CalculateTargetPosition(direction);
            DrawArrowLine(targetPosition, direction.magnitude);
            //cancelButton.gameObject.SetActive(true);  // Hiển thị nút hủy khi đang kéo joystick
            //SoundSkill5.SetActive(true);   // Bật âm thanh chiêu
        }
        else if (isCharging)  // Khi thả joystick
        {
            isCharging = false;
            SpawnSkill(targetPosition);  // Tạo chiêu thức khi thả joystick
            ResetLine();  // Reset LineRenderer
           // cancelButton.gameObject.SetActive(false);  // Ẩn nút hủy khi chiêu được thực hiện
            //SoundSkill5.SetActive(false);  // Tắt âm thanh chiêu
        }
    }

    Vector2 CalculateTargetPosition(Vector2 direction)
    {
        // Giới hạn độ dài theo trục Y (giảm tác động theo chiều dọc)
        float length = maxLineLength * Mathf.Clamp01(direction.y > 0 ? 0.7f : 1f);
        return (Vector2)player.position + direction.normalized * length;
    }

    void DrawArrowLine(Vector2 target, float magnitude)
    {
        float lineWidth = Mathf.Lerp(minLineWidth, maxLineWidth, magnitude / maxLineLength);

        lineRenderer.gameObject.SetActive(true);
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth * 0.8f;  // Thu nhỏ ở cuối để tạo hiệu ứng mũi tên
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, target);
    }

    void ResetLine()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.gameObject.SetActive(false);
    }

    void SpawnSkill(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)player.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // Tính góc xoay

        // Tạo skillPrefab với góc xoay đúng hướng
        GameObject skill = Instantiate(skillPrefab, player.position, Quaternion.Euler(0, 0, angle));

        StartCoroutine(MoveSkill(skill, target));
    }

    System.Collections.IEnumerator MoveSkill(GameObject skill, Vector2 target)
    {
        Vector2 startPosition = skill.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < travelTime)
        {
            skill.transform.position = Vector2.Lerp(startPosition, target, elapsedTime / travelTime);  // Di chuyển dần đến target
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        skill.transform.position = target;  // Đảm bảo viên đạn kết thúc chính xác ở target

        // Kiểm tra va chạm với kẻ địch
        Collider2D hit = Physics2D.OverlapCircle(skill.transform.position, 0.5f);  // Kiểm tra va chạm xung quanh viên đạn

        if (hit != null && hit.CompareTag("Enemy"))  // Nếu trúng kẻ địch
        {
            EnemyController enemyController = hit.GetComponent<EnemyController>();  // Lấy đối tượng EnemyController từ kẻ địch
            if (enemyController != null)
            {
                enemyController.TakeDamage(GameManager.Singleton.dameao, Color.red);  // Gọi hàm TakeDamage để trừ máu
                Debug.Log("Kẻ địch bị trúng chiêu và mất máu!");  // In ra log để kiểm tra
            }
        }

        Destroy(skill, 2f);  // Hủy viên đạn sau 2 giây
    }



    // Hủy chiêu khi nhấn nút hủy
    void CancelSkill()
    {
        isCharging = false;
        ResetLine();  // Xóa LineRenderer
        cancelButton.gameObject.SetActive(false);  // Ẩn nút hủy
        SoundSkill5.SetActive(false);  // Tắt âm thanh chiêu
    }

    // Xử lý khi chuột đi vào vùng nút hủy
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;  // Khi chuột vào vùng nút hủy
    }

    // Xử lý khi chuột ra khỏi vùng nút hủy
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;  // Khi chuột ra khỏi vùng nút hủy
    }
}
