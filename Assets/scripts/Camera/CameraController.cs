using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

    [System.Serializable]
    public class MoveByMouse
    {
        public float accelX;
        public float accelZ;
        public float accelOffset = 0.1f;
        public float maxAccel = 2.0f;
        public float minAccel = -2.0f;
        public float screenOffset = 5f;
    }

    public MoveByMouse mouseMove = new MoveByMouse();
    private Vector3 currentCursorPos, oldCursorPos;

	// Use this for initialization
	void Start () { 
        oldCursorPos = Input.mousePosition;
        currentCursorPos = Input.mousePosition;
        Debug.Log(Screen.width + "/" + Screen.height);
    }
	
	// Update is called once per frame
	void Update () {
        currentCursorPos = Input.mousePosition;
        Debug.Log(currentCursorPos);
        //Need to move camera to the left
        if(currentCursorPos.x <= mouseMove.screenOffset)
        {          
            if (mouseMove.accelX > mouseMove.minAccel)
            {
                mouseMove.accelX -= mouseMove.accelOffset * Time.deltaTime;
            }
        }

        //Need to move camera to the right
        else if (currentCursorPos.x >= Screen.width - mouseMove.screenOffset)
        {
            
            if (mouseMove.accelX < mouseMove.maxAccel)
            {
                mouseMove.accelX += mouseMove.accelOffset * Time.deltaTime;
            }
            
        }
        else
        {
            mouseMove.accelX = 0;
        }

        //Need to move camera to top
        if (currentCursorPos.y >= Screen.height - mouseMove.screenOffset)
        {
            if (mouseMove.accelZ < mouseMove.maxAccel)
            {
                mouseMove.accelZ += mouseMove.accelOffset * Time.deltaTime;
            }
        }

        //Need to move camera to bottom
        else if (currentCursorPos.y <= mouseMove.screenOffset)
        {
            if (mouseMove.accelZ > mouseMove.minAccel)
            {
                mouseMove.accelZ -= mouseMove.accelOffset * Time.deltaTime;
            }
        }

        else
        {
            mouseMove.accelZ = 0;
        }

        UpdatePosition();
        oldCursorPos = currentCursorPos;
    }

    private void UpdatePosition()
    {
        transform.position = new Vector3(transform.position.x + mouseMove.accelX, transform.position.y, transform.position.z + mouseMove.accelZ);
    }
}
