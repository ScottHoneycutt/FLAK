using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPBar : MonoBehaviour {

    //Public variables----
    public GameObject uiHealth;
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        //Healthbar matches health----
        RectTransform healthRectTransform = uiHealth.GetComponent<RectTransform>();
        UnitHP hpAmount = player.GetComponent<UnitHP>();

        float maxHealth = (float)hpAmount.maximumHealth;
        float currentHP = (float)hpAmount.health;
        float slope = 290 / maxHealth;
        float xPosition = slope * currentHP;
        xPosition = xPosition - 290;

        healthRectTransform.anchoredPosition = new Vector2((xPosition), 0);
    }
}
