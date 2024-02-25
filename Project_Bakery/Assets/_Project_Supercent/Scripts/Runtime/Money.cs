using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHeight = 5f;   // 최대 높이

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

            Quaternion tan = Quaternion.Slerp(transform.rotation, Quaternion.Euler(path[2]), t);
            transform.rotation = tan;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}


