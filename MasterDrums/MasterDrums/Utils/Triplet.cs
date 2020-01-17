namespace MasterDrums.Utils
{
    /// <summary>
    /// Class representing a tuple of three objects (a triplet)
    /// </summary>
    /// <typeparam name="T1">Type of the first object</typeparam>
    /// <typeparam name="T2">Type of the second object</typeparam>
    /// <typeparam name="T3">Type of the third object</typeparam>
    class Triplet<T1, T2, T3>
    {
        public Triplet(T1 i1, T2 i2, T3 i3)
        {
            this.Item1 = i1;
            this.Item2 = i2;
            this.Item3 = i3;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
    }
}
