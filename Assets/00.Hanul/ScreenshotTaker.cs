using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotTaker : MonoBehaviour
{
    public string screenshotFileName = "screenshot.png";
    public KeyCode screenshotKey = KeyCode.P;

    void Update()
    {
        // 지정된 키가 눌리면 스크린샷을 찍습니다.
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }

    void TakeScreenshot()
    {
        // 스크린샷을 찍고 파일에 저장합니다.
        ScreenCapture.CaptureScreenshot(screenshotFileName);
        Debug.Log("Screenshot taken and saved as " + screenshotFileName);
    }
}
