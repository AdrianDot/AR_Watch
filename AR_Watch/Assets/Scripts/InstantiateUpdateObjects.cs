// ####################################################
// # AR-Watch Demo by Adrian Schroeder @Adrian_Schr   #
// # Date: 02-17-2021                                 #
// ####################################################

using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation
{
public class InstantiateUpdateObjects : MonoBehaviour
{
    [SerializeField] private GameObject ARSessionOrigin;
    [SerializeField] private GameObject watchPrefab;
    [SerializeField] private GameObject debugUI;
    private GameObject currentWatchObj;
    private ARTrackedImageManager trackedImageManager;
    private TrackableId currentTrackableId;
    private XRReferenceImage currentDetectedImageName;
    private ARTrackedImage currentARTrackedImage;    
    private ShowDebugLines showDebugLinesScript;
    private string debugText;
    private HandleWatchAnimations watchAnimScript;
    private const string referenceImageName = "Img06";
   

    void OnEnable() 
    {
        // cache the ARTrackerImagemager-component
        trackedImageManager = ARSessionOrigin.GetComponent<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnChanged;
    }

     // caches the debugging script and text-comp.
     void Start()
    {
        showDebugLinesScript = debugUI.GetComponent<ShowDebugLines>();
        debugText = showDebugLinesScript.debugText;
    }


    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // iterates through all the newly detected images
        // but not through the ones whos tracking was lost and are being detected again
        foreach (var newImage in eventArgs.added)
        {     
            // check if the detected images is our targeted watchImage
            if(newImage.referenceImage.name == referenceImageName)
            {
                RegisterNewImage(newImage);            
            }
            else
            {
                showDebugLinesScript.ChangeDebugText("OnChanged()/newImages - Newly Detected Image is not the targeted watchImage");
            }
        }

    // updates all already tracked images
    foreach (var updatedImage in eventArgs.updated)
    {
        // checks if the image is the targeted watchImage
        if(currentTrackableId.Equals(updatedImage.trackableId))
        {
            switch (updatedImage.trackingState)
            {
                case TrackingState.Tracking:
                {
                    if(currentWatchObj == null)
                    {
                        InstantiateNewWatchFace(updatedImage.transform);
                        showDebugLinesScript.ChangeDebugText("OnChanged()/updatedImages - instantiating watchFace in updated");
                    }
                    else
                    {
                        UpdateWatchPosition(updatedImage);
                    }                    
                    break;
                }
                case TrackingState.Limited:
                {
                     DestroyLostWatchFace();
                    showDebugLinesScript.ChangeDebugText("OnChanged()/updatedImages - TrackingState is: Limited");
                    break;
                }
                case TrackingState.None:
                {
                    DestroyLostWatchFace();
                    showDebugLinesScript.ChangeDebugText("OnChanged()/updatedImages - TrackingState is: None");
                    break;
                }                
                default:
                {
                    showDebugLinesScript.ChangeDebugText("OnChanged()/updatedImages - SwitchCase-Error: default was hit");
                    break; 
                }                
            }
        }
    }

    // iterates though all lost tracking images 
    foreach (var removedImage in eventArgs.removed)
    {
        showDebugLinesScript.ChangeDebugText("OnChanged()/removedImage - list was called");
    }
}

// instantiates the watches at the currentLocation of the detected image
private void InstantiateNewWatchFace(Transform instationTransform)
{
    currentWatchObj = Instantiate(watchPrefab, instationTransform);
    watchAnimScript = currentWatchObj.GetComponent<HandleWatchAnimations>();
    showDebugLinesScript.ChangeDebugText("CreateNewWatchFace() - watch instaniated");
}

// resets all the required references and value in case the image tracking has been lost
private void ResetInstantiationValues()
{
    currentTrackableId = new TrackableId();
    currentWatchObj = null;
    currentARTrackedImage = null;
    showDebugLinesScript.ChangeDebugText("ResetInstantionValues() - values reseted");
}

// starts the animation and destroys the current watches than
private void DestroyLostWatchFace()
{
    watchAnimScript.BeginnExitAnimation();
    Destroy(currentWatchObj, 0.5f);
}

private void RegisterNewImage(ARTrackedImage ImageToRegister)
{
    currentARTrackedImage = ImageToRegister;
    currentTrackableId = ImageToRegister.trackableId;    
    InstantiateNewWatchFace(ImageToRegister.transform);
    showDebugLinesScript.ChangeDebugText("RegisterNewImage() - a new image was registered");   
}

// updates the position of the instantiated gameobject with the new position of Image
private void UpdateWatchPosition(ARTrackedImage trackedImage)
{
    currentWatchObj.transform.position = trackedImage.transform.position;
    currentWatchObj.transform.rotation = trackedImage.transform.rotation;
}


}
}


