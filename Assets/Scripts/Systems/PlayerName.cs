using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private InputField inputFieldName; // Ô nhập tên của người chơi
    [SerializeField] private Text messageText;         // Văn bản hiển thị thông báo (nếu có)
    [SerializeField] private float messageDuration = 2f; // Thời gian hiển thị thông báo (giây)
    [SerializeField] private GameObject panelCharacter; 


    public void CreateName()
    {
        string playerName = inputFieldName.text.Trim(); // Lấy tên người chơi và loại bỏ khoảng trắng thừa

        // Kiểm tra nếu tên người chơi không hợp lệ
        if (string.IsNullOrEmpty(playerName))
        {
            ShowMessage("Tên không được để trống!");
            return;
        }

        if (playerName.Length < 3)
        {
            ShowMessage("Tên phải có ít nhất 3 ký tự!");
            return;
        }

        if (playerName.Length > 20)
        {
            ShowMessage("Tên không được quá 20 ký tự!");
            return;
        }

        // Lưu tên người chơi (có thể dùng PlayerPrefs hoặc hệ thống lưu trữ khác)
        PlayerPrefs.SetString("PlayerName", playerName);
        panelCharacter.SetActive(false);
        ShowMessage("Tên đã được lưu: " + playerName);
        GameManager.Singleton.intro = 1;
        GameManager.Singleton.thuxathu = 0;
        GameManager.Singleton.hp = 100;
        GameManager.Singleton.level = 1;
        GameManager.Singleton.dame = 10;
        GameManager.Singleton.SaveData();
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message; // Hiển thị thông báo
            messageText.gameObject.SetActive(true); // Bật hiển thị

            // Bắt đầu Coroutine để ẩn thông báo sau một khoảng thời gian
            StartCoroutine(HideMessageAfterDelay());
        }
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration); // Chờ thời gian hiển thị
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false); // Ẩn thông báo
        }
    }
}
