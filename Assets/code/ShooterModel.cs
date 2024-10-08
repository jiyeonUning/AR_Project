using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterModel : MonoBehaviour
{
    [Header("Player Move Status")]

    [SerializeField] float shootSpeed; // 현재 파워값
    public float ShootSpeed { get { return shootSpeed; } set { shootSpeed = value; } }


    [SerializeField] float minShotSpeed = 5f; // 최소 파워값
    public float MinShotSpeed { get { return minShotSpeed; } set { minShotSpeed = value; } }


    [SerializeField] float maxShotSpeed = 15f; // 최대 파워값
    public float MaxShotSpeed { get { return maxShotSpeed; } set { maxShotSpeed = value; } }
}
