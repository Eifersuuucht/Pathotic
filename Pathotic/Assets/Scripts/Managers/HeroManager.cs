using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private Transform hero;
    [SerializeField] private float heroSpeed;

    private bool isPlayerCanMove;
    private FieldItem[] fieldItems;
    private int currentFieldItemIndex = 1;

    public void Init()
    {
        fieldItems = GameManager.Instance.FieldManager.FieldItems;
    }

    private void FixedUpdate()
    {
        if (isPlayerCanMove)
        {
            var direction = fieldItems[currentFieldItemIndex].transform.position - fieldItems[currentFieldItemIndex - 1].transform.position;
            hero.Translate(direction * heroSpeed * Time.fixedDeltaTime);

            if(Vector2.Distance(hero.transform.position, fieldItems[currentFieldItemIndex].transform.position) < 0.02f)
            {
                currentFieldItemIndex++;
                if(currentFieldItemIndex == fieldItems.Length)
                {
                    isPlayerCanMove = false;    
                }
            }
        }
    }

    public void MovePlayer()
    {
        isPlayerCanMove = true;
    }
}
