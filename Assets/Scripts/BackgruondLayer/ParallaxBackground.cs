using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    [SerializeField] private List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = ParallaxCamera.Singleton.camera2d.GetComponent<ParallaxCamera>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }

    void Move(float delta)
    {
        // Loại bỏ các lớp null khỏi danh sách
        parallaxLayers.RemoveAll(layer => layer == null);

        foreach (ParallaxLayer layer in parallaxLayers)
        {
            if (layer != null)
            {
                layer.Move(delta);
            }
        }
    }

    void OnDestroy()
    {
        // Hủy đăng ký sự kiện khi đối tượng bị hủy
        if (parallaxCamera != null)
        {
            parallaxCamera.onCameraTranslate -= Move;
        }
    }
}
