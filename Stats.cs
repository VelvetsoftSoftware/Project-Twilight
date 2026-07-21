using UnityEngine;

public class Stats : MonoBehaviour {
    public byte age = 10;
	public int funds = 244, income = 0;
	public ushort height = 458, weight  = 8900, hips = 2840; // all if pushed left 2, so height would equal 4.58
	public byte profession = 0;
	public bool introIsActive = false;
	
	[SerializeField]private dates Dates;
	
	public ushort mood = 0.0f;
	
	//Social
	public ushort elegance = 0, grace = 0, glamor = 0, negotiation = 0;
	public int reputation = 0;
	//physical
	public ushort agality = 0, athletics = 0, strength = 0, fatigue = 0, craftsmanship = 0;
	//intelligance
	public ushort stategy = 0, science = 0, history = 0, math = 0;
	//faith
	public ushort morality = 0, theology = 0, sin = 0, peity = 0;
	
	//stat growth multiplier shifted left 2 times so i would be 1.00
	public byte social = 100, physical = 100, intelligance = 100, faith = 100, stressgainfactor = 100;
	
	public void introBool() {
		introIsActive = true;
	}
	
	public void Knight() {
		physical = 150;
		social = 120;
		faith = 75;
		income = 100;
		profession = 1;
	}
	
	public void Merchant() {
		social = 150;
		intelligance = 120;
		physical = 75;
		income = 150;
		profession = 2;
	}
	
	public void Theologian() {
		faith = 150;
		intelligance = 120;
		physical = 50;
		social = 75;
		reputation = 10;
		income = 5;
		profession = 3;
	}
	
	public void Scholar() {
		intelligance = 150;
		faith = 120;
		physical = 50;
		reputation = 2;
		income = 25;
		profession = 4;
	}
	
	public void Artist() {
		social = 50;
		physical = 75;
		reputation = 10;
		income = 125;
		profession = 5;
	}
	
	public void Cartographer() {
		intelligance = 150;
		physical = 120;
		social = 75;
		income = 80;
		profession = 6;
	}
	
	public void Noble() {
		social = 150;
		intelligance = 120;
		physical = 075;
		income = 200;
		profession = 7;
	}
	
	public void Farmer() {
		physical = 150;
		faith = 120;
		intelligance = 75;
		social = 50;
		income = 10;
		profession = 8;
	}
	
	public void Craftsman() {
		physical = 150;
		social = 120;
		income = 90;
		profession = 9;
	}
	
	private void spring() {
		physical = physical + 20;
		social = social + 30;
		//const goes up quicker
		//gives extra points to releationships
	}
	
	private void summer() {
		physical = physical + 30;
		intelligance = intelligance - 20;
		//preforms better at comp and tournaments
	}
	
	private void autumn() {
		intelligance = intelligance + 20;
		social = social - 20;
		//
	}
	
	private void winter() {
		physical = physical + 10;
		intelligance = intelligance + 10;
		faith = faith + 30;
		social = social - 20;
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
		stressgainfactor = stressgainfactor + 30;
		physical = physical + 40;
		//gains extra stress reduction during vacations
	}
	
	private void b() {
		stressgainfactor = stressgainfactor - 20;
		intelligance = intelligance - 20;
		social = social - 10;
		//art and craftsmenship gos up
		// gain more stats for art/craftsmenship
	}
	
	private void ab() {
		intelligance = intelligance + 20;
		social = social - 10;
		faith = faith + 10;
		//unlocks unique options to solve delemias
	}
	
	private void o() {
		social = social + 30;
		physical = physical + 10;
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
