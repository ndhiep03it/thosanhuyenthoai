using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcFollow : MonoBehaviour
{
    public Transform player; // Tham chiếu tới transform của Player
    public float speed = 3.0f; // Tốc độ di chuyển của NPC
    public float stoppingDistance = 1.5f; // Khoảng cách tối thiểu để dừng lại
    public float maxFollowDistance = 10.0f; // Khoảng cách tối đa NPC sẽ theo dõi Player

    private Animator animator; // Animator để quản lý trạng thái
    private SpriteRenderer spriteRenderer; // Để flip sprite
    private bool isCollidingWithPlayer = false; // Kiểm tra trạng thái va chạm với Player

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Kiểm tra khoảng cách giữa NPC và Player
        float distance = Vector3.Distance(transform.position, player.position);

        if (!isCollidingWithPlayer && distance <= maxFollowDistance && distance > stoppingDistance)
        {
            // Di chuyển và đổi trạng thái sang "Walk"
            MoveTowardsPlayer();
            animator.SetBool("walk", true);
        }
        else
        {
            // Dừng di chuyển và chuyển sang trạng thái "Idle"
            animator.SetBool("walk", false);
        }
    }

    void MoveTowardsPlayer()
    {
        // Hướng về phía Player
        Vector3 direction = (player.position - transform.position).normalized;

        // Di chuyển NPC về phía Player
        transform.position += direction * speed * Time.deltaTime;

        // Lật NPC dựa trên hướng di chuyển
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false; // Không lật
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true; // Lật
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Dừng NPC khi va chạm với Player
            isCollidingWithPlayer = true;
            animator.SetBool("walk", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Tiếp tục di chuyển khi thoát va chạm
            isCollidingWithPlayer = false;
        }
    }
}
