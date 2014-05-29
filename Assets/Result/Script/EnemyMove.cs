using UnityEngine;
using System.Collections;

// -------------------------------------------------------
//
// 敵の動きのスクリプト
//
// <概要>
// 敵が動いて規定の座標にきたら吹き飛んでいく
//
// Created by 前山　直澄 on 2014.05.05
// -------------------------------------------------------

public class EnemyMove : MonoBehaviour {

    // 敵がはっけよーいからのこったの画像にかわるX座標
    [SerializeField]
    private float torikumiX = 0;

    // 敵の取り組み時のアニメーション
    [SerializeField]
    private Texture2D[] torikumiAnims = null;

    // 敵が進み始めるまでの時間
	[SerializeField]
	private float waitHakkiyoi = 1.0f;

    // 敵が進むスピード
	[SerializeField]
	private float speed = 0.3f;

    // 敵が死ぬ座標
	[SerializeField]
	private float deadX = 5.0f;

    // 敵が吹き飛ばされてから破棄されるまでの時間
	[SerializeField]
	private float destoryCount = 1.0f;

    // 敵が吹き飛ばされるスピード
	[SerializeField]
	private float awaiSpeed = 2.0f;

    // 敵が吹き飛ばされるときの回転速度
	[SerializeField]
	private float rotSpeed = 10.0f;

    // 敵が大きくなる間隔
	[SerializeField]
	private float scaleEach = 0.5f;

    // 敵の初期の大きさ
	[SerializeField]
	private float firstScale = 1.0f;

    // 敵が吹き飛ばされるときのエフェクト
	[SerializeField]
	private GameObject effect = null;

    // 吹き飛ぶときのアニメーション
	[SerializeField]
	private Texture2D[] hukitobi = null;

    // マネージャー
	[SerializeField]
	private GameObject mana = null;

	void Start () {
		StartCoroutine(Enemy());
	}

	IEnumerator Enemy()
	{
		yield return new WaitForSeconds(waitHakkiyoi);

        StartCoroutine(Torikumi());
		StartCoroutine(Move());
	}

	IEnumerator Move()
	{
		while(true)
		{
			if(transform.position.x >= deadX)
			{
				StartCoroutine(Dead());
				if(transform.localScale.x >= firstScale + scaleEach * 2)
				{
					renderer.material.mainTexture = hukitobi[2];
				}
				else if(transform.localScale.x >= firstScale + scaleEach)
				{
					renderer.material.mainTexture = hukitobi[1];
				}
				else
				{
					renderer.material.mainTexture = hukitobi[0];
				}

                transform.parent = null;
				effect.SetActive(true);

				mana.SendMessage("HitCount");

				break;
			}
			transform.Translate(speed,0,0);
			yield return 0;
		}
	}

	IEnumerator Dead()
	{
		float x = Random.Range(0,1.0f);
		float y = Random.Range(0,1.0f);
		var vec = Vector3.Normalize(new Vector3(x, y, 0)) * awaiSpeed;

		var t = Time.time;
		var deadtime = t + destoryCount;

		while(true)
		{
			transform.position += new Vector3(-vec.x, vec.y, 0);
			transform.Rotate(0,0,rotSpeed);

			if(Time.time >= deadtime)
			{
				Destroy(gameObject);
				break;
			}

			yield return 0;
		}
	}

    IEnumerator Torikumi()
    {
        while (true)
        {
            if (transform.position.x >= torikumiX)
            {
                if (transform.localScale.x >= firstScale + scaleEach * 2)
                {
                    renderer.material.mainTexture = torikumiAnims[2];
                }
                else if (transform.localScale.x >= firstScale + scaleEach)
                {
                    renderer.material.mainTexture = torikumiAnims[1];
                }
                else
                {
                    renderer.material.mainTexture = torikumiAnims[0];
                }

                break;
            }
            yield return 0;
        }
    }
}