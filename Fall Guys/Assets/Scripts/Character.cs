using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;  //MonoBehaviourPun
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rotation))]
[RequireComponent(typeof(Mouse))]

public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] float power; //점프
    [SerializeField] float gravity = 9.81f; //점프

    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 inputDirection; //점프

    [SerializeField] Rotation rotation;
    [SerializeField] Camera virtualCamera;
    [SerializeField] CharacterController characterController;

    [SerializeField] Vector3 initializeDirection;  //초기위치 저장

    private void Awake()
    {
        rotation = GetComponent<Rotation>();
        characterController = GetComponent<CharacterController>();
        if (speed == 0) speed = 5f;
    }
    void Start()
    {
        initializeDirection = transform.position;

        if (virtualCamera == null) virtualCamera = GetComponentInChildren<Camera>();
        DisableCamera();
    }
    public void InitializePosition()
    {
        characterController.enabled = false;
        transform.position = initializeDirection;

        characterController.enabled = true;
    }

    private void Update()
    {
        //나 자신이 아니면 움직이지 못하도록함
        if (photonView.IsMine == false) return;

        if (EventSystem.current.currentSelectedGameObject != null && 
            EventSystem.current.currentSelectedGameObject.GetComponent<UnityEngine.UI.InputField>() != null)
        {
            return;
        }

        Control();

        Move();

        Rotate();

        Jump();

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
        //Vector3 modifiedTransform = transform.TransformDirection(direction * speed * Time.deltaTime);
        //characterController.Move(modifiedTransform);

        //점프+ 수정
        Vector3 modifiedTransform = new Vector3(inputDirection.x,direction.y,inputDirection.z);
        characterController.Move(modifiedTransform * speed * Time.deltaTime);

        direction.y = modifiedTransform.y;
    }

    void Control()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.z = Input.GetAxisRaw("Vertical");

        inputDirection.Normalize();

        inputDirection = characterController.transform.TransformDirection(inputDirection);
    }

    void Jump()
    {
        if (characterController.isGrounded)
        {
            direction.y = -1.0f;

            if (Input.GetButtonDown("Jump"))
            {
                direction.y = power;
            }
        }
        else
        {
            direction.y -=Time.deltaTime * gravity;
        }
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


    //ㅁㅁtrash

    //bool isGravity = false;
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.CompareTag("Ground") == true)
    //    {
    //        Debug.Log("Ground");
    //        //중력 비활성화 코드 필요
    //        isGravity = true;
    //        return;
    //    }

    //}

    //void OnGravity()
    //{
    //    if (!isGravity)//&& characterController.isGrounded == false
    //    {
    //        Vector3 directions = transform.TransformDirection(0, Time.deltaTime * gravity, 0);
    //        characterController.Move(directions);
    //    }
    //    else
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space) && isGravity)
    //        {
    //            isGravity = false;
    //            //Vector3 directions = transform.TransformDirection(0,  5f, 0);
    //            //characterController.Move(directions);
    //        }

    //    }
    //}

}
