using UnityEngine;
using UnityEngine.U2D.Animation;

public class ClickManager : MonoBehaviour
{
    private Clickable[] _clickables;

    [SerializeField] private int _clicks;

    private int Clicks
    {
        get
        {
            ++_clicks;
            return _clicks % 4;
        }
    }

    private void Awake()
    {
        _clickables = FindObjectsOfType<Clickable>();
    }

    private void Start()
    {
        foreach (var item in _clickables)
        {
            item.OnDown += Cliked;
            item.OnEnter += Hovered;
        }
    }

    private void Cliked(GameObject context)
    {
        context.GetComponent<SpriteResolver>().SetCategoryAndLabel("Gowth", $"{Clicks}");

        print($"{context.name} has been clicked on");
    }

    private void Hovered(string context)
    {
        print($"{context} has been hovered over");
    }
}