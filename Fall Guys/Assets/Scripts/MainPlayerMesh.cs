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
            // ÀÚ½ÅÀÇ ·»´õ·¯¸¸ ²ô±â
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
    }
}
