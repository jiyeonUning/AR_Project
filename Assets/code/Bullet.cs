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
        // ���� transform�� rotate���� ������ transfrom rotation ���� �����ϰ� ���־� forward���� �ٲ� �� �հ� ���־�� �ϴµ� ��� ����
        // �˻��� : ��ü ȸ�� ����Ƽ ��¼�� �õ��غ���
        rb.velocity = ShotSpeed * Vector3.forward;

        // �����ð��� ������, �ٽ� Ǯ�� �ݳ����� �� �ִ� if���� �ۼ�
        remainTime -= Time.deltaTime;
        if (remainTime < 0) { pooledObject.ReturnPool(); }
    }
}
