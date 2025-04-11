using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab.ClientModels;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField emailInputField;
    [SerializeField] InputField passwordInputField;

    [SerializeField] string gameVision = "1.0f";

    public void Login()
    {
        //Ŭ���� ����
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInputField.text,
            Password = passwordInputField.text,
        };

        //PlayFabClientAPI.LoginWithEmailAddress(request,Action �̺�Ʈ �Լ��� �ּҸ� ��Ƽ� ��� - �α��� ���� �� ȣ��Ǵ� �Լ�, Action �α��� ���� �� ȣ��Ǵ� �Լ�)
        PlayFabClientAPI.LoginWithEmailAddress(request, Sucess, Fail);
    }



    //�α��� ���� �� ȣ��Ǵ� �Լ�
    public void Sucess(LoginResult loginResult)
    {
        //AutomaticallySyncScene
        //���� ������ ��� false�� ���� �� master�� ���� �Ѿ�� �ٸ� remote���� ������ ����
        //true�� �� master�� �� �Ѿ�� �� ���� �Ѿ
        PhotonNetwork.AutomaticallySyncScene = false;

        //������ ���ƾ� ���� ��Ʈ��ũ���� ���� �� ����
        PhotonNetwork.GameVersion = gameVision;

        PhotonNetwork.ConnectUsingSettings();
    }

    //�α��� ���� �� ȣ��Ǵ� �Լ�
    public void Fail(PlayFabError playFabError)
    {
        //�߻��� ���� ȣ��
        Debug.Log(playFabError.GenerateErrorReport());
    }

    //callBack�Լ��� override
    public override void OnConnectedToMaster() //override
    {
        // JoinLobby() : Ư�� �κ� �����Ͽ� �����ϴ� �Լ�
        PhotonNetwork.JoinLobby();
    }

    //�κ� ���� �� //Scene�� �κ�� �̵�
    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

}
