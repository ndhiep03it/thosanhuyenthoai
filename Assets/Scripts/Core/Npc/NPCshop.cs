using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import để dùng UI

public class NPCshop : MonoBehaviour
{
    public GameObject npcCanvas;          // Canvas hiển thị giao diện NPC
    public Transform player;              // Transform của người chơi
    public GameObject imageOpup;          
    public Text dialogueText;             // Text hiển thị hội thoại
    public float detectionRadius = 3f;    // Bán kính kiểm tra khoảng cách
    public List<string> dialogues = new List<string>();

    private bool isPlayerNear = false;    // Cờ kiểm tra người chơi đã gần NPC


   
    void Start()
    {
        npcCanvas.gameObject.SetActive(false); // Tắt canvas ban đầu
        if (dialogueText != null)
        {
            dialogueText.text = ""; // Đảm bảo ban đầu không có text
        }
    }

    void Update()
    {
        CheckPlayerDistance();
    }

    void CheckPlayerDistance()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Tính khoảng cách giữa NPC và người chơi
        float distance = Vector2.Distance(transform.position, player.position);

        // Nếu người chơi trong phạm vi và canvas chưa bật, bật canvas và hiển thị hội thoại
        if (distance <= detectionRadius && !isPlayerNear)
        {
            isPlayerNear = true;
            OpenNPCShop();
            imageOpup.SetActive(true);
            ShowDialogue();
        }
        // Nếu người chơi ra khỏi phạm vi và canvas đang bật, tắt canvas và hội thoại
        else if (distance > detectionRadius && isPlayerNear)
        {
            isPlayerNear = false;
            CloseNPCShop();
            imageOpup.SetActive(false);
            //HideDialogue();
        }
    }

    void OpenNPCShop()
    {
        npcCanvas.gameObject.SetActive(true); // Bật canvas
    }

    void CloseNPCShop()
    {
        npcCanvas.gameObject.SetActive(false); // Tắt canvas
    }

    void ShowDialogue()
    {
        if (dialogueText != null && dialogues.Count > 0)
        {
            int randomIndex = Random.Range(0, dialogues.Count);
            dialogueText.text = dialogues[randomIndex];
        }
    }
    public void Q1()
    {
        //Quest firstQuest = QuestManager.Instance.quests.Find(q => q.id == "Q1"); // Nhận nhiệm vụ đầu tiên
        //if (firstQuest != null && firstQuest.status == QuestStatus.NotStarted)
        //{
        //    QuestManager.Instance.StartQuest(firstQuest);
        //}
        QuestManager.Instance.TalkToNPC(0);
        QuestUIManager.Singleton.LoadQuestUI(); // Cập nhật UI nhiệm vụ
    }

    

    void HideDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.text = ""; // Xóa đoạn hội thoại
        }
    }
}
