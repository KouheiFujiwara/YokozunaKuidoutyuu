using UnityEngine;
using System.Collections;

// -------------------------------------------------------
//
// プレイヤーエフェクトスクリプト
//
// <概要>
// プレイヤーが敵を吹き飛ばしたときに出るエフェクト
//
// Created by 前山　直澄 on 2014.03.05
// -------------------------------------------------------

public class Effect : MonoBehaviour {

    // エフェクトが消されるまでの時間
	[SerializeField]
	private float wait = 0.1f;

	void Start ()
    {
		StartCoroutine(Hit());
	}

	IEnumerator Hit()
	{
		transform.parent = null;

		yield return new WaitForSeconds(wait);

		Destroy(gameObject);
	}
}
