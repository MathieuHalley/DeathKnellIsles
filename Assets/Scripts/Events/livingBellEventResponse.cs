using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;
using Bells;
using System;

public class livingEventsHandler : IBellEventHandler {
    public EventManager EventManager
    {
        get
        {
            //its not really clear to me what this is supposed to return
            throw new NotImplementedException();
        }
    }

    //I'm makin an assumption that the bell logic itself has run and selected what entities where in range, 
    //and whetehr they were in the major effect or minor effect of the given bells radius
    // So we are assuming this event will come in knowing we need to be affected, but jsut that we need to respond in the appropriate way
    public void OnBellEvent(BellEventData data)
    {
        if(data.isMajor)
        {
            switch(data.typeOfBellCausingEvent)
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
