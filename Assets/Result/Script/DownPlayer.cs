using UnityEngine;
using System.Collections;

// -------------------------------------------------------
//
// プレイヤー演出用スクリプトその3
//
// <概要>
// プレイヤーが落ちてくるときの演出用スクリプト
//
// Created by 前山　直澄 on 2014.05.13
// -------------------------------------------------------

// 吹き飛ばされたプレイヤーが落ちて来るときのやつ
public class DownPlayer : MonoBehaviour {

    // 画面に激突するまでの回転速度
    [SerializeField]
    private float rotSpeed = 0;

    // 画面に激突するまでの落下速度
    [SerializeField]
    private float DownSpeed1 = 0;

    // 画面に激突した後の落下速度
    [SerializeField]
    private float DownSpeed2 = 0;

    // 画面揺らしのための揺らすオブジェクト
    [SerializeField]
    private GameObject backGround = null;

    // 画面に激突するY座標
    [SerializeField]
    private float switchPosY = 0;

    // 画面に激突するまでのスケールアップ速度
    [SerializeField]
    private float zoomSpeed = 0;

    // 画面を揺らす速度
    [SerializeField]
    private float waitShakeSecond = 0;

    // 画面が激突した後エネミーが帰っていくのでそのときのエネミーを動かすオブジェクト
    [SerializeField]
    private GameObject hangerFade = null;

    // 画面に激突した後オブジェクトが破棄される座標
    [SerializeField]
    private float destroyY = -5;

    void Start()
    {
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        while (true)
        {
            yield return 0;
            transform.Rotate(0, 0, rotSpeed);
            transform.position += new Vector3(0, -DownSpeed1, 0);
            transform.localScale += new Vector3(zoomSpeed, zoomSpeed, 0);
            if(transform.position.y <= switchPosY)
            {
                break;
            }
        }
        backGround.SetActive(true);
        yield return new WaitForSeconds(waitShakeSecond);
        while(true)
        {
            yield return 0;
            transform.position += new Vector3(0, -DownSpeed2, 0);

            if (gameObject.transform.position.y <= destroyY)
            {
                hangerFade.SetActive(true);

                Destroy(gameObject);
            }
        }
    }
}
