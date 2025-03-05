using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountInfo : MonoBehaviour
{
    public Text txtUserName;
    public Text txtPassword;
    public Button removeAccount;

    private JsonManager jsonManager; // Quản lý file JSON
    private string username;         // Tên tài khoản của prefab này
    private string password;         // mật khẩu của prefab này
    private DateTime timerLogin;


    public void SelectAccount()
    {
        // Khi nhấn tự điền tk và mk vào ô input
        LoginUI.Instance.LoginObject.SetActive(true);
        LoginUI.Instance.AccountObject.SetActive(false);
        LoginUI.Instance.input_UserName.text = username;
        LoginUI.Instance.input_PassWord.text = password;
        LoginUI.Instance.textLoginAccount.text = "Chơi TK:" + username;
        Notification.Instance.ShowNotification("Bạn đã chọn TK: " + username);
        // Lưu tên người dùng và mật khẩu (không khuyến khích lưu mật khẩu trong PlayerPrefs)
        PlayerPrefs.SetString("user", username);
        PlayerPrefs.SetString("pass", password);

    }
    // Gọi khi tạo tài khoản
    public void Initialize(string username, string password, JsonManager manager)
    {
        this.username = username;
        this.password = password;
        txtUserName.text = username;
        txtPassword.text = password;
        jsonManager = manager;

        // Đăng ký sự kiện cho nút xóa
        removeAccount.onClick.AddListener(OnDeleteClicked);
    }

    // Hàm xử lý khi nút xóa được bấm
    private void OnDeleteClicked()
    {
        if (jsonManager != null)
        {
            jsonManager.RemoveAccount(username);
            Destroy(gameObject); // Xóa item khỏi UI
        }
    }




}
