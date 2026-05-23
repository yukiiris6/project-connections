using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

[ExecuteInEditMode]
public class BitmapFontRenderer : MonoBehaviour
{
    [SerializeField] private string text = "RESUME";
    [SerializeField] private Texture2D fontTexture;
    [SerializeField] private TextAsset fontDefinitionFile; // Seu arquivo .fnt aqui!
    [SerializeField] private Color textColor = Color.white;
    [SerializeField] private int extraLetterSpacing = 0;

    private struct CharInfo
    {
        public int id;
        public int x, y;
        public int width, height;
        public int xoffset, yoffset;
        public int xadvance;
    }

    private Dictionary<int, CharInfo> charDictionary = new Dictionary<int, CharInfo>();
    private int lineHeight = 0;

    private void OnValidate()
    {
        BuildText();
    }

    public void SetText(string newText)
    {
        text = newText;
        BuildText();
    }

    private void ParseFontDefinition()
    {
        charDictionary.Clear();
        if (fontDefinitionFile == null) return;

        string[] lines = fontDefinitionFile.text.Split('\n');
        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (trimmed.StartsWith("common"))
            {
                lineHeight = GetValueFor(trimmed, "lineHeight");
            }
            else if (trimmed.StartsWith("char "))
            {
                CharInfo c = new CharInfo
                {
                    id = GetValueFor(trimmed, "id"),
                    x = GetValueFor(trimmed, "x"),
                    y = GetValueFor(trimmed, "y"),
                    width = GetValueFor(trimmed, "width"),
                    height = GetValueFor(trimmed, "height"),
                    xoffset = GetValueFor(trimmed, "xoffset"),
                    yoffset = GetValueFor(trimmed, "yoffset"),
                    xadvance = GetValueFor(trimmed, "xadvance")
                };

                if (!charDictionary.ContainsKey(c.id))
                {
                    charDictionary.Add(c.id, c);
                }
            }
        }
    }

    private int GetValueFor(string line, string key)
    {
        string pattern = key + "=";
        int index = line.IndexOf(pattern);
        if (index == -1) return 0;

        int start = index + pattern.Length;
        int end = line.IndexOf(" ", start);
        if (end == -1) end = line.Length;

        string valueStr = line.Substring(start, end - start);
        if (int.TryParse(valueStr, out int result)) return result;
        return 0;
    }

    private void BuildText()
    {
        // Limpa os filhos antigos
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        if (fontTexture == null || fontDefinitionFile == null || string.IsNullOrEmpty(text)) return;

        ParseFontDefinition();

        float currentX = 0;

        foreach (char character in text)
        {
            int charId = (int)character;

            if (charDictionary.TryGetValue(charId, out CharInfo c))
            {
                if (c.width > 0 && c.height > 0)
                {
                    GameObject charObj = new GameObject($"Char_{character}");
                    charObj.transform.SetParent(transform, false);

                    Image img = charObj.AddComponent<Image>();
                    img.color = textColor;
                    img.raycastTarget = false;

                    // O PULO DO GATO: Cria o sprite dinamicamente lendo as coordenadas do .fnt
                    // O Unity conta o Y de baixo para cima, o BMFont conta de cima para baixo
                    float spriteY = fontTexture.height - c.y - c.height;
                    Rect spriteRect = new Rect(c.x, spriteY, c.width, c.height);

                    img.sprite = Sprite.Create(fontTexture, spriteRect, new Vector2(0, 1), 16f);
                    img.SetNativeSize();

                    RectTransform rect = charObj.GetComponent<RectTransform>();
                    rect.anchorMin = new Vector2(0, 0.5f);
                    rect.anchorMax = new Vector2(0, 0.5f);
                    rect.pivot = new Vector2(0, 0.5f);

                    // Posiciona respeitando estritamente os offsets inteiros do BMFont
                    int posX = Mathf.RoundToInt(currentX + c.xoffset);
                    int posY = Mathf.RoundToInt(-c.yoffset + (lineHeight / 2f));
                    rect.anchoredPosition = new Vector2(posX, posY);
                }

                currentX += c.xadvance + extraLetterSpacing;
            }
            else if (character == ' ')
            {
                // Espaço simples caso não mapeado
                currentX += 4 + extraLetterSpacing;
            }
        }

        // Reajusta o tamanho do container para você poder centralizar o botão
        RectTransform containerRect = GetComponent<RectTransform>();
        if (containerRect != null)
        {
            containerRect.sizeDelta = new Vector2(currentX, lineHeight);
        }
    }
}