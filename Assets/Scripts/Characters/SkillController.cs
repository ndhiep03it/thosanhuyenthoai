using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public enum skillType
{
    SATTHUONG,
    HOIPHUC,
    DICHCHUYEN
}

[System.Serializable]
public class Skill
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
    public int levelyc;                    // Cấp kỹ năng
    public int mp;                    //  Năng lượng tiêu hao
    public int dame;                     // Sát thương kỹ năng
    public int satthuong;                     // Sát thương kỹ năng
    public int nechieu = 0; // Số lần né chiêu
    public skillType skillType;
    [JsonIgnore] // Newtonsoft.Json sẽ bỏ qua trường này khi lưu/đọc JSON
    public AudioSource soundSkill;       // Âm thanh kỹ năng
   

    // Nâng cấp kỹ năng
    //public void UpgradeSkill()
    //{
    //    //level++;
    //    dame += 10; // Tăng sát thương mỗi cấp

    //    cooldownTime = Mathf.Max(0.5f, cooldownTime - 0.2f); // Giảm thời gian hồi, tối thiểu 0.5s
    //}

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
public class SkillController : MonoBehaviour
{
    public static SkillController Singleton;
    private string saveFilePath;


    public Text[] txtNutnhan;             // Text hiển thị phím kích hoạt
    public Skill[] skills;                // Danh sách các kỹ năng
    public bool[] canUseSkill;            // Trạng thái có thể sử dụng kỹ năng
    public Animator playerAnimator;       // Animator của nhân vật
    public PlayerController playerController; // Controller của nhân vật
    public Button[] buttonsSkillClick;    // Các nút kích hoạt kỹ năng
    public int countskill; // thứ tự kỹ năng
    //public int level;                    // Cấp kỹ năng
    //public int mp;                    //  Năng lượng tiêu hao
    //public int dame;                     // Sát thương kỹ năng
    //public int satthuong;                     // Sát thương kỹ năng
     public skillType skillType;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/playerSkill.json";
        //SaveSkill();
        if (playerAnimator == null)
        {
            Debug.LogError("Không tìm thấy Animator trên Player!");
        }
       
