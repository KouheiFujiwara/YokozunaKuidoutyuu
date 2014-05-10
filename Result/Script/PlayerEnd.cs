using UnityEngine;
using System.Collections;

public class PlayerEnd : MonoBehaviour
{
    [SerializeField]
    private GameObject flyPlayer = null;


    [SerializeField]
    private Texture2D[] anim = null;

    [SerializeField]
    private float animWaitSecond = 0;

    [SerializeField]
    private float deadSecond = 0;

	void Start()
    {
        StartCoroutine(Routine());
	}

    IEnumerator Routine()
    {
        int num = 0;
        StartCoroutine(Delay());
        while (true)
        {
            renderer.material.mainTexture = anim[num];
            yield return new WaitForSeconds(animWaitSecond);
            num++;
            if(num > anim.Length - 1) num = 0;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(deadSecond);
        flyPlayer.SetActive(true);
        Destroy(gameObject);
    }
}