using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusoManager : MonoBehaviour {

    [SerializeField]
    private GameObject enemyHangar = null;

	[SerializeField]
	private Texture2D[] harite = null;

	[SerializeField]
	private Texture2D[] rikishi = null;

	[SerializeField]
	private float waitHakkiyoi = 1.0f;

	[SerializeField]
	private GameObject player = null;

	[SerializeField]
	private GameObject enemy = null;
	
	[SerializeField]
	private float waitHariteAnim = 0.1f;

	[SerializeField]
	private float enemyWeightEach = 1.0f;
	
	[SerializeField]
	private float EnemyFirstWeight = 100.0f;

	[SerializeField]
	private int rikishiEvo = 100;

	[SerializeField]
	private float rikishiEvoScale = 0.5f;

	[SerializeField]
	private GameObject suna = null;

	[SerializeField]
	private Texture2D[] sunaEff = null;

	public int hitcount = 0;

	public int enemynum = 0;

	private bool isMuso = true;

	private float space = 2.0f;

	private int test;

	void Start () {
		StartCoroutine(Muso());
	}

	IEnumerator Muso()
	{
		test = Random.Range(100, 1000);

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

            enemyInstance.transform.parent = enemyHangar.transform;

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
		int num = 0;
		while(isMuso == true)
		{
			player.renderer.material.mainTexture = harite[num];
			num++;
			if(num >= 4) num = 0;
			yield return new WaitForSeconds(waitHariteAnim);
			if(enemynum < hitcount)
			{
				Debug.Log(enemynum);
				Debug.Log(hitcount);
				isMuso = false;

				gameObject.SendMessage("End", hitcount);
			}
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
