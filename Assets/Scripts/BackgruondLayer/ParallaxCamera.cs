using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public static ParallaxCamera Singleton;
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private float oldPosition;
    public Camera camera2d;

    void Start()
    {
        oldPosition = transform.position.x;
    }
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
           
        }
        
    }
    void Update()
    {
        if (transform.position.x != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                float delta = oldPosition - transform.position.x;
                onCameraTranslate(delta);
            }

            oldPosition = transform.position.x;
        }
    }
}
