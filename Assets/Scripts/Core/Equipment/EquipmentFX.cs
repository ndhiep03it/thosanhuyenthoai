using UnityEngine;

public class EquipmentFX : MonoBehaviour
{
    public ParticleSystem borderEffect;   // Hiệu ứng viền sáng
    public ParticleSystem sparkleEffect;  // Hiệu ứng hạt lấp lánh bên trong
    public ItemProfile itemProfile;
    //[Range(1, 16)] public int level = 1;  // Cấp độ trang bị (1-16)

    void Start()
    {
        ApplyRectangularFX(itemProfile.level);
    }

    void ApplyRectangularFX(int level)
    {
        // Kiểm tra nếu level là 0 thì ẩn hiệu ứng
        if (level == 0)
        {
            HideEffects();
            return;
        }

        Color color = GetColorByLevel(level);

        // Đổi màu viền sáng
        if (borderEffect != null)
        {
            var main = borderEffect.main;
            main.startColor = color;
            borderEffect.Play();
        }

        // Đổi màu hiệu ứng lấp lánh
        if (sparkleEffect != null)
        {
            var main = sparkleEffect.main;
            main.startColor = color;
            sparkleEffect.Play();
        }
    }

    void HideEffects()
    {
        // Dừng và ẩn hiệu ứng khi level là 0
        if (borderEffect != null)
        {
            borderEffect.Stop();
            borderEffect.gameObject.SetActive(false);  // Ẩn hiệu ứng viền
        }

        if (sparkleEffect != null)
        {
            sparkleEffect.Stop();
            sparkleEffect.gameObject.SetActive(false);  // Ẩn hiệu ứng lấp lánh
        }
    }

    Color GetColorByLevel(int level)
    {
        // Tính toán màu theo cấp độ (từ xanh lá đến đỏ)
        if (level <= 4)
            return Color.Lerp(Color.green, Color.cyan, (level - 1) / 4f);
        else if (level <= 8)
            return Color.Lerp(Color.cyan, Color.blue, (level - 5) / 4f);
        else if (level <= 12)
            return Color.Lerp(Color.blue, new Color(0.6f, 0f, 0.8f), (level - 9) / 4f); // Màu tím
        else
            return Color.Lerp(new Color(0.6f, 0f, 0.8f), Color.red, (level - 13) / 4f);
    }
}
