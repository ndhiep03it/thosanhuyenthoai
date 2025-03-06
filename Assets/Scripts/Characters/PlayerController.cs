using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Singleton;
    [SerializeField] public Joystick MovementJoystick;
    private GameObject curenTarget;
    public bool IsPlatfrom = false; // false la mobile
    public EventSystem eventSystem; // Tham chiếu đến EventSystem
    [Header("PHAMVI")]
    //public GameObject currentTarget;
    public float radius;
    public Transform ciriOrigin;

    public float moveSpeed = 5f;       // Tốc độ di chuyển của nhân vật
    public float jumpForce = 10f;     // Lực nhảy khi bay
    private Rigidbody2D rb;           // Rigidbody2D để quản lý vật lý
    private Vector2 moveDirection;    // Hướng di chuyển
    public Animator animator;        // Animator để điều khiển animation
    public SpriteRenderer spriteRenderer; // SpriteRenderer để lật hình ảnh
    private bool isGrounded = true;   // Kiểm tra xem nhân vật đang trên mặt đất hay không

    public Transform groundCheck;     // Điểm kiểm tra mặt đất
    public float groundCheckRadius = 0.2f; // Bán kính kiểm tra mặt đất
    public LayerMask groundLayer;     // Lớp định nghĩa mặt đất
    //private SkillController skillController;
    public GameObject Steps;
    public GameObject Jump;
    public float x;
    public float y;
    public GameObject audioListener;
    [SerializeField] private GameObject dameTextObj;
    [SerializeField] private Transform canvasPlayer;
    private bool isFlying = false;       // Trạng thái đang bay
    public bool isWalking = true;       // Trạng thái đang bay



    public float flySpeed = 5f; // Tốc độ bay lên/xuống
    public float rotationSpeed = 100f; // Tốc độ xoay
    //public GameObject Chimbay;
    public SpriteRenderer chim;
    public Text txtprogress;
    public GameObject panelDie;
    public GameObject Miss;
    public Transform transformMiss;
    [Header("SKILL OBJ")]
    public SpriteRenderer[] SkillObj;
    [Header("Skill 3")]
    private Coroutine blinkCoroutine; // Lưu trữ coroutine nhấp nháy
    public Color blinkColor = Color.red; // Màu nhấp nháy (ví dụ: màu đỏ khi bị đánh)
    public float blinkDuration = 0.1f; // Thời gian mỗi lần nhấp nháy
    public int blinkCount = 1; // Số lần nhấp nháy
    private Color originalColor; // Màu gốc của SpriteRenderer
    public GameObject Xeng;
    public GameObject canvas;
    public CinemachineVirtualCamera CinemachineVirtual;
    public GameObject EggesAnimUI;
    public GameObject HaoquangSP;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
           
        }
        else
        {
           
        }
    }
    void Start()
    {
        //GameManager.Singleton.hp += EquipmentManager.Singleton.hp;
        //GameManager.Singleton.mp += EquipmentManager.Singleton.mp;
        //GameManager.Singleton.dame += EquipmentManager.Singleton.dame;
        //GameManager.Singleton.chimang += EquipmentManager.Singleton.chimang;
        
        
        audioListener.SetActive(false);
        
        StartCoroutine(timeActiveAudioListen());
        LoadPositionPlayerGame();
       

        rb = GetComponent<Rigidbody2D>(); // Lấy Rigidbody2D gắn với nhân vật
        //spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer gắn với nhân vật
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        //animator = GetComponent<Animator>(); // Lấy Animator gắn với nhân vật
                                             // skillController = GetComponent<SkillController>();
       // InvokeRepeating("GameManager.Singleton.SaveData", 0f, 1f);
    }

    public void SetTarget(GameObject target)
    {
        if (curenTarget != null)
        {
            PickUpItem pickUpItem = curenTarget.GetComponent<PickUpItem>();
            if (pickUpItem != null)
            {
                pickUpItem.HideArrow();
            }
        }

        curenTarget = target;
        PickUpItem pickUpItem1 = target.GetComponent<PickUpItem>();
        if (pickUpItem1 != null)
        {
            pickUpItem1.ShowArrow();
        }
    }
    IEnumerator timeActiveAudioListen()
    {
        yield return new WaitForSeconds(1.5f);
        audioListener.SetActive(true);
       


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 pos = ciriOrigin == null ? Vector3.zero : ciriOrigin.position;
        Gizmos.DrawSphere(pos, radius);
    }
    
    public void LoadPositionPlayer()
    {
        // Kiểm tra nếu có vị trí spawn được lưu trữ
        if (PlayerPrefs.HasKey("SpawnX") && PlayerPrefs.HasKey("SpawnY"))
        {
            float spawnX = PlayerPrefs.GetFloat("SpawnX");
            float spawnY = PlayerPrefs.GetFloat("SpawnY");

            // Đặt vị trí của Player
            transform.position = new Vector2(spawnX, spawnY);
        }
    }
    
    public void LoadPositionPlayerGame()
    {
        // Kiểm tra nếu có vị trí spawn được lưu trữ
        if (GameManager.Singleton.map != null)
        {
            x = GameManager.Singleton.x;
            y = GameManager.Singleton.y;

            // Đặt vị trí của Player
            transform.position = new Vector2(x, y);
            SceneManager.LoadScene(GameManager.Singleton.map);
        }
    }
    public void ResetJoystick()
    {
        MovementJoystick.ResetJoystick();

    }
    public void OnMapChangeStart()
    {
        MovementJoystick.StopJoystick(true); // Dừng joystick khi qua map
    }

    public void OnMapChangeEnd()
    {
        MovementJoystick.StopJoystick(false); // Kích hoạt lại joystick sau khi map load xong
    }
    void Update()
    {
        
        

    }





    void FixedUpdate()
    {

        if (Input.GetMouseButtonUp(0))
        {
            ResetJoystick();
        }

        // Kiểm tra trạng thái trên mặt đất
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Nhận input từ cả joystick và bàn phím
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        Vector2 joystickInput = new Vector2(MovementJoystick.Direction.x, MovementJoystick.Direction.y);
        Vector2 keyboardInput = new Vector2(moveX, moveY);
        if (isWalking)
        {
            // Ưu tiên input nào đang có giá trị
            if (joystickInput.magnitude > 0.1f)
            {
                moveDirection = joystickInput.normalized; // Di chuyển bằng joystick
            }
            else
            {
                moveDirection = keyboardInput.normalized; // Di chuyển bằng bàn phím
            }

            // Áp dụng di chuyển theo trục X và Y
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

            // Animation di chuyển và lật nhân vật
            if (moveDirection.magnitude > 0)
            {
                animator.SetBool("Run", true);
                Steps.SetActive(true);

                if (moveDirection.x > 0) // Di chuyển phải
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    canvas.transform.localScale = new Vector3(1f, 1f, 1f);
                    Thongbao.Singleton.txtTrangthai.gameObject.transform.localScale = new Vector3(0.007537152f, 0.007537152f, 0.007537152f);

                    chim.flipX = true;
                    HaoquangSP.SetActive(false);
                }
                else if (moveDirection.x < 0) // Di chuyển trái
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    canvas.transform.localScale = new Vector3(-1f, 1f, 1f);
                    chim.flipX = false;
                    Thongbao.Singleton.txtTrangthai.gameObject.transform.localScale = new Vector3(0.007537152f, 0.007537152f, 0.007537152f);
                    HaoquangSP.SetActive(false);

                }
                else if (moveDirection.x == 0) // Đứng Im
                {
                    HaoquangSP.SetActive(true);
                }
            }
            else
            {
                animator.SetBool("Run", false);
                Steps.SetActive(false);
                HaoquangSP.SetActive(true);

            }

            // Kiểm tra nếu nhân vật chết
            if (GameManager.Singleton.hp <= 0)
            {
                GameManager.Singleton.hp = 0;
                moveSpeed = 0;
                animator.SetTrigger("Die");
            }

            // Tăng tốc khi giữ phím LeftShift
            if (Input.GetKey(KeyCode.LeftShift) && moveDirection.magnitude > 0)
            {
                rb.velocity = moveDirection * (moveSpeed * 2f); // Tăng gấp đôi tốc độ di chuyển
                animator.SetBool("Run", true);
            }
            else
            {
                rb.velocity = moveDirection * moveSpeed; // Di chuyển với tốc độ bình thường
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Dừng hẳn nhân vật khi không đi bộ
            animator.SetBool("Run", false);
            Steps.SetActive(false);
        }
    }

    public void DaodatAnim(bool isdaodat)
    {
        animator.SetBool("Daodat", isdaodat);
    }
    public void Attack()
    {
        // Lấy tất cả các đối tượng trong bán kính tấn công
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(ciriOrigin.position, radius);
        UImanager.Singleton.SetValue();
        isWalking = false;
       
        // Kiểm tra nếu chiêu 5 đang được chọn
        if (SkillController.Singleton.countskill == 4)
        {
            if (SkillController.Singleton.skills[4].level > 0)
            {
                Thongbao.Singleton.ShowThongbao("Đang hồi phục chiêu 5.");
                SkillController.Singleton.skills[SkillController.Singleton.countskill].GetHoiphuc(SkillController.Singleton.skills[4].skillType);
                StartCoroutine(timeWalking());
                return; // Kết thúc hàm, không tấn công
            }
            else
            {
                Thongbao.Singleton.ShowThongbao("Chiêu 5 đã sẵn sàng để sử dụng!");
            }
        }

        // Tiến hành tấn công các đối tượng trong phạm vi
        foreach (Collider2D collider in hitColliders)
        {
            EnemyController enemyController = collider.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                bool isCriticalHit = IsCriticalHit();

                // Tính sát thương
                int damage = GameManager.Singleton.dameao + SkillController.Singleton.skills[SkillController.Singleton.countskill].GetDamage(SkillController.Singleton.skills[SkillController.Singleton.countskill].skillType);
                Color color = isCriticalHit ? Color.yellow : Color.white;

                if (isCriticalHit)
                {
                    damage *= 2; // Sát thương chí mạng x2
                }

                // Gây sát thương lên kẻ địch
                enemyController.TakeDamage(damage, color);
            }
        }
        StartCoroutine(timeWalking());
    }

    IEnumerator timeWalking()
    {
        yield return new WaitForSeconds(0.55f);
        isWalking = true;
    }
    //Skill 3
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
    // Hàm kiểm tra tỷ lệ chí mạng
    private bool IsCriticalHit()
    {
       // int criticalHitRate = 25;  // Ví dụ tỷ lệ chí mạng 25%
        int randomValue = Random.Range(0, 101);  // Giá trị ngẫu nhiên từ 0 đến 100

        return randomValue <= GameManager.Singleton.chimangao;
    }

    public void Takedame(int dame)
    {
        // Tính tỷ lệ né chiêu dựa trên điểm né chiêu
        float maxDodgeChance = 0.95f; // Tỷ lệ né tối đa (95%)
        float dodgeChance = Mathf.Clamp(GameManager.Singleton.neao / 1000f, 0f, maxDodgeChance); // Tỷ lệ né, tối đa 95%
        
        // Kiểm tra nếu né thành công
        if (UnityEngine.Random.Range(0f, 1f) < dodgeChance)
        {
            // Người chơi né thành công
            //Thongbao.Singleton.ShowThongbao($"Bạn đã né thành công đòn tấn công! (Tỷ lệ né: {dodgeChance * 100:F1}%)");

            // Hiển thị hiệu ứng hụt chiêu (nếu có)
            if (Miss != null && transformMiss != null)
            {
               GameObject obj =  Instantiate(Miss, transformMiss.position, Quaternion.identity);
               TextMesh txtPlayer = obj.GetComponent<TextMesh>();
               txtPlayer.text = "Miss";
            }
            return; // Thoát hàm vì người chơi không nhận sát thương
        }

        // Trường hợp không né được, nhận sát thương
        GameManager.Singleton.hp -= dame;

        // Hiển thị hiệu ứng mất máu
        if (Miss != null && transformMiss != null)
        {
            GameObject obj = Instantiate(Miss, transformMiss.position, Quaternion.identity);
            TextMesh txtPlayer = obj.GetComponent<TextMesh>();
            txtPlayer.text = $"- {dame}"; // Hiển thị số máu mất
            SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Hit); // Phát âm thanh trúng
        }

        if (GameManager.Singleton.hp <= 0)
        {
            // Người chơi chết
            GameManager.Singleton.hp = 0; // Đảm bảo không âm máu
            panelDie.SetActive(true); // Hiển thị giao diện chết
            moveSpeed = 0; // Dừng di chuyển
            //Thongbao.Singleton.ShowThongbao("Bạn đã bị hạ gục!");
            StopAllCoroutines();
            animator.SetTrigger("Die");
        }
        else
        {
            // Người chơi vẫn còn sống
            moveSpeed = 5; // Giữ tốc độ di chuyển
        }
        UImanager.Singleton.SetValue();
        // Cập nhật giao diện hoặc logic khác nếu cần (tùy chỉnh thêm)
    }


    // Hàm hiển thị thông báo về lượng máu và ki hút được
    public void ShowTextPlayer(int hut,Color messageColor)
    {
        // Hiển thị thông báo về hút máu và ki (thêm hiệu ứng chữ nếu cần)
        string message = "";

        // Hiển thị hút máu với màu đỏ
        if (hut > 0)
        {
            message += $"+{hut}";
                                       // Tạo đối tượng và hiển thị hiệu ứng
            if (Miss != null && transformMiss != null)
            {
                GameObject obj = Instantiate(Miss, transformMiss.position, Quaternion.identity);
                TextMesh txtPlayer = obj.GetComponent<TextMesh>();
                txtPlayer.text = message;  // Hiển thị thông báo
                txtPlayer.color = messageColor;  // Đặt màu cho văn bản
            }
            SoundManager.Instance.PlaySound(SoundManager.SoundEffect.SoundPlayerText); // Phát âm thanh trúng
        }

        

       
    }



    public void DisableEventSystem()
    {
        if (eventSystem != null)
        {
            eventSystem.gameObject.SetActive(false); // Tắt EventSystem

        }
    }

    public void EnableEventSystem()
    {
        if (eventSystem != null)
        {
            eventSystem.gameObject.SetActive(true); // Bật EventSystem

        }
    }

    public void ShowExp(int exp)
    {
        // Cộng thêm EXP vào tổng levelcount
        GameManager.Singleton.levelcount += exp;

        // Hiển thị EXP nhận được
        ShowExpGain(exp);

        // Xử lý cấp độ và EXP dư
        while (true)
        {
            int currentLevel = GameManager.Singleton.level;
            int nextLevelExp = GetExpForLevel(currentLevel + 1);

            if (GameManager.Singleton.levelcount >= nextLevelExp)
            {
                GameManager.Singleton.level++;
                GameManager.Singleton.diemkynang++;
                GameManager.Singleton.levelcount -= nextLevelExp;

                // Gọi thông báo lên cấp
                ShowLevelUpMessage(GameManager.Singleton.level);
            }
            else
            {
                break;
            }
        }

        // Cập nhật thanh trượt EXP và phần trăm tiến độ
        UpdateExpUI(GameManager.Singleton.level, GameManager.Singleton.levelcount);
    }

    // Hàm hiển thị EXP nhận được
    private void ShowExpGain(int exp)
    {
        GameObject obj = Instantiate(dameTextObj, canvasPlayer, false);
        Text textDamage = obj.GetComponent<Text>();
        textDamage.text = "+ " + exp;
        textDamage.color = Color.yellow;
    }
    

    // Hàm cập nhật thanh trượt EXP và tiến độ
    public void UpdateExpUI(int currentLevel, int currentExp)
    {
        int nextLevelExp = GetExpForLevel(currentLevel + 1);
        float progress = (float)currentExp / nextLevelExp;
        GameManager.Singleton.levelcountNext = nextLevelExp;
        // Cập nhật slider
        UImanager.Singleton.sliderCountBar.maxValue = GetExpForLevel(currentLevel + 1);
        UImanager.Singleton.sliderCountBar.value = currentExp;
        UImanager.Singleton.sliderCountBar.maxValue = GetExpForLevel(currentLevel + 1);

        // Hiển thị phần trăm tiến độ
        UImanager.Singleton.txtLevelCount.text = $"+{Mathf.Clamp(Mathf.RoundToInt(progress * 100), 0, 100)}%";
    }

    // Hàm hiển thị thông báo lên cấp
    private void ShowLevelUpMessage(int level)
    {
        GameObject obj = Instantiate(dameTextObj, canvasPlayer, false);
        Text textLevelUp = obj.GetComponent<Text>();
        textLevelUp.text = "Lên cấp " + level + "!";
        textLevelUp.color = Color.green;

        // Phát âm thanh lên cấp (nếu có)
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.LevelUp);
    }

    // Hàm lấy EXP ngưỡng cho cấp độ
    private int GetExpForLevel(int level)
    {
        int[] expThresholds = {
        0, 100, 300, 600, 1000, 1500, 2100, 2800, 3600, 4500, // Levels 1-10
        5500, 6600, 7800, 9100, 10500, 12000, 13600, 15300, 17100, 19000, // Levels 11-20
        21000, 23100, 25300, 27600, 30000, 32500, 35100, 37800, 40600, 43500, // Levels 21-30
        46500, 49600, 52800, 56100, 59500, 63000, 66600, 70300, 74100, 78000, // Levels 31-40
        82000, 86100, 90300, 94600, 99000, 103500, 108100, 112800, 117600, 122500 // Levels 41-50
    };

        return (level - 1 >= 0 && level - 1 < expThresholds.Length) ? expThresholds[level - 1] : 0;
    }



    public void SetCamera(float setx)
    {
        CinemachineVirtual.m_Lens.OrthographicSize = setx;
    }

    public void DefaultCamera()
    {
        CinemachineVirtual.m_Lens.OrthographicSize = 4.86f;
    }

    public void AnimThuHoachtrung()
    {
        EggesAnimUI.SetActive(true);
        StartCoroutine(timeTatAnimtrung());

    }
    IEnumerator timeTatAnimtrung()
    {
        yield return new WaitForSeconds(1f);
        EggesAnimUI.SetActive(false);
    }
}
