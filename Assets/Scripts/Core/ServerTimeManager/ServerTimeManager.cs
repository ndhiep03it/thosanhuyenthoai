using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ServerTimeManager : MonoBehaviour
{
    public static ServerTimeManager Instance;
    public static DateTime ServerTime;
    private const string TIME_API_URL = "https://www.timeapi.io/api/Time/current/zone?timeZone=Asia/Ho_Chi_Minh";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
    }

    public IEnumerator FetchServerTime()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(TIME_API_URL))
        {
            yield return request.SendWebRequest();

            // Kiểm tra nếu có lỗi kết nối
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Lỗi khi lấy thời gian từ máy chủ: {request.error}");
                yield break;
            }

            string json = request.downloadHandler.text;

            // Kiểm tra phản hồi rỗng
            if (string.IsNullOrEmpty(json))
            {
                Debug.LogError("Lỗi: Phản hồi từ server bị rỗng!");
                yield break;
            }

            try
            {
                // Phân tích JSON
                TimeApiResponse response = JsonUtility.FromJson<TimeApiResponse>(json);

                // Kiểm tra giá trị `dateTime`
                if (string.IsNullOrEmpty(response.dateTime))
                {
                    Debug.LogError("Lỗi: Trường 'dateTime' bị thiếu hoặc null!");
                    yield break;
                }

                // Chuyển đổi thành DateTime
                if (DateTime.TryParse(response.dateTime, out DateTime serverTime))
                {
                    ServerTime = serverTime;
                    Debug.Log($"Thời gian máy chủ: {ServerTime}");
                }
                else
                {
                    Debug.LogError("Lỗi: Không thể chuyển đổi thời gian từ server!");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Lỗi khi phân tích JSON: {e.Message}");
            }
        }
    }

    [Serializable]
    private class TimeApiResponse
    {
        public string dateTime;
    }
}
