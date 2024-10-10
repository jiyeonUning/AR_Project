using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [SerializeField] ObjectPool ObjectPool;
    [SerializeField] ShooterModel Model;
    [SerializeField] PlayerInput input;
    [SerializeField] Slider gauge;

    [SerializeField] public Transform muzzlePoint;

    private PooledObject pooledObject;
    private Bullet bullet;
    private Rigidbody rb;

    private bool PowerBarMoving = false;


    // 공의 transform과 rotate에서 설정한 transfrom rotation 값을 동일하게 해주어 forward값을 바꿀 수 잇게 해주어야 하는데 어떻게 하지
    // 검색어 : 물체 회전 유니티 어쩌고를 시도해보자

    private void Awake()
    {
        Model = GetComponent<ShooterModel>();
        ObjectPool = GetComponent<ObjectPool>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (input.actions["Shot"].WasPressedThisFrame()) { Charge(); }
        if (input.actions["Shot"].WasReleasedThisFrame()) { Shot(); }
    }

    //======================================================================
    //======================================================================

    void Charge()
    {
        if (PowerBarMoving == false)
        {
            PowerBarMoving = true;

            // 총알을 대여함과 동시에 해당 값의 정보를 저장
            pooledObject = ObjectPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);

            // 총알 클래스의 컴포넌트를 가져온다
            if (pooledObject != null)
            {
                bullet = pooledObject.GetComponent<Bullet>();
                rb = bullet.GetComponent<Rigidbody>();
            }

            rb.useGravity = false;

            StartCoroutine(PowerBarRoutine());
        }
    }

    void Shot()
    {
        // Model.발사력을 게이지 바와 연동
        Model.ShootSpeed = gauge.value;
        // 발사
        rb.useGravity = true;
        rb.velocity = Model.ShootSpeed * Vector3.forward;

        PowerBarMoving = false;
    }

    //======================================================================
    //======================================================================

    IEnumerator PowerBarRoutine()
    {
        while (PowerBarMoving)
        {
            gauge.value += Time.deltaTime * gauge.maxValue;
            yield return null;
        }

        while (PowerBarMoving == false)
        {
            gauge.value = gauge.minValue;
            yield return null;
        }
    }
}


