using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] PooledObject pooledObject;
    private Rigidbody rb;

    [SerializeField] float returnTime;
             private float remainTime;

    private void Awake()
    {
        pooledObject = GetComponent<PooledObject>();    
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable() { remainTime = returnTime; }

    private void OnDisable() { Init(); }

    void Update()
    {
        remainTime -= Time.deltaTime;
        if (remainTime < 0) { pooledObject.ReturnPool(); }
    }

    void Init() { rb.velocity = Vector3.zero; }
}
