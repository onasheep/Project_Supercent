using UnityEngine;

public class test : MonoBehaviour
{
    public Transform target; // 이동할 목표 지점
    public float speed = 5f; // 이동 속도

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;

            // 목표 지점에 도달하면 정지
            if (distance < 0.1f)
            {
                return;
            }
            direction.y = direction.y * 0.5f;

            // 부드럽게 이동하기 위해 Slerp를 사용하여 이동
            Vector3 newPosition = Vector3.Slerp(transform.position, target.position, Time.fixedDeltaTime * speed);

            // 새로운 위치로 이동
            transform.position = newPosition;
        }
    }

    // 목표 지점 변경 시 호출되는 함수
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}