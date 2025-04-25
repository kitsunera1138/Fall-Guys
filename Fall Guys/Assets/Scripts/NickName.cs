using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class NickName : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputField;
    [SerializeField] Button submitButton;

    private void Start()
    {
        //inputfield 이벤트 코드에서 넣기
        inputField.onValueChanged.AddListener(Confirm);
    }
    public void Confirm(string text)
    {
        text = inputField.text;
        //inputField.text에 입력값이 있는 경우 버튼 활성화
        //비어 있으면 true, 비어 있지않으면 false라서 !반대
        submitButton.interactable = !string.IsNullOrEmpty(text);
    }

    //닉네임 저장
    public void SetName()
    {
        //if (!PlayerPrefs.HasKey("Name"))
        //{
        //    if (text != "")
        //        PlayerPrefs.SetString("Name", text);
        //    return;
        //}

        PlayerPrefs.SetString("Name", inputField.text);
        PhotonNetwork.NickName = PlayerPrefs.GetString("Name");

        gameObject.SetActive(false);
        Debug.Log("PhotonNetwork.NickName :" + PhotonNetwork.NickName);
    }

}
