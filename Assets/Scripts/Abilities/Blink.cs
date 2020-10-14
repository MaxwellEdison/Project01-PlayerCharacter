using UnityEngine;

public class Blink : Ability
{
    [SerializeField] GameObject _blinkBoxSpawned = null;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 tempPosition;
    [SerializeField] Quaternion tempRotation;
    //[SerializeField] Rigidbody playerRB;
    [SerializeField] Rigidbody targetRB;


    float duration = .3f;

    private void Awake()
    {

    }
    public override void Use(Transform origin, Transform target)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //playerRB = player.GetComponent<Rigidbody>();
        tempPosition = player.transform.position;
        tempRotation = player.transform.rotation;
        float[] qarray = new float[4];
        float[] barray = new float[4];
        float[] carray = new float[4];
        for(int i=0; i<4; i++)
        {
            Random.InitState(System.DateTime.Now.Millisecond + Mathf.RoundToInt(Time.deltaTime) + i);
            qarray[i] = Mathf.Deg2Rad*Random.Range(0.0f, 0.5f);
        }


        Quaternion wobble = new Quaternion(qarray[0],qarray[1], qarray[2], qarray[3]);

        GameObject[] o_blinkBox = new GameObject[4];
        GameObject[] t_blinkBox = new GameObject[4];
        for(int i=0; i<4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Random.InitState(System.DateTime.Now.Millisecond + Mathf.RoundToInt(Time.deltaTime) + j);
                barray[j] = Random.Range(10.0f, 90f);
            }
            Quaternion wobble2 = new Quaternion(barray[0], barray[1], barray[2], barray[3]);
            o_blinkBox[i] = Instantiate(_blinkBoxSpawned, new Vector3(origin.transform.position.x, origin.transform.position.y + 0.5f, origin.transform.position.z), wobble2);
            o_blinkBox[i].transform.localScale = new Vector3(i, i, i);
            o_blinkBox[i].GetComponent<BlinkFX>().wobb = wobble2;
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Random.InitState(System.DateTime.Now.Millisecond + Mathf.RoundToInt(Time.deltaTime) + j);
                carray[j] = Random.Range(10.0f, 90f);
            }
            Quaternion wobble3 = new Quaternion(carray[0], carray[1], carray[2], carray[3]);
            t_blinkBox[i] = Instantiate(_blinkBoxSpawned, new Vector3(target.transform.position.x, target.transform.position.y + 1.875f, target.transform.position.z), wobble3);
            t_blinkBox[i].transform.localScale = new Vector3(3.6f + i, 3.6f +  i, 3.6f + i);
        }

        CharacterController plrControl = player.GetComponent<CharacterController>();

        plrControl.enabled = false;
        targetRB = target.GetComponent<Rigidbody>();


        player.transform.SetPositionAndRotation(target.transform.position, tempRotation);


        targetRB.position = new Vector3(tempPosition.x,tempPosition.y+.5f,tempPosition.z);
        target.rotation = wobble;

        plrControl.enabled = true;


        Debug.Log("Blinked to " + target.name);
        for(int i = 0; i < 4; i++)
        {
            Destroy(o_blinkBox[i], duration);
            Destroy(t_blinkBox[i], duration);
        }

    }
}
