using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int intro = 0;
    public int hp = 100;
    public int level = 1;
    public int dame = 10;

    private void Start()
    {
        //hp = GameManager.Singleton.hpmax;
       // GameManager.Singleton.LoadData(); // Tải dữ liệu
    }

    public void Save()
    {
       // GameManager.Singleton.SaveData();
    }

    public void Load()
    {
        GameManager.Singleton.LoadData();
        Debug.Log($"Máu: {hp}, Cấp độ: {level},Tấn công:{dame}");
    }




}
