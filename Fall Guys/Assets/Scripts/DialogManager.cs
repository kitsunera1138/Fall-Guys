using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputField;
    [SerializeField] Transform parentTransform; //content
    [SerializeField] ScrollRect scrollRect;


    private void Update()
    {
        //Enter를 눌렀는가
        if (Input.GetKeyDown(KeyCode.Return)) //Enter 키
        {
            //inputField.ActivateInputField(): inputField를 활성화 시킵니다.
            inputField.ActivateInputField();

            //채팅을 입력 안한상태에서 Enter누르면 return
            if (inputField.text.Length <= 0) { return; } //"" 출력방지 //막 엔터치면 빈 글짜 계속 설치가능한거 방지

            //채팅을 입력해야 채팅창에 출력
            string talk = PhotonNetwork.LocalPlayer.NickName + " : " + inputField.text;

            //첫 번째 매개변수 : 호출할 함수의 이름,
            //두 번째 매개변수: 현재 룸에 있는 클라이언트에게 호출할 대상
            //세 번째 매개변수 : 호출할 함수의 매개 변수]

            //Rpc Target.All : 현재 룸에 있는 모든 클라이언트에게
            //Talk() 함수를 실행하라는 명령을 전달합니다.
            photonView.RPC("Talk", RpcTarget.All, talk);

            //inputField의 텍스트를 초기화합니다.
            inputField.text = "";

            //채팅을 입력한 후에도 이어서 입력을 할 수 있도록 설정합니다.
            inputField.ActivateInputField();
        }
    }

    //[PunRPC]  :  photonView.RPC 사용
    [PunRPC]
    void Talk(string message) //객체가 입력한 채팅 설치 기능만 가능하도록 하게함 (초기화X - RPC라 전체적으로 영향 받아서)
    {
        //prefab을 하나 생성한 다음 text에 값을 설정합니다.
        GameObject talk = GameObject.Instantiate(Resources.Load<GameObject>("Talk"));

        //prefab 오브젝트의 Text 컴포넌트로 접근해서 text의 값을 설정합니다.
        talk.GetComponent<Text>().text = message;

        //스크롤 뷰 - content 오브젝트의 자식으로 등록합니다.
        talk.transform.SetParent(parentTransform);


        //Canvas를 수동으로 동기화 시킵니다.
        Canvas.ForceUpdateCanvases(); // 즉시 레이아웃 갱신

        //스크롤의 위치를 초기화 합니다.
        scrollRect.verticalNormalizedPosition = 0.0f;

        //inputField의 텍스트를 초기화합니다.
        //이거 밖으로 뺌 //RPC 쓰니 다른사람 채팅치면 내 InputFieldtext 지워지기때문에
        //inputField.text = "";
    }

    //// 새 채팅 메시지가 추가될 때 호출
    //public void ScrollToBottom()
    //{
    //    //Canvas.ForceUpdateCanvases(); // 즉시 레이아웃 갱신
    //    scrollRect.verticalNormalizedPosition = 0f; // 가장 아래로
    //}
}
