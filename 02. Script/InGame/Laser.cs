using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Tower
{
    private float Timer;

    [SerializeField] 
    private List<Transform> firePos;
    public string FirePosTagName = "FirePos";
    //[Header("Prefabs")]
    //public GameObject beamLineRendererPrefab; //Put a prefab with a line renderer onto here.
    //public GameObject beamStartPrefab; //This is a prefab that is put at the start of the beam.
    //public GameObject beamEndPrefab; //Prefab put at end of beam.

    public GameObject beamStart;
    public GameObject beamEnd;
    public GameObject beam;
    public LineRenderer lineRenderer;

    [Header("Beam Options")]
    public bool alwaysOn = true; //Enable this to spawn the beam when script is loaded.
    //public bool beamCollides = true; //Beam stops at colliders
    public float beamLength = 100; //Ingame beam length
    public float beamEndOffset = 0f; //How far from the raycast hit point the end effect is positioned
    public float textureScrollSpeed = 0f; //How fast the texture scrolls along the beam, can be negative or positive.
    public float textureLengthScale = 1f;   //Set this to the horizontal length of your texture relative to the vertical. 
    [SerializeField] private LayerMask m_viewTargetMask;
    [SerializeField] private LayerMask m_viewObstacleMask;

    [Header("BeamSetup")]
    private float m_viewRadius = 30f;
    [Range(-180f, 180f)]
    [SerializeField] private float m_viewRotateZ = 0f;

    public int LaserDamage = 15;
    //Example: if texture is 200 pixels in height and 600 in length, set this to 3

    // Start is called before the first frame update
    private void Awake()
    {
        InvokeRepeating("UPDATETARGET", 0f, 0.005f);
        
        turnSpeed = 8;

        FindFirePos();
    }
    private Vector3 AngleToDirZ(float angleInDegree)
    {
        // radian = (계산하고자 하는 360도 각도) * Mathf.Deg2Rad;
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 90f);
    }
    // Start is called before the first frame update
    void Start()
    {
        partToRotate = transform;
        //  FindFirePos();
    }
    void FindFirePos()
    {
        Transform[] tr = GetComponentsInChildren<Transform>();
        //Debug.Log("FirePos call : " + GetComponentsInChildren<Transform>().);
        for (int i = 0; i < tr.Length; i++) {
            Debug.Log("FirePos call : " + tr[i].gameObject.name) ;
            if (tr[i].gameObject.tag.Equals(FirePosTagName))
            {
                //Debug.Log("FirePos : " + transform.GetChild(i).transform);
                firePos.Add(tr[i].gameObject.transform);
            }
        }
    }
    private void OnEnable()
    {
        if (alwaysOn) //When the object this script is attached to is enabled, spawn the beam.
            SpawnBeam();
    }
    public void SpawnBeam() //This function spawns the prefab with linerenderer
    {
        if (beam){
            Vector3 originPos = firePos[0].position;

            if (beamStart)
                beamStart = Instantiate(beamStart);

            if (beamEnd)
                beamEnd = Instantiate(beamEnd);

            beam = Instantiate(beam);
            beam.transform.position = originPos;
            beam.transform.parent   = firePos[0];
            beam.transform.rotation = firePos[0].rotation;
            lineRenderer = beam.GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;

            beamStart.transform.position = originPos;
        }
    }

    public void RemoveBeam() //This function removes the prefab with linerenderer
    {
        if (beam)
            Destroy(beam);

        if (beamStart)
            Destroy(beamStart);

        if (beamEnd)
            Destroy(beamEnd);
    }
    private void OnDisable() //If the object this script is attached to is disabled, remove the beam.
    {
        RemoveBeam();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (firePos != null)
        {
            //ATTACK(firePos.Length);
            ATTACK();
        }

        if (target == null)
        {
            //타겟 없고 레이저 쓸때
            return;
        }

        LOCKONTARGET();
    }
    public override void LOCKONTARGET()
    {
        if (target == null)
            target.position = Vector3.zero;

        Vector3 dir = target.position - transform.position;
        var angle = Quaternion.FromToRotation(Vector3.up, dir).eulerAngles.z;
        Quaternion lookRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        // Lerp  0 ~ 100의 가중치 값이 회전하면서 가중치 값이 변하기 때문에
        // 초기에는 빠르게 회전 하나 이후에는 천천히 회전 함.

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(new Vector3(0, 0, rotation.z));

    }
    public override void UPDATETARGET()
    {
        base.UPDATETARGET();
    }
    public Vector3 DirFromAngle(float angleInDegrees)
    {
        //탱크의 좌우 회전값 갱신
        angleInDegrees += transform.eulerAngles.y;
        //경계 벡터값 반환
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    public override void ATTACK()
    {
        Vector2 originPos = firePos[0].position;
        Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

        beamStart.transform.position = originPos;

        Vector3 start = new Vector3(originPos.x, originPos.y, 0.0f);
        Vector3 end;

        RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, lookDir * 10f, m_viewRadius, m_viewTargetMask);

        if (rayHitedTarget) {           
            end = rayHitedTarget.point;
            Vector2 targetPos = rayHitedTarget.transform.position;
            beamEnd.transform.position = targetPos;
            end = end - (firePos[0].forward * beamEndOffset);
            Enemy _HitEnemy = rayHitedTarget.transform.gameObject.GetComponent<Enemy>();
            Timer += Time.deltaTime;

            if (Timer>=0.1f)
            {
                Timer = 0;
                _HitEnemy.TakeDamage(LaserDamage);
            }
            beamDisable(true);
        }
        else
        {
            Timer = 0;
            end = start + (firePos[0].forward * beamLength);

            beamDisable(false);
        }
        
        lineRenderer.SetPosition(0, originPos);

        lineRenderer.SetPosition(1, end);

        if (beamStart)
        {
            beamStart.transform.position = firePos[0].position;
            beamStart.transform.LookAt(end);
        }
        if (beamEnd)
        {
            beamEnd.transform.position = end;
            beamEnd.transform.LookAt(beamStart.transform.position);
        }

        float distance = Vector3.Distance(transform.position, end);
        //This sets the scale of the texture so it doesn't look stretched
        lineRenderer.material.mainTextureScale = new Vector2(distance / textureLengthScale, 1);

        //This scrolls the texture along the beam if not set to 0
        lineRenderer.material.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }
    void beamDisable(bool set)
    {
        if (beamStart)
        {
            beamStart.SetActive(set);
        }
        if (beamEnd)
        {
            beamEnd.SetActive(set);
        }
        if (beam)
        {
            beam.SetActive(set);
        }
    }
}