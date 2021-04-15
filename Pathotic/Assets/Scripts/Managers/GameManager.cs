using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FieldManager fieldManager;
    [SerializeField] private HeroManager heroManager;

    public static GameManager Instance { get; set; }
    public FieldManager FieldManager { get => fieldManager; set => fieldManager = value; }
    public HeroManager HeroManager { get => heroManager; set => heroManager = value; }

    void Start()
    {
        Instance = this;

        fieldManager.Init();
        heroManager.Init();
    }

}
