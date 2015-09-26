namespace KaeSoft.Reactive
{
    public interface IStale<out T>
    {
        bool IsStale { get; }
        T Update { get; }
    }
}