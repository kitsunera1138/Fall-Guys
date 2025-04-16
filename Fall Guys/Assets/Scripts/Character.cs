using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //MonoBehaviourPun

public class Character : MonoBehaviourPun
{
    [SerializeField] Camera virtualCamera;

    void Start()
    {
        if(virtualCamera == null) virtualCamera = GetComponentInChildren<Camera>();
        DisableCamera();
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
