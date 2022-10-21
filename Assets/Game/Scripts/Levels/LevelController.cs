using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;




public class LevelController : MonoBehaviour
{
    [System.Serializable]
    public class SpawnWave
    {
        public float DelayTime;
        public float Speed=2.5f;
        public int Count;
        public float Rate;
        [Range(0,0.3f)]public float RandomRate=0.1f;
    }
    [SerializeField] private SpawnWave[] waves;
    
    [System.Serializable]
    public enum POVTYPE
    {
        WIDTH,
        HEIGHT,
    }

    private POVTYPE povtype;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    

    [SerializeField] private PostController[] postControllers;
    public PostController[] PostControllers => postControllers;
    [SerializeField] private RoadController[] roadControllers;
    public RoadController[] RoadControllers => roadControllers;
    //NOTE: This must greater or equal than pollarController's Length
    public int CircleCount {
        get
        {
            int total = 0;
            foreach (var wave in waves)
            {
                total += wave.Count;
            }
            return total;
        }
    }
    private Color[] colorPatllets;
    
    //NOTE: use this to spawn color circle
    private Dictionary<Color, int> colorStorages;

    
    public void Init()
    {
        colorPatllets = GetRandomColors();
        SetColorStorage();
        SetPillars();
        SetRoads();
        StartCoroutine(SpawnCircles());
        SetFOV();
    }

    private void SetFOV()
    {
        var baseScale = (float) (16f / 9);
        var currentScale = (float)((float)Screen.height / Screen.width);
        cinemachineVirtualCamera.m_Lens.FieldOfView = 60 * (currentScale / baseScale);
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
        for (int i = 0; i < roadControllers.Length; i++)
        {
            roadController = roadControllers[i];
            roadController.Init(this);
        }
    }

    
    

    
    private IEnumerator SpawnCircles()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            var wave = waves[i];
            if (wave.DelayTime > 0)
            {
                yield return new WaitForSeconds(wave.DelayTime);
            }
            for (int j = 0; j < wave.Count; j++)
            {
                var roadCount = roadControllers.Length;
                var randomIndexRoad = UnityEngine.Random.Range(0, roadCount);
                roadControllers[randomIndexRoad].SpawnCircle(SpawnCircle(wave.Speed));
                yield return new WaitForSeconds(wave.Rate*UnityEngine.Random.Range(1f-wave.RandomRate,1f+wave.RandomRate));
            }
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
    public CircleController SpawnCircle(float speed)
    {
        //For now just return default circle in polling
        var circleControlelr= PollManager.Instance.CirclePoll.Instantiate(GameManager.Instance.GameDataController.GetCirclePrefab()).GetComponent<CircleController>();
        var randomColor = GetRandomColor();
        circleControlelr.SetColor(randomColor);
        circleControlelr.SetSpeed(speed);
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
