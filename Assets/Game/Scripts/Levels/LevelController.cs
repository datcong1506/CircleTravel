using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] private PillarController[] pillarControllers;
    [SerializeField] private RoadController[] roadControllers;
    
    //NOTE: This must greater or equal than pollarController's Length
    [Header("Greater or equal piller count")]
    [SerializeField]private int circleCount;

    private Color[] colorPatllets;
    
    
    public void Init()
    {
        colorPatllets = GetRandomColor();
        SetPillars();
        SetRoads();
    }

    private void SetPillars()
    {
        for (int i = 0; i < pillarControllers.Length; i++)
        {
            var pillar = pillarControllers[i];
            pillar.Init(colorPatllets[i]);
        }
    }

    private void SetRoads()
    {
        RoadController roadController;
        for (int i = 0; i < roadControllers.Length-1; i++)
        {
            roadController = roadControllers[i];
            roadController.Init(this,circleCount/roadControllers.Length);
        }

        if (roadControllers.Length > 0)
        {
            roadController = roadControllers[roadControllers.Length - 1];
            roadController.Init(this, (circleCount / roadControllers.Length) + circleCount % roadControllers.Length);
        }
      
    }

    

    //NOTE: This is complex
    // UNDONE
    //
    public CircleController GetCircle()
    {
        //For now just return default circle in polling
        return PollManager.Instance.CirclePoll.Instantiate(GameManager.Instance.GameDataController.GetCirclePrefab()).GetComponent<CircleController>();
    }



    private Color[] GetRandomColor()
    {
        var pillarCount = pillarControllers.Length;
        Color[] colors = new Color[pillarCount];


        var startHueValue = UnityEngine.Random.Range(0f, 1f);
        var offsetHueValue = (float)(1f / pillarCount);

        for (int i = 0; i < pillarCount; i++)
        {
            colors[i] = Color.HSVToRGB((startHueValue + offsetHueValue * i)%1f, 0.84f, 0.84f);
        }
        return colors;
    }
}
