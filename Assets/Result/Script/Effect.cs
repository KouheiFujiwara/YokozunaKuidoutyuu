using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

	[SerializeField]
	private float wait = 0.1f;

	void Start () {
		StartCoroutine(Hit());
	}

	IEnumerator Hit()
	{
		transform.parent = null;

		yield return new WaitForSeconds(wait);

		Destroy(gameObject);
	}
}
