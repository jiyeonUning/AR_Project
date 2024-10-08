using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] PooledObject prefab;
    [SerializeField] bool createOnEmpty = true;
    [SerializeField] int size;
    [SerializeField] int capacity;

    private Stack<PooledObject> pool;

    void Awake()
    {
        pool = new Stack<PooledObject>(capacity);

        for (int i = 0; i < size; i++)
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.Pool = this;
            pool.Push(instance);
        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation)
    {
        if (pool.Count > 0)
        {
            PooledObject instance = pool.Pop();
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.gameObject.SetActive(true);
            return instance;
        }
        else if (createOnEmpty)
        {
            PooledObject instance = Instantiate(prefab);
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.Pool = this;
            return instance;
        }
        else
        {
            return null;
        }
    }

    public void ReturnPool(PooledObject instance)
    {
        if (pool.Count < capacity)
        {
            instance.gameObject.SetActive(false);
            pool.Push(instance);
        }
        else { Destroy(instance.gameObject); }
    }
}
