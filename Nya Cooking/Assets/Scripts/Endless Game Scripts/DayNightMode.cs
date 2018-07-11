using UnityEngine;

// Скрипт описывающий смену дней в бесконечной игре
// TODO реализовать фактическую смену дней за счет подведения итогов и включения возможности прокачки способностей
// TODO а так же делать сохранение в бесконечной игре только при окончании дня

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
