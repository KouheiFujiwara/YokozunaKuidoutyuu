using UnityEngine;
using System.Collections;

// -------------------------------------------------------
//
// スコア表示スクリプト
//
// <概要>
// ドラムロールの音が鳴りそうな演出とか数字を表示するとかのやつ
//
// Created by 前山　直澄 on 2014.03.05
// -------------------------------------------------------

public class ResultManager : MonoBehaviour {

    // 使う数字の画像の配列
	[SerializeField]
	private Texture2D[] mat = null;
	
    // 体重の数字を表示するために使うオブジェクト
	[SerializeField]
	private GameObject[] weightNum = null;

    // 走行距離を表示するために使うオブジェクト
	[SerializeField]
	private GameObject[] lenghtNum = null;

    // 人抜きの表示するために使うオブジェクト
	[SerializeField]
	private GameObject[] endNum = null;

    // 数字が表示されてから次の数字が表示されるまでの時間
	[SerializeField]
	private float waitNumFromNum = 0.5f;

    // 番付のマネージャー
    [SerializeField]
    private GameObject banMana = null;

    // 一つ一つの数字の待ち時間
	[SerializeField]
	private float wait = 0.1f;

	private int[] weights = null;
	private int[] lenghts = null;
	private int[] en;

	// 後で消す
	private int weight;
	private int mileage;

    // 藤原が用意したやつ
    private Transition _TransitionObject = null;

	void Start()
	{
		//renderer.material.mainTexture 

        var unitTest = GameObject.Find("Transition");
        if(unitTest)
            _TransitionObject = GameObject.Find("Transition").GetComponent<Transition>();

		weight = Random.Range(0, 2000);
		mileage = Random.Range(0, 9999);

        if (_TransitionObject)
        {
            weight = _TransitionObject.PlayerWeight;
            mileage = _TransitionObject.PlayerMileage;
        }

        weights = new int[]
		{
			weight / 10000,
			weight % 10000 / 1000,
			weight % 1000 / 100,
			weight % 100 / 10,
            weight % 10
		};

		lenghts = new int[]
		{
			mileage / 1000,
			mileage % 1000 / 100,
			mileage % 100 / 10,
			mileage % 10
		};

		StartCoroutine(Sequence());
	}

	IEnumerator Sequence()
	{
		yield return StartCoroutine(Series(weightNum, weights));

		yield return StartCoroutine(Series(lenghtNum, lenghts));
	}

	IEnumerator Series(GameObject[] _obj, int[] _num)
	{
		for(var i=0; i<_obj.Length; i++)
		{
			yield return StartCoroutine(DrumRoll(_obj[i], _num[i]));
		}
	}
	
	IEnumerator DrumRoll(GameObject _obj, int _num)
	{
		int num = 0;
		yield return new WaitForSeconds(waitNumFromNum);
		while(true)
		{
			_obj.renderer.material.mainTexture = mat[num];
			if(num >= _num) break;
			num++;
			yield return new WaitForSeconds(wait);
		}
	}

	void End(int _count)
	{
		StartCoroutine(En(_count));
	}

	IEnumerator En(int _count)
	{
		en = new int[]
		{
			_count / 1000,
			_count % 1000 / 100,
			_count % 100 / 10,
			_count % 10
		};
		
		for(int i =0; i<4; i++)
		{
			Debug.Log(en[i]);
		}

		yield return StartCoroutine(Series(endNum, en));

        banMana.SendMessage("StartBanzuke", _count);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _TransitionObject.SendMessage("StartFade" ,"Title");
            yield return 0;
        }
	}
}
