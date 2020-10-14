using UnityEngine;

public class BlinkFX : MonoBehaviour
{
    GameObject bBall;
    GameObject bCamera;
    RenderTexture bTex;
    public int myInst;
    public Quaternion wobb;
    public Mesh sphere;
    public Shader shader;
    public Texture texture;
    public Quaternion aQuat;
    public Quaternion bQuat;
    public Quaternion abQuat;
    float a = 0;
    float b = 0;
    float c = 0;
    float d = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //myInst =
        //gameObject.transform.localScale = Vector3.one;
        bBall = new GameObject("bBall" + myInst, typeof(MeshFilter),typeof(MeshRenderer));
        bBall.transform.SetParent(gameObject.transform);
        bBall.transform.localPosition = new Vector3(0,0,0);
        bBall.transform.localScale = new Vector3(100,100,100);
        bBall.layer = 9;
        bCamera = new GameObject("bCamera" + myInst,typeof(Camera));
        bCamera.transform.SetParent(gameObject.transform);
        bCamera.transform.localPosition = Vector3.zero;
        bCamera.transform.localScale = Vector3.one;
        bCamera.layer = 9;
        bTex = new RenderTexture(512, 512, 24, RenderTextureFormat.Default);
        bTex.name = "bTex" + myInst;
        Camera bCam = bCamera.GetComponent<Camera>();
        bCam.targetTexture = bTex;
        bCam.fieldOfView = 150;
        bCam.cullingMask = ~(1<<9);
        bBall.GetComponent<MeshFilter>().mesh = sphere;
        Renderer bRenderer = bBall.GetComponent<Renderer>();
        bRenderer.material = new Material(shader);
        bRenderer.material.mainTexture = bTex;
        
        bCamera.transform.localRotation = wobb;
        Random.InitState(System.DateTime.Now.Millisecond + Mathf.RoundToInt(Time.deltaTime)+myInst);
        a = Random.Range(0, 100);
        Random.InitState(System.DateTime.Now.Millisecond + Mathf.RoundToInt(Time.deltaTime) + myInst);
        b = Random.Range(0, 100);
        Random.InitState(System.DateTime.Now.Millisecond + Mathf.RoundToInt(Time.deltaTime) + myInst);
        c = Random.Range(0, 100);
        Random.InitState(System.DateTime.Now.Millisecond + Mathf.RoundToInt(Time.deltaTime) + myInst);
        d = Random.Range(0, 100);
        aQuat = new Quaternion(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f));
        bQuat = new Quaternion(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f));
    }

    private void Update()
    {
        Quaternion plrCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().rotation;
        Vector3 e = plrCam.eulerAngles;

        a = Mathf.Repeat(a + (Time.deltaTime * 5)+Random.Range(0,1), 360);
        Random.InitState(System.DateTime.Now.Millisecond + Mathf.RoundToInt(Time.deltaTime) + myInst);
        b = Mathf.Repeat(a + (Time.deltaTime * 5) + Random.Range(0, 1), 360);
        Random.InitState(System.DateTime.Now.Millisecond + Mathf.RoundToInt(Time.deltaTime) + myInst);
        c = Mathf.Repeat(a + (Time.deltaTime * 5) + Random.Range(0, 1), 360);
        Random.InitState(System.DateTime.Now.Millisecond + Mathf.RoundToInt(Time.deltaTime) + myInst);
        d = Mathf.Repeat(a + (Time.deltaTime * 5) + Random.Range(0, 1), 100);
        Quaternion abcd = new Quaternion(a, b, c, d);
        abQuat = Quaternion.Euler(a+e.x,b+e.y,c+e.z);
        bCamera.transform.rotation = abQuat;
        //bBall.transform.rotation = abQuat;
    }
}
