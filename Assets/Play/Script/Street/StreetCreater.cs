using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StreetCreater : MonoBehaviour {

    [SerializeField]
    private GameObject[] streets;
    private List<GameObject> street_list_1;
    [SerializeField]
    private float scl = 8.0f;
    [SerializeField]
    private float street_appear_pos_z = -40.0f;
    private int count = 0;

	// Use this for initialization
	void Start()
    {
        Shuffle();
        for (int i = 0; i < streets.Length; ++i)
        {
            Instantiate(streets[i], new Vector3(0.0f, 0.0f, street_appear_pos_z + scl * (i + 1)), transform.rotation);
        }
        Instantiate(streets[0], new Vector3(0.0f, 0.0f, street_appear_pos_z + scl * (streets.Length + 1)), transform.rotation);
        Shuffle();
	}

    // Update is called once per frame
    void Update()
    {
	}

    void Shuffle()
    {
        int n = streets.Length;
        while (n > 1)
        {
            --n;
            int k = Random.Range(0, n + 1);
            var tmp = streets[k];
            streets[k] = streets[n];
            streets[n] = tmp;
        }
    }

    void CreateStreet()
    {
        if (count >= streets.Length)
        {
            Shuffle();
            count = 0;
        }
        Instantiate(streets[count], new Vector3(0.0f, 0.0f, street_appear_pos_z), transform.rotation);
        ++count;
    }
}
