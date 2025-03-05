using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Singleton để gọi từ bất cứ đâu

    [Header("Audio Source Settings")]
    public AudioSource audioSource; // AudioSource chính để phát âm thanh FX

    [Header("Sound FX Clips")]
    public List<AudioClip> soundEffects; // Danh sách các âm thanh FX (phải sắp xếp theo thứ tự `SoundEffect` enum)

    private void Awake()
    {
        // Đảm bảo Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Không phá hủy khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Enum để quản lý các âm thanh FX.
    /// </summary>
    public enum SoundEffect
    {
        Jump,
        Hit,
        Explosion,
        Death,
        Click, // Thêm các âm thanh mới ở đây
        Drop, // Thêm các âm thanh mới ở đây
        LevelUp,
        SoundPlayerText, 
        Upgrade,
        PointNotification,
        Error,
        Nangcap,
        UpgradeDungluyen,
        Water,
    }

    /// <summary>
    /// Phát một âm thanh dựa trên Enum.
    /// </summary>
    /// <param name="soundEffect">Âm thanh cần phát.</param>
    public void PlaySound(SoundEffect soundEffect)
    {
        int index = (int)soundEffect;
        if (index >= 0 && index < soundEffects.Count)
        {
            audioSource.PlayOneShot(soundEffects[index]);
        }
        else
        {
            Debug.LogWarning($"Âm thanh '{soundEffect}' không tồn tại trong danh sách!");
        }
    }
}
