using UnityEngine;

public class ShooterModel : MonoBehaviour
{
    [Header("Player Move Status")]

    [SerializeField] float shootSpeed; // 현재 파워값
    public float ShootSpeed { get { return shootSpeed; } set { shootSpeed = value; } }


    [SerializeField] float damage; // 데미지 값 (데미지값 = 파워값)
    public float Damage { get { return damage; } set { damage = shootSpeed; } }

}
