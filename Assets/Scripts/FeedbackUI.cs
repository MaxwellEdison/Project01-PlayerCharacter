using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackUI : MonoBehaviour
{
    public float shakeMax = 5f;
    public float shakeMin = -5f;
    public float shakeDuration = 0.25f;
    public Vector3 shake;
    public Vector3 homePos;
    //public GameObject feedbackObj;

    private void Start()
    {
        //GameObject feedbackObj = gameObject;
        homePos = gameObject.GetComponent<Transform>().localPosition;
        StartCoroutine(endShake(shakeDuration,homePos));
    }
    // Start is called before the first frame update
    private void Update()
    {

        shake = new Vector3(Random.Range(shakeMin, shakeMax), Random.Range(shakeMin, shakeMax), Random.Range(shakeMin, shakeMax));
        gameObject.GetComponent<Transform>().localPosition = shake;
    }

    IEnumerator endShake(float dur, Vector3 origin)
    {
        yield return new WaitForSeconds(dur);
        gameObject.GetComponent<Transform>().localPosition = origin;
        Destroy(this);
    }
    // Update is called once per frame

}
