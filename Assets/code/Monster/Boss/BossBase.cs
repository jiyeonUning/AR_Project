public interface IbossState
{
    public void Enter();
    public void Update();
    public void Exit();
}

public class BossBase : IbossState
{
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
