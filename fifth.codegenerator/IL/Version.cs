namespace Fifth.CodeGeneration.IL;

public class Version
{
    public Version(int Major, int? Minor, int? Build, int? Patch)
    {
        this.Major = Major;
        this.Minor = Minor;
        this.Build = Build;
        this.Patch = Patch;
    }

    public Version() { }

    public Version(string s)
    {
        var segs = Array.Empty<string>();
        if (s.Contains('.'))
        {
            segs = s.Split('.');
        }
        else if (s.Contains(':'))
        {
            segs = s.Split(':');
        }

        if (segs.Length > 0)
        {
            if (int.TryParse(segs[0], out var major))
            {
                Major = major;
            }
        }

        if (segs.Length > 1)
        {
            if (int.TryParse(segs[1], out var minor))
            {
                Minor = minor;
            }
        }

        if (segs.Length > 2)
        {
            if (int.TryParse(segs[2], out var build))
            {
                Build = build;
            }
        }

        if (segs.Length > 3)
        {
            if (int.TryParse(segs[3], out var patch))
            {
                Patch = patch;
            }
        }
    }

    public int Major { get; init; }
    public int? Minor { get; init; }
    public int? Build { get; init; }
    public int? Patch { get; init; }

    public void Deconstruct(out int Major, out int? Minor, out int? Build, out int? Patch)
    {
        Major = this.Major;
        Minor = this.Minor;
        Build = this.Build;
        Patch = this.Patch;
    }
}
