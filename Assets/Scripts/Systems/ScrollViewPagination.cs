using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewPagination : MonoBehaviour
{
    public Transform content; // Content chứa các item
    public Button nextButton; // Nút Next
    public Button previousButton; // Nút Previous
    public Text pageIndicator; // Hiển thị số trang (tùy chọn)

    private const int itemsPerPage = 30; // Số lượng item hiển thị trên mỗi lần
    private int currentPage = 0; // Trang hiện tại
    private int totalPages = 0; // Tổng số trang

    void Start()
    {
        UpdatePagination();
    }

    private void OnEnable()
    {
        UpdatePagination();
        PreviousPage();
    }

    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            UpdatePagination();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePagination();
        }
    }

    public void ReloadItems()
    {
        currentPage = 0; // Reset về trang đầu tiên
        ResetTab(); // Gọi hàm đồng bộ
    }

    private void ResetTab()
    {
        int totalItems = content.childCount;
        totalPages = Mathf.CeilToInt((float)totalItems / itemsPerPage);

        // Cập nhật hiển thị của từng item
        for (int i = 0; i < totalItems; i++)
        {
            GameObject item = content.GetChild(i).gameObject;

            if (i >= currentPage * itemsPerPage && i < (currentPage + 1) * itemsPerPage)
            {
                item.SetActive(true); // Bật các item trong trang hiện tại
            }
            else
            {
                item.SetActive(false); // Tắt các item không thuộc trang hiện tại
            }
        }

        // Bật/Tắt nút Next và Previous
        nextButton.interactable = currentPage < totalPages - 1;
        previousButton.interactable = currentPage > 0;

        // Cập nhật chỉ báo số trang
        if (pageIndicator != null)
        {
            pageIndicator.text = $"Trang {currentPage + 1}/{totalPages}";
        }
    }

    public void UpdatePagination()
    {
        int totalItems = content.childCount;
        totalPages = Mathf.CeilToInt((float)totalItems / itemsPerPage);

        // Cập nhật hiển thị của từng item
        for (int i = 0; i < totalItems; i++)
        {
            GameObject item = content.GetChild(i).gameObject;

            if (i >= currentPage * itemsPerPage && i < (currentPage + 1) * itemsPerPage)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }

        // Bật/Tắt nút Next và Previous
        nextButton.interactable = currentPage < totalPages - 1;
        previousButton.interactable = currentPage > 0;

        // Cập nhật chỉ báo số trang
        if (pageIndicator != null)
        {
            pageIndicator.text = $"Trang {currentPage + 1}/{totalPages}";
        }
      
    }
}
