using UnityEngine;
using System.Collections;

// -------------------------------------------------------
//
// 敵演出用スクリプト
//
// <概要>
// 敵が退場していくときの演出用
//
// Created by 前山　直澄 on 2014.05.20
// -------------------------------------------------------

public class HangerFade : MonoBehaviour {
    // 敵の動かすためのオブジェクト
    [SerializeField]
    private GameObject hanger = null;

    // 敵が退場し始めてから破棄されるまでの時間
    [SerializeField]
    private float destroyWaitTime = 1;

    // 敵が退場するスピード
    [SerializeField]
    private float fadeSpeed = 0.1f;

    void Start()
    {
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        StartCoroutine(DestoryHanger());

        while (true)
        {
            hanger.transform.position += new Vector3(fadeSpeed, 0, 0);
            yield return 0;
        }
    }

    IEnumerator DestoryHanger()
    {
        yield return new WaitForSeconds(destroyWaitTime);

        Destroy(hanger);
        Destroy(gameObject);
    }
}
