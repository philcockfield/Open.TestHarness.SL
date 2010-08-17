using System;

namespace Open.Core
{
    /// <summary>Declares a constructorless class to be run on the post-build event.</summary>
    /// <remarks>
    ///     See the 'Open.Core.PostBuild' project.<br/>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class OnPostBuildAttribute : Attribute
    {
        /// <summary>Gets or sets the tag used to determine if the post-build action should be run.</summary>
        public string Tag { get; set; }
    }
}
