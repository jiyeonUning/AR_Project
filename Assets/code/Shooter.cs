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
        // 공을 돌려 forward의 방향을 바꿔줄 수 있다.
        // 어떻게?
        // 일단 UI부터
        // 터치하는 방향으로 공이 움직이되, 뒷배경의 크기를넘어서서 움직일 순 없다
        // 터치가 끝나면 천천히 원래 방향으로 돌아간다 (예전에 작성한 Eagle 코드 참고)
        // 예전에 구성한 마우스 움직임을 참고해보자

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

        // 인풋 시스템을 통항 움직임 세팅
        Vector2 move = input.actions["Move"].ReadValue<Vector2>();
        Vector3 dir = new Vector3(move.x, 0, move.y);
        rigid.AddForce(dir * movePower, ForceMode.Force);

            velocity 구현 코드 예제
            Vector2 move = input.actions["Move"].ReadValue<Vector2>();
            Vector3 dir = new Vector3(move.x, 0, move.y);
            rigid.velocity = dir * movePower + Vector3.up * rigid.velocity.y;
         

        // 인풋 시스템을 통한 점프 세팅
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

