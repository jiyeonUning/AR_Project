using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterModel : MonoBehaviour
{
    [SerializeField] Slider gauge;

    [Header("Player Move Status")]

    [SerializeField] float shootSpeed; // 현재 파워값
    public float ShootSpeed { get { return shootSpeed; } set { shootSpeed = gauge.value; } }


    [SerializeField] float minShotSpeed = 5f; // 최소 파워값
    public float MinShotSpeed { get { return minShotSpeed; } set { minShotSpeed = value; } }


    [SerializeField] float maxShotSpeed = 15f; // 최대 파워값
    public float MaxShotSpeed { get { return maxShotSpeed; } set { maxShotSpeed = value; } }

    private void Start()
    {
        gauge = GetComponent<Slider>();
    }
}
