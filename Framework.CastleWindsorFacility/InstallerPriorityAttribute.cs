using System;

namespace Framework.CastleWindsorFacility
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InstallerPriorityAttribute : Attribute
    {
        public const int DefaultPriority = 100;

        public int Priority { get; private set; }

        public InstallerPriorityAttribute(int priority)
        {
            this.Priority = priority;
        }
    }
}