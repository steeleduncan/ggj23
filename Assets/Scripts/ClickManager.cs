using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private Clickable[] _clickables;

    private void Awake()
    {
        _clickables = FindObjectsOfType<Clickable>();
    }

    private void Start()
    {
        foreach (var item in _clickables)
        {
            item.OnDown += Foo;
            item.OnEnter += Bar;
        }
    }

    private void Foo(string context)
    {
        print($"{context} has been clicked on");
    }

    private void Bar(string context)
    {
        print($"{context} has been hovered over");
    }
}