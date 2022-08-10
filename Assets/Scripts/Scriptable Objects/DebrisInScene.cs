using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Debris In Scene")]
public class DebrisInScene : ScriptableObject
{
    public Dictionary<Debris, int> data = new Dictionary<Debris, int>();
    public Dictionary<Debris, int> pointerData = new Dictionary<Debris, int>();

    public int getTotal() {
        int total = 0;
        foreach (KeyValuePair<Debris, int> item in data) {
            int pointerAmount;
            pointerData.TryGetValue(item.Key, out pointerAmount);
            total += pointerAmount;
            total += item.Value;
        }
        return total;
    }

    public int getTotalForDebris(Debris debris) {
        int spawnTotal;
        data.TryGetValue(debris, out spawnTotal);
        int pointerTotal;
        pointerData.TryGetValue(debris, out pointerTotal);
        return spawnTotal + pointerTotal;
    }
}
