using UnityEngine;

public class RedBeam : MonoBehaviour
{
    public Transform player; // Vị trí của Player
    private LineRenderer lineRenderer;
    private Transform closestEnemy;
    public GameObject[] enemies;
    void Start()
    {
        // Gắn LineRenderer
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Cấu hình LineRenderer
        lineRenderer.startWidth = 0.1f; // Độ rộng đầu
        lineRenderer.endWidth = 0.1f;   // Độ rộng cuối
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Shader mặc định
        lineRenderer.startColor = Color.red; // Màu đầu
        lineRenderer.endColor = Color.red;   // Màu cuối
        lineRenderer.positionCount = 2;      // Số điểm (2 điểm cho 1 tia)
    }

    void Update()
    {
        // Tìm tất cả các enemy
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Tìm enemy gần nhất
        closestEnemy = FindClosestEnemy(enemies);

        // Cập nhật vị trí tia
        if (player != null && closestEnemy != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, player.position);            // Điểm đầu tại Player
            lineRenderer.SetPosition(1, closestEnemy.position);      // Điểm cuối tại Enemy gần nhất
        }
        else
        {
            lineRenderer.enabled = false; // Ẩn tia nếu thiếu Player hoặc Enemy
        }
    }

    // Hàm tìm Enemy gần nhất
    private Transform FindClosestEnemy(GameObject[] enemies)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}