using UnityEngine;

public class test : MonoBehaviour
{
    public Transform target; // �̵��� ��ǥ ����
    public float speed = 5f; // �̵� �ӵ�

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;

            // ��ǥ ������ �����ϸ� ����
            if (distance < 0.1f)
            {
                return;
            }
            direction.y = direction.y * 0.5f;

            // �ε巴�� �̵��ϱ� ���� Slerp�� ����Ͽ� �̵�
            Vector3 newPosition = Vector3.Slerp(transform.position, target.position, Time.fixedDeltaTime * speed);

            // ���ο� ��ġ�� �̵�
            transform.position = newPosition;
        }
    }

    // ��ǥ ���� ���� �� ȣ��Ǵ� �Լ�
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}