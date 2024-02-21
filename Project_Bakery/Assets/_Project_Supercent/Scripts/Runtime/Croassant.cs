using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class Croassant : MonoBehaviour
{
    private Rigidbody rigid = default;
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 15f;

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

    //IEnumerator MovePalabolic(Vector3 start, Vector3 dest)
    //{
    //    float distnace = (start - dest).magnitude;
    //    while ()
    //    {

    //    }
    //}
    //IEnumerator ParabolicProjection(Vector3 start, Vector3 dest)
    //{

    //    // �������� �Ÿ� ���
    //    float target_Distance = Vector3.Distance(start, dest);

    //    // ��ü�� ������ ������ ��ǥ���� ������ �� �ʿ��� �ӵ��� ����մϴ�.
    //    float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

    //    // �ӵ��� X,Y ����
    //    float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
    //    float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

    //    // ���� �ð��� ����Ѵ�
    //    float flight_Duration = target_Distance / Vx;

    //    // �߻�ü�� ȸ�� ���� ��ǥ���� ���ϰ� �Ѵ�
    //    transform.rotation = Quaternion.LookRotation(dest - transform.position);

    //    //��� �ð�
    //    float elapse_time = 0;

    //    Vector3 defaultScale = Projectile.localScale;

    //    //����ü�� ��ǥ ���� ���� �������� �׸��鼭 ������ �ð��� �ӵ� ��ŭ ���ٴ� �ּ� 
    //    while (elapse_time < flight_Duration)
    //    {
    //        Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

    //        elapse_time += Time.deltaTime;

    //        yield return null;
    //    }

    //} 
}
