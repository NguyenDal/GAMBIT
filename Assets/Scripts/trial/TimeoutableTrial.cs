using UnityEngine;

namespace trial
{
    public abstract class TimeoutableTrial : AbstractTrial
    {
        private readonly float _threshHold;

        protected TimeoutableTrial(int blockId, int trialId) : base(blockId, trialId)
        {
            _threshHold = trialData.TrialTime < 1 ? int.MaxValue : trialData.TrialTime;
        }

        //Code for a trial to continue
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (_runningTime > _threshHold)
            {
                // if timer runs out, player will not achieve a star for the 3 star system
                PlayerPrefs.SetInt("LevelCompleted", 0);
                PlayerPrefs.Save();

                // If not a playable trial (ie. loading screen), simply move to next level
                if (trialData.Instructional == 1)
                {
                    Progress();
                }
            }
        }
    }
}