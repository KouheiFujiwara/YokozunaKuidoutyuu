using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

    [SerializeField]
    private float shakeSecond = 0;

    [SerializeField]
    private float shakeX = 0;

    [SerializeField]
    private float shakeY = 0;

    [SerializeField]
    private GameObject ShakeObject = null;

    private float upper=Mathf.PI / 2f;

    [SerializeField]
    private float add = 0.1f;

    [SerializeField]
    private float shakePower = 10;

    void Start()
    {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        StartCoroutine(StopShake());

        while(true)
        {

            Vector3 vec = Vector3.Normalize(new Vector3(shakeX, shakeY, 0)) * shakePower * Mathf.Sin(upper);
            ShakeObject.transform.position += vec;

            upper += add;

            yield return 0;
        }
    }

    IEnumerator StopShake()
    {
        yield return new WaitForSeconds(shakeSecond);

        ShakeObject.transform.parent = null;

        Destroy(gameObject);

        yield return 0;
    }
}