        // Khởi tạo trạng thái kỹ năng
        //canUseSkill = new bool[skills.Length];
        for (int i = 0; i < skills.Length; i++)
        {

            canUseSkill[i] = true;
        }
        for (int i = 0; i < skills.Length; i++)
        {
            canUseSkill[i] = true;

            if (skills[i].cooldownFiller != null)
                skills[i].cooldownFiller.fillAmount = 1;

            if (skills[i].txtCoolDownSkill != null)
                skills[i].txtCoolDownSkill.text = "";

            txtNutnhan[i].text = "[" + skills[i].activationKey + " ]";

            // Gán sự kiện cho nút
            if (buttonsSkillClick != null && buttonsSkillClick.Length > i && buttonsSkillClick[i] != null)
            {
                int index = i; // Đảm bảo không bị closure
                buttonsSkillClick[i].onClick.AddListener(() => OnSkillButtonClicked(index));
               
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            // Kiểm tra nếu skill có level > 0 thì hiển thị nút, ngược lại ẩn đi
            bool isSkillAvailable = skills[i].level > 0;
            buttonsSkillClick[i].gameObject.SetActive(isSkillAvailable);

            // Nếu skill có thể dùng và phím được nhấn, thì kích hoạt skill
            if (isSkillAvailable && canUseSkill[i] && Input.GetKeyDown(skills[i].activationKey))
            {
                OnSkillButtonClicked(i);
            }
        }
    }
    //void Update()
    //{
    //    for (int i = 0; i < skills.Length; i++)
    //    {
    //        if (skills[0].level <= 0)
    //        {
    //            buttonsSkillClick[0].gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            buttonsSkillClick[0].gameObject.SetActive(true);
    //            if (Input.GetKeyDown(skills[i].activationKey) && canUseSkill[i])
    //            {
    //                //StartCoroutine(ActivateSkill(i));
    //                OnSkillButtonClicked(i);
    //            }
    //        }
    //        if (skills[1].level <= 0)
    //        {
    //            buttonsSkillClick[1].gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            buttonsSkillClick[1].gameObject.SetActive(true);
    //            if (Input.GetKeyDown(skills[i].activationKey) && canUseSkill[i])
    //            {
    //                //StartCoroutine(ActivateSkill(i));
    //                OnSkillButtonClicked(i);
    //            }
    //        }
    //        if (skills[2].level <= 0)
    //        {
    //            buttonsSkillClick[2].gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            buttonsSkillClick[2].gameObject.SetActive(true);
    //            if (Input.GetKeyDown(skills[i].activationKey) && canUseSkill[i])
    //            {
    //                //StartCoroutine(ActivateSkill(i));
    //                OnSkillButtonClicked(i);
    //            }
    //        }
    //        if (skills[3].level <= 0)
    //        {
    //            buttonsSkillClick[3].gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            buttonsSkillClick[3].gameObject.SetActive(true);
    //            if (Input.GetKeyDown(skills[i].activationKey) && canUseSkill[i])
    //            {
    //                //StartCoroutine(ActivateSkill(i));
    //                OnSkillButtonClicked(i);
    //            }
    //        }
    //        if (skills[4].level <= 0)
    //        {
    //            buttonsSkillClick[4].gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            buttonsSkillClick[4].gameObject.SetActive(true);
    //            if (Input.GetKeyDown(skills[i].activationKey) && canUseSkill[i])
    //            {
    //                //StartCoroutine(ActivateSkill(i));
    //                OnSkillButtonClicked(i);
    //            }
    //        }
    //        if (skills[5].level <= 0)
    //        {
    //            buttonsSkillClick[5].gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            buttonsSkillClick[4].gameObject.SetActive(true);
    //            if (Input.GetKeyDown(skills[i].activationKey) && canUseSkill[i])
    //            {
    //                //StartCoroutine(ActivateSkill(i));
    //                OnSkillButtonClicked(i);
    //            }
    //        }

    //    }
    //}
    public void OnSkillButtonClicked(int skillIndex)
    {
        countskill = skillIndex;
        if(GameManager.Singleton.theluc <= 0)
        {
            Thongbao.Singleton.ShowThongbao("Không đủ thể lực bạn đã quá mệt.");
            return;
        }
        // Kiểm tra xem kỹ năng có thể được sử dụng không (không trong thời gian hồi chiêu)
        if (canUseSkill[skillIndex])
        {
            // Lấy kỹ năng hiện tại
            var skill = skills[skillIndex];
            
            // Kiểm tra xem năng lượng hiện tại có đủ để sử dụng kỹ năng không
            if (GameManager.Singleton.mp >= skill.mp)
            {
                // Tiêu hao năng lượng
                GameManager.Singleton.mp -= skill.mp;

                // Cập nhật UI năng lượng nếu cần
                //UpdateMPUI();
                GameManager.Singleton.theluc -= 1;
                // Bắt đầu kích hoạt kỹ năng
                StartCoroutine(ActivateSkill(skillIndex));
               
                //Debug.Log($"Đang sử dụng {skills[skillIndex].skillName}!");
            }
            else
            {
                // Thông báo nếu không đủ năng lượng
                //Debug.Log($"Không đủ năng lượng để sử dụng kỹ năng {skill.skillName}!");
                Thongbao.Singleton.ShowThongbao($"Không đủ năng lượng để sử dụng kỹ năng {skill.skillName}!");
            }
        }
        else
        {
            // Thông báo nếu kỹ năng đang hồi chiêu
            //Debug.Log($"Kỹ năng {skills[skillIndex].skillName} đang hồi chiêu!");
            //Thongbao.Singleton.ShowThongbao($"Kỹ năng {skills[skillIndex].skillName} đang hồi chiêu!");
        }
    }


