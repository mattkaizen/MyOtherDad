using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUIObjectClicker : MonoBehaviour
    {
        [SerializeField] private InputReaderData inputReaderData;
        [SerializeField] private GraphicRaycaster rayCaster;
        [SerializeField] private EventSystem eventSystem;

        private PointerEventData _pointerEventData;
        private bool _isPainting;

        private void Awake()
        {
            inputReaderData.Painting += OnPainting;
            inputReaderData.Painted += OnPainted;
        }

        private void OnDisable()
        {
            inputReaderData.Painting -= OnPainting;
            inputReaderData.Painted -= OnPainted;
        }

        private void OnPainting()
        {
            _isPainting = true;
            StartCoroutine(ExecuteRoutine());
        }

        private void OnPainted()
        {
            _isPainting = false;
        }

        private IEnumerator ExecuteRoutine()
        {
            while (_isPainting)
            {
                GraphicRayCastToClickable();
                yield return null;
            }
        }

        private void GraphicRayCastToClickable()
        {
            _pointerEventData = new PointerEventData(eventSystem);
            _pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            rayCaster.Raycast(_pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                TryClick(result.gameObject);
            }
        }

        private void TryClick(GameObject uiGameObject)
        {
            if (!uiGameObject.TryGetComponent<IClickable>(out var executable)) return;
            if (!executable.WasClicked)
                executable.Click();
        }
    }
}