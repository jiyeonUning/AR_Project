using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class Shooter : MonoBehaviour
{
    [SerializeField] ObjectPool ObjectPool;
    [SerializeField] ShooterModel Model;
    [SerializeField] public Transform muzzlePoint;

    private Touch touch;

    [SerializeField] Slider gauge;


    private void Awake()
    {
        gauge = GetComponent<Slider>();
        Model = GetComponent<ShooterModel>();
        ObjectPool = GetComponent<ObjectPool>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Charge();
                    break;
                case TouchPhase.Ended:
                    Shot();
                    break;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Charge();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
    }



    void Charge()
    {
        if (touch.phase == TouchPhase.Stationary)
        {
            gauge.value += Time.deltaTime * 1.2f;

            if (touch.phase == TouchPhase.Ended) return;
            else if (gauge.value > Model.MaxShotSpeed) { gauge.value = Model.MaxShotSpeed; }
        }
        if (touch.phase == TouchPhase.Moved) { Rotate(); }
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
        ObjectPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
        gauge.value = Model.MinShotSpeed;
    }

}

