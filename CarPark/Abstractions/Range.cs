namespace CarPark.Abstractions
{
    public abstract class Range<T> where T : struct
    {
        protected Range()
        {
        }

        protected Range(T min, T max)
        {
            Min = min;
            Max = max;
        }

        public T Max { get; set; }
        public T Min { get; set; }
    }
}