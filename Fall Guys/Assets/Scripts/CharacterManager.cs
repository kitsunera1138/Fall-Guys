using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CharacterManager : MonoBehaviour
{
    [SerializeField] Transform spawnPostion;
    [SerializeField] List<Vector3> spawnPos;
    float maxCount;
    void Start()
    {
        //���� ���� maxPlayer
        maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;
        for(int i =0; i< maxCount; i++)
        {
            spawnPos.Add(new Vector3(i+1,1.3f,0));
        }

        //Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber); //get; //������ ������ ��ø�� �����ؼ� 
        //Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount); //CurrentRoom.PlayerCount ���

        //PhotonNetwork.Instantiate �濡 ���;� ������ - �α����ؼ� ���
        GameObject clone = PhotonNetwork.Instantiate("Character", Vector3.zero, Quaternion.identity);
        clone.transform.position = spawnPos[PhotonNetwork.CurrentRoom.PlayerCount];
    }

}
