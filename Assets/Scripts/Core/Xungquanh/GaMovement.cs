using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GaMovement : MonoBehaviour
{
    public float minSpeed = 1f; // Tốc độ nhỏ nhất
    public float maxSpeed = 3f; // Tốc độ lớn nhất
    public float idleTime = 2f; // Thời gian dừng giữa các lần di chuyển
    public Collider2D[] moveArea; // Vùng di chuyển

    private Vector2 targetPosition;
    private float moveSpeed;
    private bool isIdle = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public GameObject[] eggesPrefabs;
    public Transform[] vitridetrung;  // vị trí để trứng

    [Range(0f, 1f)] public float eggLayChance = 0.3f; // Tỷ lệ đẻ trứng (30%)

    // Âm thanh
    public AudioClip eggLaySound;
    public AudioClip[] randomCrowSound;
    private AudioSource audioSource;
    public float crowChance = 0.2f; // Xác suất gáy ngẫu nhiên (20%)
    public GameObject Thongbaodetrung;
    public GameObject Thongbaogay;
    public Transform ciriOrigin;
    private float radius;


    private void OnDrawGizmosSelected()
    {
        if (ciriOrigin != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(ciriOrigin.position, radius);
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (moveArea == null || moveArea.Length == 0)
        {
            Debug.LogError("Chưa gán vùng di chuyển cho cá!");
            return;
        }

        SetNewTargetPosition();

        // Gọi hàm gáy ngẫu nhiên
        StartCoroutine(RandomCrowRoutine());
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
        spriteRenderer.flipX = targetPosition.x < transform.position.x;
    }

    IEnumerator EnterIdleState()
    {
        isIdle = true;
        animator.SetBool("Walk", false); // Chuyển sang trạng thái nghỉ

        yield return new WaitForSeconds(idleTime); // Nghỉ trong thời gian idleTime

        // Thử đẻ trứng
        if (TryLayEgg(out Transform targetNest))
        {
            // Đi đến vị trí để trứng
            while (Vector2.Distance(transform.position, targetNest.position) > 0.1f)
            {
                // Di chuyển đến vị trí đẻ trứng
                transform.position = Vector2.MoveTowards(transform.position, targetNest.position, moveSpeed * Time.deltaTime);
                spriteRenderer.flipX = targetNest.position.x < transform.position.x;
                animator.SetBool("Walk", true); // Bật animation đi bộ
                yield return null;
            }

            // Khi đến nơi, dừng lại và chuyển sang animation ngồi
            animator.SetBool("Walk", false);
            animator.SetTrigger("Sit"); // Chạy animation ngồi
            yield return new WaitForSeconds(8f); // Ngồi tại vị trí 5 giây

            // Sau khi ngồi 5 giây, TIẾN HÀNH ĐẺ TRỨNG TẠI targetNest
            //GameObject eggPrefab = eggesPrefabs[Random.Range(0, eggesPrefabs.Length)];
            //Instantiate(eggPrefab, targetNest.position, Quaternion.identity);
            EggesTrigger();

            // Phát âm thanh đẻ trứng nếu có
            if (eggLaySound != null)
            {
                Thongbaodetrung.SetActive(true);
                Thongbaogay.SetActive(false);
                audioSource.PlayOneShot(eggLaySound);

                // Tắt thông báo đẻ trứng sau 3 giây
                StartCoroutine(TatThongBao(Thongbaodetrung, 3f));
            }

        }

        isIdle = false;
        SetNewTargetPosition(); // Tìm vị trí mới để đi tiếp
    }

    IEnumerator TatThongBao(GameObject thongBao, float delay)
    {
        yield return new WaitForSeconds(delay);
        thongBao.SetActive(false);
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

        //Debug.LogWarning("Không tìm được vị trí hợp lệ sau 10 lần thử.");
    }

    bool TryLayEgg(out Transform targetNest)
    {
        targetNest = null;

        if (Random.value < eggLayChance) // Xác suất đẻ trứng
        {
            if (vitridetrung.Length > 0)
            {
                // Chọn vị trí ngẫu nhiên để đẻ trứng nhưng CHƯA đẻ ngay
                targetNest = vitridetrung[Random.Range(0, vitridetrung.Length)];
                return true; // Đã chọn vị trí để đẻ trứng
            }
        }
        return false; // Không đẻ trứng
    }



    IEnumerator RandomCrowRoutine()
    {
        while (true)
        {
            // Đợi trong một khoảng thời gian ngẫu nhiên
            yield return new WaitForSeconds(Random.Range(5f, 15f));

            if (Random.value < crowChance && randomCrowSound.Length > 0)
            {
                // Chọn tiếng gáy ngẫu nhiên
                AudioClip crowSound = randomCrowSound[Random.Range(0, randomCrowSound.Length)];
                audioSource.PlayOneShot(crowSound);

                Thongbaodetrung.SetActive(false);
                Thongbaogay.SetActive(true);

                // Tắt thông báo gáy sau 2 giây
                StartCoroutine(TatThongBao(Thongbaogay, 2f));
            }

        }
    }

    public void EggesTrigger()
    {
        // Lấy tất cả các đối tượng trong bán kính tấn công
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(ciriOrigin.position, radius);
      

        // Tiến hành tấn công các đối tượng trong phạm vi
        foreach (Collider2D collider in hitColliders)
        {
            Egges gaMovement = collider.GetComponent<Egges>();

            if (gaMovement != null)
            {

                // Tăng trứng
                gaMovement.AddEgges();
            }
        }
       
    }

}
