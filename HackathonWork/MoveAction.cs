namespace HackathonWork
{
    internal class MoveAction : Action
    {
        public Factory Source { get; private set; }
        public Factory Destination { get; private set; }
        public int Units { get; private set; }

        public MoveAction(Factory source, Factory destination, int units)
        {
            Source = source;
            Destination = destination;
            Units = units;
        }
    }
}