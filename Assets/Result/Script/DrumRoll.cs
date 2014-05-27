using UnityEngine;
using System.Collections;

public class DrumRoll : MonoBehaviour {

	[SerializeField]
	private Texture2D[] mat = null;

	[SerializeField]
	private GameObject[] DrumTarget = null;

	[SerializeField]
	private Texture2D AlphaPlane = null;

	[SerializeField]
	private float speed = 0.1f;

	[SerializeField]
	private float wait = 0.1f;

	private int Num;

	void Start()
	{
		foreach(var i in DrumTarget)
		{
			i.renderer.material.mainTexture = AlphaPlane;
		}
	}

	// この関数を他のとこから呼び出すことによってスタートする
	void Go(int _num)
	{
		Num = _num;
		StartCoroutine(Main());
	}

	IEnumerator Main()
	{
		int digit = Num.ToString().Length;

		int[] divide = new int[digit];

		for(var i = 0; i<divide.Length; i++)
		{
			divide[i] = Num % (10*(int)Mathf.Pow(10, i+1)) / (10*(int)Mathf.Pow(10, i));
		}

		for(var i = 0; i<divide.Length; i++)
		{
			yield return StartCoroutine(Loop(DrumTarget[i], divide[i]));
		}
	}

	IEnumerator Loop(GameObject _obj, int _num)
	{
		int num = 0;
		yield return new WaitForSeconds(wait);
		while(true)
		{
			_obj.renderer.material.mainTexture = mat[num];
			if(num >= _num) break;
			num++;
			yield return new WaitForSeconds(speed);
		}
	}
}
