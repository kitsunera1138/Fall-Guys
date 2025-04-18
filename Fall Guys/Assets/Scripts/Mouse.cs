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
    public void SetMouse(bool state) //false ���(0) - - - - true Ȱ��ȭ(1)
    {
        Cursor.visible = state;
        //����ȯ
        Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);

        //���� ������
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
    }
}