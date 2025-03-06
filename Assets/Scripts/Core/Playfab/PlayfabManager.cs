//using PlayFab;
//using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    public GameObject Panel_Check;
    [Header("LOGIN")]
    public InputField input_UserName_Login;
    public InputField input_PassWord_Login;

    [Header("REGISTER")]
    public InputField input_UserName_Register;
    public InputField input_PassWord_Register;
    public string playfabId;
    public string namePlayer;
    public int Gold;
    public int Rubi;



    private void Start()
    {
        input_UserName_Login.text = PlayerPrefs.GetString("user");
        input_PassWord_Login.text = PlayerPrefs.GetString("pass");

        // Đặt InputField ở chế độ mật khẩu ban đầu
        // _InputField_Password.contentType = InputField.ContentType.Password;
        // _InputField_Password.ForceLabelUpdate();

        // Gắn sự kiện cho nút
        //toggleButton.onClick.AddListener(TogglePasswordVisibility);

    }
    public void Login()
    {
        // Kiểm tra tên người dùng và mật khẩu không rỗng
        if (string.IsNullOrEmpty(input_UserName_Login.text) || string.IsNullOrEmpty(input_PassWord_Login.text))
        {
            // Hiển thị thông báo lỗi
            Notification.Instance.ShowNotification("Tên người dùng và mật khẩu không được để trống!");
            return;
        }

        // Kiểm tra độ dài mật khẩu
        if (input_PassWord_Login.text.Length < 6)
        {
            Notification.Instance.ShowNotification("Mật khẩu phải có ít nhất 6 ký tự!");
            return;
        }

        // Lưu tên người dùng và mật khẩu (không khuyến khích lưu mật khẩu trong PlayerPrefs)
        PlayerPrefs.SetString("user", input_UserName_Login.text);
        PlayerPrefs.SetString("pass", input_PassWord_Login.text);

        // Tạo yêu cầu đăng nhập với PlayFab
        //var request = new LoginWithPlayFabRequest
        //{
        //    TitleId = "5A23B",
        //    Username = input_UserName_Login.text,
        //    Password = input_PassWord_Login.text,

        //    InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
        //    {
        //        GetPlayerProfile = true,
        //        GetUserAccountInfo = true,
        //        GetTitleData = true,
        //        GetUserInventory = true,
        //        GetUserVirtualCurrency = true,
        //        GetUserData = true,
        //        GetPlayerStatistics = true
        //    }
        //};

        //// Gọi API đăng nhập của PlayFab
        //PlayFabClientAPI.LoginWithPlayFab(request, OnSucces, OnErrorLogin);

        // Hiển thị panel kiểm tra đăng nhập trong lúc chờ kết quả
        Panel_Check.SetActive(true);
    }


    //private void OnError(PlayFabError error)
    //{
       
    //}

    //private void OnSucces(LoginResult result)
    //{
    //    Panel_Check.SetActive(true);
    //    JsonManager.Instance.CreateAccount();
    //    GetAccountInfoRequest request = new GetAccountInfoRequest();
    //    PlayFabClientAPI.GetAccountInfo(request, OnGetNameCheckSuccess, OnError);
    //}

    //private void OnErrorLogin(PlayFabError error)
    //{
    //    Panel_Check.SetActive(false);
    //    // Notification.Instance.SendNotification("Đăng nhập thất bại.");
    //    if (error.Error == PlayFabErrorCode.AccountBanned)
    //    {

    //        string itemTime = null;
    //        string itemReason = null;

    //        foreach (var item in error.ErrorDetails)
    //        {
    //            //Debug.Log("Reason : " + item.Key + "\t" + "Expires : " + item.Value[0]);
    //            itemTime = item.Value[0];
    //            itemReason = item.Key;
    //        }

    //        //if (!string.IsNullOrEmpty(itemTime))
    //        //{
    //        //    DateTime utcTime;
    //        //    if (DateTime.TryParse(itemTime, null, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out utcTime))
    //        //    {
    //        //        // Set the Kind property to DateTimeKind.Utc
    //        //        utcTime = DateTime.SpecifyKind(utcTime, DateTimeKind.Utc);

    //        //        DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
    //        //        itemTime = vietnamTime.ToString("yyyy-MM-dd HH:mm:ss");
    //        //    }
    //        //}

    //       // bannedPanel.SetActive(true);
    //        //banStatusText.text = "\u2192Bạn bị cấm. Lệnh cấm hết hạn vào: " + itemTime + "\nLý do: " + itemReason + "\t" + " .Liên hệ CSKH hỗ trợ:0906003264.";

    //    }
    //    else
    //    {

    //    }
    //    // Check if the error is due to invalid username or password
    //    if (error.Error == PlayFabErrorCode.InvalidUsernameOrPassword)
    //    {

    //        Notification.Instance.ShowNotification("Sai Gmail hoặc Mật khẩu. Vui lòng thử lại");


    //    }
    //    else
    //    {
    //        Notification.Instance.ShowNotification("Đã xảy ra lỗi. Vui lòng thử lại.");


    //    }
    //}

    //private void OnGetNameCheckSuccess(GetAccountInfoResult result)
    //{
    //    DontDestroyOnLoad(gameObject);

    //    Notification.Instance.ShowNotification("Đăng nhập thành công.");
    //    Application.runInBackground = true;
    //    Panel_Check.SetActive(false);
    //    namePlayer = result.AccountInfo.TitleInfo.DisplayName;
    //    //txtGmail.text= result.AccountInfo.PrivateInfo.Email;
    //    playfabId = result.AccountInfo.PlayFabId;
    //    GetVirtualCurrency();
    //}
    //public void GetVirtualCurrency()
    //{
    //    PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), OnGetPlayerGameCombinedInfoSuccess, OnError);
    //}
    //private void OnGetPlayerGameCombinedInfoSuccess(PlayFab.ClientModels.GetUserInventoryResult obj)
    //{      
    //    Gold = obj.VirtualCurrency["GD"];
    //    Rubi = obj.VirtualCurrency["RB"];

    //}
}
