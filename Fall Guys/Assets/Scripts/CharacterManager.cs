using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] List<Transform> transformList = new List<Transform>();

    private void Awake()
    {
        CreateTransform();
    }
    void Start()
    {
        //Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber); //get; //나갔다 들어오면 중첩이 가능해서 
        //Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount); //CurrentRoom.PlayerCount 사용 //1부터 시작됨

        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1; //1부터 시작해서 -1

        //PhotonNetwork.Instantiate 방에 들어와야 생성됨 - 로그인해서 등등 
        //PhotonNetwork.Instantiate ( string Resources 폴더 안 오브젝트 이름,
        //Quaternion.identity 회전 X (회전값 없음)
        GameObject clone = PhotonNetwork.Instantiate("Character", transformList[index].position, Quaternion.identity);
    }

    void CreateTransform()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            Transform clone = Instantiate(Resources.Load<Transform>("Create Position " + i));
            transformList.Add(clone);
        }
    }
}
