using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager Singleton;
    public GameObject questItemPrefab;
    public Transform questListParent;

    private void Start()
    {
        LoadQuestUI();
    }
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;

        }
        else
        {

        }

    }
    // Hiển thị toàn bộ nhiệm vụ
    public void LoadQuestUIAll()
    {
        foreach (Transform child in questListParent) Destroy(child.gameObject);

        List<Quest> quests = QuestManager.Instance.quests;

        foreach (Quest quest in quests)
        {
            GameObject questItem = Instantiate(questItemPrefab, questListParent);
            questItem.transform.Find("QuestName").GetComponent<TextMeshProUGUI>().text = quest.name;
            questItem.transform.Find("QuestDescription").GetComponent<TextMeshProUGUI>().text = quest.description;
            questItem.transform.Find("QuestStatus").GetComponent<TextMeshProUGUI>().text = GetStatusText(quest.status);

            Transform questType = questItem.transform.Find("QuestType");
            if (questType != null)
            {
                questType.GetComponent<TextMeshProUGUI>().text = GetTypeText(quest.questType);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadQuestUI();
        }
    }

    // Hiển thị nhiệm vụ hiện tại dựa theo ID nhiệm vụ
    public void LoadQuestUI()
    {
        foreach (Transform child in questListParent) Destroy(child.gameObject);

        List<Quest> quests = QuestManager.Instance.quests;
        int idNhiemVu = GameManager.Singleton.idnhiemvu; // ID nhiệm vụ hiện tại

        foreach (Quest quest in quests)
        {
            if (quest.id == idNhiemVu.ToString()) // Kiểm tra đúng nhiệm vụ
            {
                Debug.Log($"Đang tạo nhiệm vụ: {quest.name}");

                if (questItemPrefab == null)
                {
                    Debug.LogError("⚠ questItemPrefab chưa được gán trong Inspector!");
                    return;
                }

                GameObject questItem = Instantiate(questItemPrefab, questListParent);
                if (questItem == null)
                {
                    Debug.LogError("⚠ Không thể Instantiate questItemPrefab!");
                    return;
                }

                // Gán thông tin cơ bản
                questItem.transform.Find("QuestName").GetComponent<TextMeshProUGUI>().text = quest.name;
                questItem.transform.Find("QuestDescription").GetComponent<TextMeshProUGUI>().text = quest.description;
                questItem.transform.Find("QuestStatus").GetComponent<TextMeshProUGUI>().text = GetStatusText(quest.status);
                // Thêm nút nhận nhiệm vụ nếu chưa bắt đầu
                Button acceptButton = questItem.transform.Find("AcceptButton").GetComponent<Button>();
                if (acceptButton != null)
                {
                    acceptButton.gameObject.SetActive(quest.status == QuestStatus.NotStarted);
                    acceptButton.onClick.AddListener(() => AcceptQuest(quest));
                }
                // Tìm button "Nhận Thưởng"
                Button claimButton = questItem.transform.Find("ClaimRewardButton").GetComponent<Button>();
                claimButton.interactable = (quest.status == QuestStatus.Completed);

                // Xóa sự kiện cũ để tránh lỗi add nhiều lần
                claimButton.onClick.RemoveAllListeners();
                claimButton.onClick.AddListener(() => ClaimReward(quest));


                // Xử lý theo loại nhiệm vụ
                Transform questType = questItem.transform.Find("QuestType");
                if (questType != null)
                {
                    questType.GetComponent<TextMeshProUGUI>().text = GetTypeText(quest.questType);
                }

                switch (quest.questType)
                {
                    case QuestType.Kill:
                        // Hiển thị số quái cần tiêu diệt
                        Transform killTarget = questItem.transform.Find("KillTarget");
                        if (killTarget != null)
                        {
                            killTarget.GetComponent<TextMeshProUGUI>().text = $"Tiêu diệt: {quest.soquaidatieudiet}/ {quest.soquaicantieudiet} quái";
                            killTarget.gameObject.SetActive(true);
                        }
                        break;

                    case QuestType.Collect:
                        // Hiển thị vật phẩm cần thu thập
                        Transform collectTarget = questItem.transform.Find("CollectTarget");
                        if (collectTarget != null)
                        {
                            collectTarget.GetComponent<TextMeshProUGUI>().text = $"Thu thập: {quest.soluongcan} / {quest.soluongcanthuthap} Vật phẩm: {quest.requiredItem}";
                            collectTarget.gameObject.SetActive(true);
                        }
                        break;

                    case QuestType.Talk:
                        // Hiển thị NPC cần nói chuyện
                        Transform talkTarget = questItem.transform.Find("TalkTarget");
                        if (talkTarget != null)
                        {
                            talkTarget.GetComponent<TextMeshProUGUI>().text = $"Gặp NPC: {quest.targetNPC}";
                            talkTarget.gameObject.SetActive(true);
                        }
                        break;
                    case QuestType.MaxQuest:
                        // Hiển thị NPC cần nói chuyện
                        Transform talkTarget1 = questItem.transform.Find("TalkTarget");
                        if (talkTarget1 != null)
                        {
                            talkTarget1.GetComponent<TextMeshProUGUI>().text = $"Đã đạt cấp độ tối đa nhiệm vụ: {quest.name}";
                            talkTarget1.gameObject.SetActive(true);
                            questItem.transform.Find("QuestStatus").GetComponent<TextMeshProUGUI>().text = "Gặp ta sau nhé:)";

                        }
                        break;
                }
            }
        }
    }

    public void ClaimReward(Quest quest)
    {
        Thongbao.Singleton.ShowThongbaoHistory($"Nhận thưởng từ nhiệm vụ: {quest.name} 50K");

        // Thêm phần thưởng (ví dụ: +100 vàng)
        GameManager.Singleton.gold += 50000;

        // Kiểm tra nhiệm vụ tiếp theo
        if (!string.IsNullOrEmpty(quest.nextQuestId))
        {
            Quest nextQuest = QuestManager.Instance.quests.Find(q => q.id == quest.nextQuestId);
            if (nextQuest != null)
            {
                Thongbao.Singleton.ShowThongbao($"Chuyển sang nhiệm vụ tiếp theo: {nextQuest.name} (ID: {nextQuest.id})");
                Thongbao.Singleton.ShowThongbaoHistory($"Chuyển sang nhiệm vụ tiếp theo: {nextQuest.name} (ID: {nextQuest.id})");
                QuestManager.Instance.StartNextQuest(nextQuest.id);
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy nhiệm vụ tiếp theo với ID: {quest.nextQuestId}");
            }
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("Không có nhiệm vụ tiếp theo.");
            Thongbao.Singleton.ShowThongbaoHistory("Không có nhiệm vụ tiếp theo.");
        }


        QuestManager.Instance.SaveQuests();
        LoadQuestUI(); // Cập nhật lại UI
    }


    // Chuyển đổi trạng thái nhiệm vụ sang tiếng Việt
    private string GetStatusText(QuestStatus status)
    {
        switch (status)
        {
            case QuestStatus.NotStarted: return "Chưa nhận";
            case QuestStatus.InProgress: return "Đang làm";
            case QuestStatus.Completed: return "Hoàn thành";
            default: return "Không xác định";
        }
    }

    // Chuyển đổi loại nhiệm vụ sang tiếng Việt
    private string GetTypeText(QuestType type)
    {
        switch (type)
        {
            case QuestType.Collect: return "Thu thập";
            case QuestType.Kill: return "Tiêu diệt";
            case QuestType.Talk: return "Nói chuyện";
            case QuestType.MaxQuest: return "Nhiệm vụ của bạn đã tối đa";
            default: return "Không xác định";
        }
    }

    // Nhận nhiệm vụ
    public void AcceptQuest(Quest quest)
    {
        if (quest.status == QuestStatus.NotStarted)
        {
            QuestManager.Instance.StartQuest(quest);
            LoadQuestUI(); // Cập nhật lại UI sau khi nhận nhiệm vụ
        }
    }
}
