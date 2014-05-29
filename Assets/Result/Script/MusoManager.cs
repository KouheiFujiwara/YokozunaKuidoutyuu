using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// -------------------------------------------------------
//
// 無双モードのやつ
//
// <概要>
// プレイヤーが敵を吹き飛ばしていったり敵が生成されて動いたりいろいろするスクリプト
//
// Created by 前山　直澄 on 2014.03.05
// -------------------------------------------------------

public class MusoManager : MonoBehaviour {

    // エネミーの移動速度
    [SerializeField]
    private float enemySpeed = 0;

    // プレイヤーが吹き飛ばされるときに使用されるオブジェクト
    [SerializeField]
    private GameObject playerEnd = null;

    // 敵を動かすためのオブジェクト
    [SerializeField]
    private GameObject enemyHanger = null;

    // プレイヤーの張り手のアニメ
	[SerializeField]
	private Texture2D[] harite = null;

    // 力士のアニメーション
	[SerializeField]
	private Texture2D[] rikishi = null;

    // 敵が動き始めるまでの待ち時間
	[SerializeField]
	private float waitHakkiyoi = 1.0f;

    // プレイヤー
	[SerializeField]
	private GameObject player = null;

    // エネミー
	[SerializeField]
	private GameObject enemy = null;
	
    // アニメーション速度
	[SerializeField]
	private float waitHariteAnim = 0.1f;

    // 敵の生成の横の間隔
	[SerializeField]
	private float enemyWeightEach = 1.0f;
	
    // 最初の敵の体重
	[SerializeField]
	private float EnemyFirstWeight = 100.0f;
    
    // 敵力士が大きくなる間隔
	[SerializeField]
	private int rikishiEvo = 100;

    // 敵力士が大きくなるときの加算される大きさ
	[SerializeField]
	private float rikishiEvoScale = 0.5f;

    // 砂のエフェクト
	[SerializeField]
	private GameObject suna = null;

    // 砂のエフェクトのアニメーション
	[SerializeField]
	private Texture2D[] sunaEff = null;

    // ヒットカウント
	public int hitcount = 0;

    // 生成されるエネミーの数
	public int enemynum = 0;

    // 無双モード中か終わったかのフラグ
	private bool isMuso = true;

	private float space = 2.0f;

	private int test;

    private Transition _TransitionObject = null;

	void Start () {
		StartCoroutine(Muso());
	}

	IEnumerator Muso()
	{
		test = Random.Range(100, 1000);

        var unitTest = GameObject.Find("Transition");
        if(unitTest)
            _TransitionObject = GameObject.Find("Transition").GetComponent<Transition>();

        if(_TransitionObject)
            test = _TransitionObject.PlayerWeight / 2 + _TransitionObject.PlayerMileage / 10;

        if (test <= 120) test = 120;

		yield return StartCoroutine(EnemyLine());

		yield return new WaitForSeconds(waitHakkiyoi);

		StartCoroutine(Harite());
		StartCoroutine(Suna());
	}

	IEnumerator EnemyLine()
	{
		int EnemyLineNum = (int)((test - EnemyFirstWeight) / enemyWeightEach + 10);

		enemynum = EnemyLineNum - 1;

		int EnemyCount = 1;

		//int imagecount = 0;

		while(true)
		{
			GameObject enemyInstance = Object.Instantiate(enemy) as GameObject;

			enemyInstance.transform.Translate(-space*EnemyCount,0,0);

            enemyInstance.transform.parent = enemyHanger.transform;

			if(EnemyCount % rikishiEvo == 0)
			{
				enemy.transform.localScale += new Vector3(rikishiEvoScale, rikishiEvoScale, 0);
				enemy.transform.Translate(0, rikishiEvoScale/2, 0);
			}
			if(EnemyCount / rikishiEvo >= 2)
			{
				enemyInstance.renderer.material.mainTexture = rikishi[1];
			}
			else if(EnemyCount / rikishiEvo >= 1)
			{
				enemyInstance.renderer.material.mainTexture = rikishi[0];
			}
			//yield return 0;

			if(EnemyLineNum <= EnemyCount) break;

			EnemyCount++;
		}

		yield return 0;

		Destroy(enemy);
	}

	IEnumerator Harite()
	{
        StartCoroutine(HangerMove());
		int num = 0;
		while(isMuso == true)
		{
            enemyHanger.transform.position += new Vector3(enemySpeed, 0, 0);
			player.renderer.material.mainTexture = harite[num];
			num++;
			if(num >= 4) num = 0;
			yield return new WaitForSeconds(waitHariteAnim);
			if(enemynum -10 < hitcount)
			{
				Debug.Log(enemynum);
				Debug.Log(hitcount);
				isMuso = false;

                Destroy(player);
                playerEnd.SetActive(true);

				gameObject.SendMessage("End", hitcount);
			}
		}
	}

    IEnumerator HangerMove()
    {
        while(isMuso == true)
        {
            enemyHanger.transform.position += new Vector3(enemySpeed, 0, 0);
            yield return 0;
        }
    }

	IEnumerator Suna()
	{
		int num = 0;
		while(isMuso == true)
		{
			suna.renderer.material.mainTexture = sunaEff[num];
			num++;
			if(num >= 2) num = 0;
			yield return new WaitForSeconds(0.1f);
		}
	}

	void HitCount()
	{
		hitcount++;
	}
}