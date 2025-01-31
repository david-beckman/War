namespace War
{
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode]
    [CompilerGenerated]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal sealed class Strings
    {
        private static ResourceManager resourceMan;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager =>
            resourceMan ?? (resourceMan = new ResourceManager(typeof(Strings).FullName, typeof(Strings).Assembly));

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture { get; set; }

        internal static string Arg_FaceMatch => ResourceManager.GetString(nameof(Arg_FaceMatch), Culture);

        internal static string Arg_FaceMismatch => ResourceManager.GetString(nameof(Arg_FaceMismatch), Culture);

        internal static string InvalidOperation_EnumEnded => ResourceManager.GetString(nameof(InvalidOperation_EnumEnded), Culture);

        internal static string InvalidOperation_EnumNotStarted =>
            ResourceManager.GetString(nameof(InvalidOperation_EnumNotStarted), Culture);
    }
}
