using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //MonoBehaviourPun

[RequireComponent(typeof(Rotation))]
[RequireComponent(typeof(Mouse))]

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
        //�� �ڽ��� �ƴϸ� �������� ���ϵ�����
        if (photonView.IsMine == false) return;


        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();

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

        //�ٶ󺸴� ��ǥ�� �̵��ϴ� ������� ����
        Vector3 modifiedTransform = transform.TransformDirection(direction * speed * Time.deltaTime);
        characterController.Move(modifiedTransform);

    }

    float gravity = -9.81f;
    bool isGravity = false;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Ground") == true)
        {
            Debug.Log("Ground");
            //�߷� ��Ȱ��ȭ �ڵ� �ʿ�
            isGravity = false;
            return;
        }

    }
    void OnGravity()
    {
        if (characterController.isGrounded == false)
        {
            Vector3 directions = transform.TransformDirection(0, Time.deltaTime * gravity, 0);
            characterController.Move(directions);
        }
    }

    void Jump()
    {
        OnGravity();
        //Vector3 direction;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("Jump");
        //    direction = transform.TransformDirection(0, transform.position.y + 10f * Time.deltaTime, 0);
        //    characterController.Move(direction);
        //}

        ////
        //if (characterController.isGrounded == false)
        //{
        //    Debug.Log("isGroundedfalse");
        //    direction = transform.TransformDirection(0,Time.deltaTime * gravity,0);
        //    characterController.Move(direction);
        //}
        //else
        //{
        //    Debug.Log("Ground");
        //    if(Input.GetKeyDown(KeyCode.Space)) {
        //        Debug.Log("Jump");
        //        direction = transform.TransformDirection(0,(transform.position.y + 5f) * Time.deltaTime,0);
        //        characterController.Move(direction);
        //    }
        //}

        //if(characterController.isGrounded == true)
        //{

        //}
    }

    void DisableCamera()
    {
        //photonView.IsMine : ���� ��ü�� �� �ڽ��̶��
        if (photonView.IsMine)
        {
            //���� �ڽ� ��ü ���� �� ���� ī�޶� ����
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            //�� �ڽ��� �ƴ� ��� - Remote ���� ��ü�� ���
            //���� ��ü�� ī�޶� ��Ȱ��ȭ
            virtualCamera.gameObject.SetActive(false);
        }
    }
}
