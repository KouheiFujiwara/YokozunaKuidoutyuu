// -------------------------------------------------------
//
// 道更新スクリプト
//
// <概要>
// 道の移動を行う
// また、解放する際に新しい道の生成命令を行う
//
// Created by 高井康彬 on 2014.03.05
// -------------------------------------------------------

using UnityEngine;

public class UpdateStreet : MonoBehaviour {

    private PlayManager play_mgr;                  // プレイマネージャー
    [SerializeField]
    private float street_disappear_pos_z = 0.0f;   // 道が消滅するz座標

	// Use this for initialization
	void Start ()
    {
        play_mgr = GameObject.Find("PlayManager").GetComponent<PlayManager>();  // プレイマネージャー取得
	}
	
	// Update is called once per frame
	void Update ()
    {
        // 世界全体のスピードと同じ速度でz方向へ進んで行き、消滅座標を超えたら解放する
        transform.position += new Vector3(0.0f, 0.0f, play_mgr.WorldSpeed);
        if (transform.position.z >= street_disappear_pos_z)
        {
            // 解放する際に新しい道オブジェクトを生成する
            play_mgr.SendMessage("CreateStreet");
            Destroy(gameObject);
        }
	}
}
