using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class countDaodat : MonoBehaviour
{
    public Slider sliderXucdat;
    public float diggingTime = 5f; // Thời gian đếm ngược (5 giây)
    private float count = 0f;

    void Start()
    {
        sliderXucdat.maxValue = diggingTime; // Đặt giá trị tối đa của slider là thời gian đào
        sliderXucdat.value = 0f; // Bắt đầu từ 0
    }

    private void OnEnable()
    {
        // Đặt lại slider mỗi khi panel hoặc object được kích hoạt lại
        count = 0f;
        sliderXucdat.value = 0f; // Bắt đầu từ 0
        sliderXucdat.gameObject.SetActive(true); // Đảm bảo slider vẫn hiển thị
    }
    void Update()
    {
        if (count < diggingTime)
        {
            count += Time.deltaTime; // Tăng dần theo thời gian
            sliderXucdat.value = count; // Cập nhật giá trị slider
        }
        else
        {
            FinishDigging(); // Kết thúc quá trình đào khi đạt đến thời gian tối đa
        }
    }

    private void FinishDigging()
    {
        //Debug.Log("Đã hoàn thành đào đất!");
        // Tắt hoặc ẩn slider nếu muốn
        sliderXucdat.gameObject.SetActive(false);
        // Thực hiện hành động khác sau khi kết thúc quá trình đào
    }
}
