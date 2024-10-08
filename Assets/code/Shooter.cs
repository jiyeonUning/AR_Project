using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] ObjectPool ObjectPool;
    [SerializeField] ShooterModel Model;
    [SerializeField] public Transform muzzlePoint;

    private Touch touch;


    private void Start()
    {
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
            // ������ �� ���� ����       
        }


        if (touch.phase == TouchPhase.Moved)
        {
            //���� �������� ����
        }
    }

    void Shot()
    {
        ObjectPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
    }

}

