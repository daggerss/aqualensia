using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureInDive : MonoBehaviour
{
    public Creature creature;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.sprite = creature.Sprite;
    }
}
