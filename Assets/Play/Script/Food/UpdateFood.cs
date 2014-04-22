// -------------------------------------------------------
//
// 食べ物・毒更新スクリプト
//
// <概要>
// 食べ物と毒の移動
//
// Created by 高井康彬 on 2014.03.05
// -------------------------------------------------------

using UnityEngine;
using System.Collections;

public class UpdateFood : MonoBehaviour {

    private PlayManager play_mgr;
    [SerializeField]
    private int kg = 10;
    [SerializeField]
    private float power = 10.0f;
    [SerializeField]
    private float street_disappear_pos_z = 48.0f;

	// Use this for initialization
	void Start()
    {
        play_mgr = GameObject.Find("PlayManager").GetComponent(typeof(PlayManager)) as PlayManager;
	}
	
	// Update is called once per frame
	void Update()
    {
        transform.position += new Vector3(0.0f, 0.0f, play_mgr.world_speed);
        if (transform.position.z >= street_disappear_pos_z)
        {
            Destroy(gameObject);
        }
	}

    void Eaten()
    {
        GameObject player_obj = GameObject.FindGameObjectWithTag("Player");
        player_obj.SendMessage("AddHP", power);
        player_obj.SendMessage("AddWeight", kg);
        Destroy(gameObject);
    }
}
