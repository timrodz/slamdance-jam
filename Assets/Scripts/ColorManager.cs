using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    public static ColorManager Instance { get; private set; }

    [HideInInspector]
    public Color PositiveSpaceColor = Color.white;

    [HideInInspector]
    public Color NegativeSpaceColor = Color.black;

    private List<NegativeSpaceImage> imageList = new List<NegativeSpaceImage>();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

    }

    // Use this for initialization
    void Start() {

        Material pos = Resources.Load("Materials/Positive Space") as Material;
        PositiveSpaceColor = pos.color;

        Material neg = Resources.Load("Materials/Negative Space") as Material;
        NegativeSpaceColor = neg.color;

        PopulateImageList();

        AssignColors();

    }

    public void PopulateImageList() {

        imageList.Clear();

        NegativeSpaceImage[] negativeSpaceImages = FindObjectsOfType<NegativeSpaceImage>();

        for (int i = 0; i < negativeSpaceImages.Length; i++) {

            imageList.Add(negativeSpaceImages[i]);

        }

    }

    public void AssignColors() {

        foreach(NegativeSpaceImage n in imageList) {

            n.AssignColor();

        }

    }

    public void InvertColors() {

        PopulateImageList();

        foreach(NegativeSpaceImage n in imageList) {

            n.InvertColor();

        }

        AssignColors();

    }

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate() {

        AssignColors();

    }

}