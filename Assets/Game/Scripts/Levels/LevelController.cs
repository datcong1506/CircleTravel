using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] private PostController[] postControllers;
    public PostController[] PostControllers => postControllers;
    [SerializeField] private RoadController[] roadControllers;
    public RoadController[] RoadControllers => roadControllers;
    //NOTE: This must greater or equal than pollarController's Length
    [Header("Greater or equal piller count")]
    [SerializeField]private int circleCount;
    public int CircleCount => circleCount;
    private Color[] colorPatllets;
    
    //NOTE: use this to spawn color circle
    private Dictionary<Color, int> colorStorages;

    
    public void Init()
    {
        colorPatllets = GetRandomColors();
        SetColorStorage();
        SetPillars();
        SetRoads();
    }

    private void SetPillars()
    {
        for (int i = 0; i < postControllers.Length; i++)
        {
            var pillar = postControllers[i];
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


    private void SetColorStorage()
    {
        colorStorages = new Dictionary<Color, int>();
        for (int i = 0; i < colorPatllets.Length; i++)
        {
            colorStorages.Add(colorPatllets[i],0);
        }
    }
    
    
    //NOTE: This is complex
    // UNDONE
    //
    public CircleController SpawnCircle()
    {
        //For now just return default circle in polling
        var circleControlelr= PollManager.Instance.CirclePoll.Instantiate(GameManager.Instance.GameDataController.GetCirclePrefab()).GetComponent<CircleController>();
        var randomColor = GetRandomColor();
        circleControlelr.SetColor(randomColor);
        colorStorages[randomColor] = colorStorages[randomColor] + 1;
        return circleControlelr;
    }


    private Color GetRandomColor()
    {
        Color color=Color.white;
        int min=1000;
        for (int i = 0; i < colorStorages.Count; i++)
        {
            var colorStorage = colorStorages.ElementAt(i);
            if (colorStorage.Value < min)
            {
                min = colorStorage.Value;
                color = colorStorage.Key;
            }
        }
        return color;
    }
    
    
    private Color[] GetRandomColors()
    {
        var pillarCount = postControllers.Length;
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
