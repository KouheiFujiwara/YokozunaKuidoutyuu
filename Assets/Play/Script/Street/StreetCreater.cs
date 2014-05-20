// -------------------------------------------------------
//
// 道生成スクリプト
//
// <概要>
// 道の生成を行う
//
// Created by 高井康彬 on 2014.03.05
// -------------------------------------------------------

using System;
using System.Linq;
using UnityEngine;

public class StreetCreater : MonoBehaviour {

    [SerializeField]
    private GameObject[] streets;               // 道オブジェクト配列
    [SerializeField]
    private float scl = 0.0f;                   // 道オブジェクトのスケール
    [SerializeField]
    private float street_appear_pos_z = 0.0f;   // 道が出現するz座標
    private int count = 0;                      // 道生成用カウント

	// Use this for initialization
	void Start()
    {
        // 最初に道の順番をシャッフル
        Shuffle();

        // 予め配列内の全ての道のプレハブを所定の位置に生成しておく
        for (int i = 0; i < streets.Length; ++i)
        {
            Instantiate(streets[i], new Vector3(0.0f, 0.0f, street_appear_pos_z + scl * (i + 1)), transform.rotation);
        }
        Instantiate(streets[0], new Vector3(0.0f, 0.0f, street_appear_pos_z + scl * (streets.Length + 1)), transform.rotation);

        // 次の生成に備えシャッフルしておく
        Shuffle();
	}

    // Update is called once per frame
    void Update()
    {
	}

    // 道順シャッフル関数
    private void Shuffle()
    {
        streets = streets.OrderBy(i => Guid.NewGuid()).ToArray();
    }

    // 道生成関数
    void CreateStreet()
    {
        // カウントが配列の要素数を超えたら配列内をシャッフルしてカウントを0に戻す
        if (count >= streets.Length)
        {
            Shuffle();
            count = 0;
        }

        // 配列内の道オブジェクトを順に生成していく
        Instantiate(streets[count], new Vector3(0.0f, 0.0f, street_appear_pos_z), transform.rotation);
        ++count;
    }
}
