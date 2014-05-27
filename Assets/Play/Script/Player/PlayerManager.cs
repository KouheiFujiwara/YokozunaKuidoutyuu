// -------------------------------------------------------
//
// プレイヤー関連マネージャースクリプト
//
// <概要>
// プレイヤーのステータスを保持し、ステータスの計算や、
// 食べ物・毒との当たり判定等を行う
//
// Created by 高井康彬 on 2014.03.06
// -------------------------------------------------------

using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    private PlayManager play_mgr;           // プレイマネージャースクリプト
    private float hit_range;                // 当たり判定距離
    [SerializeField]
    private float first_hit_range = 1.0f;   // 初期の当たり判定距離
    [SerializeField]
    private float max_hp = 100.0f;          // 体力の最大値
    [SerializeField]
    private float hp = 100.0f;              // 現在の体力
    [SerializeField]
    private float hp_fall = 0.1f;           // 体力の自然下降値
    [SerializeField]
    private int min_weight = 50;            // 体重の最低値
    [SerializeField]
    private int max_weight = 10000000;      // 体重の最大値
    [SerializeField]
    private int weight = 50;                // 現在の体重
    [SerializeField]
    private float mileage = 0.0f;           // 走行距離

    // 体力のプロパティ
    public float Hp
    {
        get { return this.hp; }
    }
    // 体重のプロパティ
    public int Weight
    {
        get { return this.weight; }
    }
    // 走行距離のプロパティ
    public float Mileage
    {
        get { return this.mileage; }
    }

	// Use this for initialization
	void Start()
    {
        play_mgr = GameObject.Find("PlayManager").GetComponent<PlayManager>();  // プレイマネージャースクリプト取得
        hit_range = first_hit_range;                                            // 当たり判定距離初期化
	}
	
	// Update is called once per frame
	void Update()
    {
        // プレイシーケンスの場合のみ以下の処理を行う
        if (play_mgr.Seq == PlayManager.SeqType.SEQ_PLAY)
        {
            // 体力が0以下ならプレイマネージャーのゲームオーバー関数を呼び、
            // そうでなければ体力を自然下降値分減らす
            if (hp <= 0)
            {
                hp = 0;
                play_mgr.SendMessage("GameOver");
            }
            else hp -= hp_fall;

            // プレイヤーと食べ物・毒との当たり判定関数呼び出し
            Eat();

            // 走行距離に世界全体のスピードを加算する
            mileage += play_mgr.world_speed;
        }
	}

    // プレイヤーと食べ物・毒との当たり判定関数
    private void Eat()
    {
        // 食べ物をタグで検索
        GameObject[] food_obj = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject go in food_obj)
        {
            // プレイヤーと食べ物の距離を計算
            float distance = Vector3.Distance(go.transform.position, transform.position);
            if (hit_range > distance)
            {
                PlaySE();                   // SE再生
                go.SendMessage("Eaten");    // 食べ物側の解放関数呼び出し
            }
        }

        // 毒をタグで検索
        GameObject[] poison_obj = GameObject.FindGameObjectsWithTag("Poison");
        foreach (GameObject go in poison_obj)
        {
            float distance = Vector3.Distance(go.transform.position, transform.position);
            if (hit_range > distance)
            {
                PlaySE();
                go.SendMessage("Eaten");
            }
        }
        
        // プレイヤーレベルスクリプトの体重チェック関数呼び出し
        GetComponent<PlayerLevel>().SendMessage("CheckWeight");
    }

    // 体重変更関数
    // arg1... 変更値
    void AddWeight(int _kg)
    {
        weight += _kg;
        if (weight > max_weight) weight = max_weight;
        if (weight < min_weight) weight = min_weight;
    }

    // 体力変更関数
    // arg1... 変更値
    void AddHP(float _power)
    {
        hp += _power;
        if (hp > max_hp) hp = max_hp;
        if (hp <= 0) hp = 0;
    }

    // 当たり判定距離変更関数
    // arg1... 変更値
    void ChangeRange(float _scl)
    {
        hit_range = first_hit_range * _scl;
    }

    // SE再生関数
    private void PlaySE()
    {
        GetComponent<AudioSource>().Play();
    }
}
