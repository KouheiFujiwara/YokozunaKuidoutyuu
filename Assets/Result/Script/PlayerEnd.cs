using UnityEngine;
using System.Collections;

// -------------------------------------------------------
//
// プレイヤー演出用スクリプトその1
//
// <概要>
// プレイヤーが敵に負けて踏ん張ってるときのやつ
//
// Created by 前山　直澄 on 2014.05.13
// -------------------------------------------------------

public class PlayerEnd : MonoBehaviour
{
    // 吹き飛ばされるときのオブジェクト
    [SerializeField]
    private GameObject flyPlayer = null;
    
    // 踏ん張ってるときのアニメーション
    [SerializeField]
    private Texture2D[] anim = null;

    // アニメーション速度
    [SerializeField]
    private float animWaitSecond = 0;

    // 吹き飛ばされるまでの時間
    [SerializeField]
    private float deadSecond = 0;

	void Start()
    {
        StartCoroutine(Routine());
	}

    IEnumerator Routine()
    {
        int num = 0;
        StartCoroutine(Delay());
        while (true)
        {
            renderer.material.mainTexture = anim[num];
            yield return new WaitForSeconds(animWaitSecond);
            num++;
            if(num > anim.Length - 1) num = 0;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(deadSecond);
        flyPlayer.SetActive(true);
        Destroy(gameObject);
    }
}