using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private void Awake()
    {
        SetMouse(false);
    }
    public void SetMouse(bool state) //false 잠금(0) - - - - true 활성화(1)
    {
        Cursor.visible = state;
        //형변환
        Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);

        //삼항 연산자
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
    }
}