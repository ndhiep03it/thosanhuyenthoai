using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossAI : MonoBehaviour
{
    public static BossAI Singleton;
    public Transform player;            // Vị trí của người chơi
    public float attackRange = 3f;      // Phạm vi tấn công của Boss
    public float specialAbilityRange = 5f; // Phạm vi chiêu thức đặc biệt
    public float lowHealthThreshold = 30f; // Ngưỡng máu thấp để sử dụng chiêu thức mạnh
    public float timeBetweenActions = 2f; // Thời gian giữa các hành động của Boss
    //public float moveSpeed = 2f;        // Tốc độ di chuyển của Boss

    private float nextActionTime = 0f;   // Thời gian Boss có thể thực hiện hành động tiếp theo
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteRendererShadow;

    public BossAbility[] bossAbilities;  // Các chiêu thức của Boss

    public int health;                // Máu của Boss
    private Animator animator;           // Animator để điều khiển các hoạt ảnh



    public GameObject bulletPrefab;       // Prefab của đạn
    public float bulletSpeed = 5f;        // Tốc độ bay của đạn
    public int numberOfBullets = 8;       // Số lượng đạn tỏa ra
    public float bulletSpread = 180;      // Góc tỏa ra của đạn (tạo ra phạm vi góc)
    void Start()
    {
        health = 100; // Khởi tạo máu của Boss
        animator = GetComponent<Animator>(); // Lấy component Animator
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy component Animator
        Attack();
    }

    private void Awake()
    {
        if (Singleton == null) {
            Singleton = this;
        } 
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            // Xoay Boss về phía người chơi
            FlipSprite(distanceToPlayer);
            // Điều khiển hoạt ảnh di chuyển và tấn công
            if (distanceToPlayer <= attackRange)
            {
                // Nếu Boss gần người chơi, tấn công
                //UseAbility(bossAbilities[0]);  // Chiêu thức tấn công (ví dụ: Heavy Punch)
                animator.SetBool("Walk", false); // Dừng đi bộ khi tấn công
               
            }
            else if (distanceToPlayer <= specialAbilityRange && health < lowHealthThreshold)
            {
                // Nếu Boss có máu thấp và gần người chơi, sử dụng chiêu thức đặc biệt
                UseAbility(bossAbilities[1]);  // Chiêu thức mạnh (ví dụ: Shockwave)
                animator.SetBool("Walk", false); // Dừng đi bộ khi sử dụng chiêu thức
                //imator.SetTrigger("Attack");  // Kích hoạt hoạt ảnh tấn công
            }
            else
            {
                // Nếu không có tình huống đặc biệt, Boss có thể di chuyển
                // UseAbility(bossAbilities[Random.Range(0, bossAbilities.Length)]);
              
                //animator.SetBool("Walk", true); // Kích hoạt hoạt ảnh đi bộ
                //MoveTowardsPlayer(distanceToPlayer);
               // StopCoroutine(attackTime());
            }

            nextActionTime = Time.time + timeBetweenActions;
        }
    }
    // Xoay sprite của Boss đối diện với người chơi
    void FlipSprite(float distanceToPlayer)
    {
        if (distanceToPlayer > attackRange)
        {
            // Xoay sprite theo hướng của người chơi
            Vector3 direction = (player.position - transform.position).normalized;
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false;  // Nếu người chơi ở bên phải Boss
                spriteRendererShadow.flipX = false;  // Nếu người chơi ở bên phải Boss
            }
            else
            {
                spriteRenderer.flipX = true;   // Nếu người chơi ở bên trái Boss
                spriteRendererShadow.flipX = true;   // Nếu người chơi ở bên trái Boss
            }
        }
    }
    // Phương thức di chuyển Boss về phía người chơi, nhưng không quá sát
    void MoveTowardsPlayer(float distanceToPlayer)
    {
        float minDistance = attackRange + 1f;  // Khoảng cách tối thiểu mà Boss giữ từ người chơi

        // Nếu Boss ở xa người chơi và vẫn cần di chuyển
        if (distanceToPlayer > attackRange && distanceToPlayer > minDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            //transform.position += direction * moveSpeed * Time.deltaTime; // Di chuyển về phía người chơi
        }
        else if (distanceToPlayer <= minDistance)
        {
            // Dừng di chuyển khi gần đủ khoảng cách tối thiểu
            animator.SetBool("Walk", false); // Dừng hoạt ảnh đi bộ
        }
    }


    void UseAbility(BossAbility ability)
    {
        if (ability.CanActivate())
        {
           
            ability.ActivateAbility();
           
        }
    }

    // Cập nhật máu của Boss khi bị tấn công
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }
    public void Attack()
    {
        StartCoroutine(attackTime());
    }

    IEnumerator attackTime()
    {
        int attackCount = 100;  // Số lần tấn công (bạn có thể thay đổi tùy ý)

        for (int i = 0; i < attackCount; i++)
        {
            animator.SetTrigger("Attack");  // Kích hoạt hoạt ảnh tấn công
            PerformRandomAction();  // Gọi hành động ngẫu nhiên
            yield return new WaitForSeconds(5f);  // Đợi 5 giây trước khi tiếp tục
        }
    }



    // Hàm 2: Dùng chiêu thức đặc biệt
    void SpecialAbility()
    {
       // Debug.Log("Special ability function called!");
        // Các hành động chiêu thức đặc biệt ở đây
    }

    // Hàm 3: Di chuyển
    void Move()
    {
        //Debug.Log("Move function called!");
        // Các hành động di chuyển ở đây
    }
    // Hàm 4: Thực hiện hành động ngẫu nhiên
    void PerformRandomAction()
    {
        int randomChoice = Random.Range(0, 3); // Lựa chọn ngẫu nhiên 0, 1, hoặc 2
        switch (randomChoice)
        {
            case 0:
                ShootBullets();
                break;
            case 1:
                SpecialAbility();
                break;
            case 2:
                Move();
                break;
        }
    }
    // Phương thức bắn đạn
    public void ShootBullets()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            // Tính toán góc bắn cho từng viên đạn
            float angle = transform.eulerAngles.z + Random.Range(-bulletSpread, bulletSpread);

            // Tạo viên đạn và thiết lập vị trí
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));

            // Lấy Rigidbody2D của đạn để di chuyển
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Chuyển hướng của đạn
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                rb.velocity = direction * bulletSpeed;
            }
        }
    }
}
