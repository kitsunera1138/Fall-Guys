using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float mouseX;

    //head
    [SerializeField] float mouseY;
    private void Awake()
    {
        if (speed == 0) speed = 200f;
    }

    //ī�޶� head �� �Ʒ�
    public void OnMouseX()
    {
        mouseY += Input.GetAxisRaw("Mouse Y") * speed * Time.deltaTime;
    }
    //ī�޶� head �� �Ʒ�
    public void RotateX(Transform transformPosition)
    {
        mouseY = Mathf.Clamp(mouseY, -65, 65);
        transformPosition.localEulerAngles = new Vector3(-mouseY, 0, 0);
        //transformPosition.localRotation = Quaternion.Euler(-mouseX, 0f, 0f);
    }

    //�÷��̾� �¿�
    public void OnMouseY()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * speed * Time.deltaTime;
    }

    public void RotateY(Transform transformPosition)
    {
        transformPosition.eulerAngles = new Vector3(0, mouseX, 0);
    }
}
