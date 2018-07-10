using UnityEngine;

public class DayNightMode : MonoBehaviour
{
    public float DayTime = 30;
	
	// Update is called once per frame
	void FixedUpdate () {
		TimeCalculating();
	}

    void TimeCalculating()
    {
        if (DayTime > 0)
        {
            DayTime -= Time.deltaTime;
        }

        if (DayTime < 0)
        {
            DayChanging();
        }
    }

    void DayChanging()
    {
        print("День сменился");
        DayTime = 30;
    }
}
