// -------------------------------------------------------
//
// 食べ物・毒更新スクリプト
//
// <概要>
// 食べ物と毒の移動、プレイヤーに食べられた際の処理を行う
//
// Created by 高井康彬 on 2014.03.05
// -------------------------------------------------------

using UnityEngine;

public class UpdateFood : MonoBehaviour {

    private PlayManager play_mgr;                   // プレイマネージャー
    [SerializeField]
    private int kg = 0;                             // 増える体重
    [SerializeField]
    private float power = 0.0f;                     // 体力の変動値(正で回復、負でダメージ)
    [SerializeField]
    private float food_disappear_pos_z = 0.0f;      // 食べ物と毒が消滅するz座標

	// Use this for initialization
	void Start()
    {
        play_mgr = GameObject.Find("PlayManager").GetComponent<PlayManager>();  // プレイマネージャー取得
	}
	
	// Update is called once per frame
	void Update()
    {
        // 世界全体のスピードと同じ速度でz方向へ進んで行き、消滅座標を超えたら解放する
        transform.position += new Vector3(0.0f, 0.0f, play_mgr.WorldSpeed);
        if (transform.position.z >= food_disappear_pos_z)   Destroy(gameObject);
	}

    // 摂取関数
    // tips... プレイヤーが呼び出す
    void Eaten()
    {
        // プレイヤーを取得し、体力と体重の加算関数を呼び出した後、自身を解放する
        GameObject player_obj = GameObject.FindGameObjectWithTag("Player");
        player_obj.SendMessage("AddHP", power);
        player_obj.SendMessage("AddWeight", kg);
        Destroy(gameObject);
    }
}
