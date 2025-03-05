using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 1f; // Tốc độ di chuyển lên trên
    public float destroyTime = 1f; // Thời gian tồn tại trước khi bị hủy
    public Vector3 offset = new Vector3(0, 1, 0); // Độ dịch chuyển ban đầu
    public Vector3 randomizeIntensity = new Vector3(0.5f, 0, 0); // Độ ngẫu nhiên hóa vị trí

    private void Start()
    {
        // Random vị trí ban đầu (nếu cần)
        transform.position += offset;
        transform.position += new Vector3(
            Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
            Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
            Random.Range(-randomizeIntensity.z, randomizeIntensity.z)
        );

        // Hủy đối tượng sau `destroyTime`
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        // Di chuyển chữ đi lên theo thời gian
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
    }
}
