using UnityEngine;
using UnityEngine.SceneManagement; // Để sử dụng chức năng load scene

public class NextMapOnTrigger : MonoBehaviour
{
    [Header("Cấu hình map chuyển")]
    public string nextMapName; // Tên của map cần chuyển (scene)
    public Vector2 spawnPosition; // Vị trí spawn trong map mới
    private bool isTransitioning = false; // Đảm bảo chỉ thực hiện chuyển map một lần
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTransitioning)
        {
            isTransitioning = true;

            // Lưu trữ vị trí spawn cho map mới
            PlayerPrefs.SetFloat("SpawnX", spawnPosition.x);
            PlayerPrefs.SetFloat("SpawnY", spawnPosition.y);
            GameManager.Singleton.map = nextMapName;

            // Reset joystick khi nhả chuột hoặc cảm ứng         
            if (Input.GetMouseButtonUp(0) || Input.touchCount == 0)
            {
                PlayerController.Singleton.ResetJoystick();
                PlayerController.Singleton.OnMapChangeStart();
            }

            // Chuyển sang map mới
            SceneManager.LoadScene(nextMapName);

            // Các thao tác khác
            UImanager.Singleton.ClickPanelLoading();
            PlayerController.Singleton.LoadPositionPlayer();
            GameManager.Singleton.SaveData();

            isTransitioning = false; // Reset trạng thái chuyển map
            PlayerController.Singleton.OnMapChangeEnd();
        }
    }

}
