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

    public static int RemainingBombs = 2;

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
            //         Console.Error.WriteLine(links[i]);
        }

        // build the factorie's 
        Factories = new Dictionary<int, Factory>();

        for (int i = 0; i < factoryCount; i++)
        {
            Factories.Add(i, new Factory(i, links));
        }
        for (int i = 0; i < factoryCount; i++)
        {
            //    Console.Error.WriteLine($"Factory {i}");
            foreach (FactoryLink fl in Factories[i].Links)
            {
                //    Console.Error.WriteLine($"Link to {fl.Target.Id} dist {fl.Distance}");

                List<int> steps = new List<int>();
                steps.Add(i);
                List<Route> routes = new List<Route>();
                int distance = fl.Distance;
                SearchShorterRoutes(routes, steps, fl.Target.Id, 0, ref distance);
                fl.AlternativeRoutes = new List<Route>();
                if (routes.Count > 0)
                {
                    routes.Sort((r1, r2) =>
                    {
                        if (r1.Distance > r2.Distance) { return 1; }
                        if (r1.Distance < r2.Distance) { return -1; }
                        if (r1.Steps.Count < r2.Steps.Count) { return 1; }
                        if (r1.Steps.Count > r2.Steps.Count) { return -1; }
                        return 0;
                    });
                    Route route = routes.First();
                    fl.AlternativeRoutes.Add(route);
                    //   routes = routes.Where(r=>r.Steps[0] != route.Steps[0]).ToList();
                }
            }
        }

    }

    static void SearchShorterRoutes(List<Route> routes, List<int> steps, int endId, int routedistance, ref int directDistance)
    {
        int f = steps.Last();
        foreach (FactoryLink fl in Factories[f].Links.Where(l => !steps.Contains(l.Target.Id)))
        {
            if (fl.Distance + routedistance < directDistance)
            {
                if (fl.Target.Id == endId)
                {
                    // create alternative route

                    Route r = new Route();
                    r.Distance = fl.Distance + routedistance;
                    r.Steps = new List<int>();
                    foreach (int s in steps)
                    {
                        r.Steps.Add(s);
                    }
                    r.Steps.RemoveAt(0);
                    routes.Add(r);
                    directDistance = r.Distance;
                }
                else
                {
                    steps.Add(fl.Target.Id);
                    SearchShorterRoutes(routes, steps, endId, fl.Distance + routedistance, ref directDistance);
                    steps.Remove(fl.Target.Id);
                }
            }
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
        List<Factory> myFactories = Factories.Where(f => f.Value.Owner == 1).Select(f => f.Value).ToList();
        List<Move> moves = new List<Move>();

        foreach (Factory f in myFactories)
        {
            Console.Error.Write($"({f.Id})");
            for (int i = 0; i < 20; i++)
            {
                Console.Error.Write($" {f.CyborgPredictionCount[i]}");
            }
            Console.Error.WriteLine();
            int surplus = f.SurplusCyborgs;
            f.Links.Sort((l1, l2) =>
            {
                if (l1.Score > l2.Score) { return -1; }
                if (l1.Score < l2.Score) { return 1; }

                return 0;
            });
            List<Move> factoryMoves = new List<Move>();
            if ((f.Production < 3) && (surplus > 10))
            {
                FactoryLink fl = f.Links.First();
                if (fl.Target.Owner == 1)
                {
                    moves.Add(new Move() { SourceId = f.Id, Inc = true, });
                    surplus -= 10;
                }
            }

            foreach (FactoryLink fl in f.Links)
            {
                bool sendBomb = false;
                if (RemainingBombs > 1)
                {
                    sendBomb = (fl.Target.Owner == -1)

                         && (fl.Target.Production >= 2);
                }
                else if (RemainingBombs > 0)
                {
                    sendBomb = (fl.Target.Owner == -1) && (fl.Target.Production == 3) && (fl.Target.CyborgCount > 50);
                }

                if (!sendBomb)
                {
                    if (surplus > 0)
                    {
                        if (fl.Target.Owner != 1)
                        {
                            int neededTroops = fl.Target.CyborgCount + fl.Distance * fl.Target.Production;
                            int troops = (neededTroops < surplus) ? neededTroops + 1 : surplus;
                            int target = fl.Target.Id;
                            if (fl.AlternativeRoutes.Count > 0)
                            {
                                target = fl.AlternativeRoutes.First().Steps.First();
                            }
                            factoryMoves.Add(new Move() { SourceId = f.Id, Target = target, Troops = troops });
                            surplus -= troops;
                        }
                        else
                        {
                            if (fl.Target.SurplusCyborgs < 0)
                            {
                                int troops = (-fl.Target.SurplusCyborgs < surplus) ? -fl.Target.SurplusCyborgs + 1 : surplus;
                                int target = fl.Target.Id;
                                if (fl.AlternativeRoutes.Count > 0)
                                {
                                    int alternativesIndex = 0;
                                    while ((alternativesIndex < fl.AlternativeRoutes.Count)
                                        && Factories[fl.AlternativeRoutes[alternativesIndex].Steps.First()].Owner == -1)
                                    {
                                        alternativesIndex++;
                                    }
                                    if (alternativesIndex < fl.AlternativeRoutes.Count)
                                    {
                                        target = fl.AlternativeRoutes[alternativesIndex].Steps.First();
                                    }
                                }
                                factoryMoves.Add(new Move() { SourceId = f.Id, Target = target, Troops = troops });
                                surplus -= troops;
                            }
                        } //*/
                    }
                }
                else
                {
                    moves.Add(new Move() { SourceId = f.Id, Target = fl.Target.Id, SendBomb = true, });
                    RemainingBombs--;
                }
            }
            factoryMoves.Sort((fm1, fm2) =>
            {
                if (fm1.Target < fm2.Target) { return -1; }
                if (fm1.Target > fm2.Target) { return 1; }
                return 0;
            });
            int trc = 0;
            int t = -1;
            foreach (Move m in factoryMoves)
            {
                if ((t != -1) && (t != m.Target))
                {
                    moves.Add(new Move() { SourceId = m.SourceId, Target = t, Troops = trc });
                    trc = 0;
                }
                trc += m.Troops;
                t = m.Target;
            }
            if (t != -1)
            {
                moves.Add(new Move() { SourceId = f.Id, Target = t, Troops = trc });
                trc = 0;
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
    private bool _cyborPredictionCalculted;


    private List<FactoryLink> _links;
    private int[] _cyborPrediction = new int[20];


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


    public int[] CyborgPredictionCount
    {
        get
        {
            if (!_cyborPredictionCalculted)
            {
                List<Troop> inboundTroops = Player.Troops.Where(t => t.Target == Id).OrderBy(t => t.RemaingTurns).ToList();

                int ic = 0;
                int count = CyborgCount;
                int powner = Owner;
                _cyborPrediction[0] = count;

                for (int i = 1; i < 20; i++)
                {
                    count += Production * powner;
                    while (ic < inboundTroops.Count && inboundTroops[ic].RemaingTurns == i)
                    {
                        count += inboundTroops[ic].CyborgCount * inboundTroops[ic].Owner;
                        ic++;
                    }
                    _cyborPrediction[i] = count;
                }
                _cyborPredictionCalculted = true;
            }
            return _cyborPrediction;
        }
    }


    public int SurplusCyborgs
    {
        get
        {
            if (!_surplusCyborgs.HasValue)
            {
                int maxTurns = 0;
                int production = 0;
                int inbound = 0;
                foreach (Troop troop in Player.Troops.Where(t => t.Target == Id && t.Owner != Owner))
                {
                    inbound += troop.CyborgCount * troop.Owner;
                    if (troop.RemaingTurns > maxTurns)
                    {
                        //production += (troop.RemaingTurns - maxTurns) *Production;
                        maxTurns = troop.RemaingTurns;
                    }

                }
                _surplusCyborgs = this.CyborgCount + inbound + production;
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
        _cyborPredictionCalculted = false;
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

    public override string ToString()
    {
        return $"{FactoryId1} {FactoryId2} {Distance}";
    }
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
            return p / d;
        }
    }

    public List<Route> AlternativeRoutes { get; set; }
}

public class Route
{
    public int Distance { get; set; }

    public List<int> Steps { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"dist: {Distance} steps:");
        foreach (int s in Steps)
        {
            sb.Append($"{s} ");
        }
        return sb.ToString();
    }
}

public class Move
{
    public int SourceId { get; set; }
    public int Target { get; set; }
    public int Troops { get; set; }
    public bool SendBomb { get; set; }
    public bool Inc { get; set; }

    public override string ToString()
    {
        if (Inc)
        {
            return $"INC {SourceId}";
        }

        if (SendBomb)
        {
            return $"BOMB {SourceId} {Target}";
        }
        return $"MOVE {SourceId} {Target} {Troops}";
    }
}
