// -------------------------------------------------------
//
// UIのライフマネージャー
//
// <概要>
// UIで使うライフを管理
//
// Created by 藤原康平 on 2014.03.06
// -------------------------------------------------------

using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

    private int HpPointNum;
    private int Hp;

    public int GetHpNum
    {
        get { return HpPointNum; }
    }

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        //  プレイヤーマネージャーのHpを受け取る
        Hp = (int)GameObject.Find("Player").GetComponent<PlayerManager>().Hp;
        //  ポイント番号に割り振る
        HpPointNum = Hp / 9;
	}
}