    public void AutoSkill()
    {
        if (skills == null || skills.Length == 0)
        {
            Debug.LogError("[AutoSkill] Không có kỹ năng nào để kích hoạt!");
            return;
        }

        for (int i = 0; i < 5; i++)
        {
            if (skills[i] == null)
            {
                Debug.LogWarning($"[AutoSkill] Skill tại vị trí {i} bị null!");
                continue;
            }

            StartCoroutine(ActivateSkill(i));
        }
    }


    IEnumerator ActivateSkill(int skillIndex)
    {
        if (skills == null || skills.Length <= skillIndex || skills[skillIndex] == null)
        {
            Debug.LogError($"[ActivateSkill] Skill tại index {skillIndex} bị null hoặc ngoài phạm vi.");
            yield break;
        }

        if (canUseSkill == null || canUseSkill.Length <= skillIndex)
        {
            Debug.LogError($"[ActivateSkill] canUseSkill[{skillIndex}] không hợp lệ.");
            yield break;
        }

        if (!canUseSkill[skillIndex])
        {
            yield break;
        }

        canUseSkill[skillIndex] = false;

        if (playerController != null)
        {
            playerController.Attack();
        }
        else
        {
            Debug.LogWarning("[ActivateSkill] playerController bị null.");
        }

        // Kiểm tra animation
        if (playerAnimator != null && !string.IsNullOrEmpty(skills[skillIndex].animationTriggerName))
        {
            playerAnimator.SetTrigger(skills[skillIndex].animationTriggerName);
        }

        // Kích hoạt hiệu ứng kỹ năng
        if (skills[skillIndex].skillEffect != null)
        {
            skills[skillIndex].skillEffect.SetActive(true);
            StartCoroutine(HideSkill(skillIndex, 0.5f)); // Thời gian có thể điều chỉnh theo từng skill
        }
        else
        {
            Debug.LogWarning($"[ActivateSkill] skillEffect của skill[{skillIndex}] bị null.");
        }
        //switch (skillIndex)
        //{ 
        //    case 0:
        //        break;
        //    case 1:
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //    case 4:
        //        break;
        //    case 5:
        //        skills[skillIndex].skillEffect.SetActive(true);
        //        StartCoroutine(HideSkill(skillIndex, 10f)); // Thời gian có thể điều chỉnh theo từng skill
        //        break;
        //    default:

        //        break;
        //}

        // Phát âm thanh kỹ năng
        if (skills[skillIndex].soundSkill != null)
        {
            skills[skillIndex].soundSkill.Play();
        }

        // Hồi chiêu
        float cooldown = skills[skillIndex].cooldownTime;
        float elapsedTime = 0;

        while (elapsedTime < cooldown)
        {
            elapsedTime += Time.deltaTime;

            if (skills[skillIndex].cooldownFiller != null)
            {
                skills[skillIndex].cooldownFiller.fillAmount = elapsedTime / cooldown;
            }

            if (skills[skillIndex].txtCoolDownSkill != null)
            {
                skills[skillIndex].txtCoolDownSkill.text = (cooldown - elapsedTime).ToString("F1");
            }

            yield return null;
        }

        // Hoàn thành hồi chiêu
        canUseSkill[skillIndex] = true;

        if (skills[skillIndex].cooldownFiller != null)
        {
            skills[skillIndex].cooldownFiller.fillAmount = 1;
        }

        if (skills[skillIndex].txtCoolDownSkill != null)
        {
            skills[skillIndex].txtCoolDownSkill.text = "";
        }

        if (skills[skillIndex].skillEffect != null)
        {
            skills[skillIndex].skillEffect.SetActive(false);
        }
    }


