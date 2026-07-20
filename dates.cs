using UnityEngine;
using UnityEngine.UI;

public class dates : MonoBehaviour{
	public int daydaughter = 1, monthdaughter = 1, daydad = 1, monthdad = 1;
	public ushort daughteryear = 1276, dadyear = 1264;
	public Text date, datefather;
	
	[SerializeField] private events Events;
	
	void Update() {
		showdate();
		showdatedad();
	}
	
	public void showdate() {
		date.text = $"{daydaughter}/{monthdaughter}/{daughteryear}";
	}
	
	public void setday(int newday) {
		daydaughter = newday;
		UpdateBirthdayEvent();
	}
	
	public void setmonth(int newmonth) {
		monthdaughter = newmonth;
		UpdateBirthdayEvent();
	}
	
	public void showdatedad() {
		datefather.text = $"{daydad}/{monthdad}/{dadyear}";
	}
	
	public void setdaydad(int newday) {
		daydad = newday;
	}
	
	public void setmonthdad(int newmonth) {
		monthdad = newmonth;
	}
	
	private void UpdateBirthdayEvent() {
		if (Events == null) {
			Debug.LogError("Events not assigned in dates!");
			return;
		}

		for (byte i = 0; i < Events.birthday.instructions.Count; i++) {
			events.EventInstruction instruction = Events.birthday.instructions[i];

			switch(instruction.type) {
				case events.EventOpType.RequireDay:
					instruction.values = daydaughter;
				break;
				case events.EventOpType.RequireMonth:
					instruction.values = monthdaughter;
				break;
			}
			Events.birthday.instructions[i] = instruction;
		}
	}
}
