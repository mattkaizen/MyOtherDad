using Interfaces;
using UnityEngine;
using UnityEngine.Playables;

namespace Objects
{
    public class WorkDeskObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private PlayableDirector playableDirector;
        public void Interact()
        {
            Debug.Log("Play Cutscene");
            playableDirector.Play();
        }
    }
}