// -------------------------------------------------------
//
// プレイヤーのスプライト関連スクリプト
//
// <概要>
// スプライト切り替えによるアニメーション等を行う
//
// Created by 高井康彬 on 2014.03.05
// -------------------------------------------------------

using UnityEngine;
using System.Collections;

public class PlayerSprite : MonoBehaviour {

    [SerializeField]
    private float cycle = 0.1f;             // アニメーションが切り替わるサイクル(秒数)
    private SpriteRenderer chara_sprite;    // スプライト切り替え用SpriteRenderer
    private int walkcycle = 0;              // 歩きアニメーションのインデックス
    [SerializeField]
    private Sprite[] walk;                  // 歩きアニメーションのスプライト
    private bool busy = false;

	// Use this for initialization
	void Start ()
    {
        chara_sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine("Anim");
	}

    IEnumerator Anim()
    {
        if (!busy)
        {
            busy = true;
            yield return new WaitForSeconds(cycle);         // 指定秒数分待機
            chara_sprite.sprite = walk[walkcycle];          // アニメーション切り替え
            walkcycle += 1;
            if (walkcycle >= walk.Length) walkcycle = 0;
            busy = false;
        }
    }
}
