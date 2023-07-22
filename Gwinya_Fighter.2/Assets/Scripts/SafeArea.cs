using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    RectTransform panelSafeArea;

    Rect currentSafeArea = new Rect();
    ScreenOrientation currentOriontation = ScreenOrientation.AutoRotation;

    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();

        //store current value
        currentOriontation = Screen.orientation;
        currentSafeArea = Screen.safeArea;
    }

    void ApplySafeArea()
    {
        if (panelSafeArea == null)
            return;
        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position - safeArea.size;

        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;

        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;

        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;

        currentOriontation = Screen.orientation;
        currentSafeArea = Screen.safeArea;
    }

    void Update()
    {
        if(currentOriontation != Screen.orientation || (currentSafeArea != Screen.safeArea))
        {
            ApplySafeArea();
        }
    }
}
