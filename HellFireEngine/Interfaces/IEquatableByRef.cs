namespace HellFireEngine.Interfaces
{
    public interface IEquatableByRef<T>
    {
        bool Equals(ref T other);
    }
}
