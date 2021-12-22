using UnityEngine;
using System.Security.Cryptography;

public class Seeder : MonoBehaviour {
    public bool RandomSeedOverride = false;
    public string Seed;

    private int _Seed;

    private void Awake() {
        if (RandomSeedOverride) {
            Seed = GenSeed();
        }

        _Seed = ParseSeed(Seed);
        Random.InitState(_Seed);

        Debug.Log("Seed: " + Seed);
    }

    public string GenSeed() {
        byte[] bytes = new byte[16];

        using (var randNum = new RNGCryptoServiceProvider()) {
            randNum.GetBytes(bytes);
        }

        return System.BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }

    private int ParseSeed(string seed) {
        if (string.IsNullOrEmpty(seed)) {
            return (int)Random.Range(-999999999f, 999999999f);
        }

        return seed.GetHashCode();
    }
}
