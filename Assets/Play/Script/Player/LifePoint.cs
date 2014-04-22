// -------------------------------------------------------
//
// ライフポイントを管理するクラス
//
// <概要>
// gameObjectのポイント,HPを比較する
//
// Created by 藤原康平 on 2014.03.06
// -------------------------------------------------------

using UnityEngine;
using System.Collections;

public class LifePoint : MonoBehaviour {

    [SerializeField]
    private int point;

    //  
    private SpriteRenderer spriteR;

	// Use this for initialization
	void Start () 
    {
        spriteR = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ( point <= GameObject.Find("Gage").GetComponent<LifeManager>().GetHpNum )
            spriteR.color = new Color(1f, 1f, 1f, 1f);
        else
            spriteR.color = new Color(1f, 1f, 1f, 0f);
	}
}
