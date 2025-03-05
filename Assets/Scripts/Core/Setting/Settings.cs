using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private ParticleSystem[] particleSystems;
    public GameObject Tiaquai;
    // Các Toggle trong UI
    public Toggle spriteRendererToggle;    // Toggle để bật/tắt SpriteRenderer
    public Toggle particleEffectToggle;    // Toggle để bật/tắt Particle Effects
    public Toggle soundToggle;             // Toggle để bật/tắt âm thanh (ví dụ)
    public Toggle tiaToggle;             // Toggle để bật/tắt âm thanh (ví dụ)
    public Font[] font;
    public Dropdown dropdownFont;



    private List<Text> txtAll = new List<Text>(); // Danh sách chứa tất cả Text

    private void FixedUpdate()
    {
        LoadSettings();
    }
    private void Update()
    {
        //int savedFontIndex = PlayerPrefs.GetInt("SelectedFont", 0);
        //dropdownFont.value = savedFontIndex;
        //ChangeFont(savedFontIndex);
    }
    void Start()
    {
        spriteRenderers = FindObjectsOfType<SpriteRenderer>();
        //Invoke("LoadSettings", 1f);  // Gọi hàm sau 1 giây
        // Đọc các giá trị đã lưu trong PlayerPrefs
        bool spriteEnabled = PlayerPrefs.GetInt("SpriteRendererEnabled", 1) == 1;  // Mặc định là bật (1)
        bool particleEnabled = PlayerPrefs.GetInt("ParticleEffectsEnabled", 1) == 1; // Mặc định là bật (1)
        bool soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1; // Mặc định là bật (1)
        bool tiaEnabled = PlayerPrefs.GetInt("TiaEnabled", 1) == 1; // Mặc định là bật (1)
        FindAllTextInScene();
        SelectFont();

        
        // Cập nhật trạng thái Toggle từ PlayerPrefs
        spriteRendererToggle.isOn = spriteEnabled;
        particleEffectToggle.isOn = particleEnabled;
        soundToggle.isOn = soundEnabled;
        tiaToggle.isOn = tiaEnabled;

        // Đăng ký sự kiện thay đổi Toggle
        spriteRendererToggle.onValueChanged.AddListener(OnSpriteRendererToggle);
        particleEffectToggle.onValueChanged.AddListener(OnParticleEffectToggle);
        soundToggle.onValueChanged.AddListener(OnSoundToggle);
        tiaToggle.onValueChanged.AddListener(OnTiaToggle);
    }
    // ✅ Tìm tất cả Text trong scene, kể cả object bị ẩn
    void FindAllTextInScene()
    {
        txtAll.Clear();
        GameObject[] allObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject obj in allObjects)
        {
            Text[] texts = obj.GetComponentsInChildren<Text>(true);
            txtAll.AddRange(texts);
        }
    }
    public void LoadSettings()
    {
        FindAllTextInScene();
        spriteRenderers = FindObjectsOfType<SpriteRenderer>();
        particleSystems = FindObjectsOfType<ParticleSystem>();
        // Đọc các giá trị đã lưu trong PlayerPrefs
        bool spriteEnabled = PlayerPrefs.GetInt("SpriteRendererEnabled", 1) == 1;  // Mặc định là bật (1)
        bool particleEnabled = PlayerPrefs.GetInt("ParticleEffectsEnabled", 1) == 1; // Mặc định là bật (1)
        bool soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1; // Mặc định là bật (1)
        bool tiaEnabled = PlayerPrefs.GetInt("TiaEnabled", 1) == 1; // Mặc định là bật (1)

        // Cập nhật trạng thái Toggle từ PlayerPrefs
        spriteRendererToggle.isOn = spriteEnabled;
        particleEffectToggle.isOn = particleEnabled;
        soundToggle.isOn = soundEnabled;
        tiaToggle.isOn = tiaEnabled;

        // Đăng ký sự kiện thay đổi Toggle
        spriteRendererToggle.onValueChanged.AddListener(OnSpriteRendererToggle);
        particleEffectToggle.onValueChanged.AddListener(OnParticleEffectToggle);
        soundToggle.onValueChanged.AddListener(OnSoundToggle);
        tiaToggle.onValueChanged.AddListener(OnTiaToggle);
        int savedFontIndex = PlayerPrefs.GetInt("SelectedFont");
        ChangeFont(savedFontIndex);

    }
    // Lưu trạng thái Toggle vào PlayerPrefs
    void OnSpriteRendererToggle(bool isOn)
    {
        // Lưu trạng thái của SpriteRenderer
        PlayerPrefs.SetInt("SpriteRendererEnabled", isOn ? 1 : 0);
        ToggleSpriteRenderer(isOn);  // Cập nhật đối tượng trong game
    }

    void OnParticleEffectToggle(bool isOn)
    {
        // Lưu trạng thái của Particle Effects
        PlayerPrefs.SetInt("ParticleEffectsEnabled", isOn ? 1 : 0);
        ToggleParticleEffects(isOn);  // Cập nhật đối tượng trong game
    }

    void OnSoundToggle(bool isOn)
    {
        // Lưu trạng thái của âm thanh
        PlayerPrefs.SetInt("SoundEnabled", isOn ? 1 : 0);
        ToggleSound(isOn);  // Cập nhật trạng thái âm thanh
    }
    void OnTiaToggle(bool isOn)
    {
        
        PlayerPrefs.SetInt("TiaEnabled", isOn ? 1 : 0);
        Tiaquai.SetActive(isOn);
    }
    void ToggleSpriteRenderer(bool isOn)
    {
        // Lấy tất cả các SpriteRenderer trong scene
        //SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
        spriteRenderers = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.enabled = isOn; // Bật hoặc tắt SpriteRenderer
        }
    }


    // Hàm cập nhật Particle Effects trong game
    void ToggleParticleEffects(bool isOn)
    {
        //ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.gameObject.SetActive(isOn); // Bật hoặc tắt hiệu ứng Particle
        }
    }

    // Hàm cập nhật trạng thái âm thanh trong game
    void ToggleSound(bool isOn)
    {
        AudioListener.volume = isOn ? 1 : 0; // Bật hoặc tắt âm thanh
    }
   
    // Đảm bảo các giá trị được lưu khi game đóng
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    // ✅ Cập nhật danh sách font trong Dropdown
    public void SelectFont()
    {
        dropdownFont.options.Clear();

        List<Dropdown.OptionData> fontOptions = new List<Dropdown.OptionData>();
        foreach (Font f in font)
        {
            fontOptions.Add(new Dropdown.OptionData(f.name));
        }
        dropdownFont.AddOptions(fontOptions);

        // Thêm sự kiện thay đổi font khi chọn Dropdown
        dropdownFont.onValueChanged.AddListener(ChangeFont);
    }

    // ✅ Thay đổi font của tất cả Text khi chọn Dropdown
    void ChangeFont(int index)
    {
        

        foreach (Text txt in txtAll)
        {
            if (txt != null)
            {
                txt.font = font[index];
            }
        }
        PlayerPrefs.SetInt("SelectedFont", index);
    }
}