    //public void UpgradeAllSkills()
    //{
    //    foreach (var skill in skills)
    //    {
    //        skill.UpgradeSkill();
    //    }
    //}
    IEnumerator HideSkill(int skillIndex,float timder)
    {
        yield return new WaitForSeconds(timder);
        skills[skillIndex].skillEffect.SetActive(false);
    }
    public void SaveSkill()
    {
        try
        {
            // Ánh xạ dữ liệu
            SkillSaveData skillSaveData = MapToSaveData(skills);

            // Chuyển dữ liệu thành JSON
            string skillJson = JsonConvert.SerializeObject(skillSaveData, Formatting.Indented);

            // Mã hóa dữ liệu JSON
            string encryptedJson = EncryptionUtility.Encrypt(skillJson);

            // Lưu tệp
            File.WriteAllText(saveFilePath, encryptedJson);

           // Debug.Log("Kỹ năng đã được lưu thành công.");
        }
        catch (Exception e)
        {
            Debug.LogError("Không thể lưu kỹ năng: " + e.Message);
        }
    }
    public void LoadSkill()
    {
        try
        {
            // Đọc tệp được mã hóa
            if (!File.Exists(saveFilePath))
            {
               // Debug.LogWarning("Tệp lưu kỹ năng không tồn tại.");
                return;
            }

            string encryptedJson = File.ReadAllText(saveFilePath);

            // Giải mã dữ liệu JSON
            string decryptedJson = EncryptionUtility.Decrypt(encryptedJson);

            // Chuyển đổi JSON thành đối tượng SkillSaveData
            SkillSaveData skillSaveData = JsonConvert.DeserializeObject<SkillSaveData>(decryptedJson);

            // Ánh xạ dữ liệu JSON thành danh sách kỹ năng
            for (int i = 0; i < skills.Length; i++)
            {
                if (skillSaveData.skillsave.TryGetValue($"skill{i + 1}", out Skill savedSkill))
                {
                    skills[i].skillName = savedSkill.skillName;
                    //skills[i].skillDescriptions = savedSkill.skillDescriptions;
                    //skills[i].activationKey = savedSkill.activationKey;
                    //skills[i].cooldownTime = savedSkill.cooldownTime;
                    //skills[i].animationTriggerName = savedSkill.animationTriggerName;
                    //skills[i].maxlevel = savedSkill.maxlevel;
                    skills[i].level = savedSkill.level;
                    skills[i].mp = savedSkill.mp;
                    skills[i].dame = savedSkill.dame;
                    skills[i].satthuong = savedSkill.satthuong;
                    skills[i].nechieu = savedSkill.nechieu;



                }
                else
                {
                   // Debug.LogWarning($"Không tìm thấy kỹ năng {i + 1} trong dữ liệu lưu.");
                }
            }

           // Debug.Log("Kỹ năng đã được tải thành công.");
        }
        catch (Exception e)
        {
            Debug.LogError("Không thể tải dữ liệu kỹ năng: " + e.Message);
        }
    }

    public static SkillSaveData MapToSaveData(Skill[] skills)
    {
        var skillSaveData = new SkillSaveData
        {
            skillsave = new Dictionary<string, Skill>()
        };

        for (int i = 0; i < skills.Length; i++)
        {
            var skill = skills[i];
            skillSaveData.skillsave.Add($"skill{i + 1}", new Skill
            {
                skillName = skill.skillName,
                //skillIconPath = skill.skillIcon != null ? skill.skillIcon.name : "",
                //skillDescriptions = skill.skillDescriptions,
                //activationKey = skill.activationKey,
                //cooldownTime = skill.cooldownTime,
                //animationTriggerName = skill.animationTriggerName,
                maxlevel = skill.maxlevel,
                level = skill.level,
                mp = skill.mp,
                dame = skill.dame,
                satthuong = skill.satthuong,
                //soundSkillPath = skill.soundSkill != null ? skill.soundSkill.clip.name : ""
            });
        }

        return skillSaveData;
    }


}


[System.Serializable]
public class SkillSaveData
{
    public Dictionary<string, Skill> skillsave = new Dictionary<string, Skill>();
}
