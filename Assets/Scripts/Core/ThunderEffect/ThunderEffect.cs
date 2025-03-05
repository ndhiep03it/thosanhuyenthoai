using System.Collections;
using UnityEngine;

public class ThunderEffect : MonoBehaviour
{
    public GameObject[] thunderObjects;    // Các GameObject chứa SpriteRenderer
    public SpriteRenderer backgroundSprite; // SpriteRenderer của nền
    public AudioSource thunderSound;       // Âm thanh sấm sét
    public float minDelay = 5f;            // Thời gian chờ tối thiểu giữa các tia sấm
    public float maxDelay = 15f;           // Thời gian chờ tối đa giữa các tia sấm
    public float flashDuration = 0.2f;     // Thời gian mỗi tia sấm kéo dài


    void Start()
    {
        // Đảm bảo tất cả các GameObject sấm tắt lúc đầu
        foreach (var obj in thunderObjects)
        {
            obj.SetActive(false);
        }

        if (backgroundSprite == null)
        {
            backgroundSprite = GetComponent<SpriteRenderer>();
        }

        // Đảm bảo sấm tắt lúc đầu
        backgroundSprite.enabled = false;

        // Bắt đầu hiệu ứng sấm
        StartCoroutine(ThunderRoutine());
    }

    IEnumerator ThunderRoutine()
    {
        while (true)
        {
            // Chờ một khoảng thời gian ngẫu nhiên giữa các tia sấm
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            // Chọn một GameObject ngẫu nhiên và kích hoạt hiệu ứng
            int randomIndex = Random.Range(0, thunderObjects.Length);
            StartCoroutine(FlashThunder(thunderObjects[randomIndex]));

            // Phát âm thanh sấm
            thunderSound.Play();

            // Kích hoạt hiệu ứng nháy sáng nền
            if (backgroundSprite != null)
            {
                StartCoroutine(FlashBackground());
            }
        }
    }

    IEnumerator FlashThunder(GameObject thunderObject)
    {
        // Nháy sáng GameObject tia sét
        for (int i = 0; i < 3; i++) // Nháy 3 lần
        {
            thunderObject.SetActive(true); // Bật GameObject
            yield return new WaitForSeconds(flashDuration);
            thunderObject.SetActive(false); // Tắt GameObject
            yield return new WaitForSeconds(flashDuration / 4);
        }
    }

    IEnumerator FlashBackground()
    {
        // Nháy sáng SpriteRenderer nền
        for (int i = 0; i < 3; i++) // Nháy 3 lần
        {
            backgroundSprite.enabled = true; // Hiển thị sấm
            yield return new WaitForSeconds(flashDuration);
            backgroundSprite.enabled = false; // Tắt sấm
            yield return new WaitForSeconds(flashDuration / 2);
        }
    }
}
