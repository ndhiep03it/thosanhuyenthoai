using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Singleton;
    public Text txtNameSkill;               // Hiển thị tên và mô tả kỹ năng
    public Text txtDescriptionSkill;        // Hiển thị mô tả kỹ năng
    public Text txtScoreSkill;              // Hiển thị điểm kỹ năng
    public Text[] txtLevelSkill;              // Hiển thị cấp độ kỹ năng
    public Text[] txtPropertiesSkill;              // Hiển thị cấp độ kỹ năng
    public Image iconSkill;                 // Ảnh kỹ năng
    public Button[] buttonskill;            // Nút kỹ năng
    public GameObject[] IconTick;           // Icon hiển thị trạng thái kỹ năng (bật/tắt)

    public bool[] skillSelected;           // Trạng thái đã chọn kỹ năng
    public GameObject PanelProperties;
    public Button buttonNangcap;
    private int indexg;

    private void Start()
    {
        SkillController.Singleton.LoadSkill();
        // Khởi tạo mảng trạng thái kỹ năng
        skillSelected = new bool[SkillController.Singleton.skills.Length];
        PanelProperties.SetActive(true);
       
        // Gán sự kiện cho từng nút kỹ năng
        for (int i = 0; i < SkillController.Singleton.skills.Length; i++)
        {
            int index = i; // Biến cục bộ để tránh vấn đề closure
            buttonskill[i].onClick.AddListener(() => OnSkillButtonClicked(index));
            txtLevelSkill[i].text = SkillController.Singleton.skills[i].level.ToString();
            
        }

        // Gán sự kiện cho nút nâng cấp
        //buttonNangcap.onClick.AddListener(UpgradeSkill);
        UIData();
       
        txtDescriptionSkill.text = "";
        //txtScoreSkill.text = "Điểm kỹ năng:" + GameManager.Singleton.diemkynang;

        OnSkillButtonClicked(indexg);
        // Cập nhật trạng thái ban đầu
        UpdateUI();
    }
    private void Awake()
    {
        if (Singleton == null) Singleton = this;
    }
    private void Update()
    {
        txtScoreSkill.text = "Điểm kỹ năng: " + GameManager.Singleton.diemkynang;
        
    }
    private void OnEnable()
    {
        
        txtNameSkill.text = SkillController.Singleton.skills[0].skillName;
        iconSkill.sprite = SkillController.Singleton.skills[0].skillIcon;
        txtDescriptionSkill.text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[0].skillName}]</b>\n"
                         + $"<color=#B22222>Cấp độ tối đa:</color> {SkillController.Singleton.skills[0].maxlevel}\n"
                         + $"<color=#FFD700>Mô tả:</color> {SkillController.Singleton.skills[0].skillDescriptions}\n"
                         + $"<color=#00FF00>Phím kích hoạt:</color> {SkillController.Singleton.skills[0].activationKey}\n"
                         + $"<color=#FF4500>Thời gian hồi chiêu:</color> {SkillController.Singleton.skills[0].cooldownTime:F1} giây\n"
                         + $"<color=#1E90FF>Cấp độ kỹ năng:</color> {SkillController.Singleton.skills[0].level}\n"
                         + $"<color=#FF6347>Sát thương:</color> {SkillController.Singleton.skills[0].dame}%\n"
                         + $"<color=#32CD32>Sát thương tổng thể:</color> {SkillController.Singleton.skills[0].satthuong}\n"
                         + $"<color=#0000FF>Năng lượng tiêu hao:</color> {SkillController.Singleton.skills[0].mp}\n";
        txtScoreSkill.text = "Điểm kỹ năng: " + GameManager.Singleton.diemkynang;
    }
    public void UIData()
    {

        txtPropertiesSkill[0].text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[0].skillName}]</b>\n"
                                             + $"<color=#FF6347>Sát thương:</color> {SkillController.Singleton.skills[0].dame}%\n" +
                                              $"<color=#32CD32>Sát thương tổng thể:</color> {SkillController.Singleton.skills[0].satthuong}\n";
        txtPropertiesSkill[1].text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[1].skillName}]</b>\n"
                                             + $"<color=#FF6347>Sát thương:</color> {SkillController.Singleton.skills[1].dame}%\n" +
                                              $"<color=#32CD32>Sát thương tổng thể:</color> {SkillController.Singleton.skills[1].satthuong}\n";
        txtPropertiesSkill[2].text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[2].skillName}]</b>\n"
                                             + $"<color=#FF6347>Sát thương:</color> {SkillController.Singleton.skills[2].dame}%\n" +
                                              $"<color=#32CD32>Sát thương tổng thể:</color> {SkillController.Singleton.skills[2].satthuong}\n";
        txtPropertiesSkill[3].text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[3].skillName}]</b>\n"
                                             + $"<color=#FF6347>Sát thương:</color> {SkillController.Singleton.skills[3].dame}%\n" +
                                              $"<color=#32CD32>Sát thương tổng thể:</color> {SkillController.Singleton.skills[3].satthuong}\n";
        txtPropertiesSkill[4].text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[4].skillName}]</b>\n"
                                             + $"<color=#FF6347>Hồi phục HP - MP:</color> {SkillController.Singleton.skills[4].dame}%\n" +
                                              $"<color=#32CD32>Hồi phục tối đa</color> {SkillController.Singleton.skills[4].satthuong}\n";
        txtPropertiesSkill[5].text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[5].skillName}]</b>\n"
                                             + $"<color=#FF6347>Sát thương ma nhãn:</color> {SkillController.Singleton.skills[5].dame}%\n" +
                                              $"<color=#32CD32>Ma nhãn sát thương</color> {SkillController.Singleton.skills[5].satthuong}\n";


        txtLevelSkill[indexg].text = SkillController.Singleton.skills[indexg].level.ToString();
    }

    private void OnDisable()
    {
        PanelProperties.SetActive(true);
        txtNameSkill.text = "";
        txtDescriptionSkill.text = "";
        
    }

    public void OnSkillButtonClicked(int skillIndex)
    {
        PanelProperties.SetActive(true);
        // Bật/tắt trạng thái kỹ năng
        skillSelected[skillIndex] = !skillSelected[skillIndex];
        txtScoreSkill.text = "Điểm kỹ năng: " + GameManager.Singleton.diemkynang;
        // Lấy thông tin kỹ năng từ SkillController
        var skill = SkillController.Singleton.skills[skillIndex];
        if (skillIndex == 4)
        {
            // Cập nhật mô tả kỹ năng
            txtNameSkill.text = $"<b>[{skill.skillName}]</b>";
            iconSkill.sprite = skill.skillIcon;
            txtLevelSkill[skillIndex].text = skill.level.ToString();
            txtDescriptionSkill.text = $"<b>[Chiêu thức:{skill.skillName}]</b>\n"
                         + $"<color=#B22222>Cấp độ tối đa:</color> {skill.maxlevel}\n"
                         + $"<color=#FFD700>Mô tả:</color> {skill.skillDescriptions}\n"
                         + $"<color=#00FF00>Phím kích hoạt:</color> {skill.activationKey}\n"
                         + $"<color=#FF4500>Thời gian hồi chiêu:</color> {skill.cooldownTime:F1} giây\n"
                         + $"<color=#1E90FF>Cấp độ kỹ năng:</color> {skill.level}\n"
                         + $"<color=#FF6347>Hồi phục HP - MP:</color> {skill.dame}%\n"
                         + $"<color=#32CD32>Hồi phục tối đa::</color> {skill.satthuong}\n"
                         + $"<color=#0000FF>Năng lượng tiêu hao:</color> {skill.mp}\n";
            return;
        }
        if (skillIndex == 5)
        {
            // Cập nhật mô tả kỹ năng
            txtNameSkill.text = $"<b>[{skill.skillName}]</b>";
            iconSkill.sprite = skill.skillIcon;
            txtLevelSkill[skillIndex].text = skill.level.ToString();
            txtDescriptionSkill.text = $"<b>[Chiêu thức:{skill.skillName}]</b>\n"
                         + $"<color=#B22222>Cấp độ tối đa:</color> {skill.maxlevel}\n"
                         + $"<color=#FFD700>Mô tả:</color> {skill.skillDescriptions}\n"
                         + $"<color=#00FF00>Phím kích hoạt:</color> {skill.activationKey}\n"
                         + $"<color=#FF4500>Thời gian hồi chiêu:</color> {skill.cooldownTime:F1} giây\n"
                         + $"<color=#1E90FF>Cấp độ kỹ năng:</color> {skill.level}\n"
                         + $"<color=#FF6347>Sát thương ma nhãn:</color> {skill.dame}%\n"
                         + $"<color=#32CD32>Ma nhãn sát thương:</color> {skill.satthuong}\n"
                         + $"<color=#0000FF>Năng lượng tiêu hao:</color> {skill.mp}\n";
            return;
        }
        // Cập nhật thông tin kỹ năng vào Text
        txtNameSkill.text = $"<b>[{skill.skillName}]</b>";
        iconSkill.sprite = skill.skillIcon;
        txtLevelSkill[skillIndex].text = skill.level.ToString();
        txtDescriptionSkill.text = $"<b>[Chiêu thức:{skill.skillName}]</b>\n"
                         + $"<color=#B22222>Cấp độ tối đa:</color> {skill.maxlevel}\n"
                         + $"<color=#FFD700>Mô tả:</color> {skill.skillDescriptions}\n"
                         + $"<color=#00FF00>Phím kích hoạt:</color> {skill.activationKey}\n"
                         + $"<color=#FF4500>Thời gian hồi chiêu:</color> {skill.cooldownTime:F1} giây\n"
                         + $"<color=#1E90FF>Cấp độ kỹ năng:</color> {skill.level}\n"
                         + $"<color=#FF6347>Sát thương:</color> {skill.dame}%\n"
                         + $"<color=#32CD32>Sát thương tổng thể:</color> {skill.satthuong}\n"
                         + $"<color=#0000FF>Năng lượng tiêu hao:</color> {skill.mp}\n";

        // Debug log để kiểm tra thông tin được in ra
        //Debug.Log(skillIndex);
        //Debug.Log($"Skill {SkillController.Singleton.skills[skillIndex].skillName} selected: {skillSelected[skillIndex]}");
        UIData();

        // Cập nhật giao diện
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < IconTick.Length; i++)
        {
            if (IconTick[i] != null)
            {
                // Bật/tắt IconTick dựa trên trạng thái `skillSelected`
                //IconTick[i].SetActive(skillSelected[i]);
            }
        }
    }
    public void Nangcap(int index)
    {
        // Lấy kỹ năng hiện tại
        indexg = index;
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);

    }
    public void ResetDiem()
    {
        // Tính tổng điểm kỹ năng hiện tại
        int sumDiemkynang = 0;
        foreach (var skill in SkillController.Singleton.skills)
        {
            sumDiemkynang += skill.level; 
            skill.level = 0;             
            skill.dame = 0;              
            skill.satthuong = 0;
            skill.mp = 0;
        }

        // Cộng điểm kỹ năng vào tổng điểm của người chơi
        GameManager.Singleton.diemkynang += sumDiemkynang;
       
        Thongbao.Singleton.ShowThongbao($"Đã reset kỹ năng. Tổng điểm kỹ năng hoàn lại: {sumDiemkynang}");
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Upgrade);
        UIData();
    }

    public void UpgradeSkill()
    {
        //var skill = SkillController.Singleton.skills[indexg];
        // Kiểm tra nếu cấp ddppj nâng cấp kỹ năng
        if (GameManager.Singleton.level < SkillController.Singleton.skills[indexg].levelyc)
        {
            // Thông báo kỹ năng đã đạt cấp tối đa
            Thongbao.Singleton.ShowThongbao($"Bạn chưa đủ cấp độ {SkillController.Singleton.skills[indexg].levelyc} để nâng cấp Kỹ năng {SkillController.Singleton.skills[indexg].skillName} !");
            return; // Dừng phương thức nếu đạt max level
        }

        // Kiểm tra nếu kỹ năng đã đạt cấp tối đa
        if (SkillController.Singleton.skills[indexg].level >= SkillController.Singleton.skills[indexg].maxlevel)
        {
            // Thông báo kỹ năng đã đạt cấp tối đa
            Thongbao.Singleton.ShowThongbao($"Kỹ năng {SkillController.Singleton.skills[indexg].skillName} đã đạt cấp độ tối đa!");
            return; // Dừng phương thức nếu đạt max level
        }
        // **Kiểm tra cấp độ nhân vật yêu cầu để nâng cấp**
        int requiredPlayerLevel = SkillController.Singleton.skills[indexg].level * 5; // Ví dụ: mỗi cấp kỹ năng cần nhân vật đạt cấp hiện tại * 5
        if (GameManager.Singleton.level < requiredPlayerLevel)
        {
            Thongbao.Singleton.ShowThongbao($"Cần đạt cấp {requiredPlayerLevel} để nâng cấp kỹ năng {SkillController.Singleton.skills[indexg].skillName}.");
            SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Upgrade);
            return;
        }

        // Kiểm tra xem có đủ điểm để nâng cấp không (cần lớn hơn hoặc bằng 2)
        if (GameManager.Singleton.diemkynang >= 2)
        {
            switch (indexg)
            {
                case 0:
                    //skill 1
                    // Tăng cấp độ kỹ năng
                    SkillController.Singleton.skills[indexg].level += 1;
                    SkillController.Singleton.skills[indexg].nechieu += 1;

                    // Tăng sát thương hoặc các thuộc tính khác nếu cần
                    SkillController.Singleton.skills[indexg].dame += 10; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].satthuong += SkillController.Singleton.skills[indexg].dame * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].mp += 2 * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                                                                          //SkillController.Singleton.skills[indexg].cooldownTime = Mathf.Max(0.5f, SkillController.Singleton.skills[indexg].cooldownTime - 0.2f); // Giảm thời gian hồi, tối thiểu 0.5s

                    // Trừ điểm kỹ năng (mỗi lần nâng cấp tốn 2 điểm)
                    GameManager.Singleton.diemkynang -= 1;

                    // Cập nhật UI cấp độ kỹ năng
                    txtLevelSkill[indexg].text = SkillController.Singleton.skills[indexg].level.ToString();
                    iconSkill.sprite = SkillController.Singleton.skills[indexg].skillIcon;
                    // Cập nhật điểm kỹ năng trong UI
                   

                    // Cập nhật mô tả kỹ năng
                    txtDescriptionSkill.text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[indexg].skillName}]</b>\n"
                                 + $"<color=#B22222>Cấp độ tối đa:</color> {SkillController.Singleton.skills[indexg].maxlevel}\n"
                                 + $"<color=#FFD700>Mô tả:</color> {SkillController.Singleton.skills[indexg].skillDescriptions}\n"
                                 + $"<color=#00FF00>Phím kích hoạt:</color> {SkillController.Singleton.skills[indexg].activationKey}\n"
                                 + $"<color=#FF4500>Thời gian hồi chiêu:</color> {SkillController.Singleton.skills[indexg].cooldownTime:F1} giây\n"
                                 + $"<color=#1E90FF>Cấp độ kỹ năng:</color> {SkillController.Singleton.skills[indexg].level}\n"
                                 + $"<color=#FF6347>Sát thương:</color> {SkillController.Singleton.skills[indexg].dame}%\n"
                                 + $"<color=#32CD32>Sát thương tổng thể:</color> {SkillController.Singleton.skills[indexg].satthuong}\n"
                                 + $"<color=#0000FF>Năng lượng tiêu hao:</color> {SkillController.Singleton.skills[indexg].mp}\n";
                    break;
                case 1:
                    //skill 2
                    // Tăng cấp độ kỹ năng
                    SkillController.Singleton.skills[indexg].level += 1;
                    SkillController.Singleton.skills[indexg].nechieu += 1;

                    // Tăng sát thương hoặc các thuộc tính khác nếu cần
                    SkillController.Singleton.skills[indexg].dame += 25; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].satthuong += SkillController.Singleton.skills[indexg].dame * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].mp += 5 * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                                                                          //SkillController.Singleton.skills[indexg].cooldownTime = Mathf.Max(0.5f, SkillController.Singleton.skills[indexg].cooldownTime - 0.2f); // Giảm thời gian hồi, tối thiểu 0.5s

                    // Trừ điểm kỹ năng (mỗi lần nâng cấp tốn 2 điểm)
                    GameManager.Singleton.diemkynang -= 1;

                    // Cập nhật UI cấp độ kỹ năng
                    txtLevelSkill[indexg].text = SkillController.Singleton.skills[indexg].level.ToString();
                    iconSkill.sprite = SkillController.Singleton.skills[indexg].skillIcon;
                    // Cập nhật điểm kỹ năng trong UI
                    

                    // Cập nhật mô tả kỹ năng
                    txtDescriptionSkill.text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[indexg].skillName}]</b>\n"
                                 + $"<color=#B22222>Cấp độ tối đa:</color> {SkillController.Singleton.skills[indexg].maxlevel}\n"
                                 + $"<color=#FFD700>Mô tả:</color> {SkillController.Singleton.skills[indexg].skillDescriptions}\n"
                                 + $"<color=#00FF00>Phím kích hoạt:</color> {SkillController.Singleton.skills[indexg].activationKey}\n"
                                 + $"<color=#FF4500>Thời gian hồi chiêu:</color> {SkillController.Singleton.skills[indexg].cooldownTime:F1} giây\n"
                                 + $"<color=#1E90FF>Cấp độ kỹ năng:</color> {SkillController.Singleton.skills[indexg].level}\n"
                                 + $"<color=#FF6347>Sát thương:</color> {SkillController.Singleton.skills[indexg].dame}%\n"
                                 + $"<color=#32CD32>Sát thương tổng thể:</color> {SkillController.Singleton.skills[indexg].satthuong}\n"
                                 + $"<color=#0000FF>Năng lượng tiêu hao:</color> {SkillController.Singleton.skills[indexg].mp}\n";
                    break;
                case 2:
                    //skil3
                    // Tăng cấp độ kỹ năng
                    SkillController.Singleton.skills[indexg].level += 1;
                    SkillController.Singleton.skills[indexg].nechieu += 1;

                    // Tăng sát thương hoặc các thuộc tính khác nếu cần
                    SkillController.Singleton.skills[indexg].dame += 45; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].satthuong += SkillController.Singleton.skills[indexg].dame * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].mp += 5 * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                                                                          //SkillController.Singleton.skills[indexg].cooldownTime = Mathf.Max(0.5f, SkillController.Singleton.skills[indexg].cooldownTime - 0.2f); // Giảm thời gian hồi, tối thiểu 0.5s

                    // Trừ điểm kỹ năng (mỗi lần nâng cấp tốn 2 điểm)
                    GameManager.Singleton.diemkynang -= 1;

                    // Cập nhật UI cấp độ kỹ năng
                    txtLevelSkill[indexg].text = SkillController.Singleton.skills[indexg].level.ToString();
                    iconSkill.sprite = SkillController.Singleton.skills[indexg].skillIcon;
                    // Cập nhật điểm kỹ năng trong UI
                   

                    // Cập nhật mô tả kỹ năng
                    txtDescriptionSkill.text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[indexg].skillName}]</b>\n"
                                 + $"<color=#B22222>Cấp độ tối đa:</color> {SkillController.Singleton.skills[indexg].maxlevel}\n"
                                 + $"<color=#FFD700>Mô tả:</color> {SkillController.Singleton.skills[indexg].skillDescriptions}\n"
                                 + $"<color=#00FF00>Phím kích hoạt:</color> {SkillController.Singleton.skills[indexg].activationKey}\n"
                                 + $"<color=#FF4500>Thời gian hồi chiêu:</color> {SkillController.Singleton.skills[indexg].cooldownTime:F1} giây\n"
                                 + $"<color=#1E90FF>Cấp độ kỹ năng:</color> {SkillController.Singleton.skills[indexg].level}\n"
                                 + $"<color=#FF6347>Sát thương:</color> {SkillController.Singleton.skills[indexg].dame}%\n"
                                 + $"<color=#32CD32>Sát thương tổng thể:</color> {SkillController.Singleton.skills[indexg].satthuong}\n"
                                 + $"<color=#0000FF>Năng lượng tiêu hao:</color> {SkillController.Singleton.skills[indexg].mp}\n";
                    break;
                case 3:
                    //skil3
                    // Tăng cấp độ kỹ năng
                    SkillController.Singleton.skills[indexg].level += 1;
                    SkillController.Singleton.skills[indexg].nechieu += 1;

                    // Tăng sát thương hoặc các thuộc tính khác nếu cần
                    SkillController.Singleton.skills[indexg].dame += 45; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].satthuong += SkillController.Singleton.skills[indexg].dame * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].mp += 5 * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                                                                          //SkillController.Singleton.skills[indexg].cooldownTime = Mathf.Max(0.5f, SkillController.Singleton.skills[indexg].cooldownTime - 0.2f); // Giảm thời gian hồi, tối thiểu 0.5s

                    // Trừ điểm kỹ năng (mỗi lần nâng cấp tốn 2 điểm)
                    GameManager.Singleton.diemkynang -= 1;

                    // Cập nhật UI cấp độ kỹ năng
                    txtLevelSkill[indexg].text = SkillController.Singleton.skills[indexg].level.ToString();
                    iconSkill.sprite = SkillController.Singleton.skills[indexg].skillIcon;
                    // Cập nhật điểm kỹ năng trong UI


                    // Cập nhật mô tả kỹ năng
                    txtDescriptionSkill.text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[indexg].skillName}]</b>\n"
                                 + $"<color=#B22222>Cấp độ tối đa:</color> {SkillController.Singleton.skills[indexg].maxlevel}\n"
                                 + $"<color=#FFD700>Mô tả:</color> {SkillController.Singleton.skills[indexg].skillDescriptions}\n"
                                 + $"<color=#00FF00>Phím kích hoạt:</color> {SkillController.Singleton.skills[indexg].activationKey}\n"
                                 + $"<color=#FF4500>Thời gian hồi chiêu:</color> {SkillController.Singleton.skills[indexg].cooldownTime:F1} giây\n"
                                 + $"<color=#1E90FF>Cấp độ kỹ năng:</color> {SkillController.Singleton.skills[indexg].level}\n"
                                 + $"<color=#FF6347>Sát thương:</color> {SkillController.Singleton.skills[indexg].dame}%\n"
                                 + $"<color=#32CD32>Sát thương tổng thể:</color> {SkillController.Singleton.skills[indexg].satthuong}\n"
                                 + $"<color=#0000FF>Năng lượng tiêu hao:</color> {SkillController.Singleton.skills[indexg].mp}\n";
                    break;
                case 4:
                    //skil3
                    // Tăng cấp độ kỹ năng
                    SkillController.Singleton.skills[indexg].level += 1;
                    SkillController.Singleton.skills[indexg].nechieu += 1;

                    // Tăng sát thương hoặc các thuộc tính khác nếu cần
                    SkillController.Singleton.skills[indexg].dame += 45; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].satthuong += SkillController.Singleton.skills[indexg].dame * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].mp += 5 * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                                                                          //SkillController.Singleton.skills[indexg].cooldownTime = Mathf.Max(0.5f, SkillController.Singleton.skills[indexg].cooldownTime - 0.2f); // Giảm thời gian hồi, tối thiểu 0.5s

                    // Trừ điểm kỹ năng (mỗi lần nâng cấp tốn 2 điểm)
                    GameManager.Singleton.diemkynang -= 1;

                    // Cập nhật UI cấp độ kỹ năng
                    txtLevelSkill[indexg].text = SkillController.Singleton.skills[indexg].level.ToString();
                    iconSkill.sprite = SkillController.Singleton.skills[indexg].skillIcon;
                    // Cập nhật điểm kỹ năng trong UI


                    // Cập nhật mô tả kỹ năng
                    txtDescriptionSkill.text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[indexg].skillName}]</b>\n"
                                 + $"<color=#B22222>Cấp độ tối đa:</color> {SkillController.Singleton.skills[indexg].maxlevel}\n"
                                 + $"<color=#FFD700>Mô tả:</color> {SkillController.Singleton.skills[indexg].skillDescriptions}\n"
                                 + $"<color=#00FF00>Phím kích hoạt:</color> {SkillController.Singleton.skills[indexg].activationKey}\n"
                                 + $"<color=#FF4500>Thời gian hồi chiêu:</color> {SkillController.Singleton.skills[indexg].cooldownTime:F1} giây\n"
                                 + $"<color=#1E90FF>Cấp độ kỹ năng:</color> {SkillController.Singleton.skills[indexg].level}\n"
                                 + $"<color=#FF6347>Hồi phục HP - MP:</color> {SkillController.Singleton.skills[indexg].dame}%\n"
                                 + $"<color=#32CD32>Hồi phục tối đa:</color> {SkillController.Singleton.skills[indexg].satthuong}\n"
                                 + $"<color=#0000FF>Năng lượng tiêu hao:</color> {SkillController.Singleton.skills[indexg].mp}\n";
                    break;
                case 5:
                    //skil3
                    // Tăng cấp độ kỹ năng
                    SkillController.Singleton.skills[indexg].level += 1;
                    SkillController.Singleton.skills[indexg].nechieu += 1;

                    // Tăng sát thương hoặc các thuộc tính khác nếu cần
                    SkillController.Singleton.skills[indexg].dame += 60; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].satthuong += SkillController.Singleton.skills[indexg].dame * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                    SkillController.Singleton.skills[indexg].mp += 10 * 2; // Ví dụ: tăng 10 sát thương mỗi cấp
                                                                          //SkillController.Singleton.skills[indexg].cooldownTime = Mathf.Max(0.5f, SkillController.Singleton.skills[indexg].cooldownTime - 0.2f); // Giảm thời gian hồi, tối thiểu 0.5s

                    // Trừ điểm kỹ năng (mỗi lần nâng cấp tốn 2 điểm)
                    GameManager.Singleton.diemkynang -= 1;

                    // Cập nhật UI cấp độ kỹ năng
                    txtLevelSkill[indexg].text = SkillController.Singleton.skills[indexg].level.ToString();
                    iconSkill.sprite = SkillController.Singleton.skills[indexg].skillIcon;
                    // Cập nhật điểm kỹ năng trong UI


                    // Cập nhật mô tả kỹ năng
                    txtDescriptionSkill.text = $"<b>[Chiêu thức:{SkillController.Singleton.skills[indexg].skillName}]</b>\n"
                                 + $"<color=#B22222>Cấp độ tối đa:</color> {SkillController.Singleton.skills[indexg].maxlevel}\n"
                                 + $"<color=#FFD700>Mô tả:</color> {SkillController.Singleton.skills[indexg].skillDescriptions}\n"
                                 + $"<color=#00FF00>Phím kích hoạt:</color> {SkillController.Singleton.skills[indexg].activationKey}\n"
                                 + $"<color=#FF4500>Thời gian hồi chiêu:</color> {SkillController.Singleton.skills[indexg].cooldownTime:F1} giây\n"
                                 + $"<color=#1E90FF>Cấp độ kỹ năng:</color> {SkillController.Singleton.skills[indexg].level}\n"
                                 + $"<color=#FF6347>Hồi phục HP - MP:</color> {SkillController.Singleton.skills[indexg].dame}%\n"
                                 + $"<color=#32CD32>Hồi phục tối đa:</color> {SkillController.Singleton.skills[indexg].satthuong}\n"
                                 + $"<color=#0000FF>Năng lượng tiêu hao:</color> {SkillController.Singleton.skills[indexg].mp}\n";
                    break;

            }

            SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Upgrade);
            UIData();
            txtScoreSkill.text = "Điểm kỹ năng: " + GameManager.Singleton.diemkynang;
            // Thông báo nâng cấp thành công
            Thongbao.Singleton.ShowThongbao($"Kỹ năng {SkillController.Singleton.skills[indexg].skillName} đã được nâng cấp lên cấp {SkillController.Singleton.skills[indexg].level}!");
            SkillController.Singleton.SaveSkill();
            SkillController.Singleton.LoadSkill();
        }
        else
        {
            // Thông báo nếu không đủ điểm kỹ năng
            Thongbao.Singleton.ShowThongbao("Không đủ điểm kỹ năng để nâng cấp. Cần ít nhất 2 điểm.");
            SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Error);
        }

        // Cập nhật giao diện
        UpdateUI();
    }

}
