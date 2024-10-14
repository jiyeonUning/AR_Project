using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public enum MonsterState { Idle, Trace, Attack, Return, Damaged, Dead, Size }
    [SerializeField] MonsterState curState = MonsterState.Idle;

    private MonsterBase[] states = new MonsterBase[(int)MonsterState.Size];
    [SerializeField] IdleState    idleState;
    [SerializeField] TraceState   traceState;
    [SerializeField] AttackState  attackState;
    [SerializeField] ReturnState  returnState;
    [SerializeField] DamagedState damagedState;
    [SerializeField] DeadState    deadState;

    [SerializeField] GameObject player;
    [SerializeField] Animator animator;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");

        states[(int)MonsterState.Idle]    = idleState;
        states[(int)MonsterState.Trace]   = traceState;
        states[(int)MonsterState.Attack]  = attackState;
        states[(int)MonsterState.Return]  = returnState;
        states[(int)MonsterState.Damaged] = damagedState;
        states[(int)MonsterState.Dead]    = deadState;
    }

    private void Start()
    {
        states[(int)curState].Enter();
    }

    private void OnDestroy()
    {
        states[(int)curState].Exit();
    }

    private void Update()
    {
        states[(int)curState].Update();
    }

    public void ChangeState(MonsterState nextState)
    {
        states[(int)curState].Exit();
        curState = nextState;
        states[(int)curState].Enter();
    }

    //=================================================================================================

    [System.Serializable]
    private class IdleState : MonsterBase
    {
        [SerializeField] MonsterController monster;
        [SerializeField] MonsterModel model;


        public override void Enter()
        {
            model.StayPos = monster.transform.position;
        }

        public override void Update()
        {
            // Idle 행동 구현
            Quaternion q = monster.player.transform.rotation;
            // 좌, 아래, 우, 위, 원상태
            monster.transform.rotation = Quaternion.LookRotation(new Vector3(q.x, q.y, q.z));
            monster.animator.Play("move");

            // 다른 상태로 전환
            if (Vector3.Distance(monster.transform.position, monster.player.transform.position) < model.TraceRange) { monster.ChangeState(MonsterState.Trace); }
        }
    }

    //=================================================================================================

    [System.Serializable]
    private class TraceState : MonsterBase
    {
        [SerializeField] MonsterController monster;
        [SerializeField] MonsterModel model;


        public override void Update()
        {
            // Trace 행동 구현
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, monster.player.transform.position, model.MoveSpeed * Time.deltaTime);

            // 다른 상태로 전환
            if (Vector3.Distance(monster.transform.position, monster.player.transform.position) > model.TraceRange) { monster.ChangeState(MonsterState.Return); }
            else if (Vector3.Distance(monster.transform.position, monster.player.transform.position) < model.AttackRange) { monster.ChangeState(MonsterState.Attack); }
        }
    }

    //=================================================================================================

    [System.Serializable]
    private class AttackState : MonsterBase
    {
        [SerializeField] MonsterController monster;
        [SerializeField] MonsterModel model;
        [SerializeField] Quaternion playerRot;


        public override void Enter()
        {
            monster.player.transform.rotation = playerRot;
            model.IsAttackeed = true;
        }

        public override void Update()
        {
            // Attack 행동 구현
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, monster.player.transform.position + new Vector3(0, 0, -1), model.MoveSpeed * Time.deltaTime);
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, monster.player.transform.position, model.MoveSpeed * Time.deltaTime);

            if (monster.transform.position == monster.player.transform.position)
            {
                monster.player.transform.rotation = Quaternion.Euler(playerRot.x + 180, playerRot.y + 0, playerRot.z + 0);
                monster.player.transform.rotation = Quaternion.Euler(playerRot.x + -180, playerRot.y + 0, playerRot.z + 0);
            }

            // 다른 상태로 전환
            if (Vector3.Distance(monster.transform.position, monster.player.transform.position) > model.TraceRange) { monster.ChangeState(MonsterState.Return); }

        }

        public override void Exit()
        {
            model.IsAttackeed = false;
        }
    }

    //=================================================================================================

    [System.Serializable]
    private class ReturnState : MonsterBase
    {
        [SerializeField] MonsterController monster;
        [SerializeField] MonsterModel model;


        public override void Update()
        {
            // Return 행동 구현
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, model.StayPos / 2, model.MoveSpeed * Time.deltaTime);

            // 다른 상태로 전환
            if (Vector3.Distance(monster.transform.position, monster.player.transform.position) < model.AttackRange) { monster.ChangeState(MonsterState.Attack); }
            else if (Vector3.Distance(monster.transform.position, model.StayPos) < 0.01f) { monster.ChangeState(MonsterState.Idle); }
        }
    }

    //=================================================================================================

    [System.Serializable]
    private class DamagedState : MonsterBase
    {
        [SerializeField] MonsterController monster;
        [SerializeField] MonsterModel model;

        [SerializeField] Vector3 curPos;
        [SerializeField] Quaternion curRot;


        public override void Enter()
        {
            curPos = monster.transform.position;
            curRot = monster.transform.rotation;
            model.IsDamaged = true;
        }

        public override void Update()
        {
            // Damaged 행동 구현
            if (model.IsDamaged)
            {
                monster.transform.position = Vector3.MoveTowards(monster.transform.position, curPos, model.MoveSpeed * Time.deltaTime);
                monster.transform.rotation = Quaternion.Euler(new Vector3(curRot.x, curRot.y, curRot.z));
                model.HP -= model.Damage;
            }
            model.IsDamaged = false;

            // 다른 상태로 전환
            if (model.HP < 0) { monster.ChangeState(MonsterState.Dead); }
            else { monster.ChangeState(MonsterState.Idle); }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ChangeState(MonsterState.Damaged);
    }

    //=================================================================================================

    [System.Serializable]
    private class DeadState : MonsterBase
    {
        [SerializeField] MonsterController monster;
        [SerializeField] MonsterModel model;


        public override void Enter()
        {
            model.IsDead = true;
        }

        public override void Update()
        {
            // Dead 행동 구현
            if (model.IsDead == true) { Destroy(monster); }
        }
    }

    //=================================================================================================
}
