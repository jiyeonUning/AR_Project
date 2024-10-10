using UnityEngine;

public class ShooterModel : MonoBehaviour
{
    [Header("Player Move Status")]

    [SerializeField] float shootSpeed; // 현재 파워값
    public float ShootSpeed { get { return shootSpeed; } set { shootSpeed = value; } }

}
