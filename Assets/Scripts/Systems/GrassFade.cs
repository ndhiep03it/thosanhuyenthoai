using System.Collections;
using UnityEngine;

public class GrassFade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float originalAlpha;
    private AudioSource audioSource;
    private Vector3 originalRotation;

    public AudioClip grassEnterSound;
    public AudioClip grassExitSound;
    public float shakeAmount = 15f; // Biên độ rung lắc
    public float shakeDuration = 0.5f; // Thời gian rung lắc
    public GameObject canvas;
    public ParticleSystem diggingEffect;  // Đối tượng Particle System cho hiệu ứng đất vãi
    public Transform diggingPosition;    // Vị trí đào, nơi sẽ phát ra hiệu ứng (có thể là vị trí của người chơi hoặc công cụ)
    private bool isPlayerInTrigger = false;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        originalAlpha = spriteRenderer.color.a;
        originalRotation = transform.eulerAngles; // Lưu góc xoay ban đầu
    }
    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Daodat();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeGrass(0f));
            PlaySound(grassEnterSound);
            StartCoroutine(ShakeGrass()); // Bắt đầu rung lắc
            canvas.SetActive(true);
            isPlayerInTrigger = true; // Đánh dấu rằng người chơi đã vào vùng kích hoạt
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeGrass(originalAlpha));
            PlaySound(grassExitSound);
            transform.eulerAngles = originalRotation; // Khôi phục góc xoay ban đầu
            canvas.SetActive(false);
            isPlayerInTrigger = false; // Đánh dấu rằng người chơi đã ra vùng kích hoạt
        }
    }

    private System.Collections.IEnumerator FadeGrass(float targetAlpha)
    {
        float duration = 1f;
        float startAlpha = spriteRenderer.color.a;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetAlpha);
    }

    private System.Collections.IEnumerator ShakeGrass()
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float zRotation = originalRotation.z + Mathf.Sin(elapsed * 20f) * shakeAmount;
            transform.eulerAngles = new Vector3(originalRotation.x, originalRotation.y, zRotation);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.eulerAngles = originalRotation; // Khôi phục góc xoay ban đầu
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void Daodat()
    {
        // Tìm item có tên "XENG" trong kho đồ
        var xengItem = Inventory.Singleton.items.Find(item => item.item.name == "XENG");

        if (xengItem != null)
        {
            Thongbao.Singleton.ShowThongbao("Bạn đã dùng xẻng! Đang đào...");
            PlayerController.Singleton.DaodatAnim(true);
            PlayerController.Singleton.isWalking = false;
            Cheatgame.singleton.HideUIEndActive();
            canvas.SetActive(false);
            UImanager.Singleton.PanelDaodat.SetActive(true);

            // Bắt đầu hiệu ứng đất vãi
            StartCoroutine(DiggingEffectSequence());
            StartCoroutine(DiggingProcess());  // Gọi hàm đếm ngược
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("Bạn không có xẻng, không thể xúc.");
            UImanager.Singleton.PanelDaodat.SetActive(false);
            canvas.SetActive(false);
        }
    }

    private IEnumerator DiggingEffectSequence()
    {
        for (int i = 0; i < 5; i++)
        {
            StartDiggingEffect();  // Gọi hiệu ứng đất vãi
            isPlayerInTrigger = false; // Đánh dấu rằng người chơi đã ra vùng kích hoạt
            yield return new WaitForSeconds(0.8f);  // Delay giữa các lần phát hiệu ứng
        }
    }
    private IEnumerator DiggingProcess()
    {
        yield return new WaitForSeconds(5f);  // Đợi trong thời gian đào đất

        // Sau khi hoàn thành, tắt PanelDaodat và nhận phần thưởng
        UImanager.Singleton.PanelDaodat.SetActive(false);
        //Thongbao.Singleton.ShowThongbao("Đào đất thành công! Bạn nhận được phần thưởng.");
       
        // Xác suất phần thưởng
        float[] probabilities = { 0.2f, 0.3f, 0.2f, 0.15f, 0.15f }; // Tổng cộng 100%
        float randValue = Random.Range(0f, 1f); // Giá trị ngẫu nhiên từ 0 đến 1

        // Xác định phần thưởng dựa trên xác suất
        int randItem = GetItemBasedOnProbability(randValue, probabilities);

        RewardType reward = (RewardType)randItem;  // Chuyển giá trị ngẫu nhiên thành RewardType

        // Gọi hàm GiveReward để thực hiện phần thưởng
        GiveReward(reward);
       
    }

    private void GiveReward(RewardType rewardType)
    {
        string itemName = null;
        Item item = null;

        switch (rewardType)
        {
            case RewardType.Coin:
                int coinValue = Random.Range(100, 5000);
                GameManager.Singleton.AddCoins(coinValue);
                Thongbao.Singleton.ShowThongbao($"Bạn nhận được {coinValue} tiền!");
                Thongbao.Singleton.ShowThongbaoHistory($"Bạn nhận được {coinValue} tiền!");
                break;

            case RewardType.Shovel:
                itemName = "XENG";
                item = Resources.Load<Item>("Items/" + itemName);
                if (item != null)
                {
                    Inventory.Singleton.BuyItem(item, 1, "Mua từ cửa hàng hoặc đào đất", 0, 100);
                    Thongbao.Singleton.ShowThongbao("Bạn nhận được 1 cái xẻng!");
                    Thongbao.Singleton.ShowThongbaoHistory("Bạn nhận được 1 cái xẻng!");
                }
                else
                {
                    Debug.LogWarning($"Item '{itemName}' không tồn tại trong Resources!");
                }
                break;
            case RewardType.NUOCTHANH:
                itemName = "NUOCTHANHDUNGLUYEN";
                item = Resources.Load<Item>("Items/" + itemName);
                if (item != null)
                {
                    Inventory.Singleton.BuyItem(item, 1, "Mua từ cửa hàng hoặc đào đất", 0, 100);
                    Thongbao.Singleton.ShowThongbao("Bạn nhận được 1 bình Nước thánh dung luyện!");
                    Thongbao.Singleton.ShowThongbaoHistory("Bạn nhận được 1 bình Nước thánh dung luyện!");
                }
                else
                {
                    //Debug.LogWarning($"Item '{itemName}' không tồn tại trong Resources!");
                }
                break;

            case RewardType.Health:
                // Giả sử có hàm AddHealth trong Player
                // Player.Singleton.AddHealth(20);
                Thongbao.Singleton.ShowThongbao("Sức khỏe của bạn đã được tăng 20!");
                Thongbao.Singleton.ShowThongbaoHistory("Sức khỏe của bạn đã được tăng 20!");
                break;

            case RewardType.Ruby:
                int coinRuby = Random.Range(1, 10);
                GameManager.Singleton.AddRuby(coinRuby);
                Thongbao.Singleton.ShowThongbao($"Bạn nhận được {coinRuby} Ruby!");
                Thongbao.Singleton.ShowThongbaoHistory($"Bạn nhận được {coinRuby} Ruby!");
                break;

            case RewardType.ItemFragment:
                itemName = "MANHXUONGCO";
                item = Resources.Load<Item>("Items/" + itemName);
                if (item != null)
                {
                    Inventory.Singleton.BuyItem(item, 1, "Mua từ cửa hàng hoặc đào đất", 0, 100);
                    Thongbao.Singleton.ShowThongbao("Bạn nhận được 1 mảnh xương cổ!");
                    Thongbao.Singleton.ShowThongbaoHistory("Bạn nhận được 1 mảnh xương cổ!");
                }
                else
                {
                    Debug.LogWarning($"Item '{itemName}' không tồn tại trong Resources!");
                }
                break;

            case RewardType.SpecialItem:
                // Giả sử có hàm AddSpecialItem trong Inventory
                // Inventory.Singleton.AddSpecialItem("Item đặc biệt");
                Thongbao.Singleton.ShowThongbao("Bạn nhận được 1 vật phẩm đặc biệt!");
                Thongbao.Singleton.ShowThongbaoHistory("Bạn nhận được 1 vật phẩm đặc biệt!");
                break;
        }

        // Các logic khác sau khi trao thưởng
        Cheatgame.singleton.HideUIEndActive();
        PlayerController.Singleton.isWalking = true;
        PlayerController.Singleton.DaodatAnim(false);

        // Xóa 1 cái xẻng sau khi đào đất
        for (int i = 0; i < Inventory.Singleton.items.Count; i++)
        {
            if (Inventory.Singleton.items[i].item.name == "XENG")
            {
                Inventory.Singleton.UnRemoveItem(i, 1);
                break;
            }
        }

        // Dừng hiệu ứng đào đất
        StopDiggingEffect();
    }


    private int GetItemBasedOnProbability(float randValue, float[] probabilities)
    {
        float cumulativeProbability = 0f;
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randValue <= cumulativeProbability)
            {
                return i;
            }
        }
        return probabilities.Length - 1; // Nếu không trúng bất kỳ thì trả lại phần thưởng cuối cùng
    }


    public void StartDiggingEffect()
    {
        // Bắt đầu hiệu ứng đất vãi khi đào
        diggingEffect.gameObject.SetActive(true);
        PlayerController.Singleton.Xeng.SetActive(true);
        diggingEffect.transform.position = diggingPosition.position;  // Đặt vị trí của particle system tại vị trí đào
        diggingEffect.Play();
    }

    public void StopDiggingEffect()
    {
        // Dừng hiệu ứng đất vãi khi đào xong
        diggingEffect.gameObject.SetActive(false);
        PlayerController.Singleton.Xeng.SetActive(false);

        diggingEffect.Stop();
    }

}
