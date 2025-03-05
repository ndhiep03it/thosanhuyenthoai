using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Số lượng xu nhận được từ mỗi coin
    public int coinValue = 1;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Kiểm tra xem xu có chạm đất không
    //    if (collision.gameObject.CompareTag("ground"))
    //    {
    //        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //        if (rb != null)
    //        {
    //            rb.velocity = Vector2.zero;  // Dừng mọi chuyển động
    //            rb.gravityScale = 0;        // Tắt trọng lực
    //            rb.isKinematic = true;      // Ngăn không cho tiếp tục bị ảnh hưởng bởi vật lý
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem player có chạm vào coin không
        if (collision.gameObject.CompareTag("Player"))
        {
            coinValue = Random.Range(100, 5000);
            // Thêm xu vào tổng số xu
            GameManager.Singleton.AddCoins(coinValue);
            Thongbao.Singleton.ShowThongbaoHistory("Bạn nhận được " + coinValue + " từ quái");
            // Hiển thị hiệu ứng thu thập (nếu cần)
            CollectEffect();

            // Xoá đối tượng coin khỏi game
            Destroy(gameObject);
        }
    }

    // Hàm hiệu ứng thu thập (tuỳ chọn)
    private void CollectEffect()
    {
        // Bạn có thể thêm hiệu ứng particle hoặc âm thanh tại đây
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Drop);
    }
}
