using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingPC : MonoBehaviour
{
    public int[] exp;
    public int dameMin;
    public int dameMax;
    public int HP;
    public int HPMAX = 100;
    public float minMoveSpeed = 1f; // Tốc độ di chuyển tối thiểu
    public float maxMoveSpeed = 3f; // Tốc độ di chuyển tối đa
    public float patrolRange = 3f; // Phạm vi tuần tra
    public float idleTime = 2f; // Thời gian nghỉ giữa các lần di chuyển
    public LayerMask groundLayer; // Lớp để kiểm tra nền đất
    public float groundCheckDistance = 0.5f; // Khoảng cách để kiểm tra nền đất
    private Vector2 targetPosition;
    private float moveSpeed; // Tốc độ di chuyển hiện tại
    private Vector2 startPosition;
    private bool movingRight = true;
    private bool isIdle = false; // Trạng thái nghỉ
    private float idleTimer = 0f;

    private SpriteRenderer spriteRenderer; // Để flip trái phải
    private Animator animator;
    private Rigidbody2D rb;
    public GameObject dameTextObj;
    public Transform transformEnemy;
    public GameObject[] Item;
    public Slider sliderHp;
    private Coroutine damageCoroutine; // Lưu trữ Coroutine đang chạy
    private Coroutine blinkCoroutine; // Lưu trữ coroutine nhấp nháy
    public Color blinkColor = Color.red; // Màu nhấp nháy (ví dụ: màu đỏ khi bị đánh)
    public float blinkDuration = 0.1f; // Thời gian mỗi lần nhấp nháy
    public int blinkCount = 5; // Số lần nhấp nháy
    private Color originalColor; // Màu gốc của SpriteRenderer

    private Collider2D moveArea;

    void Start()
    {
        sliderHp.maxValue = HPMAX;
        HP = HPMAX;
        sliderHp.value = HP;

        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Thiết lập tốc độ ngẫu nhiên ban đầu
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    void Update()
    {
        if (isIdle)
        {
            HandleIdle();
        }
        else
        {
            Patrol();
        }
        if (HP <= 0)
        {
            sliderHp.value = 0;
            HP = 0;
            rb.simulated = false;  // Dừng vật lý
            moveSpeed = 0;         // Dừng di chuyển
            gameObject.tag = "Untagged";
            // Hoạt ảnh chết (nếu có)
            if (animator != null)
            {
                animator.SetTrigger("Die");
            }

            sliderHp.value = 0;  // Cập nhật thanh máu
        }
    }

    void Patrol()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", true);

        Vector2 currentPosition = transform.position;
        float step = moveSpeed * Time.deltaTime;

        // Di chuyển tới vị trí mục tiêu
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, step);

        // Nếu đến gần vị trí mục tiêu, chuyển sang trạng thái nghỉ
        if (Vector2.Distance(currentPosition, targetPosition) < 0.1f || !CheckGround(targetPosition))
        {
            EnterIdleState();
        }

        // Lật hình quái theo hướng di chuyển
        spriteRenderer.flipX = targetPosition.x < currentPosition.x;
    }

    bool CheckGround(Vector2 position)
    {
        // Kiểm tra nền đất dưới chân
        Vector2 origin = new Vector2(position.x, position.y - 0.5f); // Điểm kiểm tra ở dưới chân
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(origin, Vector2.down * groundCheckDistance, Color.red); // Hiển thị ray kiểm tra
        return hit.collider != null;
    }

    void SetNewTargetPosition()
    {
        float randomX = Random.Range(-patrolRange, patrolRange);
        targetPosition = new Vector2(startPosition.x + randomX, startPosition.y);

        //// Nếu vị trí mới không hợp lệ, thử lại
        //if (!CheckGround(targetPosition))
        //{
        //    SetNewTargetPosition(); // Gọi đệ quy
        //}
    }


    void EnterIdleState()
    {
        isIdle = true;
        idleTimer = idleTime;

        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
    }

    void HandleIdle()
    {
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f)
        {
            isIdle = false;
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            SetNewTargetPosition();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //isIdle = true;
            //maxMoveSpeed = 0f;
           // minMoveSpeed = 0f;

            // Bắt đầu Coroutine trừ máu mỗi 2 giây
            if (damageCoroutine == null)
            {
                //damageCoroutine = StartCoroutine(DamagePlayerOverTime());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //isIdle = false;
           // maxMoveSpeed = 3f;
            //minMoveSpeed = 1f;

            // Dừng Coroutine trừ máu khi ra khỏi vùng
            if (damageCoroutine != null)
            {
                //StopCoroutine(damageCoroutine);
                //damageCoroutine = null;
                //moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            }
        }
    }

    // Coroutine trừ máu người chơi mỗi 2 giây
    private IEnumerator DamagePlayerOverTime()
    {
        while (true)
        {
            int ranđame = Random.Range(dameMin, dameMax);
            animator.SetTrigger("Attack");
            PlayerController.Singleton.Takedame(ranđame); // Trừ máu người chơi

            yield return new WaitForSeconds(2f); // Chờ 2 giây trước khi trừ máu lần nữa
        }
    }

    public void TakeDamage(int damage, Color color)
    {
        // Hành động khi bị tấn công
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Hit); // Phát âm thanh trúng

        // Tạo đối tượng hiển thị sát thương
        GameObject obj = Instantiate(dameTextObj, transformEnemy, false);
        Text textDamage = obj.GetComponent<Text>();

        // Tính toán phần trăm hút máu và hút ki từ sát thương
        float bloodStealPercent = 0.1f;  // 10% hút máu
        float kiStealPercent = 0.1f;     // 10% hút ki

        // Kiểm tra và tính toán hút máu
        if (GameManager.Singleton.hutmauao > 0)
        {
            int bloodStealAmount = Mathf.RoundToInt(damage * bloodStealPercent);  // Lượng máu hút

            // Cập nhật HP mà không vượt quá HP tối đa
            GameManager.Singleton.hp = Mathf.Min(GameManager.Singleton.hp + bloodStealAmount, GameManager.Singleton.hpao);

            // Gọi hàm hiển thị thông báo về hút máu với màu đỏ
            PlayerController.Singleton.ShowTextPlayer(bloodStealAmount, Color.red);
        }

        // Kiểm tra và tính toán hút ki
        if (GameManager.Singleton.hutkiao > 0)
        {
            int kiStealAmount = Mathf.RoundToInt(damage * kiStealPercent);  // Lượng ki hút

            // Cập nhật MP mà không vượt quá MP tối đa
            GameManager.Singleton.mp = Mathf.Min(GameManager.Singleton.mp + kiStealAmount, GameManager.Singleton.mpao);

            // Gọi hàm hiển thị thông báo về hút ki với màu xanh
            PlayerController.Singleton.ShowTextPlayer(kiStealAmount, Color.blue);
        }

        // Hiển thị màu sắc của sát thương
        textDamage.color = color;
        textDamage.text = "- " + damage;

        // Giảm máu của kẻ thù (hoặc người chơi)
        //HP -= damage;
        sliderHp.value = HP;
        //int randexp = Random.Range(exp[0], exp[1]);
        //PlayerController.Singleton.ShowExp(randexp);
        //EquipmentManager.Singleton.Giamdoben();
        // Bắt đầu hiệu ứng nhấp nháy
        StartBlinkEffect();
        // Nếu máu <= 0, thực hiện các hành động chết
        if (HP <= 0)
        {
            rb.simulated = false;  // Dừng vật lý
            moveSpeed = 0;         // Dừng di chuyển
            gameObject.tag = "Untagged";
            // Hoạt ảnh chết (nếu có)
            if (animator != null)
            {
                animator.SetTrigger("Die");
            }

            sliderHp.value = 0;  // Cập nhật thanh máu

            // Rơi vật phẩm
            DropItem();

            // Thực hiện coroutine thời gian chết
            StartCoroutine(Dietime());
        }
    }
    public void StartBlinkEffect()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = StartCoroutine(BlinkEffect());
    }

    private IEnumerator BlinkEffect()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            // Màu nhấp nháy với độ trong suốt 50%
            spriteRenderer.color = new Color(blinkColor.r, blinkColor.g, blinkColor.b, 0.5f);

            yield return new WaitForSeconds(blinkDuration);

            // Màu gốc với độ trong suốt đầy đủ
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

            yield return new WaitForSeconds(blinkDuration);
        }

        // Đảm bảo màu cuối cùng là màu gốc
        spriteRenderer.color = originalColor;

        blinkCoroutine = null;
    }





    private void DropItem()
    {
        int randItem = Random.Range(0, Item.Length);
        Instantiate(Item[randItem], transform.position, Quaternion.identity);

    }

    IEnumerator Dietime()
    {
        yield return new WaitForSeconds(3f);
        // Xóa quái (thêm delay nếu có hoạt ảnh chết)

        //MonsterSpawner.Singleton.SpawnNewMonster();
        Destroy(gameObject, 0.5f); // Điều chỉnh thời gian delay nếu cần
    }
}
