using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Bar;
    public float healthMax = 100.0f;
    private float health = 0.0f;
    public PlayManager playManager;

    void Start()
    {
        Bar = GetComponent<Image>();
    }
    void Update()
    {
        health = playManager.HP;
        float a = health / healthMax;
        //Debug.Log($"{a} health");
        Bar.fillAmount = health / healthMax;
    }
}
