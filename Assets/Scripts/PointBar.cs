using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointBar : MonoBehaviour
{
    public Image Bar;
    public float pointMax = 100.0f;
    private float point = 0.0f;
    public PlayManager playManager;

    void Start()
    {
        Bar = GetComponent<Image>();
    }


    void Update()
    {
        point = playManager.point;
        float a = point / pointMax;
        //Debug.Log($"{point} point");
        Bar.fillAmount = point / pointMax;
    }
}
