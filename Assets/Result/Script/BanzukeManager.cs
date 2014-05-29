using UnityEngine;
using System.Collections;

// -------------------------------------------------------
//
// 番付演出用スクリプト
//
// <概要>
// 番付の演出用
//
// Created by 前山　直澄 on 2014.05.20
// -------------------------------------------------------

public class BanzukeManager : MonoBehaviour {

    // 子に入れている番付の配列
    [SerializeField]
    private GameObject[] banzuke = null;

    // このオブジェクトが横に動くときのスピード
    [SerializeField]
    private float moveSpeed = 0.1f;

    // このオブジェクトが一時停止するときのX座標
    [SerializeField]
    private float stopX = 0;

    // 次の番付が赤くなるまでの待ち時間
    [SerializeField]
    private float waitNextBanzuke = 1.0f;

    // 番付が選ばれてないときの透明度(今のところ使う必要性がない)
    [SerializeField]
    private int waitAlpha = 100;

    // 番付が次の番付に行くときに必要な力士数
    [SerializeField]
    private int[] StatusUp;

    // 番付が選ばれたあとに選ばれた番付が移動するスピード
    [SerializeField]
    private float endEffectSpeed = 0.0f;

    // 番付が選ばれたあとに選ばれた番付が移動する位置
    [SerializeField]
    private GameObject banzukeEndPos = null;

    // 番付が選ばれたあとに選ばれなかった番付が破棄されるまでの時間
    [SerializeField]
    private float waitDestory = 1;

    // 番付が選ばれたあとに選ばれた番付が大きくなるスピード
    [SerializeField]
    private float addScaleSpeed = 0.1f;

    // 番付が選ばれたあとに選ばれた番付が大きくなるフレーム数
    [SerializeField]
    private int addFrame = 10;

    // 何番目の番付が選ばれるか
    private int endNum = 0;

    void StartBanzuke(int _count)
    {
        for (int i = 0; i < StatusUp.Length; i++)
        {
            if (_count <= StatusUp[i])
            {
                endNum = i;
                break;
            }
        }
            StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        while (gameObject.transform.position.x <= stopX)
        {
            transform.position += new Vector3(moveSpeed, 0, 0);
            yield return 0;
        }
        transform.position = new Vector3(stopX, transform.position.y, transform.position.z);

        var count = 0;
        while(true)
        {
            var render = banzuke[count].GetComponent<Renderer>();
            var col = render.material.color;
            render.material.color = new Vector4(col.r, col.g, col.b, 255);

            yield return new WaitForSeconds(waitNextBanzuke);

            if (count >= endNum) break;

            render.material.color = col;
            count++;
        }

        var lastObj = banzuke[count];

        yield return StartCoroutine(AddScale(lastObj));

        var vec = banzukeEndPos.transform.position - lastObj.transform.position;
        vec = Vector3.Normalize(vec) * endEffectSpeed;
        banzuke[count].transform.parent = null;

        while(true)
        {
            lastObj.transform.position += vec;
            if (Vector3.Distance(banzukeEndPos.transform.position, lastObj.transform.position) <= endEffectSpeed) break;
            yield return 0;
        }

        StartCoroutine(DestoryThis());

        while (true)
        {
            transform.position += new Vector3(moveSpeed, 0, 0);
            yield return 0;
        }
    }

    IEnumerator DestoryThis()
    {
        yield return new WaitForSeconds(waitDestory);
        Destroy(gameObject);
    }

    IEnumerator AddScale(GameObject obj)
    {
        var count = 0;
        var scale = obj.transform.localScale * addScaleSpeed;
        while(true)
        {
            obj.transform.localScale += scale;
            if (count >= addFrame) break;
            count++;
            yield return 0;
        }
    }
}
