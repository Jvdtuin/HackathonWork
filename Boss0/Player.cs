using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
public class Player
{
    public static Dictionary<int, Factory> Factories;

    public static List<Troop> Troops;

    public static List<Bomb> Bombs;

    public static int RemainingBombs = 0;

    static void Main(string[] args)
    {
        GameInitialize();
        // game loop
        while (true)
        {
            TurnInitialize();

            List<Move> moves = DetermenMoves();

            TurnOutput(moves);
        }
    }

    static void GameInitialize()
    {
        string[] inputs;
        int factoryCount = int.Parse(Console.ReadLine()); // the number of factories
        int linkCount = int.Parse(Console.ReadLine()); // the number of links between factories

        // load the links        
        Link[] links = new Link[linkCount];
        for (int i = 0; i < linkCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int factory1 = int.Parse(inputs[0]);
            int factory2 = int.Parse(inputs[1]);
            int distance = int.Parse(inputs[2]);
            links[i] = new Link(factory1, factory2, distance);
        }

        // build the factorie's 
        Factories = new Dictionary<int, Factory>();

        for (int i = 0; i < factoryCount; i++)
        {
            Factories.Add(i, new Factory(i, links));
        }

    }

    static void TurnInitialize()
    {
        Troops = new List<Troop>();
        Bombs = new List<Bomb>();
        int entityCount = int.Parse(Console.ReadLine()); // the number of entities (e.g. factories and troops)
        for (int i = 0; i < entityCount; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int entityId = int.Parse(inputs[0]);
            string entityType = inputs[1];
            int arg1 = int.Parse(inputs[2]);
            int arg2 = int.Parse(inputs[3]);
            int arg3 = int.Parse(inputs[4]);
            int arg4 = int.Parse(inputs[5]);
            int arg5 = int.Parse(inputs[6]);
            if (entityType == "FACTORY")
            {
                Factory f = Factories[entityId];
                f.Update(arg1, arg2, arg3);

            }
            if (entityType == "TROOP")
            {
                Troop troop = new Troop(entityId)
                {
                    Owner = arg1,
                    Source = arg2,
                    Target = arg3,
                    CyborgCount = arg4,
                    RemaingTurns = arg5,
                };
                Troops.Add(troop);
            }
            if (entityType == "BOMB")
            {
                Bomb bomb = new Bomb(entityId)
                {
                    Owner = arg1,
                    Source = arg2,
                    Target = arg3,
                    RemaingTurns = arg4,
                };
                Bombs.Add(bomb);
            }
        }
    }

    static List<Move> DetermenMoves()
    {
        List<Move> moves = new List<Move>();
        List<Factory> myFactories = Factories.Where(f => f.Value.Owner == 1).Select(f => f.Value).ToList();

        myFactories.Sort((f1, f2) =>
        {
            if (f1.CyborgCount < f2.CyborgCount) { return 1; }
            if (f1.CyborgCount > f2.CyborgCount) { return -1; }
            return 0;
        });

        Factory factory = myFactories.FirstOrDefault();
        if (factory != null)
        {
            List<FactoryLink> others = factory.Links.Where(f => f.Target.Owner != 1).ToList();

            others.Sort((f1, f2) => 
            {
                if (f1.Score < f2.Score ) { return -1; }
                if (f1.Score > f2.Score) { return 1; }
                return 0;
            });

            FactoryLink fl = others.FirstOrDefault();
            if (fl != null)
            {
                Move m = new Move()
                {
                    SourceId = factory.Id,
                    Target = fl.Target.Id,
                    Troops = factory.CyborgCount,
                };
                moves.Add(m);

            }
        }
        return moves;
    }

    static void TurnOutput(List<Move> moves)
    {
        if (moves.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            string separator = "";
            foreach (Move move in moves)
            {
                sb.Append($"{separator}{move}");
                separator = ";";
            }
            Console.WriteLine(sb.ToString());
        }
        else
        {
            Console.WriteLine("WAIT");
        }
    }
}

public abstract class Entity
{
    protected Entity(int id)
    {
        Id = id;
    }

    public int Id { get; }

    public int Owner { get; set; }
}


public class Factory : Entity
{
    private int? _surplusCyborgs;

    private List<FactoryLink> _links;

    public Factory(int id, Link[] links) : base(id)
    {
        List<FactoryLink> flinks = new List<FactoryLink>();

        foreach (Link l in links)
        {
            if (l.FactoryId1 == id)
            {
                flinks.Add(new FactoryLink(l.FactoryId2, l.Distance));
            }
            if (l.FactoryId2 == id)
            {
                flinks.Add(new FactoryLink(l.FactoryId1, l.Distance));
            }

        }

        _links = flinks;
    }

    public int CyborgCount { get; private set; }

    public int Production { get; private set; }

    public List<FactoryLink> Links
    {
        get
        {
            return _links;
        }
    }



    public int SurplusCyborgs
    {
        get
        {
            if (!_surplusCyborgs.HasValue)
            {
                int maxTurns = 0;
                int inbound = 0;
                foreach (Troop troop in Player.Troops.Where(t => t.Target == Id && t.Owner != Owner))
                {
                    inbound += troop.CyborgCount * troop.Owner;
                    if (troop.RemaingTurns > maxTurns)
                    {
                        maxTurns = troop.RemaingTurns;
                    }
                }
                _surplusCyborgs = this.CyborgCount + inbound + maxTurns * this.Production;
            }
            return _surplusCyborgs.Value;
        }
    }

    internal void Update(int owner, int cyborgCount, int production)
    {
        Owner = owner;
        CyborgCount = cyborgCount;
        Production = production;
        _surplusCyborgs = null;
    }
}

public class Troop : Entity
{
    public Troop(int id) : base(id)
    {

    }

    public int Source { get; set; }
    public int Target { get; set; }
    public int CyborgCount { get; set; }
    public int RemaingTurns { get; set; }
}

public class Bomb : Entity
{
    public Bomb(int id) : base(id)
    {
    }

    public int Source { get; set; }
    public int Target { get; set; }
    public int RemaingTurns { get; set; }

}

public class Link
{
    public Link(int f1, int f2, int d)
    {
        FactoryId1 = f1;
        FactoryId2 = f2;
        Distance = d;
    }

    public int FactoryId1 { get; }

    public int FactoryId2 { get; }

    public int Distance { get; }
}

public class FactoryLink
{
    private int _targetId;

    private int _distanace;

    public FactoryLink(int target, int distance)
    {
        _targetId = target;
        _distanace = distance;
    }

    public Factory Target
    {
        get
        {
            return Player.Factories[_targetId];
        }
    }
    public int Distance { get { return _distanace; } }

    public double Score
    {
        get
        {

            double d = _distanace;
            double p = Target.Production;
            double c = Math.Abs(p * Target.Owner) * d + Target.CyborgCount;
            double g = Math.Max(0.01, Target.Production);
           
            return c / g + d / g + d;
        }

    }
}

public class Move
{
    public int SourceId { get; set; }
    public int Target { get; set; }
    public int Troops { get; set; }
    public bool SendBomb { get; set; }

    public override string ToString()
    {
        if (SendBomb)
        {
            return $"BOMB {SourceId} {Target}";
        }
        return $"MOVE {SourceId} {Target} {Troops}";
    }
}
