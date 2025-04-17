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
        //�� �ڽ��� �ƴϸ� �������� ���ϵ�����
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

        //�ٶ󺸴� ��ǥ�� �̵��ϴ� ������� ����
        Vector3 modifiedTransform = transform.TransformDirection(direction * speed * Time.deltaTime);
        characterController.Move(modifiedTransform);

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
