using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //MonoBehaviourPun

[RequireComponent(typeof(Rotation))]
public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;
    [SerializeField] Rotation rotation;
    [SerializeField] Camera virtualCamera;
    [SerializeField] CharacterController characterController;

    private void Awake()
    {
        rotation = GetComponent<Rotation>();
        characterController = GetComponent<CharacterController>();
        if (speed == 0) speed = 5f;
    }
    void Start()
    {
        if (virtualCamera == null) virtualCamera = GetComponentInChildren<Camera>();
        DisableCamera();
    }

    private void Update()
    {
        //나 자신이 아니면 움직이지 못하도록함
        if (photonView.IsMine == false) return;


        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();

        Move();

        Rotate();
    }

    void Rotate()
    {
        rotation.OnMouseY();

        rotation.RotateY(transform);
    }

    public void Move()
    {
        //direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical") ).normalized;

        //if (direction != Vector3.zero)
        //characterController.Move(direction * speed * Time.deltaTime);

        //바라보는 좌표로 이동하는 방식으로 변경
        Vector3 modifiedTransform = transform.TransformDirection(direction * speed * Time.deltaTime);
        characterController.Move(modifiedTransform);

    }

    void DisableCamera()
    {
        //photonView.IsMine : 현재 객체가 나 자신이라면
        if (photonView.IsMine)
        {
            //동적 자신 객체 생성 시 메인 카메라를 꺼줌
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            //나 자신이 아닌 경우 - Remote 원격 객체인 경우
            //원격 객체의 카메라 비활성화
            virtualCamera.gameObject.SetActive(false);
        }
    }
}
