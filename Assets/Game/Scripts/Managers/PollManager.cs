using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollManager : Singleton<PollManager>
{
    private PollsCache<GameObject> circlePoll;

    public PollsCache<GameObject> CirclePoll
    {
        get
        {
            if (circlePoll == null)
            {
                circlePoll = new PollsCache<GameObject>();
            }

            return circlePoll;
        }
    }
}

