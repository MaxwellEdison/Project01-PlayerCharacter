using UnityEngine;

public class BlinkFX : MonoBehaviour
{
    GameObject bBall;
    GameObject bCamera;
    RenderTexture bTex;
    public Quaternion wobb;
    public Mesh sphere;
    public Shader shader;
    public Texture texture;
    Quaternion aquat;
    Quaternion bQuat;
    float a = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.transform.localScale = Vector3.one;
        bBall = new GameObject("bBall", typeof(MeshFilter),typeof(MeshRenderer));
        bBall.transform.SetParent(gameObject.transform);
        bBall.transform.localPosition = new Vector3(0,0,0);
        bBall.transform.localScale = new Vector3(100,100,100);
        bBall.layer = 9;
        bCamera = new GameObject("bCamera",typeof(Camera));
        bCamera.transform.SetParent(gameObject.transform);
        bCamera.transform.localPosition = Vector3.zero;
        bCamera.transform.localScale = Vector3.one;
        bCamera.layer = 9;
        bTex = new RenderTexture(512, 512, 24, RenderTextureFormat.Default);
        bCamera.GetComponent<Camera>().targetTexture = bTex;
        bCamera.GetComponent<Camera>().cullingMask = ~(1<<9);
        bBall.GetComponent<MeshFilter>().mesh = sphere;
        bBall.GetComponent<Renderer>().material = new Material(shader);
        bBall.GetComponent<Renderer>().material.mainTexture = bTex;
        bCamera.transform.localRotation = wobb;

    }

    private void Update()
    {
        
        a = a + Time.deltaTime;
        aquat = new Quaternion(0.0001f, 0.0001f, 0.0001f, 0.0001f);
        bQuat = new Quaternion((Random.Range(0f, 0.1f)) * .00005f, (Random.Range(0f, 0.1f)) * .00005f, (Random.Range(0f, 0.1f)) * .00005f, (Random.Range(0f, 0.1f) * .00005f));
        bCamera.transform.rotation = Quaternion.Slerp(aquat,bQuat,a);
    }
}
