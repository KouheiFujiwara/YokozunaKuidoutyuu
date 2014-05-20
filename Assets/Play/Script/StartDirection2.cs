// -------------------------------------------------------
//
// スタート演出スクリプトその2
//
// <概要>
// スタート演出オブジェクトその2を移動させる為のスクリプト
//
// Created by 高井康彬 on 2014.03.06
// -------------------------------------------------------

using UnityEngine;

public class StartDirection2 : MonoBehaviour {

    [SerializeField]
    private float raise_speed = 0.0f;   // 上昇速度
    [SerializeField]
    private float wait_height = 0.0f;   // 待機する高さ
    [SerializeField]
    private float wait_time = 0.0f;     // 待機する秒数
    private float time = 0.0f;          // 経過時間

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update()
    {
        // 待機する高さまで上昇したらWait関数が呼ばれ、上昇を止めてtimeをカウントし始める
        // 待機する秒数分カウントが終わったらtrueが返ってくるので、上昇を再開する
        if (transform.position.y >= wait_height && !Wait())
        {
            Debug.Log("wait");
        }
        else transform.Translate(0.0f, raise_speed, 0.0f);

        // ある程度の高さ分上昇したら解放
        const float HEIGHT = 8.0f;
        if (transform.position.y >= HEIGHT) Destroy(gameObject);
	}

    // 待機関数
    // ret.... [ false : 秒数カウント中 ] [ true : 秒数カウント終了 ]
    private bool Wait()
    {
        time += Time.deltaTime;
        if (time >= wait_time) return true;
        else return false;
    }
}
