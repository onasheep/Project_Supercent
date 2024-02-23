using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Croassant : MonoBehaviour
{
    private Rigidbody rigid = default;
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 15f;
    // TODO ������ �׽�Ʈ
    public float maxHeight = 10f;   // �ִ� ����
    private void Awake()
    {
        Init();
    }
    void Start()
    {

    }

    void Init()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void Spawn(Vector3 dir)
    {
        rigid.AddForce(dir * speed, ForceMode.VelocityChange);
    }

    //public void SimulateProjectile(ObjectStacker stacker)
    //{
    //    rigid.isKinematic = true;

    //    Vector3 start = transform.positxion;

    //    // ������ ��� ������ ���
    //    Vector3 midPoint = (start + end) / 2f;
    //    midPoint += Vector3.up * maxHeight;

    //    // ������ � ����Ʈ ���
    //    Vector3[] path = new Vector3[] { start, midPoint, end };

    //    // ������ ��� ����Ʈ�� ���� �̵��ϴ� ������ � ����
    //    StartCoroutine(FollowBezierCurve(path, stacker.GetStackPos()));
    //}

    public void SimulateProjectile(Vector3 dest)
    {

        Vector3 start = transform.position;
        Vector3 end = dest;

        // ������ ��� ������ ���
        Vector3 midPoint = (start + end) / 2f;
        midPoint += Vector3.up * maxHeight;

        // ������ � ����Ʈ ���
        Vector3[] path = new Vector3[] { start, midPoint, end };

        // ������ ��� ����Ʈ�� ���� �̵��ϴ� ������ � ����
        StartCoroutine(FollowBezierCurve(path));
    }
    private IEnumerator FollowBezierCurve(Vector3[] path)
    {
        float t = 0f;
        while (t < 1f)
        {

            t += Time.deltaTime * 3f;
            t = Mathf.Clamp01(t);

            Vector3 newPosition = Bezier.GetPoint(path[0], path[1], path[2], t);
            transform.position = newPosition;

            Quaternion tan = Quaternion.Slerp(transform.rotation, Quaternion.Euler(path[2]), t);
            transform.rotation = tan;
            yield return null;
        }
        rigid.isKinematic = true;


    }
}
    //public void SimulateProjectile(Vector3 dest)
    //{
    //    rigid.isKinematic = true;

    //    Vector3 start = transform.position;
    //    Vector3 end = dest;

    //    // ������ ��� ������ ���
    //    Vector3 midPoint = (start + end) / 2f;
    //    midPoint += Vector3.up * maxHeight;

    //    // ������ � ����Ʈ ���
    //    Vector3[] path = new Vector3[] { start, midPoint, end };

    //    // ������ ��� ����Ʈ�� ���� �̵��ϴ� ������ � ����
    //    StartCoroutine(FollowBezierCurve(path, dest));
    //}

    //private IEnumerator FollowBezierCurve(Vector3[] path, Vector3 dest_)
    //{
    //    float distanceTravelled = 0f;
    //    float totalDistance = Vector3.Distance(transform.position, dest_);

    //    // ���� ������ ������ ������ �ݺ�
    //    while (distanceTravelled < totalDistance)
    //    {
    //        // ���� ��ġ���� ��ǥ ���������� �Ÿ�
    //        float distanceToDestination = Vector3.Distance(transform.position, dest_);

    //        // ������ ��� ���� �̵��ϴ� ���� ���
    //        float t = 1f - (distanceToDestination / totalDistance);

    //        // ������ ��� ���� �̵�
    //        Vector3 midPoint = (transform.position + dest_) / 2f;
    //        midPoint += Vector3.up * maxHeight;
    //        Vector3[] newpath = new Vector3[] { transform.position, midPoint, dest_ };
    //        Vector3 newPosition = Bezier.GetPoint(newpath[0], newpath[1], newpath[2], t);
    //        transform.position = newPosition;

    //        Quaternion tan = Quaternion.Slerp(transform.rotation, Quaternion.Euler(path[2]), t);
    //        transform.rotation = tan;

    //        // �Ÿ� ������Ʈ
    //        distanceTravelled = Vector3.Distance(transform.position, dest_);

    //        yield return null;
    //    }
    //}



    // ������ � ����� ���� ���� Ŭ����
    public static class Bezier
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * p0 + 2f * oneMinusT * t * p1 + t * t * p2;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        //// t�� 0�� 1 ������ ������ ����
        //t = Mathf.Clamp01(t);

        // ������ ��� ���Լ��� ���
        return 2f * (1f - t) * (p1 - p0) + 2f * t * (p2 - p1);
    }
}
