namespace TerribleSettingsAuditor.Core.Helpers;

internal static class MaskingHelper
{
    public static string MaskMiddle(string? value, int? showLeft = null, int? showRight = null, char maskChar = '*')
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        int left = Math.Max(0, showLeft ?? 0);
        int right = Math.Max(0, showRight ?? 0);

        // If nothing is requested, mask the whole thing
        if (left == 0 && right == 0)
        {
            return new string(maskChar, value.Length);
        }

        // If the visible counts would expose the whole value, just return it
        if (left + right >= value.Length)
        {
            return value;
        }

        string leftPart = left > 0 ? value.Substring(0, left) : string.Empty;
        string rightPart = right > 0 ? value.Substring(value.Length - right, right) : string.Empty;

        int maskedLength = value.Length - left - right;
        string maskedPart = new string(maskChar, maskedLength);

        return $"{leftPart}{maskedPart}{rightPart}";
    }
}
