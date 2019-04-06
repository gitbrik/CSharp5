using System.Windows.Controls;

namespace _05_Lab_CS.Tools.Navigation
{
    internal interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}
