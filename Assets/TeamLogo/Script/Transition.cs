// -------------------------------------------------------
//
// トランジション
//
// <概要>
// トランジションの処理とゲーム全体の管理を行うクラス
//
// Created by 藤原康平 on 2014.03.06
// -------------------------------------------------------

using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {

    // 　フェード中ならばtrue
    private bool fadeComplete = false;
    //  Transitionのアルファ値
    private float alpha = 0 ;
    //  黒のテクスチャ
    private Texture2D blackTexture;

    //  フェードイン・アウトする時間を決める
    [SerializeField]
    private float interval;

    //  PlaySeq～Resultのプレイヤー体重変数
    private int playerWeight = 0;
    public int PlayerWeight
    {
        set { playerWeight = value; }
        get { return this.playerWeight; }
    }

    //  PlaySeq～Resultのプレイヤー進捗変数
    private int playerMileage = 0;
    public int PlayerMileage
    {
        set { playerMileage = value; }
        get { return this.playerMileage; }
    }

    void Awake()
    {
        //  ゲーム中破棄されないオブジェクトに設定
        DontDestroyOnLoad(this);

        //  黒のテクスチャを生成
        this.blackTexture = new Texture2D (32, 32, TextureFormat.RGB24, false); 
        this.blackTexture.ReadPixels(new Rect(0, 0, 32, 32),0,0, false);
        this.blackTexture.SetPixel(0,0,Color.white) ;
        this.blackTexture.Apply() ;
    }

	// Use this for initialization
	void Start () 
    {

	}

    //  フェード処理を開始する関数
    //  第１引数：移動したいシーン名の名前
    void StartFade(string SceneName )
    {
        if( !fadeComplete )
            StartCoroutine(FadeInOutCoroutine(SceneName));
    }

    void OnGUI()
    {
        if (!this.fadeComplete)
            return;

        GUI.color = new Color(0, 0, 0, this.alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.blackTexture);

    }

    //  フェードイン・アウト処理のコルーチン
    //  第１引数：移動したいシーン名の名前
    private IEnumerator FadeInOutCoroutine(string SceneName)
    {
        this.fadeComplete = true;
        var time = 0f;
        while (time <= interval)
        {
            this.alpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return null;
        }

        Application.LoadLevel(SceneName);

        time = 0 ;
        while (time <= interval)
        {
            this.alpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return null;
        }
        this.fadeComplete = false;
    }
}
