using UnityEngine;
using System.Collections;

public class DownPlayer : MonoBehaviour {

    [SerializeField]
    private float rotSpeed = 0;

    [SerializeField]
    private float DownSpeed1 = 0;

    [SerializeField]
    private float DownSpeed2 = 0;

    [SerializeField]
    private GameObject backGround = null;

    [SerializeField]
    private float switchPosY = 0;

    [SerializeField]
    private float zoomSpeed = 0;

    [SerializeField]
    private float waitShakeSecond = 0;

    void Start()
    {
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        while (true)
        {
            yield return 0;
            transform.Rotate(0, 0, rotSpeed);
            transform.position += new Vector3(0, -DownSpeed1, 0);
            transform.localScale += new Vector3(zoomSpeed, zoomSpeed, 0);
            if(transform.position.y <= switchPosY)
            {
                break;
            }
        }
        backGround.SetActive(true);
        yield return new WaitForSeconds(waitShakeSecond);
        while(true)
        {
            yield return 0;
            transform.position += new Vector3(0, -DownSpeed2, 0);
        }
    }
}
