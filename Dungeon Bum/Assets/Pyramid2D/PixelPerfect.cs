using UnityEngine;
using System.Collections;

public class PixelPerfect : MonoBehaviour
{
    public float PixelSize;

    void Start()
    {
        float s_baseOrthographicSize = Screen.height / PixelSize / 4.0f;
        Camera.main.orthographicSize = s_baseOrthographicSize;
    }
}
