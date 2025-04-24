using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.XR;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] double time;
    [SerializeField] double initializeTime;

    private void Start()
    {
        initializeTime = PhotonNetwork.Time;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Entered"); 

        if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Debug.Log("Game Start");
        }
    }

    private void Update()
    {
        time = PhotonNetwork.Time - initializeTime;
        //Debug.Log("time" + time);

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Exit();
        }
    }

    //게임 씬에서 나감
    public void Exit()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //로비 목록 띄움
        PhotonNetwork.JoinLobby();

        //게임 씬에서 로비씬으로
        PhotonNetwork.LoadLevel("Lobby");
    }
}
