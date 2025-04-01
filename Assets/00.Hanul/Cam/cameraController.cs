using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Camera cam;
    public float speed;
    public Vector3 originPos = new Vector3(0f, 0f, -10f);
    public float camMinX;
    public float camMaxX;

    bool isResetting = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (cam.transform.position.x < camMaxX)
                cam.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (cam.transform.position.x > camMinX)
                cam.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isResetting = true;
        }

        if (isResetting)
        {
            if (cam.transform.position != originPos)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, originPos, speed / 2 * Time.deltaTime);

                if (Vector3.Distance(cam.transform.position, originPos) < 0.1f)
                {
                    cam.transform.position = originPos;
                    isResetting = false;
                }
            }
        }
    }
}
