// ####################################################
// # AR-Watch Demo by Adrian Schroeder @Adrian_Schr   #
// # Date: 02-17-2021                                 #
// ####################################################

using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation
{
    public class HandleWatchAnimations : MonoBehaviour
    {
            
        [SerializeField] private GameObject[] WatchObjects;
        private Animator[] WatchFaceAnimators;

        // triggers the Watch
        void OnEnable()
        {
            WatchFaceAnimators = new Animator[WatchObjects.Length];

            for(int i = 0; i < WatchObjects.Length; i++)
            {      
                WatchFaceAnimators[i] = WatchObjects[i].GetComponent<Animator>();
                WatchFaceAnimators[i].SetBool("StartWatchFaceAnimation", true);              
            }
        }

        // starts the Exit-Animations
        public void BeginnExitAnimation()
        {
            for(int i = 0; i < WatchObjects.Length; i++)
            {
                WatchFaceAnimators[i].SetBool("BeginnExitAnimation", true);              
            }
        }

    }
}
