using com.ootii.Messages;
using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUdon.VR
{
    public abstract class VRMode : MonoBehaviour
    {
        public GameObject root;

        public MediaPlayer mediaPlayer;

        [SerializeField]
        private bool isScreenLocked;

        [SerializeField]
        private Transform originParent;

        protected void Awake()
        {
            if (mediaPlayer == null)
            {
                mediaPlayer = GameObject.FindObjectOfType<MediaPlayer>();
            }

            originParent = root.transform.parent;
        }

        public void ScreenLock()
        {
            MessageDispatcher.SendMessageData(GameEvent.recenterEvent, null);
            root.transform.parent = Camera.main.transform;
        }
        public void ScreenUnlock()
        {
            MessageDispatcher.SendMessageData(GameEvent.recenterEvent, null);
            root.transform.parent = originParent;
        }

        public virtual void Show()
        {
            // Fire recenter event
            MessageDispatcher.SendMessageData(GameEvent.recenterEvent, null);

            if (root != null)
            {
                root.SetActive(true);
            }
        }

        public virtual void Hide()
        {
            // Fire recenter event
            MessageDispatcher.SendMessageData(GameEvent.recenterEvent, null);

            if (root != null)
            {
                root.SetActive(false);
            }
        }
    }

}
