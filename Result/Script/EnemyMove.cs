using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    [SerializeField]
    private float torikumiX = 0;

    [SerializeField]
    private Texture2D[] torikumiAnims = null;

	[SerializeField]
	private float waitHakkiyoi = 1.0f;

	[SerializeField]
	private float speed = 0.3f;

	[SerializeField]
	private float deadX = 5.0f;

	[SerializeField]
	private float destoryCount = 1.0f;

	[SerializeField]
	private float awaiSpeed = 2.0f;

	[SerializeField]
	private float rotSpeed = 10.0f;

	[SerializeField]
	private float scaleEach = 0.5f;

	[SerializeField]
	private float firstScale = 1.0f;

	[SerializeField]
	private GameObject effect = null;

	[SerializeField]
	private Texture2D[] hukitobi = null;

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