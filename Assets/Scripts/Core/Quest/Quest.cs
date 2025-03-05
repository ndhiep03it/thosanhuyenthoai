using System;

[Serializable]
public class Quest
{
    public string id;
    public string name;
    public string description;

    public QuestStatus status;
    public QuestType questType; // Loại nhiệm vụ


    // Dữ liệu riêng cho từng loại nhiệm vụ
    public int soquaicantieudiet;   // Số quái cần tiêu diệt
    public int soquaidatieudiet;  // Số quái đã tiêu diệt

    public Item requiredItem;   // Vật phẩm cần thu thập
    public int soluongcan;   // Số lượng cần
    public int soluongcanthuthap;  // Số lượng đã thu thập
    public string targetNPC;
    public string nextQuestId;

    public Quest(string id, string name, string description, QuestStatus status)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.status = status;
    }
}

public enum QuestStatus { NotStarted, InProgress, Completed }
public enum QuestType
{
    Kill,    // Tiêu diệt quái
    Collect, // Nhặt vật phẩm
    Talk,     // Nói chuyện với NPC
    MaxQuest     // Gioi han nv
}

