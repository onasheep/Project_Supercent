using UnityEngine;

public class test : MonoBehaviour
{
    public Transform startPoint;    // ��� ���� Transform
    public Transform endPoint;      // ���� ���� Transform
    public float maxHeight = 10f;   // �ִ� ����


    public void Start()
    {
        SimulateProjectile();
    }
    // ������ � �ùķ��̼� �Լ�
    public void SimulateProjectile()
    {
        Vector3 start = startPoint.position;
        Vector3 end = endPoint.position;

        // ������ ��� ������ ���
        Vector3 midPoint = (start + end) / 2f;
        midPoint += Vector3.up * maxHeight;

        // ������ � ����Ʈ ���
        Vector3[] path = new Vector3[] { start, midPoint, end };

        // ������ ��� ����Ʈ�� ���� �̵��ϴ� ������ � ����
        StartCoroutine(FollowBezierCurve(path));
    }

    // ������ ��� ���� �̵��ϴ� ������ � ����
    private System.Collections.IEnumerator FollowBezierCurve(Vector3[] path)
    {
        float t = 0f;
        float duration = 1f; // �̵��� �ɸ��� �ð�

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            Vector3 newPosition = Bezier.GetPoint(path[0], path[1], path[2], t);
            transform.position = newPosition;
            yield return null;
        }
    }
}

// ������ � ����� ���� ���� Ŭ����
public static class Bezier
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * p0 + 2f * oneMinusT * t * p1 + t * t * p2;
    }
}