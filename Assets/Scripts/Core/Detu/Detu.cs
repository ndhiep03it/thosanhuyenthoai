using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillDetu
{
    public string skillName;
    [JsonIgnore] // Newtonsoft.Json sẽ bỏ qua trường này khi lưu/đọc JSON// Tên kỹ năng
    public Sprite skillIcon;             // Ảnh kỹ năng
    [JsonIgnore] // Newtonsoft.Json sẽ bỏ qua trường này khi lưu/đọc JSON
    public string skillDescriptions;     // Mô tả kỹ năng
    public KeyCode activationKey;        // Phím kích hoạt kỹ năng
    [JsonIgnore] // Newtonsoft.Json sẽ bỏ qua trường này khi lưu/đọc JSON
    public float cooldownTime;           // Thời gian hồi chiêu

    [JsonIgnore] // Newtonsoft.Json sẽ bỏ qua trường này khi lưu/đọc JSON
    public GameObject skillEffect;       // Hiệu ứng kỹ năng (Prefab)
    [JsonIgnore] // Newtonsoft.Json sẽ bỏ qua trường này khi lưu/đọc JSON
    public Image cooldownFiller;         // Filler UI để hiển thị trạng thái cooldown
    [JsonIgnore] // Newtonsoft.Json sẽ bỏ qua trường này khi lưu/đọc JSON
    public Text txtCoolDownSkill;        // Text hiển thị thời gian hồi chiêu
    [JsonIgnore] // Newtonsoft.Json sẽ bỏ qua trường này khi lưu/đọc JSON
    public string animationTriggerName;  // Tên trigger trong Animator
    public int maxlevel;                    // Tối đa skill kỹ năng
    public int level;                    // Cấp kỹ năng
    public int mp;                    //  Năng lượng tiêu hao
    public int dame;                     // Sát thương kỹ năng
    public int satthuong;                     // Sát thương kỹ năng
    public int nechieu = 0; // Số lần né chiêu
    public skillType skillType;
    [JsonIgnore] // Newtonsoft.Json sẽ bỏ qua trường này khi lưu/đọc JSON
    public AudioSource soundSkill;       // Âm thanh kỹ năng


  

    // Lấy sát thương
    public int GetDamage(skillType skillType)
    {
        switch (skillType)
        {
            case skillType.SATTHUONG:
                return dame + (level * 5 + satthuong);
            default:
                // Trường hợp mặc định nếu không thuộc các skill trên
                return 0;
        }
    }
    public int GetHoiphuc(skillType skillType)
    {
        switch (skillType)
        {
            case skillType.HOIPHUC:
                GameManager.Singleton.StartCoroutine(HoiphucRoutine()); // Bắt đầu coroutine hồi phục
                return 1; // Trả về 1 để xác nhận rằng coroutine đã được gọi
            default:
                return 0;
        }
    }

    public IEnumerator HoiphucRoutine()
    {
        int value = 50; // Giá trị hồi phục mỗi lần
        int soLanHoiPhuc = 0;
        while (soLanHoiPhuc < 5)
        {
            bool hpDay = GameManager.Singleton.hp >= GameManager.Singleton.hpao;
            bool mpDay = GameManager.Singleton.mp >= GameManager.Singleton.mpao;

            if (hpDay && mpDay)
            {
                Thongbao.Singleton.ShowThongbao("HP và MP đã đầy.");
                yield break; // Dừng coroutine nếu HP và MP đã đầy
            }
            else
            {
                int hpTruoc = GameManager.Singleton.hp;
                int mpTruoc = GameManager.Singleton.mp;

                GameManager.Singleton.hp = Mathf.Min(GameManager.Singleton.hp + value, GameManager.Singleton.hpao);
                GameManager.Singleton.mp = Mathf.Min(GameManager.Singleton.mp + value, GameManager.Singleton.mpao);

                int hpHoiPhuc = GameManager.Singleton.hp - hpTruoc;
                int mpHoiPhuc = GameManager.Singleton.mp - mpTruoc;

                Thongbao.Singleton.ShowThongbao($"Đang hồi phục HP: {hpHoiPhuc}, MP: {mpHoiPhuc} (Lần {soLanHoiPhuc + 1}/5).");

                soLanHoiPhuc++;
                yield return new WaitForSeconds(1f); // Đợi 5 giây trước khi tiếp tục
            }
        }
        Thongbao.Singleton.ShowThongbao("Hồi phục hoàn tất.");
    }

    public int Hoiphuc()
    {
        int value = 0; // Giá trị hồi phục mặc định

        bool hpDay = GameManager.Singleton.hp >= GameManager.Singleton.hpao;
        bool mpDay = GameManager.Singleton.mp >= GameManager.Singleton.mpao;

        if (hpDay && mpDay)
        {
            Thongbao.Singleton.ShowThongbao("HP và MP đã đầy.");
        }
        else
        {
            int hpTruoc = GameManager.Singleton.hp;
            int mpTruoc = GameManager.Singleton.mp;

            GameManager.Singleton.hp = Mathf.Min(GameManager.Singleton.hp + value + dame + satthuong, GameManager.Singleton.hpao);
            GameManager.Singleton.mp = Mathf.Min(GameManager.Singleton.mp + value + dame + satthuong, GameManager.Singleton.mpao);

            int hpHoiPhuc = GameManager.Singleton.hp - hpTruoc;
            int mpHoiPhuc = GameManager.Singleton.mp - mpTruoc;

            Thongbao.Singleton.ShowThongbao($"Đang hồi phục HP: {hpHoiPhuc}, MP: {mpHoiPhuc}.");
        }

        return value;
    }


}
public class Detu : MonoBehaviour
{
    public static Detu singleton;
    public float speed = 5f;
    public Transform target;
    public float Distance = 2f;
    public Animator Animator;
    public Skill[] skills;                // Danh sách các kỹ năng
    public bool[] canUseSkill;            // Trạng thái có thể sử dụng kỹ năng
    public bool isAttack = false;
    public GameObject[] Enemies; // Danh sách các Enemy
    private bool isTansatActive = false; // Trạng thái tàn sát
    private int currentEnemyIndex = 0; // Chỉ số Enemy hiện tại
    public float targetRadius = 1.0f; // Khoảng cách tối thiểu để coi là đến nơi
    private Coroutine currentSkillCoroutine;
    public int countskill; // thứ tự kỹ năng
    private Rigidbody2D rb;
    public GameObject Miss;
    public Transform transformMiss;
    public GameObject canvas;
    public float distanceToEnemy;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        for (int i = 0; i < skills.Length; i++)
        {
            canUseSkill[i] = true;
        }
        target = GameObject.FindGameObjectWithTag("Player1").transform;
    }
    private void Awake()
    {
        if (singleton == null) singleton = this;
    }
    
    void Update()
    {
        if (target != null)
        {
            Distance = 1f;
            distanceToEnemy = Vector2.Distance(transform.position, target.position);
            Vector3 moveDirection = (target.position - transform.position).normalized; // Hướng di chuyển
            if (isAttack) // Chế độ tấn công quái
            {
                GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
                if (enemy != null)
                {
                    target = enemy.transform;
                    

                }
                else
                {
                }
                if (distanceToEnemy > 7) // Dịch chuyển tức thời nếu quá xa
                {
                    transform.position = target.position + new Vector3(-Distance, 0, 0);
                    rb.velocity = Vector2.zero; // Dừng di chuyển ngay
                }
                else if (distanceToEnemy < Distance) // Nếu gần quái, dừng & đánh
                {
                    Animator.SetBool("Run", false);
                    rb.velocity = Vector2.zero;
                    if (currentSkillCoroutine == null)
                    {
                        currentSkillCoroutine = StartCoroutine(AutoSkillLoop());
                    }
                }
                else // Nếu chưa đủ gần, di chuyển bằng Rigidbody
                {
                    rb.velocity = moveDirection * speed; // Di chuyển mượt bằng Rigidbody

                    // Đổi hướng pet
                    transform.localScale = new Vector3(moveDirection.x > 0 ? -1f : 1f, 1f, 1f);
                    canvas.transform.localScale = new Vector3(moveDirection.x > 0 ? -1f : 1f, 1f, 1f);
                    Animator.SetBool("Run", true);
                }
            }
            else
            {
                Distance = 2f;

                // Nếu khoảng cách quá xa (lớn hơn 20), dịch chuyển ngay lập tức
                if (distanceToEnemy > 4f)
                {
                    transform.position = target.position + new Vector3(-Distance, 0, 0); // Dịch pet về gần người chơi
                }
                else if (distanceToEnemy < Distance)
                {
                    // Nếu gần người chơi, chuyển sang trạng thái nhàn rỗi
                    Animator.SetBool("Run", false);
                    


                }
                else
                {
                    // Di chuyển pet đến vị trí của người chơi
                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                    // Đặt hướng của pet dựa trên vị trí của người chơi
                    if (transform.position.x < target.position.x)
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                        canvas.transform.localScale = new Vector3(-1f, 1f, 1f);
                    }
                    else if (transform.position.x > target.position.x)
                    {
                        transform.localScale = new Vector3(1f, 1f, 1f);
                        canvas.transform.localScale = new Vector3(1f, 1f, 1f);
                    }

                    // Đặt trạng thái di chuyển cho animator
                    Animator.SetBool("Run", true);
                }
            }
            
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player1").transform;
        }
        if (DetuManager.singleton.hp <= 0)
        {
            // Người chơi chết
            DetuManager.singleton.hp = 0; // Đảm bảo không âm máu

            //Animator.SetTrigger("Die");
            //Animator.SetBool("Run", false);
            skills[0].skillEffect.SetActive(false);
            skills[1].skillEffect.SetActive(false);
            speed = 0; // Dừng di chuyển
        }
        else
        {
            // Người chơi vẫn còn sống
            speed = 4; // Giữ tốc độ di chuyển
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Follow();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AutoSkill();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Hoisinh();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero; // Dừng chuyển động
            rb.bodyType  = RigidbodyType2D.Static;
            rb.simulated = false;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.simulated = true;

        }
    }

    private void Hoisinh()
    {
        // Người chơi vẫn còn sống
        speed = 4; // Giữ tốc độ di chuyển
        DetuManager.singleton.hp = 100;
        DetuManager.singleton.mp = 100;
        currentSkillCoroutine = null;
        for (int i = 0; i < skills.Length; i++)
        {
            canUseSkill[i] = true;
        }
    }

    IEnumerator AutoSkillLoop()
    {
        while (isAttack)
        {
            if (DetuManager.singleton.theluc > 0)
            {
                AutoSkill();
            }
            else
            {
                Thongbao.Singleton.ShowThongbao("Đệ của bạn đã hết thể lực");
            }
            
            yield return new WaitForSeconds(0.55f); // Tấn công mỗi giây
        }
        //currentSkillCoroutine = null; // Khi dừng, đặt lại coroutine
    }



    public void AutoSkill()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (canUseSkill[i])
            {
                StartCoroutine(ActivateSkill(i));
            }
        }
    }

    IEnumerator ActivateSkill(int skillIndex)
    {
        if (!canUseSkill[skillIndex]) yield break;

        //Debug.Log($"Kích hoạt kỹ năng {skillIndex}");
        canUseSkill[skillIndex] = false;
        EnemyController enemyController = target.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            bool isCriticalHit = IsCriticalHit();

            // Tính sát thương
            int damage = 50 + skills[countskill].GetDamage(skills[countskill].skillType);
            Color color = isCriticalHit ? Color.yellow : Color.white;

            if (isCriticalHit)
            {
                damage *= 2; // Sát thương chí mạng x2
            }

            // Gây sát thương lên kẻ địch
            enemyController.TakeDamage(damage, color);
        }

        // Kích hoạt animation kỹ năng
        if (Animator != null && !string.IsNullOrEmpty(skills[skillIndex].animationTriggerName))
        {
            Animator.SetTrigger(skills[skillIndex].animationTriggerName);
        }
        // Hiệu ứng kỹ năng
        if (skills[skillIndex].skillEffect != null)
        {
            switch (skillIndex)
            {
                case 0:
                    skills[0].skillEffect.SetActive(true);
                    StartCoroutine(HideSkill(0, 0.5f));
                    break;
                case 1:
                    skills[1].skillEffect.SetActive(true);
                    StartCoroutine(HideSkill(1, 0.5f));
                    break;
                
            }
           

        }
        // Phát âm thanh kỹ năng
        if (skills[skillIndex].soundSkill != null)
        {
            skills[skillIndex].soundSkill.Play();
        }

        // Bắt đầu hồi chiêu
        float cooldown = skills[skillIndex].cooldownTime;
        float elapsedTime = 0;

        while (elapsedTime < cooldown)
        {
            elapsedTime += Time.deltaTime;

            //// Cập nhật thanh hiển thị hồi chiêu
            //if (skills[skillIndex].cooldownFiller != null)
            //{
            //    skills[skillIndex].cooldownFiller.fillAmount = elapsedTime / cooldown;
            //}

            //// Cập nhật văn bản hiển thị thời gian còn lại
            //if (skills[skillIndex].txtCoolDownSkill != null)
            //{
            //    float remainingTime = Mathf.Max(0, cooldown - elapsedTime);
            //    skills[skillIndex].txtCoolDownSkill.text = remainingTime.ToString("F1");
            //}

            yield return null;
        }

        // Hoàn tất hồi chiêu
        canUseSkill[skillIndex] = true;
        if (skills[skillIndex].txtCoolDownSkill != null) 
        {
            skills[skillIndex].skillEffect.SetActive(false);
        }
        
        if (DetuManager.singleton.theluc > 0)
        {
            DetuManager.singleton.theluc -= 1;
        }
        else
        {
           
        }
        //Debug.Log($"Kỹ năng {skillIndex} đã hồi chiêu!");
    }
    // Hàm kiểm tra tỷ lệ chí mạng
    private bool IsCriticalHit()
    {
        // int criticalHitRate = 25;  // Ví dụ tỷ lệ chí mạng 25%
        int randomValue = Random.Range(0, 101);  // Giá trị ngẫu nhiên từ 0 đến 100

        return randomValue <= GameManager.Singleton.chimangao;
    }

    IEnumerator HideSkill(int skillIndex, float timder)
    {
        yield return new WaitForSeconds(timder);
        skills[skillIndex].skillEffect.SetActive(false);
    }

    public void Attack()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            target = enemy.transform;
            Thongbao.Singleton.ShowThongbao("Đã tấn công: Đối tượng " + target.gameObject.name);
            isAttack = true;
            currentSkillCoroutine = null;
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("Không tìm thấy kẻ địch để tấn công!");
        }
    }
    public void Follow()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player1");
        if (player != null)
        {
            target = player.transform;
            Thongbao.Singleton.ShowThongbao("Đã đi theo: Đối tượng " + target.gameObject.name);
            isAttack = false;
            //StopCoroutine(AutoSkillLoop());
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("Không tìm thấy người chơi để đi theo!");
        }
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
                GameObject obj = Instantiate(Miss, transformMiss.position, Quaternion.identity);
                TextMesh txtPlayer = obj.GetComponent<TextMesh>();
                txtPlayer.text = "Miss";
            }
            return; // Thoát hàm vì người chơi không nhận sát thương
        }

        // Trường hợp không né được, nhận sát thương
        DetuManager.singleton.hp -= dame;

        // Hiển thị hiệu ứng mất máu
        if (Miss != null && transformMiss != null)
        {
            GameObject obj = Instantiate(Miss, transformMiss.position, Quaternion.identity);
            TextMesh txtPlayer = obj.GetComponent<TextMesh>();
            txtPlayer.text = $"- {dame}"; // Hiển thị số máu mất
            SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Hit); // Phát âm thanh trúng
        }

        if (DetuManager.singleton.hp <= 0)
        {
            // Người chơi chết
            DetuManager.singleton.hp = 0; // Đảm bảo không âm máu
            speed = 0; // Dừng di chuyển
            //Thongbao.Singleton.ShowThongbao("Bạn đã bị hạ gục!");
            StopAllCoroutines();
            Animator.SetTrigger("Die");
        }
        else
        {
            // Người chơi vẫn còn sống
            speed = 4; // Giữ tốc độ di chuyển
            Animator.SetBool("Run", true);
           
        }
        //UImanager.Singleton.SetValue();
        // Cập nhật giao diện hoặc logic khác nếu cần (tùy chỉnh thêm)
    }
}
