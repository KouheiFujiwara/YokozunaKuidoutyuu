// -------------------------------------------------------
//
// 食べ物・毒生成スクリプト
//
// <概要>
// 食べ物と毒の生成を行う
//
// Created by 高井康彬 on 2014.03.05
// -------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class FoodCreater : MonoBehaviour {

    [SerializeField]
    private GameObject[] foods;     // 食べ物オブジェクト配列
    [SerializeField]
    private GameObject[] poison;    // 毒オブジェクト配列

    [SerializeField]
    private float food_cycle = 1.0f;            // 食べ物生成サイクル(秒数)
    [SerializeField]
    private float poison_cycle = 3.5f;          // 毒生成サイクル(秒数)
    [SerializeField]
    private float food_appear_pos_z = -40.0f;   // 食べ物生成z座標
    [SerializeField]
    private float food_appear_width = 3.5f;     // 食べ物が生成される範囲(横幅)
    [SerializeField]
    private float food_appear_pos_y = 1.51f;    // 食べ物生成y座標
    private bool food_busy = false;             // 食べ物生成コルーチン制御用フラグ
    private bool poison_busy = false;           // 毒生成コルーチン制御用フラグ
    private int food_count = 0;                 // 食べ物オブジェクト配列インデックス
    private int poison_count = 0;               // 毒オブジェクト配列インデックス

    // 食べ物生成サイクルのプロパティ
    public float FoodCycle
    {
        get { return this.food_cycle; }
        set { this.food_cycle = value; }
    }

    // 毒生成サイクルのプロパティ
    public float PoisonCycle
    {
        get { return this.poison_cycle; }
        set { this.poison_cycle = value; }
    }

	// Use this for initialization
	void Start()
    {
	}
	
	// Update is called once per frame
	void Update()
    {
        // プレイシーケンスの場合のみ以下の処理を行う
        if (GetComponent<PlayManager>().Seq == PlayManager.SeqType.SEQ_PLAY)
        {
            StartCoroutine("CreateFood");
            StartCoroutine("CreatePoison");
        }
	}

    // 食べ物シャッフル関数
    void Shuffle()
    {
        foods = foods.OrderBy(i => Guid.NewGuid()).ToArray();
    }

    IEnumerator CreateFood()
    {
        if (!food_busy)
        {
            food_busy = true;
            yield return new WaitForSeconds(food_cycle);

            float pos_x = UnityEngine.Random.Range(-food_appear_width, food_appear_width);

            if (food_count >= foods.Length)
            {
                Shuffle();
                food_count = 0;
            }
            Instantiate(foods[food_count], new Vector3(pos_x, food_appear_pos_y, food_appear_pos_z), transform.rotation);
            ++food_count;

            food_busy = false;
        }
    }

    IEnumerator CreatePoison()
    {
        if (!poison_busy)
        {
            poison_busy = true;
            yield return new WaitForSeconds(poison_cycle);

            float pos_x = UnityEngine.Random.Range(-food_appear_width, food_appear_width);

            if (poison_count >= poison.Length) poison_count = 0;
            Instantiate(poison[poison_count], new Vector3(pos_x, food_appear_pos_y, food_appear_pos_z), transform.rotation);
            ++poison_count;

            poison_busy = false;
        }
    }
}
