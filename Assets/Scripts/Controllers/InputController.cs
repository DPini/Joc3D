using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    enum inputSettings { MouseOnly, KeyBoardOnly, MouseAndKeyboard };
    inputSettings inputSetting = inputSettings.MouseAndKeyboard;
    KeyCode mouseKeyPressed;
    float accumX;
    float accumY;

	

    public bool checkInput(KeyCode key)
    {
        if ( ( inputSetting == inputSettings.KeyBoardOnly ||
            inputSetting == inputSettings.MouseAndKeyboard ) &&
            Input.GetKey(key)) return true;

        if ((inputSetting == inputSettings.MouseOnly ||
            inputSetting == inputSettings.MouseAndKeyboard) &&
            mouseKeyPressed == key )
        {
            mouseKeyPressed = KeyCode.None;
            return true;
        }

        return false;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            accumX = 0;
            accumY = 0;
        }
        if (Input.GetMouseButton(0))
        {
            accumX += Input.GetAxis("Mouse X");
            accumY += Input.GetAxis("Mouse Y");
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (accumX != 0 || accumY != 0)
            {
                // Horizontal movement
                if (Mathf.Abs(accumX) > Mathf.Abs(accumY))
                {
                    if (accumX < 0)
                    {
                        // Left
                        mouseKeyPressed = KeyCode.LeftArrow;
                    }
                    else
                    {
                        // Right
                        mouseKeyPressed = KeyCode.RightArrow;
                    }
                }
                //Vertical movement
                else
                {
                    if (accumY < 0)
                    {
                        // Down
                        mouseKeyPressed = KeyCode.DownArrow;
                    }
                    else
                    {
                        // Up
                        mouseKeyPressed = KeyCode.UpArrow;
                    }
                }

            }
        }
    }

}
