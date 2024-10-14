using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterModel : MonoBehaviour
{
    [Header("Monster Status")]

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }


    [SerializeField] float monsterPower;
    public float MonsterPower { get { return monsterPower; } set { monsterPower = value; } }


    [SerializeField] Vector3 stayPos; // 대기할 기준위치
    public Vector3 StayPos { get { return stayPos; } set { stayPos = value; } }


    [SerializeField] float traceRange;  // 따라가기 사정거리
    public float TraceRange { get { return traceRange; } set { traceRange = value; } }


    [SerializeField] float attackRange; // 공격 사정거리
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }


    [SerializeField] bool isAttacked;
    public bool IsAttackeed { get { return isAttacked; } set { isAttacked = value; } }


    [SerializeField] bool isDamaged;
    public bool IsDamaged { get { return isDamaged; } set { isDamaged = value; } }


    [SerializeField] bool isDead;
    public bool IsDead { get { return isDead; } set { isDead = value; } }


    [SerializeField] int hp;
    public int HP { get { return hp; } set { hp = value; } }


    [SerializeField] int damage;
    public int Damage { get { return damage; } set { damage = value; } }

    private void Awake()
    {
        moveSpeed = 7f;
        monsterPower = 5f;

        traceRange = 5f;
        attackRange = 2f;

        isAttacked = false;
        isDamaged = false;
        isDead = false;

        hp = 50;
        damage = 10;
    }
}
