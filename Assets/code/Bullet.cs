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

        // �����ð��� ������, �ٽ� Ǯ�� �ݳ����� �� �ִ� if���� �ۼ�
        remainTime -= Time.deltaTime;
        if (remainTime < 0) { pooledObject.ReturnPool(); }
    }
}
