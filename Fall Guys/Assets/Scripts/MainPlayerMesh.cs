using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MainPlayerMesh : MonoBehaviourPun
{
    void Start()
    {
        if (photonView.IsMine)
        {
            // �ڽ��� �������� ����
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
    }
}
