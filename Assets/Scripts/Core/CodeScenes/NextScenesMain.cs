using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextScenesMain : MonoBehaviour
{
    public GameObject PanelDark;
    public GameObject P_DIEUKHOAN;
    public GameObject P_UI;
    public int dieukhoan = 0;
    public Button buttondieukhoan;
    public Scrollbar scrollbar;

    public void NextLevelPlay()
    {
        PanelDark.SetActive(true);
        StartCoroutine(nextlv());
    }

    private void Start()
    {
        // Lấy trạng thái điều khoản đã đồng ý hay chưa (0: chưa, 1: đã đồng ý)
        dieukhoan = PlayerPrefs.GetInt("dieukhoan", 0); // Mặc định là 0 nếu chưa có

        // Nếu chưa đồng ý điều khoản, bật Panel điều khoản
        P_DIEUKHOAN.SetActive(dieukhoan == 0);
        P_UI.SetActive(dieukhoan == 1);

        // Đặt nút đồng ý điều khoản không thể bấm khi mới vào
        buttondieukhoan.interactable = false;

        // Gán sự kiện khi thanh cuộn thay đổi
        scrollbar.onValueChanged.AddListener(CheckScroll);
    }

    IEnumerator nextlv()
    {
        yield return new WaitForSeconds(2f);
        if (GameManager.Singleton.intro == 1)
        {
            SceneManager.LoadSceneAsync(3);
        }
        else
        {
            SceneManager.LoadSceneAsync(1);
        }
    }

    public void CheckScroll(float value)
    {
        // Nếu thanh cuộn ở cuối (giá trị = 0), bật button
        buttondieukhoan.interactable = (value <= 0.01f);
    }

    public void Dongydieukhoan()
    {
        // Lưu trạng thái đã đồng ý điều khoản
        PlayerPrefs.SetInt("dieukhoan", 1);
        PlayerPrefs.Save();

        // Tắt panel điều khoản sau khi đồng ý
        P_DIEUKHOAN.SetActive(false);
        P_UI.SetActive(true);
    }
}
