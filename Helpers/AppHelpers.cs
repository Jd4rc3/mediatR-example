using HashidsNet;

namespace MediatrExample.Helpers;

public static class AppHelpers
{
    private const string HasIdsSalt = "s3cret_s4lt";

    public static string ToHashId(this int number) =>
        GetHasher().Encode(number);

    public static int FromHashId(this string encoded) =>
        GetHasher().Decode(encoded).FirstOrDefault();

    private static Hashids GetHasher() => new(HasIdsSalt, 8);
}