using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    //방이름
    private Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
    //room 생성 부모 위치
    [SerializeField] Transform parentPosition;

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void OnCreateRoom()
    {
        //방 설정
        RoomOptions roomOptions = new RoomOptions();

        //방 인원 2 ~ 4명 들어올 수 있게 설정합니다.
        roomOptions.MaxPlayers = Random.Range(2, 5);

        //공개: 룸을 공개할 수 있도록 설정합니다.
        roomOptions.IsOpen = true;

        //활성화 : 룸을 보일 수 있도록 설정합니다.
        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom("Room", roomOptions);
    }

    //방 갱신 시 호출 
    //인원 수가 바뀌었을 때 //룸이 삭제되었을 때 //룸이 생성되었을 때
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab = null;

        foreach (RoomInfo roomInfo in roomList)
        {
            //룸이 삭제된 경우
            if (roomInfo.RemovedFromList == true)
            {
                //삭제된 룸 찾았다면
                //딕셔너리에서 찾아서 roomlistRoom 오브젝트 삭제, 딕셔너리 삭제
                dictionary.TryGetValue(roomInfo.Name, out prefab);

                Destroy(prefab);
                dictionary.Remove(roomInfo.Name);
            }
            else //룸의 정보가 변경되는 경우
            {
                //룸이 처음 생성되는 경우
                if(dictionary.ContainsKey(roomInfo.Name) == false)
                {
                    GameObject clone = Instantiate(Resources.Load<GameObject>("Room"), parentPosition);

                    clone.GetComponent<Information>().View(roomInfo.Name, roomInfo.PlayerCount, roomInfo.MaxPlayers);

                    dictionary.Add(roomInfo.Name, clone);
                }
                else //룸의 정보가 변경되는 경우
                {
                    dictionary.TryGetValue(roomInfo.Name, out prefab);
                    prefab.GetComponent<Information>().View(roomInfo.Name, roomInfo.PlayerCount, roomInfo.MaxPlayers);
                }

            }
        }
    }

}

