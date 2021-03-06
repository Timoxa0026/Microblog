#region << Using >>

using Microblog.UI.App_Start;
using WebActivator;

#endregion

[assembly: PreApplicationStartMethod(
        typeof(IncodingStart), "PreStart")]

namespace Microblog.UI.App_Start
{
    #region << Using >>

    using Microblog.Domain;
    using Microblog.UI.Controllers;

    #endregion

    public static class IncodingStart
    {
        public static void PreStart()
        {
            Bootstrapper.Start();
            new DispatcherController(); // init routes
        }
    }
}