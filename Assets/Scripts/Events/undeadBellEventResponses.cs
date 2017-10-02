using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;
using Bells;
using System;

public class undeadEventHandler : MonoBehaviour, IBellEventHandler {
    public EventManager EventManager
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public void OnBellEvent(BellEventData data)
    {
        if (data.isMajor)
        {
            switch (data.typeOfBellCausingEvent)
            {
                case BellType.hypnos:
                    {
                        onHypnosMajor();
                        break;
                    }
                case BellType.oizys:
                    {
                        onOizysMajor();
                        break;
                    }
                case BellType.hemera:
                    {
                        onHemeraMajor();
                        break;
                    }
                case BellType.ponos:
                    {
                        onPonosMajor();
                        break;
                    }
                case BellType.elpis:
                    {
                        onElpisMajor();
                        break;
                    }
                case BellType.keres:
                    {
                        onKeresMajor();
                        break;
                    }
                default:
                    {
                        //badly formed or otherwise not able to be processed
                        break;
                    }
            }
        }
        else
        {
            switch (data.typeOfBellCausingEvent)
            {
                case BellType.hypnos:
                    {
                        onHypnosMinor();
                        break;
                    }
                case BellType.oizys:
                    {
                        onOizysMinor();
                        break;
                    }
                case BellType.hemera:
                    {
                        onHemeraMinor();
                        break;
                    }
                case BellType.ponos:
                    {
                        onPonosMinor();
                        break;
                    }
                case BellType.elpis:
                    {
                        onElpisMinor();
                        break;
                    }
                case BellType.keres:
                    {
                        onKeresMinor();
                        break;
                    }
                default:
                    {
                        //badly formed or otherwise not able to be processed
                        break;
                    }
            }
        }
    }

    public void onHypnosMinor()
    {

    }
    public void onHypnosMajor()
    {

    }

    public void onOizysMinor()
    {

    }
    public void onOizysMajor()
    {

    }

    public void onHemeraMinor()
    {

    }
    public void onHemeraMajor()
    {

    }

    public void onPonosMinor()
    {

    }
    public void onPonosMajor()
    {

    }

    public void onElpisMinor()
    {

    }
    public void onElpisMajor()
    {

    }

    public void onKeresMinor()
    {

    }
    public void onKeresMajor()
    {

    }
}
