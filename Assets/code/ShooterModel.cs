using UnityEngine;

public class ShooterModel : MonoBehaviour
{
    [Header("Player Move Status")]

    [SerializeField] float shootSpeed; // ���� �Ŀ���
    public float ShootSpeed { get { return shootSpeed; } set { shootSpeed = value; } }


    [SerializeField] float damage; // ������ �� (�������� = �Ŀ���)
    public float Damage { get { return damage; } set { damage = shootSpeed; } }

}
