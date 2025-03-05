using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class DailyCheckInUI : MonoBehaviour
{
    public Text streakText;
    public Text rewardText;
    public Button claimButton;

    private DailyCheckInManager checkInManager;

    void Awake()
    {
        StartCoroutine(InitializeUI());
    }

    void Start()
    {
        
        StartCoroutine(InitializeUI());
        //UpdateUI();
    }

    private IEnumerator InitializeUI()
    {
        yield return new WaitUntil(() => FindObjectOfType<DailyCheckInManager>() != null);
        checkInManager = FindObjectOfType<DailyCheckInManager>();

        if (checkInManager == null)
        {
            Debug.LogError("Không tìm thấy DailyCheckInManager!");
            yield break;
        }

        UpdateUI();
    }



    public void ClaimReward()
    {
        StartCoroutine(ClaimRewardCoroutine());
    }

    private IEnumerator ClaimRewardCoroutine()
    {
        claimButton.interactable = false; // Vô hiệu hóa nút
        checkInManager.ClaimReward();
        UpdateUI();
        yield return new WaitForSeconds(1.5f); // Chặn spam trong 1.5 giây
        claimButton.interactable = !checkInManager.IsRewardClaimed(); // Bật lại nếu có thể nhận
    }



    void UpdateUI()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            streakText.text = "Không có kết nối mạng!";
            rewardText.text = "Hãy kiểm tra lại internet!";
            claimButton.interactable = false;
            return;
        }

        streakText.text = $"Chuỗi ngày: {checkInManager.GetStreak()} ngày";
        //rewardText.text = $"Phần thưởng: {checkInManager.GetReward()} vàng";
        claimButton.interactable = !checkInManager.IsRewardClaimed();
    }
    void Update()
    {
        TimeSpan timeLeft = checkInManager.GetTimeUntilNextCheckIn();
        rewardText.text = $"Thời gian còn lại đến điểm danh mới:: {timeLeft.Hours} giờ {timeLeft.Minutes} phút {timeLeft.Seconds}s";
    }
}
