using System;
using System.Collections.Generic;
using UnityEngine;

public class Textures : MonoBehaviour
{
    [Serializable]
    public class TextureEntry
    {
        // Texture key
        public string key;

        // Texture (Sprite)
        public Sprite texture;
    }

    // Textures Instance
    private static Textures instance;

    // All Available Textures
    public List<TextureEntry> textureEntries = new List<TextureEntry>();

    // Awake
    private void Awake()
    {
        // Check If Exists
        if (Textures.instance == null)
        {
            Textures.instance = this;
        }
        else
        {
            Destroy(this);

            Debug.LogError("Deleted Second Textures Handler In Scene");
        }
    }

    // Get Sprite By Key 
    public static Sprite GetSprite(string key)
    {
        if (key == null || key.Length == 0 || Textures.instance == null)
        {
            return null;
        }

        // Iterate Texture Entries
        foreach (TextureEntry textureEntry in Textures.instance.textureEntries)
        {
            if (textureEntry.key.Equals(key))
            {
                return textureEntry.texture;
            }
        }
        return null;
    }
}
