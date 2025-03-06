using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIManager : MonoBehaviour
{
    public static EquipmentUIManager Singleton;
    public Image slotAoImage;    // Ô hiển thị hình ảnh áo
    public Image slotQuanImage;  // Ô hiển thị hình ảnh quần
    public Image slotGangImage;  // Ô hiển thị hình ảnh găng tay
    public Image slotGiayImage;  // Ô hiển thị hình ảnh giày
    public Image slotRadaImage;  // Ô hiển thị hình ảnh radar
    public Image slotCanhImage;  // Ô hiển thị hình ảnh cánh

    public Image slotDaychuyenmage;    // Ô hiển thị hình ảnh áo
    public Image slotNhanImage;  // Ô hiển thị hình ảnh quần
    public Image slotVukhiImage;  // Ô hiển thị hình ảnh găng tay
    public Image slotPetImage;  // Ô hiển thị hình ảnh giày
    public Image slotPhukienImage;  // Ô hiển thị hình ảnh radar
    
    public GameObject[] ticks;
    public Sprite[] spritesAnhcap;
    public Image[] BoderAnhcap;

    public EquipmentManager equipmentManager;
    public Text[] txtTentrangbi;
    //
   

    private void OnEnable()
    {
       

        if (equipmentManager != null)
        {
            UpdateEquipmentUI();
        }
        UpdateEquipmentSprites();

    }
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
           
        }
        
    }



    public void UpdateEquipmentSprites()
    {
        // Tạo danh sách các trang bị
        var equipmentList = new[]
        {
        equipmentManager.playerEquipment.ao,
        equipmentManager.playerEquipment.quan,
        equipmentManager.playerEquipment.gang,
        equipmentManager.playerEquipment.giay,
        equipmentManager.playerEquipment.rada,
        equipmentManager.playerEquipment.canh,
        equipmentManager.playerEquipment.daychuyen,
        equipmentManager.playerEquipment.nhan,
        equipmentManager.playerEquipment.vukhi,
        equipmentManager.playerEquipment.pet,
        equipmentManager.playerEquipment.phukien
    };

        int index = 0; // Dùng để duyệt qua mảng BoderAnhcap
        foreach (var equipment in equipmentList)
        {
            if (index >= BoderAnhcap.Length)
            {
                //Debug.LogWarning("Số lượng BoderAnhcap không đủ để gán tất cả trang bị!");
                break;
            }

            if (equipment != null) // Kiểm tra nếu equipment không phải null
            {
                if (equipment.level > 0 && equipment.level <= spritesAnhcap.Length)
                {
                    // Gán sprite tương ứng với level
                    BoderAnhcap[index].sprite = spritesAnhcap[equipment.level - 1];
                    txtTentrangbi[index].gameObject.SetActive(false);

                }
                else
                {
                    // Gán sprite mặc định (vị trí 0 trong spritesAnhcap)
                    BoderAnhcap[index].sprite = spritesAnhcap[0];
                    txtTentrangbi[index].gameObject.SetActive(true);


                    //Debug.LogWarning($"Level của trang bị tại vị trí {index} bằng 0 hoặc không hợp lệ, gán sprite mặc định!");
                }
            }
            else
            {
                // Gán sprite mặc định nếu equipment là null
                BoderAnhcap[index].sprite = spritesAnhcap[0];
                txtTentrangbi[index].gameObject.SetActive(true);


                //Debug.LogWarning($"Trang bị tại vị trí {index} là null, gán sprite mặc định!");
            }

            index++; // Tăng chỉ số cho BoderAnhcap
        }
    }




    public void UpdateEquipmentUI()
    {
        if (equipmentManager == null || equipmentManager.playerEquipment == null)
        {
            //Debug.LogWarning("Không tìm thấy EquipmentManager hoặc PlayerEquipment.");
            return;
        }
        SetSlotImage(slotAoImage, equipmentManager.playerEquipment.ao, ticks[0]);
        SetSlotImage(slotQuanImage, equipmentManager.playerEquipment.quan, ticks[1]);
        SetSlotImage(slotGangImage, equipmentManager.playerEquipment.gang, ticks[2]);
        SetSlotImage(slotGiayImage, equipmentManager.playerEquipment.giay, ticks[3]);
        SetSlotImage(slotRadaImage, equipmentManager.playerEquipment.rada, ticks[4]);
        SetSlotImage(slotCanhImage, equipmentManager.playerEquipment.canh, ticks[10]);

        SetSlotImage(slotDaychuyenmage, equipmentManager.playerEquipment.daychuyen, ticks[5]);
        SetSlotImage(slotNhanImage, equipmentManager.playerEquipment.nhan, ticks[6]);
        SetSlotImage(slotVukhiImage, equipmentManager.playerEquipment.vukhi, ticks[7]);
        SetSlotImage(slotPetImage, equipmentManager.playerEquipment.pet, ticks[8]);
        SetSlotImage(slotPhukienImage, equipmentManager.playerEquipment.phukien, ticks[9]);
        UpdateEquipmentSprites();
        equipmentManager.LoadSprender();
    }

    private void SetSlotImage(Image slotImage, DataEquipment equipmentData,GameObject tick)
    {
        if (equipmentData == null || string.IsNullOrEmpty(equipmentData.spriteName))
        {
            // Nếu không có trang bị, xóa hình ảnh
            slotImage.sprite = null;
            slotImage.color = new Color(1, 1, 1, 0); // Ẩn ô (đặt alpha thành 0)
            slotImage.gameObject.SetActive(false);
            tick.gameObject.SetActive(false);
        }
        else
        {
            slotImage.gameObject.SetActive(true);
            tick.gameObject.SetActive(true);

            // Load Sprite từ Resources
            Sprite equipmentSprite = Resources.Load<Sprite>($"Item/{equipmentData.spriteName}");

            if (equipmentSprite != null)
            {
                slotImage.sprite = equipmentSprite;
                slotImage.color = Color.white; // Hiển thị ô (đặt alpha thành 1)
            }
            else
            {
                //Debug.LogWarning($"Không thể tải Sprite: {equipmentData.spriteName}");
                slotImage.sprite = null;
                slotImage.color = new Color(1, 1, 1, 0);
            }
        }
    }

    


}
