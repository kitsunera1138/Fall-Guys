using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Parallax Scrolling
public class Parallax : MonoBehaviour
{
    //�������� �����̴� �̹����� ��� rawImage�� �̴ϸ�, Parallax scroll
    [SerializeField] RawImage rawImage;
    [SerializeField] Rect rect;
    [SerializeField] float speed;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }
    void Start()
    {
        rect = rawImage.uvRect;
        if (speed == 0) speed = 0.025f;
    }

    void Update()
    {
        rect.x += speed * Time.deltaTime;
        rawImage.uvRect = rect;
    }
}
