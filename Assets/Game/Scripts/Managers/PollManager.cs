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


    private PollsCache<GameObject> circleOnAirPoll;
    public PollsCache<GameObject> CircleOnAirPoll
    {
        get
        {
            if (circleOnAirPoll == null)
            {
                circleOnAirPoll = new PollsCache<GameObject>();
            }

            return circleOnAirPoll;
        }
    }

    private PollsCache<GameObject> circleOnPostPoll;
    public PollsCache<GameObject> CircleOnPostPoll
    {
        get
        {
            if (circleOnPostPoll == null)
            {
                circleOnPostPoll = new PollsCache<GameObject>();
            }

            return circleOnPostPoll;
        }
    }


    [SerializeField] private GameObject pathPrefab;
    private ObjectPolling pathPolling;
    public ObjectPolling PathPolling
    {
        get
        {
            return pathPolling;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        CreatePolling();
    }

    private void CreatePolling()
    {
        if (pathPolling == null)
        {
            pathPolling = new ObjectPolling(null, pathPrefab, 5);
        }
    }
}

