using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class statgain : MonoBehaviour {
	
	[SerializeField] private bitpacker Bitpacker;
	[SerializeField] private Stats stats;
	[SerializeField] private jobs Jobs;
	public Button maidButton, chapelCleanerButton, babySitterButton, houseWorkButton, chimneySweeperButton;
	private Dictionary<int, jobs.Activity> _activityLookup;
	byte firstsecond = 0;
	
	public Dictionary<int, jobs.Activity> activityLookup {
		get {
			if (_activityLookup == null){
				if(Jobs == null) Debug.LogError("Jobs not assigned!");
				_activityLookup = new Dictionary<int, jobs.Activity>() {
					{ 1, Jobs.maid },
					{ 2, Jobs.babySitter },
					{ 3, Jobs.chapelCleaner },
					{ 4, Jobs.houseWork },
					/*{ 5, Jobs.chimmneySweeper },*/
				};
			}
			return _activityLookup;
		}
	}
	
	public void startroutine(int x){
		jobs.Activity job = activityLookup[x];

		ushort currentOperation = 0;
		ref ushort opStat = ref stats.elegance;
		ref byte modifier = ref stats.social;
		
		for(ushort m = 0; m < 8; m++) {
			byte currentStat = Bitpacker.unpacker(m, job);
			if(firstsecond == 0) 
				firstsecond = 1;
			else 
				firstsecond = 0;
				
			currentOperation = Bitpacker.statsUnpacker(1, firstsecond, m, job);
				
			switch(currentStat) {
				case 0: opStat = ref stats.elegance; modifier = ref stats.social; break;
				case 2: opStat = ref stats.grace; modifier = ref stats.social; break;
				case 4: opStat = ref stats.glamor; modifier = ref stats.social; break;
				case 6: opStat = ref stats.negotiation; modifier = ref stats.social; break;
				
				case 8: opStat = ref stats.agality; modifier = ref stats.physical; break;
				case 10: opStat = ref stats.athletics; modifier = ref stats.physical; break;
				case 12: opStat = ref stats.strength; modifier = ref stats.physical; break;
				case 14: opStat = ref stats.craftsmanship; modifier = ref stats.physical; break;
				
				case 1: opStat = ref stats.stategy; modifier = ref stats.intelligance; break;
				case 3: opStat = ref stats.science; modifier = ref stats.intelligance; break;
				case 5: opStat = ref stats.history; modifier = ref stats.intelligance; break;
				case 7: opStat = ref stats.math; modifier = ref stats.intelligance; break;
				
				case 9: opStat = ref stats.morality; modifier = ref stats.faith; break;
				case 11: opStat = ref stats.theology; modifier = ref stats.faith; break;
				case 13: opStat = ref stats.sin; modifier = ref stats.faith; break;
				case 15: opStat = ref stats.peity; modifier = ref stats.faith; break;
			}
				
			if(currentOperation != 0) {
				// --- increase ---
				if(m <= 3) {
					if (opStat < 1000){
						opStat = (ushort)(opStat + calcstatgain(opStat, job.Proficiency, stats.fatigue, modifier, job));
						if(opStat > 1000){
							opStat = 1000;
						}
					}
				} 
				// --- decrease --- 
				else {
					if (opStat > 0){
						int loss = (modifier + weightedrandomnumber()) * currentOperation;
						if (opStat - loss >= 0)
							opStat -= (ushort)Mathf.Clamp(loss, 0, opStat);
						else
							opStat = 0;
					}
				}
			}
		}
		
		// Add funds
		stats.funds += mathforfunds(job, opStat);

		// --- Fatigue increase ---
		int fatigueGain = ((stats.fatigue + weightedrandomnumber()) * (byte)(job.PackedStats & 0x000000000F)) * stats.stressgainfactor;
		stats.fatigue += (ushort)Mathf.Clamp(fatigueGain, 0, ushort.MaxValue - stats.fatigue);
	}
	
	private int weightedrandomnumber(){
		byte rng = mathlib.byteRNG(); // Assumes byteRNG is accessible in mathlib or locally
		
		// Map 0-255 to percentage (0-99)
		int roll = (rng * 100) / 255;

		if (roll < 50) return 0;       // 50% chance
		if (roll < 70) return -1;      // 20% chance (50 to 69)
		if (roll < 90) return 1;       // 20% chance (70 to 89)
		if (roll < 95) return -2;      // 5% chance  (90 to 94)
		return 2;                      // 5% chance  (95 to 99)
	}
	
	private (uint skillPower, uint effectiveFatigue, uint masteryBonus) GetSharedCalculations(uint stat, ushort fatigue, ushort proficiency) {
		// 1. Skill Power & Saturation
		uint sqrtcomp = mathlib.Quicksqrt(stat);
		uint satcompDivider = stat + 300;
		uint satcomp = satcompDivider == 0 ? 0 : mathlib.Divider(stat * 100, satcompDivider); 
		uint skillPower = mathlib.Divider((4 * sqrtcomp * satcomp), 100);
		
		// 2. Effective Fatigue
		uint effectiveFatigue;
		if (stats.age <= 14)
			effectiveFatigue = mathlib.Divider(mathlib.Exponent(fatigue, 3), 10000);
		else
			effectiveFatigue = mathlib.Divider(mathlib.Exponent(fatigue, 2), 10000);

		if (effectiveFatigue > 1000)
			effectiveFatigue = 1000;

		// 3. Mastery Bonus
		uint masteryBonus = 0;
		switch (proficiency) {
			case 100: masteryBonus = 2; break;
			case 200: masteryBonus = 4; break;
			case 300: masteryBonus = 6; break;
			default:  masteryBonus = 0; break;
		}

		return (skillPower, effectiveFatigue, masteryBonus);
	}
	
	private int mathforfunds(jobs.Activity job, uint stat) {
		var shared = GetSharedCalculations(stat, stats.fatigue, job.Proficiency);
		
		int randomchance = weightedrandomnumber();
		
		// Condition factors in fatigue penalty, mood, and mastery bonus
		int condition = (int)((1000 - shared.effectiveFatigue) + stats.mood + shared.masteryBonus);
	
		if (condition < 0) condition = 0;
		else if (condition > 1000) condition = 1000;

		int randterm = randomchance * 2;
	
		uint scaledCondition = mathlib.Divider((uint)Mathf.Max(0, condition), 10);
		uint jobscore = (uint)(shared.skillPower * mathlib.Divider(scaledCondition, 100) + randterm);
	
		if (jobscore >= 50) {
			Debug.Log("you got cash " + jobscore);
			return Bitpacker.moneyUnpacker(job);
		} else {
			return 0;
		}
	}
	
	private ushort calcstatgain(uint stat, ushort proficiency, ushort fatigue, uint growthmultiplayer, jobs.Activity job) {
		var shared = GetSharedCalculations(stat, fatigue, proficiency);
		int randBias = weightedrandomnumber();

		// Simplified calculation utilizing the shared mastery bonus directly
		int baseGain = (int)(growthmultiplayer / 50) + (int)shared.masteryBonus;
		int finalGain = baseGain + randBias;

		return (ushort)Mathf.Clamp(finalGain, 0, 20);
	}
}
