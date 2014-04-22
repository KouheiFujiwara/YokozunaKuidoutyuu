// -------------------------------------------------------
//
// タイトル画面でのシーンセレクトクラス
//
// <概要>
// シーン選択
// セレクトカーソルオブジェクトのスクリプト
//
// Created by 藤原康平 on 2014.03.06
// -------------------------------------------------------

using UnityEngine;
using System.Collections;

public class SelectScene : MonoBehaviour {

    //  セレクト番号
    private int SelectNum = 0;

    //  カーソルを少しだけ画面手前側にする値
    private const float ZCursor = -1;

    //  カーソルを横にずらす値
    [SerializeField]
    private float XCursor;

    //  移動するシーンオブジェクト
    [SerializeField]
    private GameObject[] SceneObjects;

    //  シーン遷移に使うオブジェクト
    private GameObject transition;

	// Use this for initialization
	void Start() 
    {
        transition = GameObject.Find("Transition");
	}
	
	// Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectNum++;
            if (SceneObjects.Length <= SelectNum) SelectNum = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectNum--;
            if (-1 >= SelectNum) SelectNum = SceneObjects.Length - 1; 
        }

        gameObject.transform.position = SceneObjects[SelectNum].transform.position;
        gameObject.transform.position += new Vector3(XCursor, 0f, ZCursor);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transition.SendMessage("StartFade", SceneObjects[SelectNum].name);
        }

    }

}
