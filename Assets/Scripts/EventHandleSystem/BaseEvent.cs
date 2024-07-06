namespace EventHandleSystem
{
    public struct ChangeStarEvent
    {
        public readonly int StartNumber;

        public ChangeStarEvent(int num)
        {
            StartNumber = num;
        }
    }
}