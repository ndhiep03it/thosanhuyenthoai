using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    private string savePath;

    public List<Quest> quests = new List<Quest>();
   // public QuestUIManager questUI;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        savePath = Application.persistentDataPath + "/quests.json";
        //SaveQuests();
        LoadQuests();
    }

    //public void AddQuest(Quest quest)
    //{
    //    quests.Add(quest);
    //    SaveQuests();
    //}

    public void UpdateQuestStatus(QuestStatus newStatus)
    {
        int idNhiemVu = GameManager.Singleton.idnhiemvu; // ID nhiệm vụ đang làm

        Quest quest = quests.Find(q => q.id == idNhiemVu.ToString());
        if (quest != null)
        {
            quest.status = newStatus;
            SaveQuests();
        }
    }


    public void SaveQuests()
    {
        string json = JsonUtility.ToJson(new QuestList(quests), true);
        // Mã hóa dữ liệu trước khi lưu
        string encryptedJson = EncryptionUtility.Encrypt(json);
        File.WriteAllText(savePath, encryptedJson);
    }

    public void LoadQuests()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string json = File.ReadAllText(savePath);
                if (string.IsNullOrEmpty(json)) return;

                string jsonDecoded = EncryptionUtility.Decrypt(json); // Giải mã dữ liệu
                QuestList data = JsonUtility.FromJson<QuestList>(jsonDecoded);

                if (data != null && data.quests != null)
                {
                    quests = data.quests;
                    //Debug.Log($"Đã load {quests.Count} nhiệm vụ từ file!");
                }
                else
                {
                    //Debug.LogWarning("Dữ liệu nhiệm vụ bị lỗi hoặc trống, tạo danh sách mới!");
                    quests = new List<Quest>();
                }
            }
            catch (System.Exception ex)
            {
                //Debug.LogError("Lỗi khi load dữ liệu nhiệm vụ: " + ex.Message);
                quests = new List<Quest>();
            }
        }
        else
        {
            quests = new List<Quest>(); // Nếu file chưa tồn tại, tạo danh sách rỗng
        }
    }




    public void StartQuest(Quest quest)
    {
        quest.status = QuestStatus.InProgress;
        SaveQuests();
        Thongbao.Singleton.ShowThongbaoHistory($"Bắt đầu nhiệm vụ: {quest.name}");
    }
    public void EnemyKilled(int questId)
    {
        Quest quest = quests.Find(q => q.questType == QuestType.Kill && q.status == QuestStatus.InProgress && q.id == questId.ToString());
        if (quest != null)
        {
            quest.soquaidatieudiet++;
            Thongbao.Singleton.ShowThongbao($"Đã tiêu diệt {quest.soquaidatieudiet}/{quest.soquaicantieudiet} quái cho nhiệm vụ {quest.name}");
            QuestUIManager.Singleton.LoadQuestUI();
            if (quest.soquaidatieudiet >= quest.soquaicantieudiet)
            {
                quest.status = QuestStatus.Completed;
                //// Chuyển sang nhiệm vụ tiếp theo nếu có
                //if (!string.IsNullOrEmpty(quest.nextQuestId))
                //{
                //    StartNextQuest(quest.nextQuestId);
                //}
                QuestUIManager.Singleton.LoadQuestUI();
                Thongbao.Singleton.ShowThongbaoHistory($"Nhiệm vụ {quest.name} hoàn thành!");
                Thongbao.Singleton.ShowThongbao($"Nhiệm vụ {quest.name} hoàn thành!");
            }

            SaveQuests();
        }
    }
    public void TalkToNPC(int npcId)
    {
        Quest quest = quests.Find(q => q.questType == QuestType.Talk && q.status == QuestStatus.InProgress && q.id == npcId.ToString());
        if (quest != null)
        {
            Thongbao.Singleton.ShowThongbao($"Đã nói chuyện với NPC {npcId}, hoàn thành nhiệm vụ: {quest.name}");
            quest.status = QuestStatus.Completed;
            SaveQuests();
        }
        else
        {
            Thongbao.Singleton.ShowThongbao($"Không có nhiệm vụ nào yêu cầu nói chuyện với NPC {npcId}");
        }
    }
    public void Collect(int npcId)
    {
        // Kiểm tra nhiệm vụ "Nói chuyện với NPC"
        Quest talkQuest = quests.Find(q => q.questType == QuestType.Talk && q.status == QuestStatus.InProgress && q.targetNPC == npcId.ToString());
        if (talkQuest != null)
        {
            talkQuest.status = QuestStatus.Completed;
            Thongbao.Singleton.ShowThongbao($"Nhiệm vụ '{talkQuest.name}' đã hoàn thành sau khi nói chuyện với NPC {npcId}");
            QuestUIManager.Singleton.LoadQuestUI();
            SaveQuests();
            return;
        }

        // Kiểm tra nhiệm vụ "Thu thập vật phẩm"
        Quest collectQuest = quests.Find(q => q.questType == QuestType.Collect && q.status == QuestStatus.InProgress);
        if (collectQuest != null)
        {
            collectQuest.soluongcan++;
            QuestUIManager.Singleton.LoadQuestUI();
            Thongbao.Singleton.ShowThongbao($"Nhặt được 1 {collectQuest.requiredItem}. Đã thu thập: {collectQuest.soluongcan}/{collectQuest.soluongcanthuthap}");

            // Nếu đủ số lượng, hoàn thành nhiệm vụ
            if (collectQuest.soluongcan >= collectQuest.soluongcanthuthap)
            {
                collectQuest.status = QuestStatus.Completed;
                Thongbao.Singleton.ShowThongbao($"Nhiệm vụ '{collectQuest.name}' đã hoàn thành sau khi thu thập đủ {collectQuest.requiredItem}");
                QuestUIManager.Singleton.LoadQuestUI();
            }

            SaveQuests();
        }
    }


    //public void StartNextQuest(string nextQuestId)
    //{
    //    Quest nextQuest = quests.Find(q => q.id == nextQuestId);
    //    if (nextQuest != null && nextQuest.status == QuestStatus.NotStarted)
    //    {
    //        nextQuest.status = QuestStatus.InProgress;
    //        GameManager.Singleton.idnhiemvu = int.Parse(nextQuestId); // Cập nhật ID nhiệm vụ hiện tại
    //        Debug.Log($"➡ Bắt đầu nhiệm vụ mới: {nextQuest.name} (ID: {nextQuest.id})");
    //        SaveQuests();
    //    }
    //}
    public void StartNextQuest(string nextQuestId)
    {
        Quest nextQuest = quests.Find(q => q.id == nextQuestId);
        if (nextQuest != null)
        {
            if (nextQuest.status == QuestStatus.NotStarted)
            {
                nextQuest.status = QuestStatus.InProgress;
            }

            // Cập nhật nhiệm vụ hiện tại trong GameManager
            GameManager.Singleton.idnhiemvu = int.Parse(nextQuestId);
            Thongbao.Singleton.ShowThongbaoHistory($"Bắt đầu nhiệm vụ mới: {nextQuest.name} (ID: {nextQuest.id})");

            SaveQuests();
            QuestUIManager.Singleton.LoadQuestUI(); // Cập nhật UI
        }
        else
        {
            Debug.LogWarning($"⚠ Không tìm thấy nhiệm vụ với ID: {nextQuestId}");
        }
    }



}
public static class QuestDatabase
{
    private static Dictionary<string, Quest> questDictionary = new Dictionary<string, Quest>();

    public static void LoadQuests(List<Quest> questList)
    {
        questDictionary.Clear();
        foreach (Quest quest in questList)
        {
            questDictionary[quest.id] = quest;
        }
    }

    public static Quest GetQuestById(string id)
    {
        return questDictionary.ContainsKey(id) ? questDictionary[id] : null;
    }
}

[System.Serializable]
public class QuestList
{
    public List<Quest> quests;
    public QuestList(List<Quest> quests) { this.quests = quests; }
}
