using UnityEngine;
using System.Collections;

// -------------------------------------------------------
//
// ステータス表示スクリプト
//
// <概要>
// 体重と走行距離の表示に使うやつ
//
// Created by 前山　直澄 on 2014.05.27
// -------------------------------------------------------

public class UIStatus : MonoBehaviour {
    [SerializeField]
    private Texture2D[] numTexs = null;

    // プレイヤーマネージャー(体重等を取得するため)
    [SerializeField]
    private GameObject pManager = null;

    // 貼り付ける板ポリたち
    [SerializeField]
    private GameObject[] weightNums, mileageNums = { null, null };

    // 体重等が入ってるコンポーネントPlayerManagerクラス
    private PlayerManager PComp = null;

    // PlayerManagerコンポーネントを取得してコルーチンをスタートする
	void Start ()
    {
        PComp = pManager.GetComponent<PlayerManager>();
        
        StartCoroutine(Corountine());
	}

    // 
    IEnumerator Corountine()
    {
        yield return 0;
    }

    void Status(GameObject[] _objs, int[] _status)
    {
        for (int i = 0; i < _status.Length; i++)
        {
            _objs[i].renderer.material.mainTexture = numTexs[_status[i]];
        }
    }

    private int[] IntToIntArray(int _num)
    {
        int digit = _num.ToString().Length;

        int[] divide = new int[digit];

        for (var i = 0; i < divide.Length; i++)
        {
            divide[i] = _num % ((int)(10 * (int)Mathf.Pow(10, i + 1))) / (int)((10 * (int)Mathf.Pow(10, i)));
        }

        return divide;
    }
}