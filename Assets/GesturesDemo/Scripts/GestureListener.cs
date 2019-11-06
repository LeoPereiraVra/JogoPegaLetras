using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;


public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
    public TextMeshProUGUI Tempo;
   

	private bool swipeLeft;
	private bool swipeRight;
    private bool swipeDown;
    private bool swipeUp;
    private bool click;
    private bool wave;
    private bool jump;


    public bool IsSwipeLeft()
	{
		if(swipeLeft)
		{
			swipeLeft = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeRight()
	{
		if(swipeRight)
		{
			swipeRight = false;
			return true;
		}
		
		return false;
	}

    public bool IsSwipeDown()
    {
        if (swipeDown)
        {
            swipeDown = false;
            return true;
        }

        return false;
    }

    public bool IsSwipeUp()
    {
        if (swipeUp)
        {
            swipeUp = false;
            return true;
        }

        return false;
    }

    public bool IsClick()
    {
        if (click)
        {
            click = false;
            return true;
        }

        return false;
    }

    public bool IsJump()
    {
        if (jump)
        {
            jump = false;
            return true;
        }

        return false;
    }

    public bool IsWave()
    {
        if (wave)
        {
            wave = false;
            return true;
        }

        return false;
    }


    public void ContarTempo(String tempo)
    {
        if (Tempo != null)
            Tempo.GetComponent<TextMeshProUGUI>().text = tempo;
    }


    public void UserDetected(uint userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;
		
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeDown);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);
        manager.DetectGesture(userId, KinectGestures.Gestures.Click);
        manager.DetectGesture(userId, KinectGestures.Gestures.Push);
        manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        manager.DetectGesture(userId, KinectGestures.Gestures.Wave);
        
        if (GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = GestureInfo.name;
		}
	}
	
	public void UserLost(uint userId, int userIndex)
	{
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = string.Empty;
		}
	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		if (gesture == KinectGestures.Gestures.Click)
        {
            if (progress < 0.3 )
                ContarTempo("");
            else if (progress < 0.43)
                ContarTempo("5");
            else if (progress < 0.57)
                ContarTempo("4");
            else if (progress < 0.71)
                ContarTempo("3");
            else if (progress < 0.85)
                ContarTempo("2");
            else if (progress < 0.99)
                ContarTempo("1");
            else ContarTempo("0");
        } 
        
       
    }

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		string sGestureText = gesture + " detected";
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = sGestureText;
		}

        if (gesture == KinectGestures.Gestures.SwipeLeft)
            swipeLeft = true;
        else if (gesture == KinectGestures.Gestures.SwipeRight)
            swipeRight = true;
        else if (gesture == KinectGestures.Gestures.SwipeDown)
            swipeDown = true;
        else if (gesture == KinectGestures.Gestures.SwipeUp)
            swipeUp = true;
        else if (gesture == KinectGestures.Gestures.Click)
            click = true;
        else if (gesture == KinectGestures.Gestures.Wave)
            wave = true;
        else if (gesture == KinectGestures.Gestures.Jump)
            jump = true;
        ContarTempo("");

        return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
        if (gesture == KinectGestures.Gestures.Click)
            ContarTempo(""); ;
        return true;
	}
	
}
