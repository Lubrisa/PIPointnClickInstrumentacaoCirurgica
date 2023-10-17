using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomTools
{
    /// <summary>
    /// Class used to execute methods after a period of time.
    /// </summary>
    public class GameTimer
    {
        #region Properties

        /// <summary>
        /// Event Invoked when the timer ends.
        /// </summary>
        private event EventHandler OnTimeEnd;

        /// <summary>
        /// Event Invoked every frame while the timer is running.
        /// </summary>
        private event EventHandler<TickEventArgs> OnTick;

        /// <summary>
        /// The time remaining to the timer stop running.
        /// </summary>
        public float RemainingTime { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Start a timer.
        /// </summary>
        /// <param name="timerDuration"> How long the timer will keep runnning in seconds. </param>
        /// <param name="caller"> The object that started the timer. </param>
        /// <param name="methods"> An array of methods that needs to be called when the timer runs out. </param>
        public void StartTimer(float timerDuration, MonoBehaviour caller, params EventHandler[] methods)
        {
            // If this method is called on a timer that has alredy been started, ends the coroutine execution.
            if (RemainingTime > 0) caller.StopCoroutine(RunTimer());

            // Sets the remaining time as the duration given as argument.
            RemainingTime = timerDuration;

            // Register the methods given as argument as listeners of the OnTimeEnd event.
            AddOnTimeEndListener(methods);

            // Starts the timer coroutine.
            caller.StartCoroutine(RunTimer());
        }

        /// <summary>
        /// Increases the remaining time of the timer to run out.
        /// </summary>
        /// <param name="extraTime"> The time to be added in the remaining time in seconds. </param>
        public void IncreaseRemainingTime(float extraTime) => RemainingTime += extraTime; // Sum the remaining time current value with the value given as argument.

        /// <summary>
        /// Decreases the remaining time of the timer to run out.
        /// </summary>
        /// <param name="timeDiscount"> The time to be taken in the remaining time in seconds. </param>
        public void DecreaseRemainingTime(float timeDiscount) => RemainingTime -= timeDiscount; // Subtracts the remaining time current value with the value given as argument.

        /// <summary>
        /// Adds listeners to the OnTimeEnd event of the timer.
        /// </summary>
        /// <param name="methods"> The array of methods to be added as listeners. </param>
        public void AddOnTimeEndListener(params EventHandler[] methods)
        {
            if (OnTimeEnd is null) OnTimeEnd += methods[0];

            // Compares every element in the OnTimeEnd event listeners list with every element in the array of methods given the be added.
            // Elements that alredy are present in the listeners list aren't added.
            List<bool> isRepetition = CheckMethodRepetition(OnTimeEnd.GetInvocationList(), methods);
            for (int i = 0; i < isRepetition.Count; i++)
            {
                if (!isRepetition[i]) OnTimeEnd += methods[i];
            }
        }

        /// <summary>
        /// Remove listeners of the OnTimeEnd event of the timer.
        /// </summary>
        /// <param name="methods"> The array of methods to be removed as listeners. </param>
        public void RemoveOnTimeEndListener(params EventHandler[] methods)
        {
            if (OnTimeEnd is null) return;

            // Compares every element in the OnTimeEnd event listeners list with every element in the array of methods given the be added.
            // Elements that aren't present in the listeners list aren't removed.
            List<bool> isRepetition = CheckMethodRepetition(OnTimeEnd.GetInvocationList(), methods);
            for (int i = 0; i < isRepetition.Count; i++)
            {
                if (isRepetition[i]) OnTimeEnd -= methods[i];
            }
        }

        /// <summary>
        /// Adds listeners to the OnTick event of the timer.
        /// </summary>
        /// <param name="methods"> The array of methods to be added as listeners. </param>
        public void AddOnTickListener(params EventHandler<TickEventArgs>[] methods)
        {
            if (OnTick is null) OnTick += methods[0];

            // Compares every element in the OnTick event listeners list with every element in the array of methods given the be added.
            // Elements that alredy are present in the listeners list aren't added.
            List<bool> isRepetition = CheckMethodRepetition(OnTick.GetInvocationList(), methods);
            for (int i = 0; i < isRepetition.Count; i++)
            {
                if (!isRepetition[i]) OnTick += methods[i];
            }
        }

        /// <summary>
        /// Remove listeners of the OnTick event of the timer.
        /// </summary>
        /// <param name="methods"> The array of methods to be removed as listeners. </param>
        public void RemoveOnTickListener(params EventHandler<TickEventArgs>[] methods)
        {
            if (OnTick is null) return;

            // Compares every element in the OnTick event listeners list with every element in the array of methods given the be added.
            // Elements that aren't present in the listeners list aren't removed.
            List<bool> isRepetition = CheckMethodRepetition(OnTick.GetInvocationList(), methods);
            for (int i = 0; i < isRepetition.Count; i++)
            {
                if (isRepetition[i]) OnTick -= methods[i];
            }
        }

        /// <summary>
        /// The coroutine called to run the timer.
        /// </summary>
        private IEnumerator RunTimer()
        {
            // While the remaining time is greater than zero, do the following.
            while (RemainingTime > 0)
            {
                // decreases the remaining time acordingly to the time difference between the current frame and the last one.
                RemainingTime -= Time.deltaTime;

                // Invokes the OnTick event.
                TickEventArgs tickArgs = new();
                tickArgs.RemainingTime = RemainingTime;
                OnTick?.Invoke(this, tickArgs);

                // Awaits one frame.
                yield return new WaitForEndOfFrame();
            }

            // When the time runs out, call the method to end the timer cicle.
            EndTimer();
        }

        /// <summary>
        /// Invokes the OnTimeEnd event and unsubscribe every listener of the events.
        /// </summary>
        private void EndTimer()
        {
            // Invokes the OnTimeEnd event.
            OnTimeEnd?.Invoke(this, EventArgs.Empty);

            // Removes every listener of the OnTimeEnd event.
            if (OnTimeEnd is not null) OnTimeEnd = null;

            // Removes every listener of the OnTick event.
            if (OnTick is not null) OnTick = null;
        }

        /// <summary>
        /// Checks if two method references are equal.
        /// </summary>
        /// <param name="firstMethods"> The first array of methods to be compared. </param>
        /// <param name="secondMethods"> The second array of methods to be compared. </param>
        /// <returns></returns>
        private List<bool> CheckMethodRepetition(Delegate[] firstMethods, Delegate[] secondMethods)
        {
            List<bool> returnList = new List<bool>();

            // Iterates through the two arrays given as arguments, comparing every element of both.
            // If two elements are identical, adds to the return list a 'true' value, else, adds a 'false' value.
            foreach (var method in firstMethods)
            {
                bool isCopy = false;
                foreach (var otherMethod in secondMethods)
                {
                    if (otherMethod.Method == method.Method)
                    {
                        isCopy = true;
                        break;
                    }
                }

                if (isCopy) returnList.Add(true);
                else returnList.Add(false);
            }

            return returnList;
        }

        #endregion Methods
    }

    /// <summary>
    /// GameTimer class OnTick event arguments.
    /// </summary>
    public class TickEventArgs : EventArgs
    {
        public float RemainingTime;
    }
}