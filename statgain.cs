using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class statgain : MonoBehaviour {
	
	[SerializeField] private bitpacker Bitpacker;
	[SerializeField] private Stats stats;
	[SerializeField] private jobs Jobs;
	private byte difficulty = 0;
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
						opStat = (ushort)(opStat + calcstatgain(opStat, job.Proficiency, stats.fatigue, 50, modifier, job));
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
		stats.funds += mathforfunds(job, stats.elegance);

		// --- Fatigue increase ---
		int fatigueGain = (stats.physical + weightedrandomnumber()) * (int)(job.PackedStats & 0xF);
		stats.fatigue += (ushort)Mathf.Clamp(fatigueGain, 0, ushort.MaxValue - stats.fatigue);
	}
	
	int weightedrandomnumber(){
		int randomweight = UnityEngine.Random.Range(1, 100);

		if (randomweight <= 6) return -2;
		if (randomweight <= 17) return -1;
		if (randomweight <= 58) return 0;
		if (randomweight <= 89) return 1;
		return 2;
	}
	
	int mathforfunds(jobs.Activity job, uint stat) {
		uint k = 300;
		uint a = 4;
		uint sqrtcomp = mathlib.Quicksqrt(stat);
		
		// Replaced standard division with mathlib.Divider for saturation computation
		uint satcompDivider = stat + k;
		uint satcomp = satcompDivider == 0 ? 0 : mathlib.Divider(stat * 100, satcompDivider); 

		uint skillpower = (a * sqrtcomp * satcomp) / 100;
		uint effectivefatigue;
		int randomchance = UnityEngine.Random.Range(0, 10);

		// Replaced division scaling with mathlib.Divider
		if (stats.age <= 14)
			effectivefatigue = mathlib.Divider(mathlib.Exponent(stats.fatigue, 3), 1000000);
		else
			effectivefatigue = mathlib.Divider(mathlib.Exponent(stats.fatigue, 2), 1000000);

		if (effectivefatigue > 1000)
			effectivefatigue = 1000;

		int condition = (int)((1000 - effectivefatigue) + stats.mood + job.Proficiency);
	
		if (condition < 0)
			condition = 0;
		else if (condition > 1000)
			condition = 1000;

		int randterm = (randomchance - difficulty) * 2;
	
		// Replaced standard division with mathlib.Divider for percentage normalization
		long scaledCondition = mathlib.Divider((uint)Mathf.Max(0, condition), 10); // condition / 1000 scaled down to percentage
		long jobscore = ((long)skillpower * scaledCondition / 100) + randterm;
	
		if (jobscore >= 50) {
			Debug.Log("you got cash " + jobscore);
			return Bitpacker.moneyUnpacker(job);
		} else {
			return 0;
		}
	}
	
	ushort calcstatgain(uint stat, byte proficiency, ushort fatigue, uint difficultyRating, uint growthmultiplayer, jobs.Activity job) {
		int randBias = weightedrandomnumber();

		uint k = 300;
		uint a = 4;
		uint sqrtComp = mathlib.Quicksqrt(stat);
		
		uint ageDiff = (uint)Mathf.Max(0, stats.age - 10);
		uint decay = 2 * mathlib.Divider(mathlib.Exponent(88, (byte)ageDiff), 100);

		uint satcompDivider = stat + k;
		uint satcomp = satcompDivider == 0 ? 0 : mathlib.Divider(stat * 100, satcompDivider);
		uint skillPower = (a * sqrtComp * satcomp) / 100;
		
		uint mastery = 0;
		uint effectiveFatigue;

		// Replaced division scaling with mathlib.Divider
		if (stats.age <= 14)
			effectiveFatigue = mathlib.Divider(mathlib.Exponent(fatigue, 3), 10000);
		else
			effectiveFatigue = mathlib.Divider(mathlib.Exponent(fatigue, 2), 10000);

		if (effectiveFatigue > 1000)
			effectiveFatigue = 1000;

		int completedTiers = proficiency; 
		switch (completedTiers) {
			case 1: mastery = 50; break;
			case 2: mastery = 100; break;
			case 3: mastery = 150; break;
		}

		uint proficiencyFactor = 100 + mastery; 
		
		// Replaced standard division for difficulty scaling with mathlib.Divider
		uint difficultyPenalty = mathlib.Divider(difficultyRating * 100, 20);
		uint difficultyFactor = 100 - difficultyPenalty; 
		uint condition = (1000 - effectiveFatigue);

		// Combined calculation utilizing the reciprocal multiplier pipeline
		long totalEffect = (((long)growthmultiplayer * skillPower * proficiencyFactor * condition * difficultyFactor * decay) / 100000000) + randBias;

		// Final scale down via mathlib.Divider instead of raw division
		long strengthGain = Mathf.Clamp((int)mathlib.Divider((uint)Mathf.Abs((int)totalEffect), 4), 1, 20);
		Debug.Log("strengthGain " + strengthGain);
		
		return (ushort)strengthGain;
	}
}
