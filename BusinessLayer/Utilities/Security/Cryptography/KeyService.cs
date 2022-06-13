using Entities.DatabaseModels;
using Entities.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Utilities.Security.Cryptography;

public class KeyService : IKeyService
{
    private readonly char[] alphanumericalCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    private readonly char[] uppercaseLettersAndNumbers =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    private readonly char[] secureUppercaseLettersAndNumbers =
        "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

    private readonly char[] testCharacters =
        "123456789".ToCharArray();

    public string GenerateUniqueKey(int size, char[] charArray)
    {
        byte[] data = new byte[4 * size];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(data);
        StringBuilder result = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * 4);
            var idx = rnd % charArray.Length;

            result.Append(charArray[idx]);
        }

        return result.ToString();
    }
}

