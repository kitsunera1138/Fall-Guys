using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MasterManager : MonoBehaviourPunCallbacks
{
    //캐싱
    [SerializeField] WaitForSeconds waitForSeconds = new WaitForSeconds(5f);
    [SerializeField] GameObject platform;
    [SerializeField] Vector3 direction = new Vector3(-12.5f, 0, -27.5f);

    void Start()
    {
        if (direction == Vector3.zero) direction = new Vector3(-12.5f, 0, -27.5f);

        if (PhotonNetwork.IsMasterClient == true)
        {
            // PhotonNetwork.Instantiate(Resources 폴더 안 오브젝트 이름, pos, rotation);
            platform = PhotonNetwork.InstantiateRoomObject("Platform", direction, Quaternion.identity);
            //InstantiateRoomObject 마스터가 나가도 오브젝트 파괴되지 않음

            StartCoroutine(Activate());
        }
    }

    IEnumerator Activate()
    {
        while (true)
        {
            yield return waitForSeconds;

            if (PhotonNetwork.CurrentRoom != null)
            {
                yield break;
            }

            if (platform.activeSelf)
            {
                platform.SetActive(false);
            }
            else
            {
                platform.SetActive(true);
            }
        }
    }

    //마스터 클라이언트가 나갔을 경우
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //나가면 알아서 다음 사람이 0번째 가기 때문에
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
        if (platform == null) 
        { platform = GameObject.Find("Platform(Clone)"); }

        //Master바꾼후 다시 코루틴 호출
        StartCoroutine(Activate());
    }
}
