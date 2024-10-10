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
        // Model.�߻���� ������ �ٿ� ����
        Model.ShootSpeed = gauge.value;
        // �Ѿ��� �뿩�԰� ���ÿ� �ش� ���� ������ ����
        PooledObject pooledObject =  ObjectPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
        // �Ѿ� Ŭ������ ����, �ش� ������Ʈ�� �����ͼ�,
        Bullet bullet = pooledObject.GetComponent<Bullet>();
        // �Ѿ� Ŭ������ �߻�°� ���� Ŭ�������� ������ �߻���� �������������ν� �߻���� �������� �� �ִ�.
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


