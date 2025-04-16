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
        //현재 방의 maxPlayer
        maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;
        for(int i =0; i< maxCount; i++)
        {
            spawnPos.Add(new Vector3(i+1,1.3f,0));
        }

        //Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber); //get; //나갔다 들어오면 중첩이 가능해서 
        //Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount); //CurrentRoom.PlayerCount 사용

        //PhotonNetwork.Instantiate 방에 들어와야 생성됨 - 로그인해서 등등
        GameObject clone = PhotonNetwork.Instantiate("Character", Vector3.zero, Quaternion.identity);
        clone.transform.position = spawnPos[PhotonNetwork.CurrentRoom.PlayerCount];
    }

}
