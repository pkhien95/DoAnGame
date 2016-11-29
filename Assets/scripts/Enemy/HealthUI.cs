﻿using UnityEngine;
using System.Collections;

public class HealthUI : MonoBehaviour {

    public int maxHealth = 100;
    public int curHealth = 100;

    public float healthBarLength;

    private Vector3 position;

    // Use this for initialization
    void Start()
    {
        healthBarLength = Screen.width / 6;
    }

    // Update is called once per frame
    void Update()
    {

        AddjustCurrentHealth(0);
    }

    void OnGUI()
    {
        position = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(position.x, Screen.height - position.y, healthBarLength, 20), curHealth + "/" + maxHealth);
        Debug.Log(position);
    }

    public void AddjustCurrentHealth(int adj)
    {
        curHealth += adj;

        if (curHealth < 0)
            curHealth = 0;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (maxHealth < 1)
            maxHealth = 1;

        healthBarLength = (Screen.width / 6) * (curHealth / (float)maxHealth);
    }
}