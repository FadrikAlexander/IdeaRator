using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

//This class for handling the Link stuff in the TextMeshPro Asset
public class TMPLinkHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    IdeaRatorBrain ideaRatorBrain;
    TextMeshProUGUI textMeshPro;
    Canvas canvas;
    Camera mainCamera;

    private int m_selectedLink = -1;
    private bool isHoveringObject = false;

    [SerializeField]
    Color32 selectedColor;
    Color32 startColor;

    void Awake()
    {
        ideaRatorBrain = FindObjectOfType<IdeaRatorBrain>();

        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        canvas = gameObject.GetComponentInParent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            mainCamera = null;
        else
            mainCamera = canvas.worldCamera;

        startColor = textMeshPro.color;
    }

    void LateUpdate()
    {
        if (isHoveringObject)
        {
            // Get the Selected Link
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, Input.mousePosition, mainCamera);

            // Clear previous Link selection.
            if (((linkIndex == -1 && m_selectedLink != -1) || linkIndex != m_selectedLink) && m_selectedLink != -1)
            {
                TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[m_selectedLink];

                // Iterate through each of the characters of the word.
                for (int i = 0; i < linkInfo.linkTextLength; i++)
                {
                    int characterIndex = linkInfo.linkTextfirstCharacterIndex + i;
                    changeColor(characterIndex, startColor);
                }

                // Update Geometry
                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

                m_selectedLink = -1;
            }

            // Handle new Link selection.
            if (linkIndex != -1 && linkIndex != m_selectedLink)
            {
                m_selectedLink = linkIndex;
                
                TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];

                // Iterate through each of the characters of the word.
                for (int i = 0; i < linkInfo.linkTextLength; i++)
                {
                    int characterIndex = linkInfo.linkTextfirstCharacterIndex + i;
                    changeColor(characterIndex, selectedColor);
                }
                // The First Letter most often get colored, I couldn't figure out why so this is a simple fix
                // Let me know if anyone was able to solve it
                if(m_selectedLink!=0)
                    changeColor(0, startColor);

                // Update Geometry
                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            }
        }
    }

    void changeColor(int characterIndex, Color32 color)
    {
        // Get the index of the material / sub text object used by this character.
        int meshIndex = textMeshPro.textInfo.characterInfo[characterIndex].materialReferenceIndex;

        int vertexIndex = textMeshPro.textInfo.characterInfo[characterIndex].vertexIndex;

        // Get a reference to the vertex color
        Color32[] vertexColors = textMeshPro.textInfo.meshInfo[0].colors32;

        Color32 c = color;

        vertexColors[vertexIndex + 0] = c;
        vertexColors[vertexIndex + 1] = c;
        vertexColors[vertexIndex + 2] = c;
        vertexColors[vertexIndex + 3] = c;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoveringObject = true;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        isHoveringObject = false;
    }

    // Send ID on Click to IdeaRator Brain
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, Input.mousePosition, mainCamera);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];
            ideaRatorBrain.changeIdea(linkInfo.GetLinkID());
        }
    }
}
