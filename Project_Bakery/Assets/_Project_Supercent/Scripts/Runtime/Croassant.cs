using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Croassant : MonoBehaviour
{
    private Rigidbody rigid = default;
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 15f;
    // TODO 베지어 테스트
    public float maxHeight = 10f;   // 최대 높이
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

  

    public void SimulateProjectile(Vector3 dest)
    {

        Vector3 start = transform.position;
        Vector3 end = dest;

        // 베지어 곡선의 제어점 계산
        Vector3 midPoint = (start + end) / 2f;
        midPoint += Vector3.up * maxHeight;

        // 베지어 곡선 포인트 계산
        Vector3[] path = new Vector3[] { start, midPoint, end };

        // 베지어 곡선의 포인트에 따라 이동하는 포물선 운동 구현
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

            Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.Euler(path[2]), t);
            transform.rotation = rot;
            yield return null;
        }
        rigid.isKinematic = true;

    }

}

    // 베지어 곡선 계산을 위한 보조 클래스
    public static class Bezier
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * p0 + 2f * oneMinusT * t * p1 + t * t * p2;
    }

}
