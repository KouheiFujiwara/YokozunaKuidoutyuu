using UnityEngine;
using System.Collections;

// -------------------------------------------------------
//
// プレイヤー演出用スクリプトその2
//
// <概要>
// プレイヤーが吹き飛んでいくときのやつ
//
// Created by 前山　直澄 on 2014.05.13
// -------------------------------------------------------

public class FlyAway : MonoBehaviour {

    // 吹き飛ぶときにどの程度真上方向から左右にずれるか
    [SerializeField]
    private float upGap = 0;

    // 吹き飛ぶスピード
    [SerializeField]
    private float speed = 0;

    // 吹き飛んだ後何秒後に手前に落ちてくるか
    [SerializeField]
    private float waitZoom = 0;

    // 手前に落ちるオブジェクト
    [SerializeField]
    private GameObject DownPlayer = null;

    // 回転速度
    [SerializeField]
    private float rotSpeed = 0;

    // 吹き飛ぶ方向
    private Vector3 vec;

    void Start()
    {
        StartCoroutine(DeadEnd());
    }

    IEnumerator DeadEnd()
    {
        vec = Vector3.Normalize(new Vector3(upGap, 1, 0)) * speed;

        StartCoroutine(Fly());
        yield return new WaitForSeconds(waitZoom);

        DownPlayer.SetActive(true);
        Destroy(gameObject);
    }

    IEnumerator Fly()
    {
        while (true)
        {
            transform.Rotate(0, 0, rotSpeed);
            transform.position += vec;
            yield return 0;
        }
    }
}