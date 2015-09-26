namespace Andy.Lib.Interfaces
{
    public interface ITranslator<in T, out T1>
    {
        T1 Translate(T value);
    }
}