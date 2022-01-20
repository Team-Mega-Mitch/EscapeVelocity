using UnityEngine;
using System.Security.Cryptography;

public class Seeder : MonoBehaviour {
    public bool RandomSeedOverride = false;
    public string Seed;

    private string _OriginSeed;

    private void Awake() {
        _OriginSeed = Seed;

        if (RandomSeedOverride) {
            Seed = GenSeed();
        }

        SetSeed(Seed);
        Debug.Log("Seed: " + Seed);
    }

    public static void SetSeed(string seed) {
        Random.InitState(ParseSeed(seed));
    }

    public string GenSeed() {
        byte[] bytes = new byte[16];

        using (var randNum = new RNGCryptoServiceProvider()) {
            randNum.GetBytes(bytes);
        }

        return System.BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }

    private static int ParseSeed(string seed) {
        if (string.IsNullOrEmpty(seed)) {
            return (int)Random.Range(-999999999f, 999999999f);
        }

        return seed.GetHashCode();
    }
}
