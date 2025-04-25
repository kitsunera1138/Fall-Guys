using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject namePanel;


    void Start()
    {
        ////이름 비어 있지 않으면 (닉네임 패널 안키도록)
        //if (string.IsNullOrEmpty(PlayerPrefs.GetString("Name")) == true) return;

        PhotonNetwork.NickName = PlayerPrefs.GetString("Name");

        //PhotonNetwork.NickName 닉네임이 비어있다면 ""
        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        { //닉네임 없는 경우 닉네임 패널을 화면에 띄어준다.
            namePanel.SetActive(true);
        }
        else
        {
            namePanel.SetActive(false);
        }

        Debug.Log("PhotonNetwork.NickName :" + PhotonNetwork.NickName);
    }

}
