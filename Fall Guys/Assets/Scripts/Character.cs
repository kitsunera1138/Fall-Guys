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
