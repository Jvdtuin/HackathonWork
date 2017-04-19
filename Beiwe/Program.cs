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
class Player
{
    public static List<Factory> FactoryList = new List<Factory>();
    static List<Troop> TroopList = new List<Troop>();
    public static List<Link> LinkList = new List<Link>();

    static void Main(string[] args)
    {
        string[] inputs;
        bool firstLoop = true;
        int bomb = 2;
        int bombId = 0;

        int factoryCount = int.Parse(Console.ReadLine()); // the number of factories

        int linkCount = int.Parse(Console.ReadLine()); // the number of links between factories
        for (int i = 0; i < linkCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int factory1 = int.Parse(inputs[0]);
            int factory2 = int.Parse(inputs[1]);
            int distance = int.Parse(inputs[2]);
            Factory f1 = FactoryList.FirstOrDefault(x => x.Id == factory1);
            if (f1 == null)
            {
                f1 = new Factory(factory1);
                FactoryList.Add(f1);
            }
            Factory f2 = FactoryList.FirstOrDefault(x => x.Id == factory2);
            if (f2 == null)
            {
                f2 = new Factory(factory2);
                FactoryList.Add(f2);
            }
            f1.Links.Add(new Link(f2, distance));
            f2.Links.Add(new Link(f1, distance));
        }


        // game loop
        while (true)
        {
            int entityCount = int.Parse(Console.ReadLine()); // the number of entities (e.g. factories and troops)
            bool increaseActive = false;
            bool bombActive = false;
            var separator = "";

            TroopList.Clear();
            for (int i = 0; i < entityCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int entityId = int.Parse(inputs[0]);
                string entityType = inputs[1];
                int arg1 = int.Parse(inputs[2]);
                int arg2 = int.Parse(inputs[3]);
                int arg3 = int.Parse(inputs[4]);
                int arg4 = int.Parse(inputs[5]);
                int arg5 = int.Parse(inputs[6]);

                if (entityType == "FACTORY")
                {
                    FactoryList.FirstOrDefault(x => x.Id == entityId).Update(arg1, arg2, arg3);
                }
                else if (entityType == "TROOP")
                {
                    TroopList.Add(new Troop(entityId, arg1, arg2, arg3, arg4, arg5));
                }
                else if (entityType == "BOMB")
                {
                    Console.Error.WriteLine($"bomb id {arg2} current bombid {bombId}");
                    if(arg1 != 1 && bombId == 0)
                    {
                        bombId = arg2;
                    }
                    else if(bomb > 0 != true && arg1 != 1 && arg2 != bombId && bombId != 0)
                    {
                        Factory f1 = FactoryList.FirstOrDefault(x => x.Id == factoryCount - 1);
                        Factory f2 = FactoryList.FirstOrDefault(x => x.Id == (factoryCount - 2));

                        if (f1.Owner != f2.Owner && f1.Owner != 0 && f2.Owner != 0)
                        {
                            int source = f1.Owner == 1 ? f1.Id : f2.Id;
                            int destination = f2.Owner == -1 ? f2.Id : f1.Id;

                            Console.Write(separator);
                            Console.Write($"BOMB {source} {destination}");
                            separator = ";";
                            bombActive = true;
                            bombId = arg2;
                        }
                    }
                }
            }

            foreach (Factory factory in FactoryList)
            {
                factory.Incomming = TroopList.Where(x => x.FactoryDestination == factory.Id).ToList();
                factory.Outgoing = TroopList.Where(x => x.FactoryOrigin == factory.Id).ToList();
            }

            List<Move> moveList = new List<Move>();

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            List<Factory> neutralF = FactoryList.Where(x => x.Owner == 0).ToList();
            List<Factory> myF = FactoryList.Where(x => x.Owner == 1).ToList();

            neutralF.Sort((x1, x2) =>
            {
                if (x1.Production > x2.Production) { return -1; }
                if (x1.Production < x2.Production) { return 1; }
                return 0;
            });

            myF.Sort((x1, x2) =>
            {
                if (x1.CyborgNumber > x2.CyborgNumber) { return -1; }
                if (x1.CyborgNumber < x2.CyborgNumber) { return 1; }
                return 0;
            });
            foreach (Factory f in myF)
            {
                f.Links.Sort((x1, x2) =>
                {
                    double s1 = x1.score();
                    double s2 = x2.score();
                    if(s1 > s2) { return -1; }
                    if (s1 < s2) { return 1; }
                    return 0;
                });
                int surplus = f.Surplus();
                foreach(Link l in f.Links)
                {
                    if (surplus >= (l.Factory.CyborgNumber) && l.Factory.Owner == 0 && surplus > (2 * l.Distance)  && l.Factory.Production > 0 && l.Distance < 8)
                    {
                        int sentTroopsNr = Math.Min(surplus, (l.Factory.CyborgNumber + l.Factory.Production + 2));
                        moveList.Add(new Move() { SourceFactory = f.Id, DestinationFactory = l.Factory.Id, TroopCount = (l.Factory.CyborgNumber + 1) });
                        surplus -= sentTroopsNr;
                    }
                    else if (surplus >= (l.Factory.CyborgNumber) && l.Factory.Owner == 0 && surplus > (4 * l.Distance)  && l.Factory.Production == 0 && l.Distance < 8)
                    {
                        int sentTroopsNr = Math.Min(surplus, (l.Factory.CyborgNumber + l.Factory.Production + 2));
                        moveList.Add(new Move() { SourceFactory = f.Id, DestinationFactory = l.Factory.Id, TroopCount = (l.Factory.CyborgNumber + 1) });
                        surplus -= sentTroopsNr;
                    }
                    else if (surplus >= (l.Factory.CyborgNumber) && l.Factory.Owner == -1 && surplus > (3 * l.Distance) && l.Distance < 8)
                    {
                        int sentTroopsNr = Math.Min(surplus, (l.Factory.CyborgNumber + l.Factory.Production + 5));
                        moveList.Add(new Move() { SourceFactory = f.Id, DestinationFactory = l.Factory.Id, TroopCount = (l.Factory.CyborgNumber + 1) });
                        surplus -= sentTroopsNr;
                    }
                    else if(surplus > (2 * l.Distance)  && l.Factory.Owner == 1 && l.Factory.Surplus() < 0  && l.Distance < 8)
                    {
                        int sentTroopsNr = Math.Min(surplus, (-1 * l.Factory.Surplus()));
                        moveList.Add(new Move() { SourceFactory = f.Id, DestinationFactory = l.Factory.Id, TroopCount = sentTroopsNr});
                        surplus -= sentTroopsNr;
                    }
                    else if (surplus > (0.5 * l.Factory.Surplus() * l.Distance) && l.Factory.Owner == 1 && l.Distance < 8)
                    {
                        int sentTroopsNr = (int)0.5 * surplus;
                        moveList.Add(new Move() { SourceFactory = f.Id, DestinationFactory = l.Factory.Id, TroopCount = sentTroopsNr });
                    }
                }
                //Console.Error.WriteLine($"factory {f.Id} surplus {surplus} ownership {f.Ownership} production {f.Production}");
                if(surplus > 11 && f.Ownership > 2 && f.Production < 3)
                {
                    Console.Write(separator);
                    Console.Write($"INC {f.Id}");
                    separator = ";";
                    increaseActive = true;
                }
            }


            if (moveList.Count > 0)
            {
                
                foreach(Move m in moveList)
                {
                    Console.Write(separator);
                    Console.Write(m.ToString());
                    separator = ";";
                }
                if(firstLoop)
                {
                    Factory enemyFactory = FactoryList.FirstOrDefault(x => x.Owner == -1);
                    Factory myFactory = myF.FirstOrDefault();

                    Console.Write(separator);
                    Console.Write($"BOMB {myFactory.Id} {enemyFactory.Id}");
                    firstLoop = false;
                    bomb--;
                }
                Console.WriteLine();
            }
            else if(increaseActive != true && bombActive != true)
            {
                // Any valid action, such as "WAIT" or "MOVE source destination cyborgs"
                Console.WriteLine("WAIT");
            }
            else
            {
                Console.WriteLine();
            }
        }
    }
}

