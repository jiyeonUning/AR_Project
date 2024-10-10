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

    private bool PowerBarMoving = false;


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
            StartCoroutine(PowerBarRoutine());
        }
    }

    void Shot()
    {
        // Model.발사력을 게이지 바와 연동
        Model.ShootSpeed = gauge.value;
        // 총알을 대여함과 동시에 해당 값의 정보를 저장
        PooledObject pooledObject =  ObjectPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
        // 총알 클래스을 선언, 해당 컴포넌트를 가져와서,
        Bullet bullet = pooledObject.GetComponent<Bullet>();
        // 총알 클래스의 발사력과 현재 클래스에서 변동된 발사력을 연동시켜줌으로써 발사력을 조절해줄 수 있다.
        bullet.ShotSpeed = Model.ShootSpeed;

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


