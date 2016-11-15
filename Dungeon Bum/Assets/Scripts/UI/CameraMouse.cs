using UnityEngine;
using System.Collections;

public class CameraMouse : MonoBehaviour
{
    public Vector2 MousePosition;
    public Canvas canvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
        MousePosition = canvas.transform.TransformPoint(pos);
    }
}
