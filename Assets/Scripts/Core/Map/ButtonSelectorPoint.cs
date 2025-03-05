using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectorPoint : MonoBehaviour
{
    public RectTransform arrowNhan;  // Mũi tên khi click button
    public RectTransform arrowPlayer; // Mũi tên theo button của map hiện tại
    public Button[] buttons; // Danh sách Button
    public Text[] mapTexts; // Danh sách Text chứa tên map
    public Text mapTextsButton; // Hiển thị khu vực hiện tại

    public Animator[] animators; 
    private List<Button> buttonList; // Chuyển mảng thành List

    void Start()
    {
        buttonList = new List<Button>(buttons); // Chuyển từ Button[] sang List<Button>

        // Gán sự kiện cho từng Button
        foreach (Button btn in buttonList)
        {
            btn.onClick.AddListener(() => MoveArrow(btn));
        }
    }

    void Update()
    {
        MoveArrowToMapButton(); // Cập nhật vị trí arrowPlayer theo map hiện tại
    }

    void MoveArrow(Button targetButton)
    {
        // Khi nhấn button, di chuyển arrowNhan đến vị trí button đó
        arrowNhan.position = targetButton.transform.position;

        // Tìm vị trí của button trong danh sách
        int index = buttonList.IndexOf(targetButton);
        
        // Kiểm tra nếu index hợp lệ, cập nhật text khu vực
        if (index >= 0 && index < mapTexts.Length)
        {
            mapTextsButton.text = "Khu vực: " + mapTexts[index].text;
        }
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        // Bật animator của button được nhấn, tắt các animator còn lại
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].enabled = (i == index);
        }
    }

    void MoveArrowToMapButton()
    {
        string currentMap = GameManager.Singleton.map; // Lấy map hiện tại

        foreach (Button btn in buttonList)
        {
            if (btn.gameObject.name == currentMap) // Tên button trùng với map hiện tại
            {
                arrowPlayer.position = btn.transform.position;
                arrowPlayer.gameObject.SetActive(true);
                return;
            }
        }

        // Nếu không có button nào trùng map, ẩn mũi tên
        arrowPlayer.gameObject.SetActive(false);
    }
}
