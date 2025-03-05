using UnityEngine;

public class PlayerOrderInLayer : MonoBehaviour
{
    private SpriteRenderer playerRenderer;
    public int defaultOrder = 0;      // Order ban đầu của Player
    public int frontOrder = 5;        // Order khi đứng trước cây

    private void Start()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
        //playerRenderer.sortingOrder = defaultOrder; // Gán order ban đầu
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree")) // Kiểm tra nếu chạm vào đối tượng có tag "Tree"
        {
            //playerRenderer.sortingOrder = frontOrder; // Đưa Player lên trước
            //Debug.Log("Player đứng trước cây");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree"))
        {
            //playerRenderer.sortingOrder = defaultOrder; // Khôi phục thứ tự ban đầu
            //Debug.Log("Player trở về thứ tự ban đầu");
        }
    }
}
