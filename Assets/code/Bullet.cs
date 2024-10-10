using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] PooledObject pooledObject;
    [SerializeField] Rigidbody rb;

    [SerializeField] public float ShotSpeed;
    [SerializeField] float returnTime;
    private float remainTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pooledObject = GetComponent<PooledObject>();
    }

    void OnEnable()
    {
        remainTime = returnTime;
    }

    void Update()
    {
        // 공의 transform과 rotate에서 설정한 transfrom rotation 값을 동일하게 해주어 forward값을 바꿀 수 잇게 해주어야 하는데 어떻게 하지
        // 검색어 : 물체 회전 유니티 어쩌고를 시도해보자
        rb.velocity = ShotSpeed * Vector3.forward;

        // 일정시간이 지나면, 다시 풀에 반납해줄 수 있는 if문을 작성
        remainTime -= Time.deltaTime;
        if (remainTime < 0) { pooledObject.ReturnPool(); }
    }
}
