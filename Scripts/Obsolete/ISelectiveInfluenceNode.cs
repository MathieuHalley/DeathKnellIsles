using System.Collections.Generic;

namespace Assets.Scripts
{
    public enum Influences
    {
        None = 0,                   //	0
        Boat = 1 << 0,              //	1
        Tower = 1 << 1,             //	2
        House = 1 << 2,             //	4
        Attacker = 1 << 3,          //	8
        Defender = 1 << 4,          //	16
        Villager = 1 << 5,          //	32
        HideBell = 1 << 6,          //	64
        PowerBell = 1 << 7,         //	128
        RallyBell = 1 << 8,         //	256
        SleepBell = 1 << 9,         //	512
        RetreatBell = 1 << 10,      //	1024
        RaiseBell = 1 << 11,        //	2048
        IndimidationBell = 1 << 12, //	4196
    }

    public interface ISelectiveInfluenceNode : IInfluenceNode
    {
        IList<Influences> Interests { get; }
        IDictionary<Influences, float> InterestingInfluenceMagnitudes { get; }
        void AddInterest(Influences interest);
        bool InfluenceIsInteresting(Influences interest);
        void RemoveInterest(Influences interest);
    }
}
