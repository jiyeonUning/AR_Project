using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] PooledObject pooledObject;
    [SerializeField] Rigidbody rb;
    [SerializeField] ShooterModel ShooterModel;

    [SerializeField] float returnTime;
    private float remainTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pooledObject = GetComponent<PooledObject>();
        ShooterModel = GetComponent<ShooterModel>();
    }

    void OnEnable()
    {
        remainTime = returnTime;
    }

    void Update()
    {
        rb.velocity = ShooterModel.ShootSpeed * transform.forward;

        // 일정시간이 지나면, 다시 풀에 반납해줄 수 있는 if문을 작성
        remainTime -= Time.deltaTime;
        if (remainTime < 0) { pooledObject.ReturnPool(); }
    }
}
