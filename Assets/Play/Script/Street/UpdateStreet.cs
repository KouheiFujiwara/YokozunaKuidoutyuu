using UnityEngine;
using System.Collections;

public class UpdateStreet : MonoBehaviour {

    private PlayManager play_mgr;
    [SerializeField]
    private float street_disappear_pos_z = 48.0f;

	// Use this for initialization
	void Start ()
    {
        play_mgr = GameObject.Find("PlayManager").GetComponent(typeof(PlayManager)) as PlayManager;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += new Vector3(0.0f, 0.0f, play_mgr.world_speed);
        if (transform.position.z >= street_disappear_pos_z)
        {
            play_mgr.SendMessage("CreateStreet");
            Destroy(gameObject);
        }
	}
}
