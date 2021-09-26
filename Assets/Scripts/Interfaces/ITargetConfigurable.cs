namespace ProtoTD.Interfaces
{
    public interface ITargetConfigurable
    {
        Strategy[] GetPossibleStrategies { get; }
        TargetSelector GetTargetSelector { get; }
    }
}