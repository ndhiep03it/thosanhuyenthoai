using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheatgame : MonoBehaviour
{
    public static Cheatgame singleton;
    private bool isFastSpeed = false; // Biến trạng thái để theo dõi tốc độ nhanh
    private bool isPanelActive = false; // Biến trạng thái để theo dõi chế độ Panel

    public GameObject PanelGame; // Panel Game để bật tắt
    public GameObject[] AllObj; // Đối tượng chứa tất cả các con cần bật/tắt
    public Item item;
    public int quantity;


    private void Awake()
    {
        if (singleton == null) singleton = this;
    }
    private void Update()
    {
        // Bật/tắt tốc độ nhanh khi nhấn phím K
        if (Input.GetKeyDown(KeyCode.K))
        {
            isFastSpeed = !isFastSpeed;
            Time.timeScale = isFastSpeed ? 2f : 1f;

            if (isFastSpeed)
                Thongbao.Singleton.ShowThongbao("Đã bật tốc độ " + Time.timeScale);
            else
                Thongbao.Singleton.ShowThongbao("Đã tắt tốc độ. Tốc độ hiện tại: " + Time.timeScale);
        }

        // Bật/tắt Panel Game khi nhấn phím B
        if (Input.GetKeyDown(KeyCode.B))
        {
            isPanelActive = !isPanelActive;

            // Nếu bật Panel Game
            if (isPanelActive)
            {
                foreach (GameObject child in AllObj)
                {
                    child.gameObject.SetActive(false); // Tắt tất cả các đối tượng con
                }
                PanelGame.SetActive(true); // Bật Panel Game
            }
            else
            {
                foreach (GameObject child in AllObj)
                {
                    child.gameObject.SetActive(true); // Bật lại tất cả các đối tượng con
                }
                PanelGame.SetActive(false); // Tắt Panel Game
            }
        }
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            AddItemToInventory(item, quantity);

        }
    }
    public void AddItemToInventory(Item item, int quantity)
    {
        string result = Inventory.Singleton.AddItem(item, quantity,"Vật phẩm được buff.");
        //Debug.Log(result); // Hiển thị kết quả trong Console hoặc xử lý thêm
        Thongbao.Singleton.ShowThongbao(result);
    }


    public void HideUIEndActive()
    {
        isPanelActive = !isPanelActive;

        // Nếu bật Panel Game
        if (isPanelActive)
        {
            foreach (GameObject child in AllObj)
            {
                child.gameObject.SetActive(false); // Tắt tất cả các đối tượng con
            }
            
        }
        else
        {
            foreach (GameObject child in AllObj)
            {
                child.gameObject.SetActive(true); // Bật lại tất cả các đối tượng con
            }
           
        }
    }

}
