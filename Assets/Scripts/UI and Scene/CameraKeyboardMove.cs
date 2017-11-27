using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeyboardMove : MonoBehaviour
{
    private static readonly float[] BoundsX = new float[] { -10f, 5f };
    private static readonly float[] BoundsZ = new float[] { -18f, -4f };

    public float speed = 5.0f;
    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            CheckBounds();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            CheckBounds();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            CheckBounds();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
            CheckBounds();
        }
    }

    private void CheckBounds()
    {
        //make sure it remains within bounds; get a reference to the position and then check if it's within bounds, if it's not, then set it to that position
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);
        transform.position = pos;
    }

    public void PanCamera(string whichDirection)
    {
        switch (whichDirection)
        {
            case "left":
                transform.position += Vector3.left * speed * Time.deltaTime;
                break;
            case "right":
                transform.position += Vector3.right * speed * Time.deltaTime;
                break;
            case "up":
                transform.position += Vector3.forward * speed * Time.deltaTime;
                break;
            case "down":
                transform.position += Vector3.back * speed * Time.deltaTime;
                break;
        }
        CheckBounds();
    }
}
