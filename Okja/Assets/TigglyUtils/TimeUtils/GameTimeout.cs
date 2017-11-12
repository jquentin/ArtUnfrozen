using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// This class can be used to easily setup a timeout callback.
/// It needs to be constructed, then started with the Coroutine Start.
/// It will call the callback everytime the predicate is true for delay seconds.
/// It can be a one shot timeout or repeating.
/// The callback can be an Action (void function) or a Coroutine (Ienumerator function).
/// If it's a coroutine, the timeout will hold until the coroutine completes before starting the countdown
/// for the next trigger of the callback (assuming repeating is set to true).
/// This requires a component to call StartCoroutine on.
/// The Timeout can be cancelled at any time using the Cancel function.
/// </summary>
public class GameTimeout
{

	Action actionCallback;

	float delay;

	bool repeating;

	Func<bool> predicate;

	Func<IEnumerator> coroutineCallback;

	MonoBehaviour component;

	bool isCanceled = false;

	public GameTimeout(float delay, Func<bool> predicate, Action callback, bool repeating)
	{
		this.delay = delay;
		this.predicate = predicate;
		this.actionCallback += callback;
		this.repeating = repeating;
	}

	public GameTimeout(float delay, Func<bool> predicate, Func<IEnumerator> callback, bool repeating, MonoBehaviour component)
	{
		this.delay = delay;
		this.predicate = predicate;
		this.coroutineCallback += callback;
		this.repeating = repeating;
		this.component = component;
	}

	public IEnumerator Start () 
	{
		bool firstTime = true;
		while ((repeating || firstTime) && !isCanceled)
		{
			yield return new WaitForSecondsWhile(delay, predicate);
			if (isCanceled)
				break;
			if (actionCallback != null)
				actionCallback();
			if (coroutineCallback != null)
				yield return component.StartCoroutine(coroutineCallback());
			firstTime = false;
		}
	}

	public void Cancel()
	{
		isCanceled = true;
	}

}

/// <summary>
/// This extension class helps creating a timeout from a MonoBehaviour script.
/// It will create the Timeout object and launch it from the current component.
/// </summary>
public static class TimeoutExtensions
{

	public static GameTimeout AddTimeout(this MonoBehaviour component, float delay, Func<bool> predicate, Action callback, bool repeating)
	{
		GameTimeout timeout = new GameTimeout(delay, predicate, callback, repeating);
		component.StartCoroutine(timeout.Start());
		return timeout;
	}

	public static GameTimeout AddTimeout(this MonoBehaviour component, float delay, Func<bool> predicate, Func<IEnumerator> callback, bool repeating)
	{
		GameTimeout timeout = new GameTimeout(delay, predicate, callback, repeating, component);
		component.StartCoroutine(timeout.Start());
		return timeout;
	}

}
