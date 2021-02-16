// ####################################################
// # AR-Watch Demo by Adrian Schroeder @Adrian_Schr   #
// # Date: 02-17-2021                                 #
// ####################################################

using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class SetOwnTimeScript : MonoBehaviour
{
	// part of the code by Jasper Flick from: https://catlikecoding.com/unity/tutorials/basics/game-objects-and-scripts/
	// Thanks, Jasper!

	[SerializeField] Transform hoursArrowObj, minutesArrowObj, secondsArrowObj;
	[SerializeField] CityName chosenCity;
	[SerializeField] GameObject textObj;
	private const float hoursToDegrees = -30f, minutesToDegrees = -6f, secondsToDegrees = -6f;	
	private TimeZoneInfo chosenTimeZone;
	private DateTime chosenTime;	
	private TextMeshProUGUI textMeshPro;
	private Color32 whiteFont = new Color32(235, 235, 235, 255);
	private  Color32 blackFont = new Color32(10, 10, 10, 255);

	void Awake()
	{
		// cache the TextMeshPro Component
		textMeshPro = textObj.GetComponent<TextMeshProUGUI>();

		#if UNITY_IOS
		switch (chosenCity)
		{
			case CityName.Venice:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Malta");
			break;
			case CityName.Okinawa:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
			break;
			case CityName.Boston:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
			break;
			case CityName.Cardiff:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/London");
			break;
			default:
			break;
		}
		#endif

		#if UNITY_EDITOR_WIN
		switch (chosenCity)
		{
			case CityName.Venice:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
			break;
			case CityName.Okinawa:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
			break;
			case CityName.Boston:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
			break;
			case CityName.Cardiff:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Greenwich Standard Time");
			break;
			default:
			break;
		}
		#endif

		#if UNITY_ANDROID
		case CityName.Venice:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Malta");
			break;
			case CityName.Okinawa:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
			break;
			case CityName.Boston:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
			break;
			case CityName.Cardiff:
			chosenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/London");
			break;
			default:
			break;
		#endif

		// checks if a TimeZone was chosen for this gameobject
		if(chosenTimeZone != null)
		{
			chosenTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, chosenTimeZone);
			CheckForDayTime(chosenTime.Hour);
		}
		InitialTimeSet();
	}

	void FixedUpdate()
	{
		// updates the time to always show current time, not the one when it was loaded
		updateTime();
	}

	// sets the initial position for the watch-hands
	// code by Jasper Flick from: https://catlikecoding.com/unity/tutorials/basics/game-objects-and-scripts/
	private void InitialTimeSet()
    {
		Vector3 hoursCurrentRotation = hoursArrowObj.localRotation.eulerAngles;
		Vector3 hoursTargetRotation = Quaternion.Euler(0f, hoursToDegrees * chosenTime.Hour, 0f).eulerAngles;
		StartCoroutine(LerpRotationInTime(hoursCurrentRotation, hoursTargetRotation, 5f, hoursArrowObj));

		Vector3 minutesCurrentRotation = minutesArrowObj.localRotation.eulerAngles;
		Vector3 minutesTargetRotation = new Vector3(0f, minutesToDegrees * chosenTime.Minute, 0f);
		StartCoroutine(LerpRotationInTime(minutesCurrentRotation, minutesTargetRotation, 5f, minutesArrowObj));
	}

	// grabs the current time and moves the watch-hands
	private void updateTime()
    {
		chosenTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, chosenTimeZone);			
		
		hoursArrowObj.localRotation = Quaternion.Euler(0f, hoursToDegrees * chosenTime.Hour, 0f);
		minutesArrowObj.localRotation =	Quaternion.Euler(0f, minutesToDegrees * chosenTime.Minute, 0f);
		secondsArrowObj.localRotation =	Quaternion.Euler(0f, secondsToDegrees * chosenTime.Second, 0f);
	}

	// starts the setup animation at the beginning
	private IEnumerator LerpRotationInTime( Vector3 startRotation, Vector3 endRotation, float totalTime, Transform transformToLerp)
	{
		var t = 0f;
		while (t < 1)
		{
			t += Time.deltaTime / totalTime;		
			float cachedLerp = Mathf.Lerp(startRotation.y, endRotation.y, t);
			transformToLerp.localRotation = Quaternion.Euler(0f, cachedLerp, 180f);
			yield return null;
		}
	}

	// checks if it's daytime or nighttime
	private void CheckForDayTime(int amountOfHours)
	{
		if(amountOfHours > 6 && amountOfHours < 19)
		{
			ChangeFontColor(true);
		}
		else
		{
			ChangeFontColor(false);
		}
	}

	private void ChangeFontColor(bool isDayTime)
	{
		// render the font black/ white according to day/night time
		if(isDayTime)
		{
			textMeshPro.color = whiteFont;
		}
		else
		{
			textMeshPro.color = blackFont;
		}
	}

}
