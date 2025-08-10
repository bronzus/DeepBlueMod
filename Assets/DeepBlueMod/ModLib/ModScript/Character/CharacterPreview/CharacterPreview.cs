using System;
using System.Collections.Generic;
using UnityEngine;

namespace kz
{
    public class CharacterPreview:MonoBehaviour
    {
        public string previewNameKey;
        public Sprite characterDefaultPreviewImage;
        public List<Sprite> characterSkinPreviewImages = new List<Sprite>();
        public List<CharacterSkin> characterSkins = new List<CharacterSkin>();
    }
}