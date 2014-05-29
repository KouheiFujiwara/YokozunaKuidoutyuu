// -------------------------------------------------------
//
// プレイマネージャースクリプト
//
// <概要>
// プレイシーンのシーケンス制御や、
// 難易度の上昇等の役割を持つ
//
// Created by 高井康彬 on 2014.03.05
// -------------------------------------------------------

using UnityEngine;
using System.Collections;

public class PlayManager : MonoBehaviour {

    // シーケンスの種類
    public enum SeqType
    {
        SEQ_START,      // スタート
        SEQ_PLAY,       // プレイ
        SEQ_GAMEOVER    // ゲームオーバー
    }
    private SeqType seq = SeqType.SEQ_START;    // 現在のシーケンス

    // 現在のシーケンスのプロパティ
    public SeqType Seq
    {
        get { return this.seq; }
    }

    public float world_speed = 1.0f;            // 世界全体のスピード
    [SerializeField]
    private GameObject start1;                  // スタート演出オブジェクトその1
    [SerializeField]
    private float start1_wait_time = 3.0f;      // スタート演出オブジェクトその1を生成した後の待機時間
    [SerializeField]
    private GameObject start2;                  // スタート演出オブジェクトその2
    [SerializeField]
    private float start2_wait_time = 1.0f;      // スタート演出オブジェクトその2を生成した後の待機時間
    [SerializeField]
    private GameObject gameover;                // ゲームオーバー演出オブジェクト
    [SerializeField]
    private float gameover_wait_time = 2.0f;    // ゲームオーバー演出オブジェクトを生成した後の待機時間
    [SerializeField]
    private float cycle = 5.0f;                 // 世界全体のスピードが加速するサイクル(秒数)
    [SerializeField]
    private float accele = 0.01f;               // 世界全体のスピードの加速度
    private float timer = 0.0f;                 // 経過時間
    [SerializeField]
    private float food_cycle_trans = 0.05f;     // 食べ物生成間隔の変動値
    [SerializeField]
    private float poison_cycle_trans = 0.1f;    // 毒生成間隔の変動値

    #region シーン遷移用変数(by藤原康平)
    private Transition _TransitionObject;
    #endregion

    // Use this for initialization
	void Start()
    {
        #region シーン遷移用オブジェクトのGetComponent(by藤原康平)
        if (GameObject.Find("Transition")) _TransitionObject = GameObject.Find("Transition").GetComponent<Transition>();
        #endregion

        // スタート演出関数を開始
        StartCoroutine("StartDirection");
	}
	
	// Update is called once per frame
	void Update()
    {
        // スタートシーケンスの場合のみ以下の処理を行う
        {
            // 一定時間毎に世界全体のスピードを加速させ、食べ物と毒の生成サイクルを変動させる
            timer += Time.deltaTime;
            if (timer >= cycle)
            {
                timer = 0.0f;
                world_speed += accele;

                GetComponent<FoodCreater>().FoodCycle += food_cycle_trans;      // 食べ物の生成間隔の長く
                GetComponent<FoodCreater>().PoisonCycle -= poison_cycle_trans;  // 毒の生成間隔を短く
            }
        }
	}

    // ゲームオーバー関数
    // tips... プレイヤー側が呼ぶ
    void GameOver()
    {
        // シーケンスをゲームオーバーに遷移させ、ゲームオーバー演出関数を開始させる
        seq = SeqType.SEQ_GAMEOVER;
        StartCoroutine("GameoverDirection");
    }

    // スタート演出関数
    private IEnumerator StartDirection()
    {
        // スタート演出オブジェクトその1を生成し、指定秒数分待機
        Instantiate(start1, start1.transform.position, transform.rotation);
        yield return new WaitForSeconds(start1_wait_time);

        // スタート演出オブジェクトその2を生成し、指定秒数分待機
        Instantiate(start2, start2.transform.position, transform.rotation);
        yield return new WaitForSeconds(start2_wait_time);
        
        // シーケンスをプレイに遷移
        seq = SeqType.SEQ_PLAY;
        yield return null;
    }

    // ゲームオーバー演出関数
    private IEnumerator GameoverDirection()
    {
        // 世界全体のスピードを0にして止める
        world_speed = 0.0f;

        // ゲームオーバー演出オブジェクトを生成し、指定秒数分待機
        Instantiate(gameover, gameover.transform.position, transform.rotation);
        yield return new WaitForSeconds(gameover_wait_time);
        
        // リザルトへ
        // ----------------
        #region シーン遷移などの処理(by藤原康平)
        //  ゲーム全体で使う変数に数値を入れる処理
        if (GameObject.Find("Transition"))
        {
            _TransitionObject.PlayerWeight = GameObject.Find("Player").GetComponent<PlayerManager>().Weight;
            _TransitionObject.PlayerMileage = (int)GameObject.Find("Player").GetComponent<PlayerManager>().Mileage;

            // シーン移動実行
            _TransitionObject.SendMessage("StartFade", "Result");
        }
        #endregion 
        yield return null;
    }
}
