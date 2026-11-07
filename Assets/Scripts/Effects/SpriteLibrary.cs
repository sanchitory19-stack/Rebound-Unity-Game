using System.Collections.Generic;
using UnityEngine;

public class SpriteLibrary : MonoBehaviour
{
    public static SpriteLibrary Instance { get; private set; }

    [System.Serializable]
    public class SpritePair
    {
        public Sprite filled;
        public Sprite outline;
    }

    [SerializeField] private SpritePair[] spritePairs;

    private Dictionary<Sprite, Sprite> outlineLookup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        outlineLookup = new Dictionary<Sprite, Sprite>();

        foreach (SpritePair pair in spritePairs)
        {
            if (pair.filled != null && pair.outline != null)
            {
                outlineLookup[pair.filled] = pair.outline;
            }
        }
    }

    public Sprite GetOutline(Sprite filledSprite)
    {
        if (outlineLookup.TryGetValue(filledSprite, out Sprite outline))
        {
            return outline;
        }

        return filledSprite;
    }
}