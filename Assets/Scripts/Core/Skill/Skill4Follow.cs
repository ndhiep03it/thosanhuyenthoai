using UnityEngine;
using System.Collections;

public class Skill4Follow : MonoBehaviour
{
    private Transform enemy;          // Vị trí của kẻ địch
    public float moveSpeed = 4f;      // Tốc độ di chuyển của tia
    public float duration = 0.2f;     // Thời gian tồn tại của tia
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>(); // Lấy Line Renderer từ GameObject
        lineRenderer.startWidth = 0.5f; // Độ dày đầu tia
        lineRenderer.endWidth = 0.5f;   // Độ dày cuối tia
        lineRenderer.positionCount = 2; // Line Renderer có 2 điểm (bắt đầu & kết thúc)

        enemy = GameObject.FindGameObjectWithTag("Enemy")?.transform; // Tìm Enemy
        if (enemy != null)
        {
            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser()
    {
        float elapsedTime = 0f; // Đếm thời gian tồn tại của tia

        while (elapsedTime < duration)
        {
            if (enemy != null)
            {
                // Cập nhật vị trí Line Renderer
                lineRenderer.SetPosition(0, transform.position);  // Điểm đầu của tia
                lineRenderer.SetPosition(1, enemy.position);      // Điểm cuối tia (kẻ địch)
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Hủy GameObject sau khi bắn
       // Destroy(gameObject);
    }
}
