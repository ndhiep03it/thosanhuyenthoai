using System.Collections;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float minSpeed = 1f; // Tốc độ nhỏ nhất
    public float maxSpeed = 3f; // Tốc độ lớn nhất
    public float idleTime = 2f; // Thời gian dừng giữa các lần di chuyển
    public Collider2D[] moveArea; // Vùng di chuyển

    private Vector2 targetPosition;
    private float moveSpeed;
    private bool isIdle = false;
    private SpriteRenderer spriteRenderer;
    public Animator animator;

    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();

        if (moveArea == null)
        {
            Debug.LogError("Chưa gán vùng di chuyển cho cá!");
            return;
        }

        SetNewTargetPosition();
    }

    void Update()
    {
        if (isIdle) return; // Nếu đang nghỉ thì không di chuyển

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            StartCoroutine(EnterIdleState()); // Nghỉ một chút trước khi bơi tiếp
        }
        else
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        // Bật animation bơi
        animator.SetBool("Walk", true);

        // Di chuyển dần đến vị trí mục tiêu
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Lật hướng nếu cần
        if (targetPosition.x < transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }


    IEnumerator EnterIdleState()
    {
        isIdle = true;
        animator.SetBool("Walk", false); // Chuyển sang trạng thái nghỉ

        yield return new WaitForSeconds(idleTime); // Nghỉ trong thời gian idleTime

        isIdle = false;
        SetNewTargetPosition(); // Tìm vị trí mới để bơi tiếp
    }

    void SetNewTargetPosition()
    {
        for (int i = 0; i < 10; i++) // Thử tìm vị trí hợp lệ tối đa 10 lần
        {
            Collider2D randomArea = moveArea[Random.Range(0, moveArea.Length)];
            Bounds bounds = randomArea.bounds;

            Vector2 potentialPosition = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            if (randomArea.OverlapPoint(potentialPosition))
            {
                targetPosition = potentialPosition;
                moveSpeed = Random.Range(minSpeed, maxSpeed);
                return;
            }
        }
    }
}
