using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum FieldItemType
{
    Start,
    Line,
    Angle,
    Finish
}

public enum FieldItemAngleType
{
    Top,
    Down,
    Right,
    Left,
    TopAndDown,
    LeftAndRight
}

public class FieldItem : MonoBehaviour
{
    [SerializeField] private FieldItemType fieldItemType;
    [SerializeField] private FieldItemAngleType rightFieldItemAngleType;

    private UnityEvent onClick = new UnityEvent();
    [SerializeField] private FieldItemAngleType currentFieldItemAngleType;

    public FieldItemType FieldItemType { get => fieldItemType; set => fieldItemType = value; }
    public UnityEvent OnClick { get => onClick; set => onClick = value; }
    public FieldItemAngleType RightFieldItemAngleType { get => rightFieldItemAngleType; set => rightFieldItemAngleType = value; }
    public FieldItemAngleType CurrentFieldItemAngleType { get => currentFieldItemAngleType; set => currentFieldItemAngleType = value; }

    private void OnMouseDown()
    {
        onClick.Invoke();   
    }
}
