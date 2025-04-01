using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotTaker : MonoBehaviour
{
    public string screenshotFileName = "screenshot.png";
    public KeyCode screenshotKey = KeyCode.P;

    void Update()
    {
        // ������ Ű�� ������ ��ũ������ ����ϴ�.
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }

    void TakeScreenshot()
    {
        // ��ũ������ ��� ���Ͽ� �����մϴ�.
        ScreenCapture.CaptureScreenshot(screenshotFileName);
        Debug.Log("Screenshot taken and saved as " + screenshotFileName);
    }
}
