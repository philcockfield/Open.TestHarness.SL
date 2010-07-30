using System;
using System.Reflection;

namespace Open.Core.Common
{
    /// <summary>Provides a way of holding onto a delegate in a weak manner.</summary>
    public abstract class WeakDelegateReference
    {
        #region Head
        protected WeakDelegateReference(object actionTarget, MethodInfo actionMethod, Type actionType)
        {
            TargetWeakReference = new WeakReference(actionTarget, false);
            Method = actionMethod;
            ActionType = actionType;
        }
        #endregion

        #region Properties
        public WeakReference TargetWeakReference { get; private set; }
        public MethodInfo Method { get; private set; }
        public Type ActionType { get; private set; }
        #endregion

        #region Methods
        public bool IsMatch(Delegate action)
        {
            if (action == null) return false;

            if (action.Target != TargetWeakReference.Target) return false;
            if (action.GetType() != ActionType) return false;
            if (action.Method != Method) return false;

            return true;
        }

        public Delegate TryGetDelegate()
        {
            if (Method.IsStatic)
            {
                return Delegate.CreateDelegate(ActionType, null, Method);
            }
            var target = TargetWeakReference.Target;
            if (target != null)
            {
                try
                {
                    return Delegate.CreateDelegate(ActionType, target, Method);
                }
                catch (MethodAccessException error)
                {
                    throw new MethodAccessException(
                        string.Format(
                            "Cannot invoke the delegate method named '{0}' because it cannot access it.  If running in Silverlight ensure that the delegate end-point is public.",
                            Method.Name),
                        error);
                }
            }
            return null;
        }
        #endregion
    }
}
