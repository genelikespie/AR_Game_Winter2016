/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
        private GameManagerScript gameManager;
        private MenuManager menuManager;
        private bool prevPauseState; // on tracking found or lost will revert to this state
        private bool quiting = false;
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Awake()
        {
            gameManager = GameManagerScript.Instance();
            menuManager = MenuManager.Instance();
        }
        

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS

        void OnApplicationQuit()
        {
            quiting = true;
        }

        protected virtual void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
            //Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            if (tag == "MainImageTarget" && gameManager)
            {
                if (quiting)
                    return;
                // restore our previous paused state
                if (prevPauseState == true && menuManager.dropped == true)
                {
                    gameManager.pauseGame();
                }
                else
                    gameManager.unPauseGame();
                MessageBoard messageBoard = MessageBoard.Instance();
                messageBoard.setTitle("Tracking Refound");
                messageBoard.setBody("The image target has been re-found. The game will now resume");
                messageBoard.ToWorldSpace();
                messageBoard.pressedBack();
            }
        }


        protected virtual void OnTrackingLost()
        {
            
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            //Debug.Log("Tracking Lost: " + this.name);
            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            if (tag == "MainImageTarget" && gameManager)
            {
                if (quiting)
                    return;
                // save our previous pause state so we can return to it when we retrack target
                prevPauseState = gameManager.paused;
                gameManager.pauseGame();
                //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
                gameManager.gameObject.SendMessage("PauseTrackableLost", SendMessageOptions.DontRequireReceiver);
                MessageBoard messageBoard = MessageBoard.Instance();
                messageBoard.setTitle("Tracking Lost!");
                messageBoard.setBody("The image target for the headquarters cannot be found! Make sure the headquarter's barcode is in view of the camera.");
                messageBoard.ToScreenSpace();
                messageBoard.activateBoard(false);
                
            }
        }

        #endregion // PRIVATE_METHODS
    }
}
