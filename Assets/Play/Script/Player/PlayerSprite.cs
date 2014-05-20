// -------------------------------------------------------
//
// プレイヤーのスプライト関連スクリプト
//
// <概要>
// スプライト切り替えによるアニメーション等を行う
//
// Created by 高井康彬 on 2014.03.05
// -------------------------------------------------------

using System.Collections;
using UnityEngine;

public class PlayerSprite : MonoBehaviour {

    [SerializeField]
    private float cycle = 0.0f;             // アニメーションが切り替わるサイクル(秒数)
    private SpriteRenderer chara_sprite;    // スプライト切り替え用SpriteRenderer
    private int walkcycle = 0;              // 歩きアニメーションのインデックス
    [SerializeField]
    private Sprite[] walk;                  // 歩きアニメーションのスプライト
    [SerializeField]
    private Sprite[] fever_walk;            // フィーバー中の歩きアニメーションのスプライト
    private bool busy = false;              // アニメーションコルーチン制御用フラグ

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

    // アニメーション関数
    private IEnumerator Anim()
    {
        if (!busy)
        {
            busy = true;

            // 指定秒数分待機
            yield return new WaitForSeconds(cycle);

            // アニメーション切り替え
            if (GetComponent<PlayerManager>().IsFever)  chara_sprite.sprite = fever_walk[walkcycle];
            else                                        chara_sprite.sprite = walk[walkcycle];

            walkcycle += 1;
            if (walkcycle >= walk.Length) walkcycle = 0;
            busy = false;
        }
    }
}
