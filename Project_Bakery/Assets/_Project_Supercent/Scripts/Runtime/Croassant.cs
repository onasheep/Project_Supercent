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

    //    // 대상까지의 거리 계산
    //    float target_Distance = Vector3.Distance(start, dest);

    //    // 물체를 지정된 각도로 목표물에 던지는 데 필요한 속도를 계산합니다.
    //    float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

    //    // 속도의 X,Y 추출
    //    float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
    //    float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

    //    // 비행 시간을 계산한다
    //    float flight_Duration = target_Distance / Vx;

    //    // 발사체를 회전 시켜 목표물을 향하게 한다
    //    transform.rotation = Quaternion.LookRotation(dest - transform.position);

    //    //경과 시간
    //    float elapse_time = 0;

    //    Vector3 defaultScale = Projectile.localScale;

    //    //투사체가 목표 지점 까지 포물선을 그리면서 정해진 시간과 속도 만큼 간다는 주석 
    //    while (elapse_time < flight_Duration)
    //    {
    //        Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

    //        elapse_time += Time.deltaTime;

    //        yield return null;
    //    }

    //} 
}
