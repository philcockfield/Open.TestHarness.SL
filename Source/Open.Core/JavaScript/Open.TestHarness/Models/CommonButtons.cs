using Open.Core;
using Open.Core.Controls.Buttons;

namespace Open.Testing.Models
{
    /// <summary>The index of common buttons.</summary>
    public class CommonButtons : ModelBase
    {
        #region Head
        private IButton addPackage;
        #endregion

        #region Properties
        /// <summary>Gets the [+] button used to add a new test package.</summary>
        public IButton AddPackage
        {
            get { return addPackage ?? (addPackage = ImageButtonFactory.Create(ImageButtons.PlusDark)); }
        }
        #endregion
    }
}
