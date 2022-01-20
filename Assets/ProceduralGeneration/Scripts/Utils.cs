using UnityEngine;
using System.Security.Cryptography;

public class Utils : MonoBehaviour {
    public static string GenHash() {
        byte[] bytes = new byte[16];

        using (var randNum = new RNGCryptoServiceProvider()) {
            randNum.GetBytes(bytes);
        }

        return System.BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }

    public static string GenUUID(int segment) {
        string results = "";
        string[] uuid = GenUUID().Split('-');

        for (int i = 0; i < segment; i++) {
            results += uuid[i];
        }

        return results;
    }

    public static string GenUUID() {
        return System.Guid.NewGuid().ToString();
    }
}
