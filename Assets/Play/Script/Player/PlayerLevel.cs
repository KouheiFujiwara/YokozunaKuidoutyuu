// -------------------------------------------------------
//
// プレイヤーレベルスクリプト
//
// <概要>
// プレイヤーのレベルUP判定と、
// レベルに応じたステータスの変更等を行う
//
// Created by 高井康彬 on 2014.03.06
// -------------------------------------------------------

using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour {

    private int level = 0;      // レベル
    [SerializeField]
    private float[] level_scl;  // レベルに応じたスケール倍率
    [SerializeField]
    private int[] level_weight; // レベルUPに必要な体重
    private Vector3 first_scl;  // 初期の大きさ

	// Use this for initialization
	void Start()
    {
        first_scl = transform.localScale;   // 大きさの初期化
	}
	
	// Update is called once per frame
	void Update()
    {
	}

    // 体重チェック関数
    // tips... 体重に応じてレベルのUP・DOWNを行う
    void CheckWeight()
    {
        if ((level != level_scl.Length - 1) &&
            GetComponent<PlayerManager>().Weight >= level_weight[level]) LevelUP();
        if ((level != 0) &&
            (GetComponent(typeof(PlayerManager)) as PlayerManager).Weight < level_weight[level - 1]) LevelDown();
    }

    // レベルUP関数
    void LevelUP()
    {
        ++level;
        ChangeScl();
    }

    // レベルDOWN関数
    void LevelDown()
    {
        --level;
        ChangeScl();
    }

    // 大きさ変更関数
    void ChangeScl()
    {
        // プレイヤーのスケールをレベルに応じた倍率分変更
        transform.localScale = first_scl * level_scl[level];

        float x = 0.0f;
        float radius = GetComponent<CharacterController>().radius * level_scl[level];   // キャラクターコントローラーの半径をレベルに応じた倍率分掛けたもの
        float half_width = GameObject.Find("Floor").transform.localScale.x * 0.5f;      // 床の半分の長さ

        // スケールを変更した後の当たり判定が壁にめり込むとすり抜けてしまうので、補正を行う
        if (transform.position.x - (radius * first_scl.x) <= -half_width + 0.5f) x = radius;
        else if (transform.position.x + (radius * first_scl.x) >= half_width - 0.5f) x = -radius;
        
        // プレイヤーを移動
        transform.Translate(x, radius, 0.0f);

        // プレイヤーマネージャーの当たり判定距離変更関数呼び出し
        GetComponent<PlayerManager>().SendMessage("ChangeRange", level_scl[level]);
    }
}
