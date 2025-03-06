using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WobblyText : MonoBehaviour
{
    public TMP_Text textComponent;
    public float amplitude = 10f; // Control the height of the wobble
    public float frequency = 2f;  // Control the speed of the wobble
    public float randomness = 0.1f; // Control the randomness of the wobble

    public float blinkFrequency = 2f; // Control the speed of the blinking effect
    public float runSpeed = 100f; // Control the speed of the running text
    private float initialXPosition;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Force initial mesh update to get proper vertex data
        textComponent.ForceMeshUpdate();
        rectTransform = textComponent.GetComponent<RectTransform>();
        initialXPosition = rectTransform.anchoredPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        // Update the running text position
        //float newXPosition = initialXPosition + Mathf.Repeat(Time.time * runSpeed, rectTransform.rect.width * 2) - rectTransform.rect.width;
        //rectTransform.anchoredPosition = new Vector2(newXPosition, rectTransform.anchoredPosition.y);

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            // Blinking effect
            if (Mathf.Sin(Time.time * blinkFrequency + i) > 0)
            {
                charInfo.isVisible = true;
            }
            else
            {
                charInfo.isVisible = false;
            }

            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; ++j)
            {
                var orig = verts[charInfo.vertexIndex + j];
                float phase = Random.Range(-randomness, randomness) + orig.x * 0.01f;
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * frequency + phase) * amplitude, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
