using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class statgain : MonoBehaviour {
	
	/*action for week:
	name of job/school/rest/vacation
	name
	pay/cost per hour
	success animation animantion
	failure animation*/
	
	[SerializeField]private bitpacker Bitpacker;
	[SerializeField]private Stats stats;
	[SerializeField]private jobs Jobs;
	private float difficulty = 0.0f;
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
	
	//we need to add the string to this to activate it
	public void startroutine(int x){
		jobs.Activity job = activityLookup[x];

		// this is only tempory for initialization
		ushort currentOperation = 0;
		ref ushort opStat = ref stats.elegance;
		ref float modifier = ref stats.social;
		
		for(ushort m = 0; m < 8; m++) {
				
			byte currentStat = Bitpacker.unpacker(m, job);
			if(firstsecond == 0) 
				firstsecond = 1;
			else 
				firstsecond = 0;
				
			currentOperation = Bitpacker.statsUnpacker(1 , firstsecond, m, job);
				
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
			
			if( currentOperation != 0) {
				// --- increase ---
				if(m <= 3) {
					if (opStat < 1000){
						float gain = (modifier + weightedrandomnumber()) * currentOperation + job.proficiency;
						opStat = (ushort)(opStat + calcstatgain(currentOperation, job.proficiency, stats.fatigue, 0.5f /*difficulty*/, modifier, job));
						if(opStat > 1000){
							opStat = 1000;
						}
					}
				} /* --- decrease --- */else {
					if (opStat > 0){
					float loss = (modifier + weightedrandomnumber()) * currentOperation;
					if (opStat - loss >= 0)
						opStat -= (ushort)Mathf.Clamp(Mathf.RoundToInt(loss), 0, opStat);
					else
						opStat = 0;
					}
				}
			}
		}
	
		// Add funds
		stats.funds += (int)mathforfunds(job, stats.funds);

		// --- Fatigue increase ---
		float fatigueGain = (stats.physical + weightedrandomnumber()) * (job.PackedStats & 0xF);
		stats.fatigue += (ushort)Mathf.Clamp(Mathf.RoundToInt(fatigueGain), 0, ushort.MaxValue - stats.fatigue);
	}
	
	float weightedrandomnumber(){
		int randomweight = UnityEngine.Random.Range(1, 100);

		if (1 <= randomweight && randomweight <= 6) {
			return -2.0f;
		} else if (7 <= randomweight && randomweight <= 17) {
			return -1.0f;
		} else if (18 <= randomweight && randomweight <= 58) {
			return 0.0f;
		} else if (59 <= randomweight && randomweight <= 89) {
			return 1.0f;
		} else if (90 <= randomweight && randomweight <= 100) {
			return 2.0f;
		}

		return 0.0f;
	}
	
	int mathforfunds(jobs.Activity job, float stat) {
		float k = 300.0f;
		float a = 4.0f;
		float sqrtcomp = Mathf.Sqrt(stat);
		float satcomp = stat / (stat + k);
		float skillpower = a * sqrtcomp * satcomp;
		float effectivefatigue;
		int randomchance = UnityEngine.Random.Range(0, 10);

		if(stats.age <= 14)
			effectivefatigue = Mathf.Pow(stats.fatigue, 3) / 1000000.0f;
		else
			effectivefatigue = Mathf.Pow(stats.fatigue, 2) / 1000000.0f;
		if(effectivefatigue > 1000.0f)
			effectivefatigue = 1000.0f;
		float condition = ((1000.0f - effectivefatigue) + stats.mood + job.proficiency);
	
		if(condition < 0.0f)
			condition = 0.0f;
		else
			condition = Mathf.Clamp01(condition / 1000f);
	
		float randterm = (randomchance - difficulty) * 2.0f;
	
		float jobscore = (skillpower * condition) + randterm;
	
		if(jobscore >= 50.0f){
			Debug.Log("you got cash" + jobscore);
			return Bitpacker.moneyUnpacker(job);
		}else
			return 0;
	}
	
	ushort calcstatgain(float stat, float proficiency, float fatigue, float difficulty, float growthmultiplayer, jobs.Activity job) {
		// Random bias (weighted, like your weightedrandomnumber)
		float randBias = weightedrandomnumber();

		// Core performance formula (similar to mathforfunds)
		float k = 300.0f;
		float a = 4.0f;
		float sqrtComp = Mathf.Sqrt(stat);
		float decay = 2 * Mathf.Pow(0.88f, (stats.age - 10));
		float satComp = stat / (stat + k);
		float skillPower = a * sqrtComp * satComp;
		float mastery = 0;

		// Fatigue scaling (same logic as before)
		float effectiveFatigue;
		if (stats.age <= 14)
			effectiveFatigue = Mathf.Pow(fatigue, 3) / 10000.0f;
		else
			effectiveFatigue = Mathf.Pow(fatigue, 2) / 10000.0f;
		if (effectiveFatigue > 1000.0f)
			effectiveFatigue = 1000.0f;
		Debug.Log("effectiveFatigue " + effectiveFatigue);

		// Proficiency modifies efficiency (like training familiarity)
		int completedTiers = Mathf.FloorToInt(proficiency); // full mastery cycles
		float fractionalProgress = proficiency - completedTiers; // progress toward next tier
		
		switch (completedTiers) {
			case 1: mastery = 0.5f; break;
			case 2: mastery = 1.0f; break;
			case 3: mastery = 1.5f; break;
		}
		
		mastery += fractionalProgress;

		float proficiencyFactor = 1.0f + (mastery / 100.0f); // +1% per proficiency point

		// Difficulty reduces effective progress
		float difficultyFactor = 1.0f - (difficulty / 20.0f); // e.g. difficulty 5 = -25%

		// Combine all influences
		float condition = Mathf.Clamp01((1000.0f - effectiveFatigue) / 1000.0f);
		float totalEffect = (growthmultiplayer * skillPower * proficiencyFactor * condition * difficultyFactor * decay) + randBias;

		// Scale to PM2-like progression (around +10–20 strength per month)
		float strengthGain = Mathf.Clamp(totalEffect * 0.25f, 1.0f, 20.0f);
		Debug.Log("strengthGain " + strengthGain);
		
		if (job.proficiency < 3.0f)
			job.proficiency += 0.1f;
		return (ushort)Mathf.Abs(Mathf.RoundToInt(strengthGain));
	}
}
