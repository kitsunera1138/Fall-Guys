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
        //클래스 생성
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInputField.text,
            Password = passwordInputField.text,
        };

        //PlayFabClientAPI.LoginWithEmailAddress(request,Action 이벤트 함수의 주소를 담아서 등록 - 로그인 성공 시 호출되는 함수, Action 로그인 실패 시 호출되는 함수)
        PlayFabClientAPI.LoginWithEmailAddress(request, Sucess, Fail);
    }



    //로그인 성공 시 호출되는 함수
    public void Sucess(LoginResult loginResult)
    {
        //AutomaticallySyncScene
        //리슨 서버일 경우 false로 설정 시 master가 씬을 넘어가도 다른 remote들은 따라가지 않음
        //true일 시 master가 씬 넘어가면 다 같이 넘어감
        PhotonNetwork.AutomaticallySyncScene = false;

        //버전이 같아야 같은 네트워크에서 만날 수 있음
        PhotonNetwork.GameVersion = gameVision;

        PhotonNetwork.ConnectUsingSettings();
    }

    //로그인 실패 시 호출되는 함수
    public void Fail(PlayFabError playFabError)
    {
        //발생한 에러 호출
        Debug.Log(playFabError.GenerateErrorReport());
    }

    //callBack함수들 override
    public override void OnConnectedToMaster() //override
    {
        // JoinLobby() : 특정 로비를 생성하여 진입하는 함수
        PhotonNetwork.JoinLobby();
    }

    //로비 접속 시 //Scene을 로비로 이동
    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

}
