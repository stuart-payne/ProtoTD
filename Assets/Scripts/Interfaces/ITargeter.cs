namespace ProtoTD.Interfaces
{
    public interface ITargeter
    {
        public void RegisterTarget(Enemy enemy);
        public void DeregisterTarget(Enemy enemy);
    }
}
