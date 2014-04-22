using UnityEngine;
using System.Collections;

public class ResultManager : MonoBehaviour {

	[SerializeField]
	private Texture2D[] mat = null;
	
	[SerializeField]
	private GameObject[] weightNum = null;

	[SerializeField]
	private GameObject[] lenghtNum = null;

	[SerializeField]
	private GameObject[] endNum = null;

	[SerializeField]
	private float waitNumFromNum = 0.5f;

	[SerializeField]
	private float wait = 0.1f;

	private int[] weight = null;
	private int[] lenght = null;
	private int[] en;

	// 後で消す
	private int test;
	private int test2;

	void Start()
	{
		//renderer.material.mainTexture 

		test = Random.Range(0, 2000);
		test2 = Random.Range(0, 99999);
		weight = new int[]
		{
			test / 1000,
			test % 1000 / 100,
			test % 100 / 10,
			test % 10
		};

		for(int i=0; i<4; i++)
		{
			Debug.Log(weight[i]);
		}

		lenght = new int[]
		{
			test2 / 10000,
			test2 % 10000 / 1000,
			test2 % 1000 / 100,
			test2 % 100 / 10,
			test2 % 10
		};

		StartCoroutine(Sequence());
	}

	IEnumerator Sequence()
	{
		yield return StartCoroutine(Series(weightNum, weight));

		yield return StartCoroutine(Series(lenghtNum, lenght));
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
	}
}
