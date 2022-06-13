using Entities.DatabaseModels;
using Entities.DTOs;

namespace BusinessLayer.Utilities.Security.Cryptography;

public interface IKeyService
{
    string GenerateUniqueKey(int size, char[] charArray);
}
