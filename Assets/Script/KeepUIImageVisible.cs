using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class KeepUIImageVisible : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        // Đảm bảo rằng UI Image luôn hiển thị và không bị che khuất
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
