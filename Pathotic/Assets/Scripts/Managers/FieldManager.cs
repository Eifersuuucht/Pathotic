using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private FieldItem[] fieldItems;

    private HeroManager heroManager;

    private FieldItemAngleType[] randomRotationAngleTypes = new[] 
    {
        FieldItemAngleType.Top,
        FieldItemAngleType.Down,
        FieldItemAngleType.Left,
        FieldItemAngleType.Right
    };
    private FieldItemAngleType[] randomRotationLineTypes = new[] 
    { 
        FieldItemAngleType.LeftAndRight,
        FieldItemAngleType.TopAndDown 
    };
    private Dictionary<FieldItemAngleType, int> fieldItemAngleTypeDictionary = new Dictionary<FieldItemAngleType, int>
    {
        { FieldItemAngleType.Top,  180},
        { FieldItemAngleType.Down,  0},
        { FieldItemAngleType.Left,  270},
        { FieldItemAngleType.Right,  90},
    };
    private Dictionary<FieldItemAngleType, int> fieldItemLineTypeDictionary = new Dictionary<FieldItemAngleType, int>
    {
        { FieldItemAngleType.LeftAndRight,  0},
        { FieldItemAngleType.TopAndDown,  90},
    };

    public FieldItem[] FieldItems { get => fieldItems; set => fieldItems = value; }

    public void Init()
    {
        heroManager = GameManager.Instance.HeroManager;
        RandomizeRoad();
    }

    private void RandomizeRoad()
    {
        var _fieldItemsToRandomize = fieldItems
            .Where(item => item.FieldItemType == FieldItemType.Line || item.FieldItemType == FieldItemType.Angle)
            .ToArray();

        foreach(var item in _fieldItemsToRandomize)
        {
            if(item.FieldItemType == FieldItemType.Line)
            {
                var _randomizedRotationLineType = randomRotationLineTypes[Random.Range(0, randomRotationLineTypes.Length)];
                Rotate(item, fieldItemLineTypeDictionary[_randomizedRotationLineType]);
                item.CurrentFieldItemAngleType = _randomizedRotationLineType;
            }
            else
            {
                var _randomizedRotationAngleType = randomRotationAngleTypes[Random.Range(0, randomRotationAngleTypes.Length)];
                Rotate(item, fieldItemAngleTypeDictionary[_randomizedRotationAngleType]);
                item.CurrentFieldItemAngleType = _randomizedRotationAngleType;

            }

            if (item.CurrentFieldItemAngleType != item.RightFieldItemAngleType)
            {
                item.OnClick.AddListener(() => 
                {
                    if (item.FieldItemType == FieldItemType.Line)
                    {
                        var futureRotationAngle = fieldItemLineTypeDictionary[item.CurrentFieldItemAngleType] - 90f;
                        if (futureRotationAngle < -1f)
                        {
                            futureRotationAngle = 90f;
                        }
                        var key = fieldItemLineTypeDictionary.FirstOrDefault(x => x.Value == futureRotationAngle).Key;
                        Rotate(item, futureRotationAngle);
                        item.CurrentFieldItemAngleType = key;

                        if (item.CurrentFieldItemAngleType == item.RightFieldItemAngleType)
                        {
                            item.OnClick.RemoveAllListeners();
                        }
                    }
                    else
                    {
                        var futureRotationAngle = fieldItemAngleTypeDictionary[item.CurrentFieldItemAngleType] - 90f;
                        if (futureRotationAngle < -1f)
                        {
                            futureRotationAngle = 270f;
                        }
                        var key = fieldItemAngleTypeDictionary.FirstOrDefault(x => x.Value == futureRotationAngle).Key;

                        Rotate(item, futureRotationAngle);
                        item.CurrentFieldItemAngleType = key;

                        if (item.CurrentFieldItemAngleType == item.RightFieldItemAngleType)
                        {
                            item.OnClick.RemoveAllListeners();
                        }
                    }

                    IsDone();
                });
            }

        }
    }

    public void Rotate(FieldItem item, float angle)
    {
        var _randomizedRotationEuler = new Vector3(item.transform.rotation.eulerAngles.x, item.transform.rotation.eulerAngles.y, angle);
        item.transform.rotation = Quaternion.Euler(_randomizedRotationEuler);
    }

    public void IsDone()
    {
        foreach (var item in fieldItems)
        {
            if(item.CurrentFieldItemAngleType != item.RightFieldItemAngleType)
            {
                return;
            }
        }

        heroManager.MovePlayer();
    }
}
