using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterModel : MonoBehaviour
{
    [Header("Player Move Status")]

    [SerializeField] float shootSpeed; // ���� �Ŀ���
    public float ShootSpeed { get { return shootSpeed; } set { shootSpeed = value; } }


    [SerializeField] float minShotSpeed = 5f; // �ּ� �Ŀ���
    public float MinShotSpeed { get { return minShotSpeed; } set { minShotSpeed = value; } }


    [SerializeField] float maxShotSpeed = 15f; // �ִ� �Ŀ���
    public float MaxShotSpeed { get { return maxShotSpeed; } set { maxShotSpeed = value; } }
}
