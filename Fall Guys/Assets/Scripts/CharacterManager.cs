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
        //Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber); //get; //������ ������ ��ø�� �����ؼ� 
        //Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount); //CurrentRoom.PlayerCount ��� //1���� ���۵�

        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1; //1���� �����ؼ� -1

        //PhotonNetwork.Instantiate �濡 ���;� ������ - �α����ؼ� ��� 
        //PhotonNetwork.Instantiate ( string Resources ���� �� ������Ʈ �̸�,
        //Quaternion.identity ȸ�� X (ȸ���� ����)
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
