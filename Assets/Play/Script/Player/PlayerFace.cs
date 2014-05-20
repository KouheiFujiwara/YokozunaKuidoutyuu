// -------------------------------------------------------
//
// UIのプレイヤーの顔のスプライトの切り替え
//
// <概要>
// スプライト切り替え
//
// Created by 藤原康平 on 2014.03.06
// -------------------------------------------------------

using UnityEngine;
using System.Collections;

public class PlayerFace : MonoBehaviour {
    //  体力の数値をもらう
    private int LifePoint;

    //  配列の番号きめる用の列挙型
    private enum SpriteNumber : int
    {
        SMILE = 0,
        NORMAL,
        WARNING
    }

    // 　マジックナンバー防止の列挙型
    private enum FaceValue : int
    {
        SMILE = 6,
        NORMAL = 2
    }

    //  各スプライト
    [SerializeField]
    private Sprite[] Faces;

    //  最終的なスプライト
    private SpriteRenderer _SpirteRenderer;

	// Use this for initialization
	void Start () {
        _SpirteRenderer  = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        LifePoint = GameObject.Find("Gage").GetComponent<LifeManager>().GetHpNum;

        if (LifePoint >= (int)FaceValue.SMILE )
        {
            _SpirteRenderer.sprite = Faces[(int)SpriteNumber.SMILE];
        }
        else if (LifePoint < (int)FaceValue.SMILE && LifePoint >= (int)FaceValue.NORMAL)
        {
            _SpirteRenderer.sprite = Faces[(int)SpriteNumber.NORMAL];
        }
        else
        {
            _SpirteRenderer.sprite = Faces[(int)SpriteNumber.WARNING];
        }
	}
}
