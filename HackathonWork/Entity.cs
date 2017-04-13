namespace HackathonWork
{
    public delegate int GetId();

    public abstract class Entity
    {
 
        protected int _id;
        protected EntityType _type;

        public Entity(EntityType type, GetId getId )
        {
            _type = type;
            _id = getId();
         //   _id = unqueEntiyId++;
        }

        public abstract string ToPlayerString(int playerIdx);

        protected string ToPlayerString(int arg1, int arg2, int arg3, int arg4, int arg5)
        {
            return $"{_id} {_type} {arg1} {arg2} {arg3} {arg4} {arg5}";
        }

        public int Id { get { return _id; } }

    }
}