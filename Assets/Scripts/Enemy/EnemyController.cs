using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    private HashSet<GameObject> objectsInTrigger = new HashSet<GameObject>();
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
    public int vitribandau; // Vị trí mặc định của quái
    public GameObject[] ItemNV;
    public GameObject hurtBloodPrefabs;

    public event System.Action OnDeath = delegate { };

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
        //animator.SetBool("Idle", false);
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
        //animator.SetBool("Idle", true);
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
        if (collision.CompareTag("Player") || collision.CompareTag("Detu1"))
        {
            // Kiểm tra nếu người chơi còn sống
            if (collision.CompareTag("Player") && GameManager.Singleton.hp > 0 && damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamagePlayerOverTime());
            }
            else if (collision.CompareTag("Detu1") && DetuManager.singleton.hp > 0 && damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamagePlayerOverTimeDetu());
            }

            // Chỉ cần set tốc độ nếu entity còn sống
            if (GameManager.Singleton.hp > 0 || DetuManager.singleton.hp > 0)
            {
                moveSpeed = 5; // Giữ tốc độ di chuyển
                isIdle = true;
                maxMoveSpeed = 0f;
                minMoveSpeed = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Detu1"))
        {
            isIdle = false;
            maxMoveSpeed = 3f;
            minMoveSpeed = 1f;

            // Dừng đúng coroutine trừ máu
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }

            // Nếu nhân vật/đệ tử còn sống, hồi tốc độ di chuyển
            if (GameManager.Singleton.hp > 0 || DetuManager.singleton.hp > 0)
            {
                // Hồi tốc độ di chuyển
                moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            }

            
        }
    }


    // Coroutine trừ máu người chơi mỗi 2 giây
    private IEnumerator DamagePlayerOverTime()
    {       
        while (GameManager.Singleton.hp > 0) // Chạy khi Detu còn sống
        {
            int ranđame = Random.Range(dameMin, dameMax);
            animator.SetTrigger("Attack");
            animator.SetBool("Walk", false);
            PlayerController.Singleton.Takedame(ranđame); // Trừ máu người chơi

            yield return new WaitForSeconds(2f); // Chờ 2 giây trước khi trừ máu lần nữa
        }

        // Khi máu Detu về 0, thoát khỏi coroutine
        yield break;
    }
    private IEnumerator DamagePlayerOverTimeDetu()
    {
        if (DetuManager.singleton == null || Detu.singleton == null)
        {
            yield break; // Thoát ngay nếu thiếu đối tượng quản lý
        }

        const float attackInterval = 2f; // Dễ dàng điều chỉnh thời gian tấn công

        while (DetuManager.singleton.hp > 0) // Chạy khi Detu còn sống
        {
            int randDame = Random.Range(dameMin, dameMax);
            animator.SetTrigger("Attack");
            Detu.singleton.Takedame(randDame); // Trừ máu đệ tử 

            yield return new WaitForSeconds(attackInterval); // Chờ trước khi gây sát thương tiếp
        }

        // Khi Detu chết, reset trạng thái
        ResetCharacterState();
    }

    private void ResetCharacterState()
    {
        isIdle = false;
        moveSpeed = 5;
        maxMoveSpeed = 3f;
        minMoveSpeed = 1f;
        // Dừng đúng coroutine trừ máu
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    public void TakeDamage(int damage, Color color)
    {
        // Hành động khi bị tấn công
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Hit); // Phát âm thanh trúng
        Instantiate(hurtBloodPrefabs, transform.position, Quaternion.identity);
        animator.SetTrigger("Hurt");
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
        Thongbao.Singleton.sliderHpEnemy.value = HP;
        Thongbao.Singleton.sliderHpEnemy.maxValue = HPMAX;
        Thongbao.Singleton.txtHPenemy.text = HP + "/" + HPMAX;

        // Giảm máu của kẻ thù (hoặc người chơi)
        HP -= damage;
        sliderHp.value = HP;
        int randexp = Random.Range(exp[0], exp[1]);
        PlayerController.Singleton.ShowExp(randexp);
        EquipmentManager.Singleton.Giamdoben();
        // Bắt đầu hiệu ứng nhấp nháy
        StartBlinkEffect();
        // Nếu máu <= 0, thực hiện các hành động chết
        if (HP <= 0)
        {
            HP = 0;
            
            switch (GameManager.Singleton.idnhiemvu) 
            {

                case 0:
                    break;
                case 1:
                    QuestManager.Instance.EnemyKilled(1);
                    break;
                case 2:
                    QuestManager.Instance.EnemyKilled(2);
                    break;
                case 3:
                    QuestManager.Instance.EnemyKilled(3);

                    break;
                case 4:
                    //QuestManager.Instance.EnemyKilled(4);
                    if (Random.value < 0.5f) // Xác suất 50%
                    {
                        // Thực hiện hành động
                       // int randItem = Random.Range(0, Item.Length);
                        Instantiate(ItemNV[0], transform.position, Quaternion.identity);
                    }

                    break;
            
            }

            
            moveSpeed = 0;         // Dừng di chuyển
            gameObject.tag = "Untagged";
            // Hoạt ảnh chết (nếu có)
            if (animator != null)
            {
                animator.SetTrigger("Die");
            }

            sliderHp.value = 0;  // Cập nhật thanh máu
            OnDeath?.Invoke();
            // Rơi vật phẩm
            DropItem();
            //rb.simulated = false;  // Dừng vật lý
            // Thực hiện coroutine thời gian chết
            //StartCoroutine(Dietime());
            //gameObject.SetActive(false);
            Destroy(gameObject);
            MonsterSpawner.Singleton.SpawnNewMonster(vitribandau);
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

        // Ẩn quái thay vì Destroy
        //gameObject.SetActive(false);
        //MonsterSpawner.Singleton.SpawnNewMonster(vitribandau);
    }

}
