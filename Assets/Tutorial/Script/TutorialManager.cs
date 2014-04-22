using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    [SerializeField]
    private Material[] Pages;

    [SerializeField]
    private int PageMax;

    private int PageNumber = 0;

	// Use this for initialization
	void Start () 
    {
        StartCoroutine(BackScene());
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PageNumber--;
            if (PageNumber <= 0) PageNumber = 0;
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            PageNumber++;
            if (PageNumber >= PageMax) PageNumber = PageMax;
        }

        renderer.material = Pages[PageNumber];           
    }

    //  タイトル画面へ戻る
    private IEnumerator BackScene()
    {
        var transition = GameObject.Find("Transition");
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transition.SendMessage("StartFade", "Title");
                break;
            }
            yield return null;
        }
    }
}
