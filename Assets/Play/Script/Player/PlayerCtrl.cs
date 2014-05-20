// -------------------------------------------------------
//
// プレイヤー操作スクリプト
//
// <概要>
// プレイヤーの操作を行う
//
// Created by 高井康彬 on 2014.03.05
// -------------------------------------------------------

using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    private PlayManager play_mgr;           // プレイマネージャー
    [SerializeField]
    private float speed = 0.0f;             // 左右の移動スピード
    private CharacterController controller; // キャラクターコントローラー

    // Use this for initialization
    void Start()
    {
        play_mgr = GameObject.Find("PlayManager").GetComponent<PlayManager>();  // プレイマネージャー取得
        controller = GetComponent<CharacterController>();                       // キャラクターコントローラー取得
    }

    // Update is called once per frame
    void Update()
    {
        // プレイシーケンスの場合のみ以下の処理を行う
        if (play_mgr.Seq == PlayManager.SeqType.SEQ_PLAY)
        {
            // 押下された←→キーに応じて移動する
            var move_dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, 0.0f);
            controller.SimpleMove(move_dir * speed * Time.deltaTime);
        }
    }
}
