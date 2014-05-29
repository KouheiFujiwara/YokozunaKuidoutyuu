using UnityEngine;
using System.Collections;

// -------------------------------------------------------
//
// 画面揺らし演出
//
// <概要>
// プレイヤーが画面にぶつかったときに画面を揺らすためのやつ
//
// Created by 前山　直澄 on 2014.05.13
// -------------------------------------------------------

public class Shake : MonoBehaviour {

    // 画面を揺らす時間
    [SerializeField]
    private float shakeSecond = 0;

    // 画面を揺らす時にXの揺らし比率
    [SerializeField]
    private float shakeX = 0;

    // 画面を揺らす時のYの揺らし比率
    [SerializeField]
    private float shakeY = 0;

    // 画面揺らしのときに揺らすオブジェクト
    [SerializeField]
    private GameObject ShakeObject = null;

    // 揺らすための計算式
    private float upper=Mathf.PI / 2f;

    // 揺れる速度
    [SerializeField]
    private float add = 0.1f;

    // どれだけ揺れるか
    [SerializeField]
    private float shakePower = 10;

    void Start()
    {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        StartCoroutine(StopShake());

        while(true)
        {

            Vector3 vec = Vector3.Normalize(new Vector3(shakeX, shakeY, 0)) * shakePower * Mathf.Sin(upper);
            ShakeObject.transform.position += vec;

            upper += add;

            yield return 0;
        }
    }

    IEnumerator StopShake()
    {
        yield return new WaitForSeconds(shakeSecond);

        ShakeObject.transform.parent = null;

        Destroy(gameObject);

        yield return 0;
    }
}
