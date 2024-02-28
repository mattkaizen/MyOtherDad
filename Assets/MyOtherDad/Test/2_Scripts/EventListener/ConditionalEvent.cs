using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace EventListener
{
    public class ConditionalEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent conditionCompleted;
        [SerializeField] private Condition currentCondition;
        [SerializeField] private int goalAccomplishedConditions;

        private int _currentAccomplishedConditions;

        [UsedImplicitly]
        public void SetConditionNumberTo(int newNumberOfConditions)
        {
            _currentAccomplishedConditions = newNumberOfConditions;
        }

        [UsedImplicitly]
        public void AddConditionCompleted()
        {
            _currentAccomplishedConditions++;

            if (IsConditionCompleted(currentCondition))
            {
                conditionCompleted?.Invoke();
            }
        }

        [UsedImplicitly]
        public void DeductConditionCompleted()
        {
            if (_currentAccomplishedConditions > 0)
                _currentAccomplishedConditions--;

            if (IsConditionCompleted(currentCondition))
            {
                conditionCompleted?.Invoke();
            }
        }

        private bool IsConditionCompleted(Condition condition)
        {
            switch (condition)
            {
                case Condition.Equals:
                {
                    if (_currentAccomplishedConditions == goalAccomplishedConditions)
                        return true;
                    break;
                }

                case Condition.Greater:
                {
                    if (_currentAccomplishedConditions > goalAccomplishedConditions)
                        return true;
                    break;
                }

                case Condition.GreaterEquals:
                {
                    if (_currentAccomplishedConditions >= goalAccomplishedConditions)
                        return true;
                    break;
                }
            }

            return false;
        }

        public enum Condition
        {
            Equals,
            Greater,
            GreaterEquals
        }
    }
}