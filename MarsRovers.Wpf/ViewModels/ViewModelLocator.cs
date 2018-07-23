/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ThoughtWorks.CodingTests.MarsRovers.ViewModels"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:ThoughtWorks.CodingTests.MarsRovers.ViewModels"
  DataContext="{Binding Source={x:Static vm:ViewModelLocator.ViewModelNameStatic}}"
*/

namespace ThoughtWorks.CodingTests.MarsRovers.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the ViewModelLocator in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocator xmlns:vm="clr-namespace:ThoughtWorks.CodingTests.MarsRovers.ViewModels"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// <para>
    /// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
    /// the ViewModelName property and bind to the ViewModelNameStatic property instead:
    /// </para>
    /// <code>
    /// xmlns:vm="clr-namespace:ThoughtWorks.CodingTests.MarsRovers.ViewModels"
    /// DataContext="{Binding Source={x:Static vm:ViewModelLocator.ViewModelNameStatic}}"
    /// </code>
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view models
            ////}
            ////else
            ////{
            ////    // Create run time view models
            ////}
        }

        #region View Model MarsRovers Reference

        private static MarsRoverViewModel marsRovers;

        /// <summary>
        /// Gets the instance of MarsRovers.
        /// </summary>
        public static MarsRoverViewModel MarsRoversStatic
        {
            get
            {
                if (marsRovers == null)
                {
                    CreateMarsRovers();
                }

                return marsRovers;
            }
        }

        /// <summary>
        /// Gets the instance of MarsRovers.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MarsRoverViewModel MarsRovers
        {
            get
            {
                return MarsRoversStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to create the MarsRovers instance.
        /// </summary>
        public static void CreateMarsRovers(bool force = false)
        {
            if (force || marsRovers == null)
            {
                marsRovers = new MarsRoverViewModel();
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the MarsRovers instance.
        /// </summary>
        public static void ClearMarsRovers()
        {
            marsRovers.Cleanup();
            marsRovers = null;
        }

        #endregion


        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            // Call ClearViewModelName() for each ViewModel.

            ClearMarsRovers();
        }
    }
}