public class Move
{
    public int SourceFactory { get; set; }
    public int DestinationFactory { get; set; }
    public int TroopCount { get; set; }

    public override string ToString()
    {
        return $"MOVE {SourceFactory} {DestinationFactory} {TroopCount}";
    }
}

public class Link
{
    Factory _factory;
    int _distance;

    public Link(Factory factory, int distance)
    {
        _factory = factory;
        _distance = distance;
    }

    public double score()
    {
        double retVal = 0.0;

        retVal = _factory.Production ;
        retVal /= _distance;

        return retVal;
    }

    public Factory Factory
    {
        get
        {
            return _factory;
        }
    }

    public int Distance
    {
        get
        {
            return _distance;
        }
    }
}

public class Factory
{
    public Factory(int id)
    {
        Id = id;
        Links = new List<Link>();
    }

    public int CyborgNumber { get; set; }
    public int Id { get; set; }
    public int Owner { get; set; }
    public int Production { get; set; }
    public List<Troop> Incomming { get; set; }
    public List<Troop> Outgoing { get; set; }
    public List<Link> Links { get; set; }
    public int Ownership { get; set; }

    public void Update(int owner, int cyborgNumber, int production)
    {
        Owner = owner;
        CyborgNumber = cyborgNumber;
        Production = production;
        Ownership = owner == 1 ? Ownership + 1 : 0;
    }
    public int Surplus()
    {
        int retVal = 0;
        int troops = 0;

        foreach(Troop t in Incomming)
        {
            if (t.Turns < 8)
            {
                troops -= t.Owner == -1 ? t.Number : 0;
            }     
        }
        retVal = troops + CyborgNumber - 1;
        return retVal;
    }   
}



public class Troop
{
    int _id, _owner, _factoryOrigin, _factoryDestination, _number, _turns;

    public Troop(int id, int owner, int factoryOrigin, int factoryDestination, int number, int turns)
    {
        Id = id;
        Owner = owner;
        FactoryOrigin = factoryOrigin;
        FactoryDestination = factoryDestination;
        Number = number;
        Turns = turns;
    }

    public int FactoryDestination
    {
        get
        {
            return _factoryDestination;
        }

        set
        {
            _factoryDestination = value;
        }
    }

    public int FactoryOrigin
    {
        get
        {
            return _factoryOrigin;
        }

        set
        {
            _factoryOrigin = value;
        }
    }

    public int Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    public int Number
    {
        get
        {
            return _number;
        }

        set
        {
            _number = value;
        }
    }

    public int Owner
    {
        get
        {
            return _owner;
        }

        set
        {
            _owner = value;
        }
    }

    public int Turns
    {
        get
        {
            return _turns;
        }

        set
        {
            _turns = value;
        }
    }
}