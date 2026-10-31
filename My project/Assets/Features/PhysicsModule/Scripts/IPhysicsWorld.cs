namespace Features.PhysicsModule.Scripts
{
    public interface IPhysicsWorld
    {
        void Register(IPhysicsBody body);
        void Unregister(IPhysicsBody body);
    }
}
