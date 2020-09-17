using System.Collections.Generic;
using UnityEngine;

public class Plantable : MonoBehaviour
{
    [Tooltip("Actual prefabs for each growth tier, in ascending order from earliest to most mature.")]
    public List<GameObject> GrowthTiers = new List<GameObject>();

    [Tooltip("Seconds between each growth step.")]
    public float GrowthStepDuration = 5f;

    [Tooltip("Total seconds needed to reach full maturity.")]
    public float FullMaturityDuration = 30f;

    [Tooltip("Multiplier for how much scaling is applied to each growth step.")]
    public float GrowthScalingMultiple = 10f;

    private float _nextGrowthTime;
    private int _stage;
    private readonly Dictionary<int, GameObject> _growthAssetMapping = new Dictionary<int, GameObject>();
    private GameObject _currentPlantable;
    private int _currentTier;

    private void Start()
    {
        _stage = 1;

        var stageCtr = 0;
        // todo: this is ugly, clean this up
        for (var i = 2; i < FullMaturityDuration; i++)
        {
            if (i % (int)(FullMaturityDuration / (GrowthTiers.Count - 1)) == 0)
            {
                _growthAssetMapping[i] = GrowthTiers[stageCtr++ + 1];
            }
        }

        // load initial tier immediately
        _currentPlantable = Instantiate(GrowthTiers[0], transform);
        _currentTier = 1;
    }

    private void Update()
    {
        if (_currentTier < GrowthTiers.Count && Time.time > _nextGrowthTime)
        {
            _nextGrowthTime = Time.time + GrowthStepDuration;
            UpgradeStage();
        }
    }

    private void UpgradeStage()
    {
        if (_growthAssetMapping.ContainsKey(++_stage))
        {
            Debug.Log($"Plantable tier upgraded to tier {_currentTier++}");
            Destroy(_currentPlantable);
            _currentPlantable = null;
            _currentPlantable = Instantiate(_growthAssetMapping[_stage], transform);
        }
    }
}
