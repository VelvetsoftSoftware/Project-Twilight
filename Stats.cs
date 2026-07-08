using UnityEngine;

public class Stats : MonoBehaviour {
    public byte age = 10;
	public int funds = 244, income = 0;
	public double height = 4.58, weight  = 89.0, hips = 28.4;
	public byte profession = 0;
	public bool introIsActive = false;
	
	[SerializeField]private dates Dates;
	
	public float mood = 0.0f;
	
	//Social
	public ushort elegance = 0, grace = 0, glamor = 0, negotiation = 0;
	public int reputation = 0;
	//physical
	public ushort agality = 0, athletics = 0, strength = 0, fatigue = 0, craftsmanship = 0;
	//intelligance
	public ushort stategy = 0, science = 0, history = 0, math = 0;
	//faith
	public ushort morality = 0, theology = 0, sin = 0, peity = 0;
	
	//stat growth multiplier
	public float social = 1.0f, physical = 1.0f, intelligance = 1.0f, faith = 1.0f, stressgainfactor = 1.0f;
	
	public void introBool() {
		introIsActive = true;
	}
	
	public void Knight() {
		physical = 1.5f;
		social = 1.2f;
		faith = 0.75f;
		income = 100;
		profession = 1;
	}
	
	public void Merchant() {
		social = 1.5f;
		intelligance = 1.2f;
		physical = 0.75f;
		income = 150;
		profession = 2;
	}
	
	public void Theologian() {
		faith = 1.5f;
		intelligance = 1.2f;
		physical = 0.5f;
		social = 0.75f;
		reputation = 10;
		income = 5;
		profession = 3;
	}
	
	public void Scholar() {
		intelligance = 1.5f;
		faith = 1.2f;
		physical = 0.5f;
		reputation = 2;
		income = 25;
		profession = 4;
	}
	
	public void Artist() {
		social = 0.5f;
		physical = 0.75f;
		reputation = 10;
		income = 125;
		profession = 5;
	}
	
	public void Cartographer() {
		intelligance = 1.5f;
		physical = 1.2f;
		social = 0.75f;
		income = 80;
		profession = 6;
	}
	
	public void Noble() {
		social = 1.5f;
		intelligance = 1.2f;
		physical = 0.75f;
		income = 200;
		profession = 7;
	}
	
	public void Farmer() {
		physical = 1.5f;
		faith = 1.2f;
		intelligance = 0.75f;
		social = 0.5f;
		income = 10;
		profession = 8;
	}
	
	public void Craftsman() {
		physical = 1.5f;
		social = 1.2f;
		income = 90;
		profession = 9;
	}
	
	private void spring() {
		physical = physical + 0.2f;
		social = social + 0.3f;
		//const goes up quicker
		//gives extra points to releationships
	}
	
	private void summer() {
		physical = physical + 0.3f;
		intelligance = intelligance - 0.2f;
		//preforms better at comp and tournaments
	}
	
	private void autumn() {
		intelligance = intelligance + 0.2f;
		social = social - 0.2f;
		//
	}
	
	private void winter() {
		physical = physical + 0.1f;
		intelligance = intelligance + 0.1f;
		faith = faith + 0.3f;
		social = social - 0.2f;
		//mysticism
		
	}
	
	public void daughterseason() {
    if ((Dates.monthdaughter == 3 && Dates.daydaughter >= 20) || 
        (Dates.monthdaughter >= 4 && Dates.monthdaughter <= 5) || 
        (Dates.monthdaughter == 6 && Dates.daydaughter < 21)) {
        spring();
    } else if ((Dates.monthdaughter == 6 && Dates.daydaughter >= 21) || 
               (Dates.monthdaughter >= 7 && Dates.monthdaughter <= 8) || 
               (Dates.monthdaughter == 9 && Dates.daydaughter < 22)) {
        summer();
    } else if ((Dates.monthdaughter == 9 && Dates.daydaughter >= 22) || 
               (Dates.monthdaughter == 10) || 
               (Dates.monthdaughter == 11 && Dates.daydaughter <= 30)) {
        autumn();
    } else if ((Dates.monthdaughter == 11 && Dates.daydaughter >= 20) || 
               (Dates.monthdaughter == 12) || 
               (Dates.monthdaughter == 1 || Dates.monthdaughter == 2) || 
               (Dates.monthdaughter == 3 && Dates.daydaughter < 20)) {
        winter();
    }
}
	
	private void a() {
		stressgainfactor = stressgainfactor + 0.3f;
		physical = physical + 0.4f;
		//gains extra stress reduction during vacations
	}
	
	private void b() {
		stressgainfactor = stressgainfactor - 0.2f;
		intelligance = intelligance - 0.2f;
		social = social - 0.1f;
		//art and craftsmenship gos up
		// gain more stats for art/craftsmenship
	}
	
	private void ab() {
		intelligance = intelligance + 0.2f;
		social = social - 0.1f;
		faith = faith + 0.1f;
		//unlocks unique options to solve delemias
	}
	
	private void o() {
		social = social + 0.3f;
		physical = physical + 0.1f;
		// natural leader
	}
	
	public void selectblood(int type) {
		if (type == 1)
			a();
		else if (type == 2)
			b();
		else if (type == 3)
			ab();
		else
			o();
	}
}
