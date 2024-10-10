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

    //***********************************************

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

            // 총알 클래스의 컴포넌트를 가져와 생성된 총알의 중력을 없애 플레이어 눈 앞에 고정시킨다.
            if (pooledObject != null)
            {
                bullet = pooledObject.GetComponent<Bullet>();
                rb = bullet.GetComponent<Rigidbody>();
                rb.useGravity = false;
            }

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

    //======================================================================
    //======================================================================

    void Rotate()
    {
        //Vector3 rotateValue = ;

        //if (input.actions["Move"].WasPressedThisFrame())
        //{
        //    transform.Rotate(rotateValue.x, rotateValue.y, 0);
        //}

    }
}


