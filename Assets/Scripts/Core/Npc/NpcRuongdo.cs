using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcRuongdo : MonoBehaviour
{
    public GameObject npcCanvas;         // Canvas hiển thị giao diện NPC
    public Transform player;         // Transform của người chơi
    public float detectionRadius = 3f; // Bán kính kiểm tra khoảng cách

    private bool isPlayerNear = false; // Cờ kiểm tra người chơi đã gần NPC


    void Start()
    {
        npcCanvas.gameObject.SetActive(false); // Tắt canvas ban đầu
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

        // Nếu người chơi trong phạm vi và canvas chưa bật, bật canvas
        if (distance <= detectionRadius && !isPlayerNear)
        {
            isPlayerNear = true;
            OpenNPCShop();
        }
        // Nếu người chơi ra khỏi phạm vi và canvas đang bật, tắt canvas
        else if (distance > detectionRadius && isPlayerNear)
        {
            isPlayerNear = false;
            CloseNPCShop();
        }
    }

    void OpenNPCShop()
    {
        npcCanvas.gameObject.SetActive(true); // Bật canvas
        //Debug.Log("Player is near NPC. Shop opened.");
    }

    void CloseNPCShop()
    {
        npcCanvas.gameObject.SetActive(false); // Tắt canvas
        //Debug.Log("Player is far from NPC. Shop closed.");
    }
    public void OpenChest()
    {
        ChestManager.Singleton.p_ruongdo.SetActive(true); // Tắt canvas
    }
}
