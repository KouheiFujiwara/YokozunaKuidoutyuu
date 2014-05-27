// -------------------------------------------------------
//
// チームロゴを最初に表示させる
//
// <概要>
// 数秒だけ画面を見せてからタイトルへと遷移する
//
// Created by 藤原古兵 on 2014.03.06
// -------------------------------------------------------
using UnityEngine;
using System.Collections;

public class TeamLogo : MonoBehaviour {

    //  次のシーンへ移動する時間を決める
    [SerializeField]
    private float nextSceneTime;

    //  時間を計る
    private float timer;

    //  Transitionオブジェクトの生成
    private GameObject transition;

	// Use this for initialization
	void Start () 
    {
        transition = GameObject.Find("Transition");
        StartCoroutine(startGameCoroutine());
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
    }

    private IEnumerator startGameCoroutine()
    {
        while(true)
        {
            if (timer >= nextSceneTime)
            {
                transition.SendMessage("StartFade", "Title");
                break;
            }
            yield return null;
        }
    }

}
