using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace Videos
{
    public class VideoPlayerListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent videoReachedEnd;
        [SerializeField] private VideoPlayer videoToListen;
        [SerializeField] private bool playOnEnable;

        private void OnEnable()
        {
            videoToListen.loopPointReached += OnReachedEnd;
            
            if(playOnEnable)
                videoToListen.Play();
        }
        
        private void OnDisable()
        {
            videoToListen.loopPointReached -= OnReachedEnd;
        }

        private void OnReachedEnd(VideoPlayer source)
        {
            videoReachedEnd?.Invoke();
        }
    }
}