using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Game.Events;

//decalre this namespace while waiting for implementation
namespace Game.Events
{
    public interface GameEvent { }
}

/// <summary>
/// class which implements the event system handler and calls our events
/// </summary>
/// 
public interface EntityEvents : GameEvent
{
    ///TODO will likely need to handle events to take damage when other entities attack

    //this also seems a reasonable place to able to declare our bell response events

    void onHypnosMinor();
    void onHypnosMajor();

    void onOizysMinor();
    void onOizysMajor();

    void onHemeraMinor();
    void onHemeraMajor();

    void onPonosMinor();
    void onPonosMajor();

    void onElpisMinor();
    void onElpisMajor();

    void onKeresMinor();
    void onKeresMajor();
}
