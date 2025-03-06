using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    public static JsonManager Instance;
    private string filePath;
    public Transform contentListAccount;
    public GameObject accountListPrefabs;

    public PlayfabManager playfabManager;
    public List<Account> accounts;


    protected void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    private void Start()
    {
        filePath = Application.persistentDataPath + "/accounts.json";
        Debug.Log(filePath);
        DisplayAllAccounts();
    }

    public void SaveAccounts(List<Account> accounts)
    {
        AccountList accountList = new AccountList { accounts = accounts };
        string json = JsonUtility.ToJson(accountList, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Tài khoản đã lưu vào: " + filePath);
    }

    public List<Account> LoadAccounts()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            AccountList accountList = JsonUtility.FromJson<AccountList>(json);
            return accountList.accounts;
        }
        return new List<Account>();
    }

    private bool AccountExists(string username, List<Account> accounts)
    {
        foreach (var account in accounts)
        {
            if (account.username == username)
            {
                return true;
            }
        }
        return false;
    }
    public void RemoveAccount(string username)
    {
        // Tải danh sách tài khoản hiện tại
        List<Account> accounts = LoadAccounts();

        // Tìm tài khoản theo username
        Account accountToRemove = accounts.Find(account => account.username == username);

        if (accountToRemove != null)
        {
            // Xóa tài khoản
            accounts.Remove(accountToRemove);

            // Lưu danh sách cập nhật vào file JSON
            SaveAccounts(accounts);
            DisplayAllAccounts();
            Notification.Instance.ShowNotification("Xóa thành công: " + username);
            //Debug.Log("Xóa thành công: " + username);
        }
        else
        {
            //Debug.LogWarning("Không thấy tên đăng nhập: " + username);
        }
    }

    public void CreateAccount()
    {
        // Lấy thời gian hiện tại theo UTC
        DateTime timeLogin = DateTime.Now;

        // Lấy thông tin tài khoản từ các InputField
        string username = playfabManager.input_UserName_Login.text;
        string password = playfabManager.input_PassWord_Login.text;

        // Thêm tài khoản với thời gian đăng nhập
        AddAccount(username, password, timeLogin);
    }


    public void AddAccount(string username, string password, DateTime timeLogin)
    {
        List<Account> accounts = LoadAccounts();

        // Kiểm tra nếu tài khoản đã tồn tại
        if (AccountExists(username, accounts))
        {
            Debug.LogWarning($"Tài khoản '{username}' đã tồn tại. Không thêm mới.");
            return;
        }

        // Xóa tài khoản cuối cùng nếu danh sách vượt quá 20
        if (accounts.Count >= 20)
        {
            Debug.LogWarning($"Vượt quá giới hạn 20 tài khoản. Đã xóa tài khoản cuối: {accounts[accounts.Count - 1].username}");
            accounts.RemoveAt(accounts.Count - 1); // Xóa tài khoản cuối
        }

        // Thêm tài khoản mới vào đầu danh sách
        accounts.Insert(0, new Account { username = username, password = password, timerLogin = timeLogin.ToString("dd-MM-yyyy HH:mm:ss") });
        Debug.Log($"Đã thêm tài khoản mới: {username} tại {timeLogin.ToString("dd-MM-yyyy HH:mm:ss")}");

        // Lưu và cập nhật danh sách
        SaveAccounts(accounts);
        DisplayAllAccounts();
    }



    public void ActiveListUI()
    {
        filePath = Application.persistentDataPath + "/accounts.json";
        DisplayAllAccounts();
    }
    void DisplayAllAccounts()
    {
        accounts =  LoadAccounts();

        // Xóa các item cũ trong danh sách
        foreach (Transform child in contentListAccount)
        {
            Destroy(child.gameObject);
        }

        // Tạo item mới cho từng tài khoản
        foreach (var account in accounts)
        {
            //Debug.Log($"Tên đăng nhập: {account.username}, Mật khẩu: {account.password}, Thời gian: {account.timerLogin}");
            GameObject obj = Instantiate(accountListPrefabs, contentListAccount, false);
            AccountInfo accountInfo = obj.GetComponent<AccountInfo>();

            // Gán dữ liệu và JsonManager
            accountInfo.Initialize(account.username, account.password, this);
        }
    }


}
