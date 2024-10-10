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
        if (input.actions["Move"].WasPressedThisFrame()) { Charge(); }
        if (input.actions["Move"].WasReleasedThisFrame()) { Shot(); }
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

    void Rotate()
    {
        // ���� ���� forward�� ������ �ٲ��� �� �ִ�.
        // ���?
        // �ϴ� UI����
        // ��ġ�ϴ� �������� ���� �����̵�, �޹���� ũ�⸦�Ѿ�� ������ �� ����
        // ��ġ�� ������ õõ�� ���� �������� ���ư��� (������ �ۼ��� Eagle �ڵ� ����)
        // ������ ������ ���콺 �������� �����غ���

        /*
    [SerializeField] PlayerInput input;
    [SerializeField] Rigidbody rigid;

    [SerializeField] float movePower;
    [SerializeField] float jumpPower;

    private void Update()
    {
        // IsPressd = GetKey
        // WasPressedthisFrame = GetKeyDown
        // WasReleasedthisFrame = GetKeyUp

        // ��ǲ �ý����� ���� ������ ����
        Vector2 move = input.actions["Move"].ReadValue<Vector2>();
        Vector3 dir = new Vector3(move.x, 0, move.y);
        rigid.AddForce(dir * movePower, ForceMode.Force);

            velocity ���� �ڵ� ����
            Vector2 move = input.actions["Move"].ReadValue<Vector2>();
            Vector3 dir = new Vector3(move.x, 0, move.y);
            rigid.velocity = dir * movePower + Vector3.up * rigid.velocity.y;
         

        // ��ǲ �ý����� ���� ���� ����
        bool jump = input.actions["Jump"].WasPressedThisFrame();
        if (jump) { rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); }
    }
         
         */

    }

    void Shot()
    {
        Model.ShootSpeed = gauge.value;
        ObjectPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
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


