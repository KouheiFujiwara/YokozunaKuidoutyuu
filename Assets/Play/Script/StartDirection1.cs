// -------------------------------------------------------
//
// スタート演出スクリプトその1
//
// <概要>
// スタート演出オブジェクトその1を移動させる為のスクリプト
//
// Created by 高井康彬 on 2014.03.06
// -------------------------------------------------------

using UnityEngine;

public class StartDirection1 : MonoBehaviour {

    [SerializeField]
    private float fall_speed = 0.0f;    // 落ちる速度

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        // 地面に向かって落ちていく
        transform.Translate(0.0f, -fall_speed, 0.0f);

        // ある程度落ちたら解放
        const float HEIGHT = -10.0f;
        if (transform.position.y <= HEIGHT) Destroy(gameObject);
	}
}
