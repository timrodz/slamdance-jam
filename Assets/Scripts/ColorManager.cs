using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    public static ColorManager Instance { get; private set; }
    
    private List<NegativeSpaceImage> imageList = new List<NegativeSpaceImage>();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {

        if (Instance != null && Instance == gameObject) {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

    }

    // Use this for initialization
    void Start() {

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
        
        PopulateImageList();

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

}