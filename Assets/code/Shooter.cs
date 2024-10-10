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

            // �Ѿ��� �뿩�԰� ���ÿ� �ش� ���� ������ ����
            pooledObject = ObjectPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);

            // �Ѿ� Ŭ������ ������Ʈ�� ������ ������ �Ѿ��� �߷��� ���� �÷��̾� �� �տ� ������Ų��.
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
        // Model.�߻���� ������ �ٿ� ����
        Model.ShootSpeed = gauge.value;
        // �߻�
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


