using System.Reflection;

namespace Allari.NetCore.Application
{
    public static class AssemblyReference
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
    }
}
